using BankApp.Utils;
using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BankApp.ViewModels
{
    internal class LoginViewModel : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        private Customer customer;

        private RelayCommand? _TestLogin;
        public Action? CloseAction { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        private Dictionary<String, List<String>> Errors { get; }

       
        public string? Username
        {
            get { return customer.Username; }
            set
            {
                ValidateProperty(value);
                ValidateProperty(this.Password,nameof(this.Password));
                customer.Username = value;
                OnPropertyChanged();
                
            }
        }
        public string? Password
        {
            get { return customer.Password; }
            set
            {
                ValidateProperty(value);
                ValidateProperty(this.Username, nameof(this.Username));
                customer.Password = value;
                OnPropertyChanged();
            }
        }

        public LoginViewModel()
        {
            customer = new Customer();
            this.Errors = new Dictionary<string, List<string>>();
        }

        #region Implementaciones clases INotifyPropertyChanged y INotifyDataErrorInfo
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        protected void OnErrorsChanged(string propertyName)
        {
            this.ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
        public bool ValidateProperty<TValue>(TValue propertyValue, [CallerMemberName] string? propertyName = null)
        {
            // Limpia los errores anteriores
            if (propertyName != null)
            {
                this.Errors.Remove(propertyName);
                OnErrorsChanged(propertyName);
            }

            return true;
        }
        public bool PropertyHasErrors(string propertyName)
        {
            return this.Errors.TryGetValue(propertyName, out List<string>? propertyErrors) && propertyErrors.Any();
        }

        public void AddError(string propertyName, string errorMessage, bool isWarning = false)
        {
            if (!this.Errors.TryGetValue(propertyName, out List<string>? propertyErrors))
            {
                propertyErrors = new List<string>();
                this.Errors[propertyName] = propertyErrors;
            }

            if (!propertyErrors.Contains(errorMessage))
            {
                if (isWarning)
                {
                    // Mueve los avisos al final
                    propertyErrors.Add(errorMessage);
                }
                else
                {
                    propertyErrors.Insert(0, errorMessage);
                }
                OnErrorsChanged(propertyName);
            }
        }

        // Devuelve todos los errores de la propiedad especificada
        // Si el argumento es null devuelve todos los errorees de todas las propiedades
        // Este metodo es llamado por los bindings de WPF cuando se levanta un evento ErrorsChanged y HasErrors devuelve true
        public IEnumerable GetErrors(string? propertyName)
        {
            return string.IsNullOrWhiteSpace(propertyName)
                       ? this.Errors.SelectMany(entry => entry.Value)
                       : this.Errors.TryGetValue(propertyName, out List<string>? errors)
                         ? errors
                         : new List<string>();
        }

        // Devuelve 'true' si alguna propiedad del viewmodel contiene errores
        public bool HasErrors => this.Errors.Any();

        #endregion

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

            using SqlConnection conn = new(conStr);

            SqlCommand command = new("SELECT id FROM Customer WHERE username COLLATE SQL_Latin1_General_CP1_CS_AS= @username AND password COLLATE SQL_Latin1_General_CP1_CS_AS =@password", conn);


            command.Parameters.AddWithValue("@username", this.Username);
            command.Parameters.AddWithValue("@password", this.Password);


            conn.Open();
            var user_Id = command.ExecuteScalar();
            

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
                AddError(nameof(this.Username), "Esta combinación de usuario y contraseña no es correcta.");
                AddError(nameof(this.Password), "Esta combinación de usuario y contraseña no es correcta.");
            }

        }

        public bool CanTestLogin()
        {
            return this.Username != null && this.Password != null;
        }
    }
}
