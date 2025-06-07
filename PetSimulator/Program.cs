using System;
using System.Threading.Tasks;
using PetSimulator.Models;
using PetSimulator.Utils;
using PetSimulator.Items;

namespace PetSimulator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "Virtual Pet Simulator";
            Console.WriteLine("=== VIRTUAL PET SIMULATOR ===");
            Console.WriteLine("Created by: Big Boss Karoomi");
            Console.WriteLine("Student ID: 229910346\n");

            PetManager.StartMonitoring();

            bool running = true;
            while (running)
            {
                Console.WriteLine("\n=== MAIN MENU ===");
                Console.WriteLine($"💰 Money: ${StoreInventory.PlayerMoney}");
                Console.WriteLine("1. Adopt Pet");
                Console.WriteLine("2. View Pets");
                Console.WriteLine("3. Use Item");
                Console.WriteLine("4. Visit Store");
                Console.WriteLine("5. Exit");
                Console.Write("Select option: ");

                switch (Console.ReadLine())
                {
                    case "1": await AdoptPet(); break;
                    case "2": ViewPets(); break;
                    case "3": await UseItem(); break;
                    case "4": VisitStore(); break;
                    case "5": running = false; break;
                    default: Console.WriteLine("Invalid option"); break;
                }
            }
        }

        static async Task AdoptPet()
        {
            Console.WriteLine("\nAdopt a:");
            Console.WriteLine("1. Dog ($100)");
            Console.WriteLine("2. Cat ($150)");
            Console.Write("Choose: ");

            var choice = Console.ReadLine();
            if (choice != "1" && choice != "2") return;

            Console.Write("Name your pet: ");
            var name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name)) return;

            if ((choice == "1" && StoreInventory.PlayerMoney < 100) ||
                (choice == "2" && StoreInventory.PlayerMoney < 150))
            {
                Console.WriteLine("Not enough money!");
                return;
            }

            Pet newPet = choice == "1" ? new Dog(name) : new Cat(name);
            StoreInventory.PlayerMoney -= choice == "1" ? 100 : 150;
            PetManager.AddPet(newPet);
            Console.WriteLine($"\nAdopted {name}!");
            newPet.MakeSound();
            await Task.Delay(1000);
        }

        static void ViewPets()
        {
            var pets = PetManager.GetAllPets();
            if (pets.Count == 0)
            {
                Console.WriteLine("\nNo pets yet!");
                return;
            }

            foreach (var pet in pets)
            {
                Console.WriteLine($"\n{pet.GetStatus()}");
            }
        }

        static async Task UseItem()
        {
            var pets = PetManager.GetLivingPets();
            if (pets.Count == 0)
            {
                Console.WriteLine("\nNo living pets to use items on!");
                return;
            }

            Console.WriteLine("\nSelect pet:");
            for (int i = 0; i < pets.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {pets[i].Name} ({pets[i].GetType().Name})");
            }

            if (!int.TryParse(Console.ReadLine(), out int petIdx) || petIdx < 1 || petIdx > pets.Count)
            {
                Console.WriteLine("Invalid selection");
                return;
            }

            var pet = pets[petIdx - 1];
            Console.WriteLine("\nSelect item:");
            Console.WriteLine("1. Food");
            Console.WriteLine("2. Toy");
            Console.Write("Choose: ");

            Item item = Console.ReadLine() switch
            {
                "1" => pet is Dog ? new DogFood() : new CatFood(),
                "2" => pet is Dog ? new ChewToy() : new LaserPointer(),
                _ => null
            };

            if (item != null)
            {
                await item.UseOnPet(pet);
                StoreInventory.EarnMoney(5);
            }
        }

        static void VisitStore()
        {
            var items = StoreInventory.GetAvailableItems();
            Console.WriteLine("\n=== PET STORE ===");
            Console.WriteLine($"Your money: ${StoreInventory.PlayerMoney}\n");

            for (int i = 0; i < items.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {items[i].Item.Name} - ${items[i].Price}");
                Console.WriteLine($"   {items[i].Item.Description}");
            }

            Console.Write("\nBuy item (1-4) or X to cancel: ");
            var input = Console.ReadLine();

            if (int.TryParse(input, out int choice) && choice >= 1 && choice <= items.Count)
            {
                if (StoreInventory.Purchase(items[choice - 1].Item))
                {
                    Console.WriteLine($"Purchased {items[choice - 1].Item.Name}!");
                }
                else
                {
                    Console.WriteLine("Not enough money!");
                }
            }
        }
    }
}