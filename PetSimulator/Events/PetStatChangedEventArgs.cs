// Events/PetStatChangedEventArgs.cs
using System;

namespace PetSimulator.Events
{
    public class PetStatChangedEventArgs : EventArgs
    {
        public string PetName { get; }
        public string StatName { get; }
        public int OldValue { get; }
        public int NewValue { get; }

        public PetStatChangedEventArgs(string petName, string statName, int oldValue, int newValue)
        {
            PetName = petName ?? throw new ArgumentNullException(nameof(petName));
            StatName = statName ?? throw new ArgumentNullException(nameof(statName));
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}
// This class is used to encapsulate the event data when a pet's stat changes.