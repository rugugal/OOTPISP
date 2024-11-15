using System;

namespace WpfApp1.Model
{
    public class CashPayment : Payment
    {
        public CashPayment(Guid subscriberId, decimal amount, string subscriberName)
        {
            SubscriberId = subscriberId;
            Amount = amount;
            SubscriberName = subscriberName;
            PaymentType = "Наличные";
        }
        public override string ToString()
        {
            return $"Абонент: {SubscriberName}, Сумма: {Amount}, Тип оплаты: {PaymentType}";
        }
        
    }
}
