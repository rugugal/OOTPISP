using System;

namespace WpfApp1.Model
{
    public class CardPayment : Payment
    {
        public string CardNumber { get; set; }
        public CardPayment(Guid subscriberId, decimal amount, string cardNumber, string subscriberName, string paymentType)
        {
            SubscriberId = subscriberId;
            Amount = amount;
            CardNumber = cardNumber;
            PaymentType = paymentType;
            SubscriberName = subscriberName;
        }
        public override string ToString()
        {
            return $"Абонент: {SubscriberName}, Сумма: {Amount}, Тип оплаты: {PaymentType}";
        }
    }

}
