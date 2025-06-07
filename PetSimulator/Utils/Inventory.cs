// Utils/Inventory.cs
using System.Collections.Generic;
using System.Linq;
using PetSimulator.Items;

namespace PetSimulator.Utils
{
    public class Inventory
    {
        private readonly List<Item> items = new List<Item>();

        public void AddItem(Item item)
        {
            if (item == null) return;
            items.Add(item);
        }

        public bool RemoveItem(Item item) => items.Remove(item);
        public IEnumerable<Item> GetItemsForPetType(PetType petType) => items.Where(i => i?.CanUseOnPetType(petType) ?? false);
        public IEnumerable<Item> GetAllItems() => items.AsReadOnly();
    }
}
// This code defines an inventory system for the game