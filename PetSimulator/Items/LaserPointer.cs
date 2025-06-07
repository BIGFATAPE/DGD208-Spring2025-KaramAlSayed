// Items/LaserPointer.cs
using PetSimulator.Items;

namespace PetSimulator.Items
{
    public class LaserPointer : Item
    {
        public LaserPointer()
        {
            Name = "Laser Pointer";
            Price = 8;
            UsageTimeSeconds = 2;
            Description = "Increases fun by 20 points for cats";
        }

        public override bool CanUseOnPetType(PetType petType) => petType == PetType.Cat;
    }
}
// This code defines a Laser Pointer item in the Pet Simulator game, specifically designed for cats to increase their fun level. (Visual Studio likes to autofill these comments)