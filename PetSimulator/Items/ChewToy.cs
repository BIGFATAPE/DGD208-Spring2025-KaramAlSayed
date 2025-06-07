// Items/ChewToy.cs
using PetSimulator.Items;

namespace PetSimulator.Items
{
    public class ChewToy : Item
    {
        public ChewToy()
        {
            Name = "Durable Chew Toy";
            Price = 10;
            UsageTimeSeconds = 5;
            Description = "Increases fun by 25 points for dogs";
        }

        public override bool CanUseOnPetType(PetType petType) => petType == PetType.Dog;
    }
}
// This code defines a Chewtoy in the pet simulator game, specificallty for dogs, woof woof indeed.