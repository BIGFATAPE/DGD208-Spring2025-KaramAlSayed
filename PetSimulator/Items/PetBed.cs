using PetSimulator.Models;

namespace PetSimulator.Items
{
    public class PetBed : Item
    {
        public PetBed()
        {
            Name = "Comfy Bed";
            Description = "Soft bed for resting";
            BoostValue = 35;
            UseTimeSeconds = 5;
        }

        protected override void ApplyEffect(Pet pet)
        {
            pet.Sleep = Math.Min(pet.Sleep + BoostValue, 100);
            Console.WriteLine($"{pet.Name} curls up and falls asleep.");
        }
    }
}