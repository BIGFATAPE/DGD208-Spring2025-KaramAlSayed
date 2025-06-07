using System;
using System.Threading.Tasks;
using PetSimulator.Models;

namespace PetSimulator.Items
{
    public abstract class Item
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public int BoostValue { get; protected set; }
        public int UseTimeSeconds { get; protected set; }

        public async Task UseOnPet(Pet pet)
        {
            if (pet == null || !pet.IsAlive)
            {
                Console.WriteLine("Cannot use items on nonexistent or deceased pets.");
                return;
            }

            Console.WriteLine($"Using {Name} on {pet.Name}... (Will take {UseTimeSeconds}s)");
            await Task.Delay(UseTimeSeconds * 1000);
            ApplyEffect(pet);
        }

        protected abstract void ApplyEffect(Pet pet);
    }
}