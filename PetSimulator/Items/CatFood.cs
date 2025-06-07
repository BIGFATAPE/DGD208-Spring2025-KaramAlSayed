using PetSimulator.Models;

namespace PetSimulator.Items
{
    public class CatFood : Item
    {
        public CatFood()
        {
            Name = "Cat Food";
            Description = "Tasty fish-flavored meal";
            BoostValue = 25;
            UseTimeSeconds = 2;
        }

        protected override void ApplyEffect(Pet pet)
        {
            if (pet is Cat cat)
            {
                cat.Hunger = Math.Min(cat.Hunger + BoostValue, 100);
                Console.WriteLine($"{cat.Name} nibbles at the food cautiously. (+{BoostValue} Hunger)");
            }
            else
            {
                Console.WriteLine($"{pet.Name} turns away from the cat food.");
            }
        }
    }
}