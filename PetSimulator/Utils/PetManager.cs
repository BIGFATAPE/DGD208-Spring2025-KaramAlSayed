using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetSimulator.Models;

namespace PetSimulator.Utils
{
    public static class PetManager
    {
        private static List<Pet> pets = new List<Pet>();
        private static bool isMonitoring = false;

        public static void AddPet(Pet pet)
        {
            pets.Add(pet);
            pet.OnPetStatusChanged += message => Console.WriteLine($"\n[System] {message}");
        }

        public static List<Pet> GetAllPets() => pets.ToList();
        public static List<Pet> GetLivingPets() => pets.Where(p => p.IsAlive).ToList();

        public static void StartMonitoring()
        {
            if (isMonitoring) return;
            isMonitoring = true;

            Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(5000); // Every 5 seconds
                    foreach (var pet in pets.ToList())
                    {
                        pet.DecreaseStats();
                    }
                }
            });
        }
    }
}