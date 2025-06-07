// Models/Pet.cs
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PetSimulator.Items;

namespace PetSimulator.Models
{
    public abstract class Pet : INotifyPropertyChanged
    {
        private int hunger;
        private int sleep;
        private int fun;
        private string name = string.Empty;

        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler? Died;

        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged();
                }
            }
        }

        public PetType Type { get; protected set; }

        public int Hunger
        {
            get => hunger;
            set
            {
                if (hunger != value)
                {
                    int oldValue = hunger;
                    hunger = Math.Clamp(value, 0, 100);
                    OnPropertyChanged();
                    if (hunger == 0) OnDied();
                }
            }
        }

        public int Sleep
        {
            get => sleep;
            set
            {
                if (sleep != value)
                {
                    int oldValue = sleep;
                    sleep = Math.Clamp(value, 0, 100);
                    OnPropertyChanged();
                    if (sleep == 0) OnDied();
                }
            }
        }

        public int Fun
        {
            get => fun;
            set
            {
                if (fun != value)
                {
                    int oldValue = fun;
                    fun = Math.Clamp(value, 0, 100);
                    OnPropertyChanged();
                    if (fun == 0) OnDied();
                }
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnDied()
        {
            Died?.Invoke(this, EventArgs.Empty);
        }

        public abstract Task UseItemAsync(Item item);
    }
}
// MEOW AND WOOF! :D