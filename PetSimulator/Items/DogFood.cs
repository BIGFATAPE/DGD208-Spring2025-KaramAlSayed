// Items/DogFood.cs
using PetSimulator.Items;

namespace PetSimulator.Items
{
    public class DogFood : Item
    {
        public DogFood()
        {
            Name = "Deluxe Dog Food";
            Price = 20;
            UsageTimeSeconds = 4;
            Description = "Hearty meal that reduces hunger by 40 points";
        }

        public override bool CanUseOnPetType(PetType petType) => petType == PetType.Dog;
    }
}
// Nam Nam for the doggos!