using PetSimulator.Models;

namespace PetSimulator.Items
{
    public class DogFood : Item
    {
        public DogFood()
        {
            Name = "Dog Food";
            Description = "Nutritious kibble for dogs";
            BoostValue = 30;
            UseTimeSeconds = 3;
        }

        protected override void ApplyEffect(Pet pet)
        {
            if (pet is Dog dog)
            {
                dog.Hunger = Math.Min(dog.Hunger + BoostValue, 100);
                Console.WriteLine($"{dog.Name} happily eats the food! (+{BoostValue} Hunger)");
            }
            else
            {
                Console.WriteLine($"{pet.Name} sniffs but refuses to eat dog food.");
            }
        }
    }
}