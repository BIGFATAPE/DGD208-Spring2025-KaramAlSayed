using System.Collections.Generic;
using System.Linq;
using PetSimulator.Items;

namespace PetSimulator.Utils
{
    public static class StoreInventory
    {
        public static int PlayerMoney { get; set; } = 200;

        private static List<StoreItem> availableItems = new List<StoreItem>
        {
            new StoreItem(new DogFood(), 25),
            new StoreItem(new CatFood(), 25),
            new StoreItem(new ChewToy(), 20),
            new StoreItem(new LaserPointer(), 30)
        };

        public static List<StoreItem> GetAvailableItems() => availableItems.ToList();

        public static bool Purchase(Item item)
        {
            var storeItem = availableItems.FirstOrDefault(i => i.Item.GetType() == item.GetType());
            if (storeItem == null || PlayerMoney < storeItem.Price) return false;

            PlayerMoney -= storeItem.Price;
            return true;
        }

        public static void EarnMoney(int amount) => PlayerMoney += amount;

        public record StoreItem(Item Item, int Price);
    }
}