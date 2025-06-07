// Items/PetBed.cs
using PetSimulator.Items;

namespace PetSimulator.Items
{
    public class PetBed : Item
    {
        public PetBed()
        {
            Name = "Comfy Pet Bed";
            Price = 30;
            UsageTimeSeconds = 10;
            Description = "Increases sleep by 50 points for all pets";
        }

        public override bool CanUseOnPetType(PetType petType) => true;
    }
}
// Comfy bed for all the pets!