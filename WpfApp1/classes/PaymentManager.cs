using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace WpfApp1
{
    public class PaymentManager
    {
        public ObservableCollection<Subscriber> Subscribers { get; private set; }
        public ObservableCollection<Payment> Payments { get; private set; }

        public PaymentManager()
        {
            Subscribers = new ObservableCollection<Subscriber>();
            Payments = new ObservableCollection<Payment>();
        }
        public Subscriber AddSubscriber(string name, string phoneNumber)
        {
            var subscriber = new Subscriber
            {
                Id = Guid.NewGuid(),
                Name = name,
                PhoneNumber = phoneNumber
            };
            Subscribers.Add(subscriber);
            return subscriber;
        }
        public void EditSubscriber(Guid id, string name, string phoneNumber)
        {
            var subscriber = Subscribers.FirstOrDefault(s => s.Id == id);
            if (subscriber != null)
            {
                subscriber.Name = name;
                subscriber.PhoneNumber = phoneNumber;
            }
            foreach (var payment in Payments)
            {
                if (payment.SubscriberId == subscriber.Id)
                {
                    payment.SubscriberName = name;
                }
            }
        }
        public void DeleteSubscriber(Guid id)
        {
            var subscriber = Subscribers.FirstOrDefault(s => s.Id == id);
            if (subscriber != null)
            {
                Subscribers.Remove(subscriber);
            }
        }
        public Payment AddCardPayment(Guid subscriberId, decimal amount, string cardNumber, string subscriberName)
        {
            var payment = new CardPayment(subscriberId, amount, cardNumber, subscriberName);
            Payments.Add(payment);
            return payment;
        }
        public Payment AddCashPayment(Guid subscriberId, decimal amount, string subscriberName)
        {
            var payment = new CashPayment(subscriberId, amount, subscriberName);
            Payments.Add(payment);
            return payment;
        }
    }
}
