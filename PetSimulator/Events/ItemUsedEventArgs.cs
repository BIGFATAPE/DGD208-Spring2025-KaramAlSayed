// Events/ItemUsedEventArgs.cs
using System;

namespace PetSimulator.Events
{
    public class ItemUsedEventArgs : EventArgs
    {
        public string ItemName { get; }
        public string PetName { get; }
        public DateTime Timestamp { get; }

        public ItemUsedEventArgs(string itemName, string petName)
        {
            ItemName = itemName ?? throw new ArgumentNullException(nameof(itemName));
            PetName = petName ?? throw new ArgumentNullException(nameof(petName));
            Timestamp = DateTime.Now;
        }
    }
}
// This class is used to encapsulate the event data when an item is used on a pet.