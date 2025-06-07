using System;
using System.Text;

namespace PetSimulator.Models
{
    public abstract class Pet
    {
        public event Action<string> OnPetStatusChanged;

        public string Name { get; set; }
        public int Hunger { get; set; } = 50;
        public int Sleep { get; set; } = 50;
        public int Fun { get; set; } = 50;
        public bool IsAlive => Hunger > 0 && Sleep > 0 && Fun > 0;

        protected void NotifyStatus(string message)
        {
            OnPetStatusChanged?.Invoke($"[{DateTime.Now:t}] {message}");
        }

        public abstract void MakeSound();

        public virtual void DecreaseStats()
        {
            Hunger = Math.Max(Hunger - 1, 0);
            Sleep = Math.Max(Sleep - 1, 0);
            Fun = Math.Max(Fun - 1, 0);

            if (!IsAlive) NotifyStatus($"{Name} has passed away...");
        }

        public string GetStatus()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Name: {Name}");
            sb.AppendLine($"Hunger: {Hunger}/100 {(Hunger < 20 ? "⚠️" : "")}");
            sb.AppendLine($"Sleep: {Sleep}/100 {(Sleep < 20 ? "⚠️" : "")}");
            sb.AppendLine($"Fun: {Fun}/100 {(Fun < 20 ? "⚠️" : "")}");
            sb.AppendLine($"Status: {(IsAlive ? "Alive" : "Deceased")}");
            return sb.ToString();
        }
    }
}