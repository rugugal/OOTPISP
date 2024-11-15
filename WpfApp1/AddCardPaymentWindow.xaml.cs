using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для AddCardPaymentWindow.xaml
    /// </summary>
    public partial class AddCardPaymentWindow : Window
    {
        public AddCardPaymentWindow()
        {
            InitializeComponent();
        }
        public decimal Amount { get;  set; }
        public string CardNumber { get;  set; }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(txtAmount.Text, out decimal amount))
            {
                Amount = amount; 

                string cardNumber = txtCardNumber.Text;

                if (IsValidCardNumber(cardNumber, out string formattedCardNumber))
                {
                    CardNumber = formattedCardNumber; 
                    DialogResult = true; 
                    Close(); 
                }
                else
                {

                    MessageBox.Show("Некорректный номер карты. Пожалуйста, введите 16 цифр.");


                    txtCardNumber.Focus();
                }
            }
            else
            {
                MessageBox.Show("Некорректная сумма. Пожалуйста, введите число.");
            }
        }
        private void ClearCardNumberPlaceholder(object sender, RoutedEventArgs e)
        {
            if (txtCardNumber.Text == "XXXX XXXX XXXX XXXX")
            {
                txtCardNumber.Text = "";
            }
        }

        private bool IsValidCardNumber(string cardNumber, out string formattedCardNumber)
        {

            cardNumber = cardNumber.Replace(" ", "");

            var regex = new Regex(@"^\d{16}$");
            if (regex.IsMatch(cardNumber))
            {
                formattedCardNumber =
                    $"{cardNumber.Substring(0, 4)} {cardNumber.Substring(4, 4)} {cardNumber.Substring(8, 4)} {cardNumber.Substring(12, 4)}";
                return true;
            }

            formattedCardNumber = null;
            return false;
        }


        private void RestoreCardNumberPlaceholder(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCardNumber.Text))
            {
                txtCardNumber.Text = "XXXX XXXX XXXX XXXX";
            }
        }

    }
}
