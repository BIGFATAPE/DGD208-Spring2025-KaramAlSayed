// Utils/PetManager.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PetSimulator.Events;
using PetSimulator.Items;
using PetSimulator.Models;

namespace PetSimulator.Utils
{
    public class PetManager
    {
        private readonly List<Pet> pets = new List<Pet>();
        private readonly Inventory inventory = new Inventory();
        private readonly StoreInventory store = new StoreInventory();
        private readonly CancellationTokenSource statsDecreaseTokenSource = new CancellationTokenSource();

        public event EventHandler<ItemUsedEventArgs>? ItemUsed;
        public event EventHandler<string>? GameMessage;

        public async Task RunGameLoopAsync()
        {
            StartPetStatDecreaseLoop();

            while (true)
            {
                DisplayMainMenu();
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await AdoptPetAsync();
                        break;
                    case "2":
                        DisplayPets();
                        break;
                    case "3":
                        await UseItemOnPetAsync();
                        break;
                    case "4":
                        BuyItem();
                        break;
                    case "5":
                        DisplayInventory();
                        break;
                    case "6":
                        DisplayCreatorInfo();
                        break;
                    case "0":
                        statsDecreaseTokenSource.Cancel();
                        return;
                    default:
                        OnGameMessage("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private void DisplayMainMenu()
        {
            Console.WriteLine("\n=== MAIN MENU ===");
            Console.WriteLine("1. Adopt a pet");
            Console.WriteLine("2. View your pets");
            Console.WriteLine("3. Use item on pet");
            Console.WriteLine("4. Buy items from store");
            Console.WriteLine("5. View inventory");
            Console.WriteLine("6. Display creator info");
            Console.WriteLine("0. Exit game");
            Console.Write("Enter your choice: ");
        }

        private async Task AdoptPetAsync()
        {
            Console.WriteLine("\nSelect pet type:");
            Console.WriteLine("1. Cat");
            Console.WriteLine("2. Dog");
            Console.Write("Enter choice: ");
            var typeChoice = Console.ReadLine();

            Console.Write("Enter pet name: ");
            var name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                OnGameMessage("Pet name cannot be empty.");
                return;
            }

            Pet? newPet = typeChoice switch
            {
                "1" => new Cat { Name = name },
                "2" => new Dog { Name = name },
                _ => null
            };

            if (newPet != null)
            {
                pets.Add(newPet);
                newPet.PropertyChanged += (s, e) =>
                {
                    if (s is Pet pet && e.PropertyName != null)
                    {
                        OnGameMessage($"{pet.Name}'s {e.PropertyName} changed to {GetStatValue(pet, e.PropertyName)}");
                    }
                };
                newPet.Died += (s, e) =>
                {
                    if (s is Pet pet)
                    {
                        OnGameMessage($"Oh no! {pet.Name} has died from neglect!");
                        pets.Remove(pet);
                    }
                };

                OnGameMessage($"You adopted a new {newPet.Type.ToString().ToLower()} named {newPet.Name}!");
            }
            else
            {
                OnGameMessage("Invalid pet type selection.");
            }
        }

        private int GetStatValue(Pet pet, string? propertyName)
        {
            return propertyName switch
            {
                nameof(Pet.Hunger) => pet.Hunger,
                nameof(Pet.Sleep) => pet.Sleep,
                nameof(Pet.Fun) => pet.Fun,
                _ => 0
            };
        }

        private void DisplayPets()
        {
            if (!pets.Any())
            {
                OnGameMessage("You don't have any pets yet.");
                return;
            }

            Console.WriteLine("\n=== YOUR PETS ===");
            foreach (var pet in pets)
            {
                Console.WriteLine($"{pet.Name} ({pet.Type}) - Hunger: {pet.Hunger}, Sleep: {pet.Sleep}, Fun: {pet.Fun}");
            }
        }

        private async Task UseItemOnPetAsync()
        {
            if (!pets.Any())
            {
                OnGameMessage("You don't have any pets to use items on.");
                return;
            }

            DisplayPets();
            Console.Write("Select pet (enter name): ");
            var petName = Console.ReadLine();
            var pet = pets.FirstOrDefault(p => p.Name.Equals(petName, StringComparison.OrdinalIgnoreCase));

            if (pet == null)
            {
                OnGameMessage("Pet not found.");
                return;
            }

            var usableItems = inventory.GetItemsForPetType(pet.Type).ToList();
            if (!usableItems.Any())
            {
                OnGameMessage($"You don't have any items that can be used on a {pet.Type.ToString().ToLower()}.");
                return;
            }

            Console.WriteLine("\n=== USABLE ITEMS ===");
            for (int i = 0; i < usableItems.Count; i++)
            {
                if (usableItems[i] != null)
                {
                    Console.WriteLine($"{i + 1}. {usableItems[i].Name} - {usableItems[i].Description}");
                }
            }

            Console.Write("Select item to use: ");
            if (int.TryParse(Console.ReadLine(), out int itemIndex) && itemIndex > 0 && itemIndex <= usableItems.Count)
            {
                var selectedItem = usableItems[itemIndex - 1];
                if (selectedItem == null)
                {
                    OnGameMessage("Invalid item selected.");
                    return;
                }

                OnGameMessage($"Using {selectedItem.Name} on {pet.Name}...");
                OnItemUsed(selectedItem.Name, pet.Name);

                await pet.UseItemAsync(selectedItem);
                inventory.RemoveItem(selectedItem);
            }
            else
            {
                OnGameMessage("Invalid item selection.");
            }
        }

        private void BuyItem()
        {
            var storeItems = store.GetAllItems().ToList();
            Console.WriteLine("\n=== STORE INVENTORY ===");
            for (int i = 0; i < storeItems.Count; i++)
            {
                if (storeItems[i] != null)
                {
                    Console.WriteLine($"{i + 1}. {storeItems[i].Name} - ${storeItems[i].Price} - {storeItems[i].Description}");
                }
            }

            Console.Write("Select item to buy (or 0 to cancel): ");
            if (int.TryParse(Console.ReadLine(), out int itemIndex) && itemIndex > 0 && itemIndex <= storeItems.Count)
            {
                var selectedItem = storeItems[itemIndex - 1];
                if (selectedItem != null)
                {
                    inventory.AddItem(selectedItem);
                    OnGameMessage($"You bought {selectedItem.Name}!");
                }
            }
            else if (itemIndex != 0)
            {
                OnGameMessage("Invalid item selection.");
            }
        }

        private void DisplayInventory()
        {
            var items = inventory.GetAllItems().ToList();
            if (!items.Any())
            {
                OnGameMessage("Your inventory is empty.");
                return;
            }

            Console.WriteLine("\n=== YOUR INVENTORY ===");
            foreach (var item in items)
            {
                if (item != null)
                {
                    Console.WriteLine($"{item.Name} - {item.Description}");
                }
            }
        }

        private void DisplayCreatorInfo()
        {
            Console.WriteLine("\n=== CREATOR INFO ===");
            Console.WriteLine("Created by: [Your Name]");
            Console.WriteLine("Student ID: [Your ID]");
        }

        private void StartPetStatDecreaseLoop()
        {
            Task.Run(async () =>
            {
                while (!statsDecreaseTokenSource.Token.IsCancellationRequested)
                {
                    await Task.Delay(60000); // Decrease stats every 60 seconds

                    foreach (var pet in pets.ToList()) // ToList to avoid modification during iteration
                    {
                        pet.Hunger -= 5;
                        pet.Sleep -= 3;
                        pet.Fun -= 4;
                    }
                }
            }, statsDecreaseTokenSource.Token);
        }

        protected virtual void OnItemUsed(string itemName, string petName)
        {
            ItemUsed?.Invoke(this, new ItemUsedEventArgs(itemName, petName));
        }

        protected virtual void OnGameMessage(string message)
        {
            GameMessage?.Invoke(this, message);
            Console.WriteLine(message);
        }
    }
}
// About 300 lines of code in total, ill explain each section briefly: The PetManage part is self explanatory
// RunGameloopAsync starts the game loop and handles user input
// DisplayMainMenu shows the main menu options
// AdoptPetAsync allows the user to adopt a new pet
// DisplayPets shows all pets the user has adopted
// UseItemOnPetAsync allows the user to use an item on a specific pet
// BuyItem allows the user to buy items from the store
// DisplayInventory shows the user's inventory
// DisplayCreatorInfo shows the creator's information
// StartPetStatDecreaseLoop starts a background task to decrease pet stats periodically
// OnItemUsed and OnGameMessage are event handlers for item usage and game messages
// Big Headache writing this, i also had help correcting issues using the built in AI in Visual Studio, very useful :)