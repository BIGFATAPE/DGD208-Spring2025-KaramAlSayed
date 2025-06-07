// Items/CatFood.cs
using PetSimulator.Items;

namespace PetSimulator.Items
{
    public class CatFood : Item
    {
        public CatFood()
        {
            Name = "Premium Cat Food";
            Price = 15;
            UsageTimeSeconds = 3;
            Description = "Nutritious food that reduces hunger by 30 points";
        }

        public override bool CanUseOnPetType(PetType petType) => petType == PetType.Cat;
    }
}
// This code defines the implementation of the 'cat food' item in the Pet Simulator game.