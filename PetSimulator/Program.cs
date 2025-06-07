// Program.cs
using System;
using System.Threading.Tasks;
using PetSimulator.Utils;

namespace PetSimulator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("==Awesome Pet Simulator==");
            Console.WriteLine("Created by Big Boss Karoomi Student ID: 229910346");

            var petManager = new PetManager();
            await petManager.RunGameLoopAsync();
        }
    }
}
