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
    public class CustomerVM : INotifyPropertyChanged
    {

        private Customer customer;

        private RelayCommand? _SearchCustomerAction;

        private RelayCommand? _NewCustomerAction;

        private RelayCommand? _UpdateCustomerAction;

        private RelayCommand? _DeleteCustomerAction;

        private RelayCommand? _ShowAccountsAction;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public int? ID
        {
            get { return customer.ID; }
            set
            {
                customer.ID = value;
                OnPropertyChanged();
            }
        }

        public string? FirstName
        {
            get { return customer.FirstName; }
            set
            {
                customer.FirstName = value;
                OnPropertyChanged();
            }
        }
        public string? LastName
        {
            get { return customer.LastName; }
            set
            {
                customer.LastName = value;
                OnPropertyChanged();
            }
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
        public string? Country
        {
            get { return customer.Country; }
            set
            {
                customer.Country = value;
                OnPropertyChanged();
            }
        }
        public string? Region
        {
            get { return customer.Region; }
            set
            {
                customer.Region = value;
                OnPropertyChanged();
            }
        }
        public string? City
        {
            get { return customer.City; }
            set
            {
                customer.City = value;
                OnPropertyChanged();
            }
        }
        public string? Address
        {
            get { return customer.Address; }
            set
            {
                customer.Address = value;
                OnPropertyChanged();
            }
        }
        public DateTime? LastUpdate
        {
            get { return customer.LastUpdate; }
            set
            {
                customer.LastUpdate = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<BankAccount> BankAccount
        {
            get { return customer.BankAccount; }
            set
            {
                customer.BankAccount = value;
                OnPropertyChanged();
            }
        }
        public CustomerVM()
        {
            customer = new Customer();
        }

        public bool UpdateBankAccountsList()
        {
            string conStr = ConfigurationManager.ConnectionStrings["bankapp"].ToString();
            SqlConnection conn;

            conn = new SqlConnection(conStr);

            conn.Open();
            SqlCommand command = new("SELECT * FROM Account WHERE customer_id=@id", conn);

            command.Parameters.AddWithValue("@id", customer.ID);

            try
            {
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    BankAccount account = new();
                    account.ID = (int)reader["id"];
                    account.CustomerID = (int)reader["customer_id"];
                    account.Number = (string)reader["account_number"];
                    account.Description = reader["description"] == DBNull.Value ? null : (string)reader["description"];

                    this.BankAccount.Add(account);

                }
                conn.Close();
                return true;
            }
            catch
            {
                return false;
            }

        }
        public void ResetCustomer()
        {
            this.ID = null;
            this.FirstName = null;
            this.LastName = null;
            this.Username = null;
            this.Password = null;
            this.Country = null;
            this.Region = null;
            this.City = null;
            this.Address = null;
            this.BankAccount.Clear();
        }
        public ICommand SearchCustomer
        {
            get
            {
                if (_SearchCustomerAction == null)
                {
                    _SearchCustomerAction = new RelayCommand(param => SearchCustomerOnDB(param), param => CanSearchCustomerOnDB(param));
                }
                return _SearchCustomerAction;
            }
        }

        public void SearchCustomerOnDB(object param)
        {
            
            _ = int.TryParse((string?)param, out int number);
            string conStr = ConfigurationManager.ConnectionStrings["bankapp"].ToString();
            SqlConnection conn;

            conn = new SqlConnection(conStr);

            conn.Open();
            SqlCommand command = new("Select * from Customer where id=@id", conn);

            command.Parameters.AddWithValue("@id", number);

            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                this.ID = (int)reader["id"];
                this.FirstName = (string)reader["first_name"];
                this.LastName = (string)reader["last_name"];
                this.Username = (string)reader["username"];
                this.Password = (string)reader["password"];
                this.Country = (string)reader["country"];
                this.Region = (string)reader["region"];
                this.City = (string)reader["city"];
                this.Address = (string)reader["address"];
                this.LastUpdate = reader["last_update"] == DBNull.Value ? null : (DateTime)reader["last_update"];
                this.BankAccount.Clear();

                conn.Close();
                MessageBox.Show("Cliente cargado.");
            }
            else
            {
                conn.Close();
                MessageBox.Show("Cliente no encontrado.");
            }

        }

        public bool CanSearchCustomerOnDB(object? param)
        {
            bool IsNumber = int.TryParse((string?)param, out _);

            return IsNumber;
        }

        public ICommand NewCustomer
        {
            get
            {
                if (_NewCustomerAction == null)
                {
                    _NewCustomerAction = new RelayCommand(param => CreateCustomerOnDB(), param => CanCreateCustomerOnDB());
                }
                return _NewCustomerAction;
            }
        }

        public void CreateCustomerOnDB()
        {
            string conStr = ConfigurationManager.ConnectionStrings["bankapp"].ToString();

            using SqlConnection connection = new(conStr);
            string query = "INSERT INTO dbo.Customer(first_name,last_name,username,password,country,region,city,address) VALUES (@first_name,@last_name,@username,@password,@country,@region,@city,@address)";

            using SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@first_name", this.FirstName);
            command.Parameters.AddWithValue("@last_name", this.LastName);
            command.Parameters.AddWithValue("@username", this.Username);
            command.Parameters.AddWithValue("@password", this.Password);
            command.Parameters.AddWithValue("@country", this.Country);
            command.Parameters.AddWithValue("@region", this.Region);
            command.Parameters.AddWithValue("@city", this.City);
            command.Parameters.AddWithValue("@address", this.Address);

            connection.Open();
            int result = command.ExecuteNonQuery();

            if (result > 0)
            {
                MessageBox.Show("Cliente creado.");
                ResetCustomer();
            }
            else
            {
                MessageBox.Show("No se ha podido crear el cliente.");
            }

        }

        public bool CanCreateCustomerOnDB()
        {
            return this.FirstName != null && this.LastName != null && this.Username != null && this.Password != null && this.Country != null && this.Region != null && this.City != null && this.Address != null;
        }

        public ICommand UpdateCustomer
        {
            get
            {
                if (_UpdateCustomerAction == null)
                {
                    _UpdateCustomerAction = new RelayCommand(param => UpdateCustomerOnDB(), param => CanUpdateCustomerOnDB());
                }
                return _UpdateCustomerAction;
            }
        }

        public void UpdateCustomerOnDB()
        {
            string conStr = ConfigurationManager.ConnectionStrings["bankapp"].ToString();

            using SqlConnection connection = new(conStr);

            string checkExist = @"IF EXISTS(SELECT * FROM Customer WHERE id = @id ";
            if (this.LastUpdate != null)
            {
                checkExist += @"AND (last_update IS NULL or last_update = @last_update))";
            }
            else
            {
                checkExist += @"AND last_update IS NULL)";
            }


            string query = checkExist + @"
                                 BEGIN
                                    UPDATE Customer 
                                    SET first_name = @first_name,
					                last_name = @last_name,
					                username = @username,
					                password = @password,
					                country = @country,
					                region = @region,
					                city = @city,
					                address = @address,
					                last_update = GETUTCDATE()
                                    WHERE id = @id;
                                 END";

            using SqlCommand command = new(query, connection);

            command.Parameters.AddWithValue("@id", this.ID);
            command.Parameters.AddWithValue("@first_name", this.FirstName);
            command.Parameters.AddWithValue("@last_name", this.LastName);
            command.Parameters.AddWithValue("@username", this.Username);
            command.Parameters.AddWithValue("@password", this.Password);
            command.Parameters.AddWithValue("@country", this.Country);
            command.Parameters.AddWithValue("@region", this.Region);
            command.Parameters.AddWithValue("@city", this.City);
            command.Parameters.AddWithValue("@address", this.Address);
            if (this.LastUpdate != null)
            {
                command.Parameters.AddWithValue("@last_update", this.LastUpdate);
            }

            connection.Open();
            int result = command.ExecuteNonQuery();

            if (result > 0)
            {
                MessageBox.Show("Cliente actualizado.");
                ResetCustomer();
            }
            else
            {
                MessageBox.Show("No se ha podido actualizar el cliente.");
            }

        }

        public bool CanUpdateCustomerOnDB()
        {
            return this.FirstName != null && this.LastName != null && this.Username != null && this.Password != null && this.Country != null && this.Region != null && this.City != null && this.Address != null;
        }

        public ICommand DeleteCustomer
        {
            get
            {
                if (_DeleteCustomerAction == null)
                {
                    _DeleteCustomerAction = new RelayCommand(param => DeleteCustomerOnDB(), param => CanDeleteCustomerOnDB());
                }
                return _DeleteCustomerAction;
            }
        }

        public void DeleteCustomerOnDB()
        {
            string conStr = ConfigurationManager.ConnectionStrings["bankapp"].ToString();

            using SqlConnection connection = new(conStr);

            string query = @"DELETE FROM Customer WHERE id = @id;";

            using SqlCommand command = new(query, connection);

            command.Parameters.AddWithValue("@id", customer.ID);


            connection.Open();
            int result = command.ExecuteNonQuery();

            if (result > 0)
            {
                MessageBox.Show("Cliente eliminado de la base de datos.");
                ResetCustomer();
            }
            else
            {
                MessageBox.Show("No se ha podido borrar el cliente.");
            }

        }

        public bool CanDeleteCustomerOnDB()
        {
            return this.ID != null;
        }

        public ICommand ShowCustomerAccountsAction
        {
            get
            {
                if (_ShowAccountsAction == null)
                {
                    _ShowAccountsAction = new RelayCommand(param => ShowCustomerAccounts(), param => CanShowCustomerAccounts());
                }
                return _ShowAccountsAction;
            }
        }

        public void ShowCustomerAccounts()
        {

            bool updateAccounts = UpdateBankAccountsList();

            if (!updateAccounts)
            {
                MessageBox.Show("Se ha producido un error inesperado al intentar recuperar las cuentas del cliente.");
            }
            else if (this.BankAccount.Count == 0)
            {
                MessageBox.Show("El cliente no tiene cuentas bancarias");
            }
            else
            {
                BankAccountListWindow bankAccountListWindow = new(this.BankAccount);
                bankAccountListWindow.ShowDialog();
            }
        }

        public bool CanShowCustomerAccounts()
        {
            return this.ID != null;
        }
    }

}


