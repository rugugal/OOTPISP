using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfApp1.Model;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private PaymentManager paymentManager;
        private Brush originalButtonBackground;
        public MainWindow()
        {
            InitializeComponent();
            originalButtonBackground = btnSetHtmlFormat.Background;
            paymentManager = new PaymentManager();
            DataContext = paymentManager;
        }

        private void EditSubscriber_Click(object sender, RoutedEventArgs e)
        {
            var selectedSubscriber = (sender as Button).DataContext as Subscriber;
            if (selectedSubscriber != null)
            {
                var editWindow = new EditSubscriberWindow(selectedSubscriber);

                if (editWindow.ShowDialog() == true)
                {
                    if (IsValidName(selectedSubscriber.Name) && IsValidPhoneNumber(selectedSubscriber.PhoneNumber,
                            out string formattedPhoneNumber))
                    {
                        paymentManager.EditSubscriber(selectedSubscriber.Id, selectedSubscriber.Name,
                            formattedPhoneNumber);
                    }
                    else
                    {
                        MessageBox.Show("Некорректное имя или номер телефона.");
                    }
                }
            }
            DisplayFormatted(paymentManager.Payments);
        }

        private void DeleteSubscriber_Click(object sender, RoutedEventArgs e)
        {
            var selectedSubscriber = (sender as Button).DataContext as Subscriber;
            if (selectedSubscriber != null)
            {
                paymentManager.DeleteSubscriber(selectedSubscriber.Id);
            }
        }

        private void btnAddSubscriber_Click(object sender, RoutedEventArgs e)
        {
            var addSubscriberWindow = new AddSubscriberWindow();
            if (addSubscriberWindow.ShowDialog() == true)
            {
                string name = addSubscriberWindow.SubscriberName;
                string phoneNumber = addSubscriberWindow.PhoneNumber;

                if (IsValidName(name) && IsValidPhoneNumber(phoneNumber, out string formattedPhoneNumber))
                {
                    paymentManager.AddSubscriber(name, formattedPhoneNumber);
                }

            }
        }

        private void btnAddCardPayment_Click(object sender, RoutedEventArgs e)
        {
            var selectedSubscriber = listSubscribers.SelectedItem as Subscriber;
            if (selectedSubscriber == null)
            {
                MessageBox.Show("Выберите абонента");
                return;
            }

            var addCardPaymentWindow = new AddCardPaymentWindow();
            if (addCardPaymentWindow.ShowDialog() == true)
            {

                var Card = addCardPaymentWindow.CardNumber;
                paymentManager.AddCardPayment(selectedSubscriber.Id, addCardPaymentWindow.Amount,
                    addCardPaymentWindow.CardNumber, selectedSubscriber.Name, $"карта ({Card})");

            }
            DisplayFormatted(paymentManager.Payments);
        }

        private void btnAddCashPayment_Click(object sender, RoutedEventArgs e)
        {
            if (!(listSubscribers.SelectedItem is Subscriber selectedSubscriber))
            {
                MessageBox.Show("Выберите абонента");
                return;
            }

            var addCashPaymentWindow = new AddCashPaymentWindow();
            if (addCashPaymentWindow.ShowDialog() == true)
            {

                paymentManager.AddCashPayment(selectedSubscriber.Id, addCashPaymentWindow.Amount,
                    selectedSubscriber.Name);
            }
            DisplayFormatted(paymentManager.Payments);
        }


        private bool IsValidName(string name)
        {
            return !string.IsNullOrEmpty(name) && name.Length > 2;
        }

        private bool IsValidPhoneNumber(string phoneNumber, out string formattedPhoneNumber)
        {
            var regex = new Regex(@"\+375\d{9}$");
            if (regex.IsMatch(phoneNumber))
            {
                formattedPhoneNumber = phoneNumber;
                return true;
            }

            formattedPhoneNumber = null;
            return false;
        }

        private void SetHtmlFormat_Click(object sender, RoutedEventArgs e)
        {
            paymentManager.SetFormatStrategy(new HtmlFormatStrategy());
            DisplayFormatted(paymentManager.Payments); // Обновляем отображение с HTML-форматом
            btnSetHtmlFormat.Background = Brushes.Purple;
            btnSetTextFormat.Background = originalButtonBackground;
        }

        private void SetTextFormat_Click(object sender, RoutedEventArgs e)
        {
            paymentManager.SetFormatStrategy(new TextFormatStrategy());
            DisplayFormatted(paymentManager.Payments); 
            btnSetTextFormat.Background = Brushes.Purple;
            btnSetHtmlFormat.Background = originalButtonBackground;
        }

        private void DisplayFormatted(ObservableCollection<Payment> payments)
        {
            paymentManager.FormattedPayments.Clear(); 

            foreach (var payment in payments)
            {
                paymentManager.FormattedPayments.Add(paymentManager.FormatPayment(payment)); 
            }
        }


    }
}