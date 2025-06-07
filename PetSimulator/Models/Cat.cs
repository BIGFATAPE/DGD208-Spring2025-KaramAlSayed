// Models/Cat.cs
using System.Threading.Tasks;
using PetSimulator.Items;

namespace PetSimulator.Models
{
    public class Cat : Pet
    {
        public Cat()
        {
            Type = PetType.Cat;
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
                case CatFood _:
                    Hunger += 30;
                    break;
                case LaserPointer _:
                    Fun += 20;
                    break;
                case PetBed _:
                    Sleep += 50;
                    break;
            }
        }
    }
}
// Meow