// Items/Item.cs
namespace PetSimulator.Items
{
    public abstract class Item
    {
        public string Name { get; protected set; } = string.Empty;
        public int Price { get; protected set; }
        public int UsageTimeSeconds { get; protected set; }
        public string Description { get; protected set; } = string.Empty;

        public abstract bool CanUseOnPetType(PetType petType);
    }

    public enum PetType
    {
        Cat,
        Dog
    }
}
// This code defines the base class for items in the Pet Simulator game, including properties like Name, Price, UsageTimeSeconds, and Description.