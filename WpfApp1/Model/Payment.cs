using System;
using System.ComponentModel;

namespace WpfApp1.Model
{
    public abstract class Payment : INotifyPropertyChanged
    {
        private string subscriberName;
        public Guid SubscriberId { get; set; }
        public string PaymentType { get; set; }
        public decimal Amount { get; set; }

        public string SubscriberName
        {
            get => subscriberName;
            set
            {
                if (subscriberName != value)
                {
                    subscriberName = value;
                    OnPropertyChanged(nameof(SubscriberName));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }



}
