// Utils/StoreInventory.cs
using System.Collections.Generic;
using PetSimulator.Items;

namespace PetSimulator.Utils
{
    public class StoreInventory
    {
        private readonly List<Item> availableItems = new List<Item>();

        public StoreInventory()
        {
            availableItems.Add(new CatFood());
            availableItems.Add(new DogFood());
            availableItems.Add(new ChewToy());
            availableItems.Add(new LaserPointer());
            availableItems.Add(new PetBed());
        }

        public IEnumerable<Item> GetAllItems() => availableItems.AsReadOnly();
    }
}
// self explanatory inventory system for the store