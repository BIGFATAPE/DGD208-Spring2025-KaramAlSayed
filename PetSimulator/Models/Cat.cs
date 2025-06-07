namespace PetSimulator.Models
{
    public class Cat : Pet
    {
        public Cat(string name)
        {
            Name = name;
            OnPetStatusChanged += message => Console.WriteLine($"Cat Update: {message}");
        }

        public override void MakeSound()
        {
            var sounds = new[] { "meows", "purrs", "hisses", "ignores you" };
            NotifyStatus($"{Name} {sounds[new Random().Next(sounds.Length)]}");
        }

        public override void DecreaseStats()
        {
            Hunger = Math.Max(Hunger - 1, 0);
            Sleep = Math.Max(Sleep - 1, 0);
            Fun = Math.Max(Fun - 3, 0); // Cats get bored faster

            if (!IsAlive) NotifyStatus($"{Name} has passed away...");
        }
    }
}