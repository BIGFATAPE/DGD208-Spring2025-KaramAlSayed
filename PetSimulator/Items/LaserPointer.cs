using PetSimulator.Models;

namespace PetSimulator.Items
{
    public class LaserPointer : Item
    {
        public LaserPointer()
        {
            Name = "Laser Pointer";
            Description = "Endless fun for cats";
            BoostValue = 40;
            UseTimeSeconds = 2;
        }

        protected override void ApplyEffect(Pet pet)
        {
            if (pet is Cat cat)
            {
                cat.Fun = Math.Min(cat.Fun + BoostValue, 100);
                Console.WriteLine($"{cat.Name} chases the dot frantically! (+{BoostValue} Fun)");
            }
            else
            {
                Console.WriteLine($"{pet.Name} doesn't understand the laser pointer.");
            }
        }
    }
}