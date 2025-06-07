// Utils/Inventory.cs - New class to manage items
using System.Collections.Generic;
using PetSimulator.Items;

namespace PetSimulator.Utils
{
    public static class Inventory
    {
        private static List<Item> availableItems = new List<Item>
        {
            new DogFood(),
            new ChewToy()
            // Can add more items here later
        };

        public static List<Item> GetAvailableItems()
        {
            // Return a copy to prevent external modifications
            return new List<Item>(availableItems);
        }
    }
}