using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model
{
    public interface IFormatStrategy
    {
        string Format(Payment payment);
    }

    public class HtmlFormatStrategy : IFormatStrategy
    {
        public string Format(Payment payment)
        {
            return $"Абонент: {payment.SubscriberName}<br>nСумма: {payment.Amount}<br>Тип оплаты: {payment.PaymentType}<br>";
        }
    }

    public class TextFormatStrategy : IFormatStrategy
    {
        public string Format(Payment payment)
        {
            return $"Абонент: {payment.SubscriberName}\nСумма: {payment.Amount}\nТип оплаты: {payment.PaymentType}";
        }
    }



}
