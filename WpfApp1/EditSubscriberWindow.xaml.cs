using System;
using System.Windows;
using WpfApp1.Model;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для EditSubscriberWindow.xaml
    /// </summary>
    public partial class EditSubscriberWindow : Window
    {
        public Subscriber Subscriber { get; private set; }

        public EditSubscriberWindow(Subscriber subscriber)
        {
            InitializeComponent();
            Subscriber = subscriber;

            txtName.Text = Subscriber.Name;
            txtPhoneNumber.Text = Subscriber.PhoneNumber;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text;
            string phoneNumber = txtPhoneNumber.Text;


            if (!IsValidName(name))
            {
                MessageBox.Show("Некорректное имя. Пожалуйста, введите имя длиной более 2 символов.");
                txtName.Focus(); 
                return;
            }
            if (IsValidPhoneNumber(phoneNumber, out string formattedPhoneNumber))
            {
                Subscriber.Name = name; 
                Subscriber.PhoneNumber = formattedPhoneNumber; 
                DialogResult = true; 
                Close(); 
            }
            else
            {
                MessageBox.Show("Некорректный номер телефона. Пожалуйста, введите номер в формате +375XXXXXXXXX.");
                txtPhoneNumber.Focus(); 
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false; 
            Close();
        }

        private bool IsValidName(string name)
        {
            return !string.IsNullOrEmpty(name) && name.Length > 2;
        }

        private bool IsValidPhoneNumber(string phoneNumber, out string formattedPhoneNumber)
        {
            var regex = new System.Text.RegularExpressions.Regex(@"\+375\d{9}$");
            if (regex.IsMatch(phoneNumber))
            {
                formattedPhoneNumber = phoneNumber;
                return true;
            }

            formattedPhoneNumber = null;
            return false;
        }
    }
}
