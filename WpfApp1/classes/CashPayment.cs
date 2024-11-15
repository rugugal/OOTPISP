using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
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
    }
}
