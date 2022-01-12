using System;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Runtime.CompilerServices;
using Microsoft.Data.SqlClient;
using BankApp.Utils;
using System.Windows.Input;
using System.Windows;

namespace BankApp.ViewModels
{
    internal class LoginViewModel
    {
        private Customer customer;

        private RelayCommand? _TestLogin;
        public Action? CloseAction { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public string? Username
        {
            get { return customer.Username; }
            set
            {
                customer.Username = value;
                OnPropertyChanged();
            }
        }
        public string? Password
        {
            get { return customer.Password; }
            set
            {
                customer.Password = value;
                OnPropertyChanged();
            }
        }

        public LoginViewModel()
        {
            customer = new Customer();
        }

        public ICommand TestLoginUser
        {
            get
            {
                if (_TestLogin == null)
                {
                    _TestLogin = new RelayCommand(param => TestLogin(), param => CanTestLogin());
                }
                return _TestLogin;
            }
        }

        public void TestLogin()
        {

            string conStr = ConfigurationManager.ConnectionStrings["bankapp"].ToString();

            SqlConnection conn = new(conStr); ;

            SqlCommand command = new("SELECT id FROM Customer WHERE username= @username AND password=@password", conn);


            command.Parameters.AddWithValue("@username", this.Username);
            command.Parameters.AddWithValue("@password", this.Password);


            conn.Open();
            var user_Id = command.ExecuteScalar();
            conn.Close();

            if (user_Id != null)
            {
                MessageBox.Show("Identificación correcta.");
                CustomerWindow CustomerWindow = new();
                App.Current.MainWindow = CustomerWindow;
                CloseAction();
                CustomerWindow.Show();
            }
            else
            {
                MessageBox.Show("Identificación incorrecta.");
            }

        }

        public bool CanTestLogin()
        {

            return this.Username != null && this.Password != null;
        }
    }
}
