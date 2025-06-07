// Utils/GameConfig.cs
namespace PetSimulator.Utils
{
    public static class GameConfig
    {
        // Stat decay happens every 1 minutes (60 seconds)
        public const int StatDecayIntervalSeconds = 120;

        
        public const int HungerDecayAmount = 4;   
        public const int SleepDecayAmount = 3;    
        public const int FunDecayAmount = 2;     

       
        public const int InitialHunger = 50;
        public const int InitialSleep = 50;
        public const int InitialFun = 50;

        
        public const int FoodEffectiveness = 50;
        public const int ToyEffectiveness = 30;
        public const int BedEffectiveness = 60;
    }
}
// So this code defines the game configuration settings for the Pet Simulator game.