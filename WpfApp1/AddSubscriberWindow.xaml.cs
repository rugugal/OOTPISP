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
    /// Логика взаимодействия для AddSubscriberWindow.xaml
    /// </summary>
    public partial class AddSubscriberWindow : Window
    {
        public string SubscriberName { get;  set; }
        public string PhoneNumber { get;  set; }

        public AddSubscriberWindow()
        {
            InitializeComponent();
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SubscriberName = txtName.Text;
            PhoneNumber = txtPhoneNumber.Text;

            if (!IsValidName(SubscriberName))
            {
                MessageBox.Show("Некорректное имя. Пожалуйста, введите имя длиной более 2 символов.");
                txtName.Focus(); 
                return; 
            }

            if (IsValidPhoneNumber(PhoneNumber, out string formattedPhoneNumber))
            {
                PhoneNumber = formattedPhoneNumber; 
                DialogResult = true;
                Close(); 
            }
            else
            {
                MessageBox.Show("Некорректный номер телефона. Пожалуйста, введите номер в формате +375XXXXXXXXX.");

                txtPhoneNumber.Focus();
            }
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

        private bool IsValidName(string name) => !string.IsNullOrEmpty(name) && name.Length > 2;
        
    }
}
