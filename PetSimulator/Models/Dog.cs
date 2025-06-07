// Models/Dog.cs
using System.Threading.Tasks;
using PetSimulator.Items;

namespace PetSimulator.Models
{
    public class Dog : Pet
    {
        public Dog()
        {
            Type = PetType.Dog;
            Hunger = 50;
            Sleep = 50;
            Fun = 50;
        }

        public override async Task UseItemAsync(Item item)
        {
            if (item == null || !item.CanUseOnPetType(Type)) return;

            await Task.Delay(item.UsageTimeSeconds * 1000);

            switch (item)
            {
                case DogFood _:
                    Hunger += 40;
                    break;
                case ChewToy _:
                    Fun += 25;
                    break;
                case PetBed _:
                    Sleep += 50;
                    break;
            }
        }
    }
}
// WOOF