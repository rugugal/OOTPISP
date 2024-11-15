using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class CardPayment : Payment
    {
        public string CardNumber { get; set; }
        public CardPayment(Guid subscriberId, decimal amount, string cardNumber, string subscriberName)
        {
            SubscriberId = subscriberId;
            Amount = amount;
            CardNumber = cardNumber;
            PaymentType = "Карта";
            SubscriberName = subscriberName;
        }
    }


}
