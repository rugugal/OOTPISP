using System;
using System.ComponentModel;
using WpfApp1.Interface;

namespace WpfApp1.Model
{
    public class Subscriber : INotifyPropertyChanged, IObserver
    {
        public Guid Id { get; set; }
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        private string phoneNumber;
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set
            {
                phoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }
        private bool isSubscribed;
        public bool IsSubscribed
        {
            get => isSubscribed;
            set
            {
                if (isSubscribed != value)
                {
                    isSubscribed = value;
                    OnPropertyChanged(nameof(IsSubscribed));
                }
            }
        }


        public string Update(string message)
        {
            if (!IsSubscribed)
            {
                return "";
            }
            return $"{Name} получил уведомление: {message}";
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
