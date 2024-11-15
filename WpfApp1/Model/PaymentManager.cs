using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace WpfApp1.Model
{
    public class PaymentManager
    {
        public ObservableCollection<Subscriber> Subscribers { get; }
        public ObservableCollection<Payment> Payments { get; private set; }
        public IFormatStrategy FormatStrategy { get; set; }
        public ObservableCollection<string> FormattedPayments { get; }
        
        public PaymentManager()
        {
            Subscribers = new ObservableCollection<Subscriber>();
            Payments = new ObservableCollection<Payment>();
            FormattedPayments = new ObservableCollection<string>();
            FormatStrategy = new TextFormatStrategy();
        }


        public void SetFormatStrategy(IFormatStrategy formatStrategy)
        {
            FormatStrategy = formatStrategy;
            var test = "test";
        }

        public string FormatPayment(Payment payment)
        {
            return FormatStrategy.Format(payment);
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

                // Обновляем имя во всех платежах данного абонента с удалением и вставкой
                for (int i = 0; i < Payments.Count; i++)
                {
                    var payment = Payments[i];
                    if (payment.SubscriberId == id)
                    {
                        // Удаляем, обновляем и вставляем элемент на прежнюю позицию
                        Payments.RemoveAt(i);
                        payment.SubscriberName = name;
                        Payments.Insert(i, payment);
                    }
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
        public Payment AddCardPayment(Guid subscriberId, decimal amount, string cardNumber, string subscriberName, string paymentType)
        {
            var payment = new CardPayment(subscriberId, amount, cardNumber, subscriberName, paymentType);
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