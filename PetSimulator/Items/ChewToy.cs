using PetSimulator.Models;

namespace PetSimulator.Items
{
    public class ChewToy : Item
    {
        public ChewToy()
        {
            Name = "Chew Toy";
            Description = "Durable rubber toy for dogs";
            BoostValue = 25;
            UseTimeSeconds = 4;
        }

        protected override void ApplyEffect(Pet pet)
        {
            pet.Fun = Math.Min(pet.Fun + BoostValue, 100);
            Console.WriteLine($"{pet.Name} plays with the toy! (+{BoostValue} Fun)");
        }
    }
}