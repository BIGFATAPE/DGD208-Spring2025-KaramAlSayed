namespace PetSimulator.Models
{
    public class Dog : Pet
    {
        public Dog(string name)
        {
            Name = name;
            OnPetStatusChanged += message => Console.WriteLine($"Dog Update: {message}");
        }

        public override void MakeSound()
        {
            NotifyStatus($"{Name} barks: Woof woof!");
        }

        public override void DecreaseStats()
        {
            Hunger = Math.Max(Hunger - 2, 0); // Dogs get hungry faster
            Sleep = Math.Max(Sleep - 1, 0);
            Fun = Math.Max(Fun - 1, 0);

            if (!IsAlive) NotifyStatus($"{Name} has passed away...");
        }
    }
}