using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WpfApp1.Interface;

namespace WpfApp1.Model
{
    public class PaymentManager : IObservable
    {
        public ObservableCollection<Subscriber> Subscribers { get; }
        public ObservableCollection<Payment> Payments { get; private set; }
        public IFormatStrategy FormatStrategy { get; set; }
        public ObservableCollection<string> FormattedPayments { get; }

        private readonly List<IObserver> observers;

        public PaymentManager()
        {
            Subscribers = new ObservableCollection<Subscriber>();
            Payments = new ObservableCollection<Payment>();
            FormattedPayments = new ObservableCollection<string>();
            FormatStrategy = new TextFormatStrategy();
            observers = new List<IObserver>();
        }
        public void AddObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        public void DeleteObserver(IObserver observer)
        {
            observers.Remove(observer);
        }

        public string Notify(string message, IObserver observer)
        {
            return observer.Update(message);
        }


        public void SetFormatStrategy(IFormatStrategy formatStrategy)
        {
            FormatStrategy = formatStrategy;
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

                for (int i = 0; i < Payments.Count; i++)
                {
                    var payment = Payments[i];
                    if (payment.SubscriberId == id)
                    {
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
        public IObserver FindSubscriber(Guid id)
        {
            var subscriber = Subscribers.FirstOrDefault(s => s.Id == id);
            return subscriber;
        }

        public Payment AddCardPayment(Guid subscriberId, decimal amount, string cardNumber, string subscriberName, string paymentType)
        {
            var payment = new CardPayment(subscriberId, amount, cardNumber, subscriberName, paymentType);
            Payments.Add(payment);
            Notify($"Платеж для {subscriberName}: {amount} ({paymentType}) выполнен.", FindSubscriber(subscriberId));

            return payment;
        }

        public Payment AddCashPayment(Guid subscriberId, decimal amount, string subscriberName)
        {
            var payment = new CashPayment(subscriberId, amount, subscriberName);
            Payments.Add(payment);
            Notify($"Платеж для {subscriberName}: {amount} (наличные) выполнен.", FindSubscriber(subscriberId));
            return payment;
        }
    }
}