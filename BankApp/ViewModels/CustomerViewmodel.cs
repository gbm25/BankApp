using BankApp.Utils;
using BankApp.Validations;
using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class CustomerVM : INotifyPropertyChanged, INotifyDataErrorInfo
    {

        private Customer customer;

        private RelayCommand? _SearchCustomerAction;

        private RelayCommand? _NewCustomerAction;

        private RelayCommand? _UpdateCustomerAction;

        private RelayCommand? _DeleteCustomerAction;

        private RelayCommand? _ShowAccountsAction;

        private string? _PlaceholderID;

        public event PropertyChangedEventHandler? PropertyChanged;

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        private Dictionary<String, List<String>> Errors { get; }

        // Mapea el nombre de una propiedad a una lista ValidationRule que pertenece a esa propiedad
        private Dictionary<String, List<ValidationRule>> ValidationRules { get; }

        #region Propiedades
        public int? ID
        {
            get { return customer.ID; }
            set
            {
                ValidateProperty(value);
                customer.ID = value;
                OnPropertyChanged();
            }
        }

        public string? FirstName
        {
            get { return customer.FirstName; }
            set
            {
                ValidateProperty(value);
                customer.FirstName = value;
                OnPropertyChanged();
            }
        }
        public string? LastName
        {
            get { return customer.LastName; }
            set
            {
                ValidateProperty(value);
                customer.LastName = value;
                OnPropertyChanged();
            }
        }
        public string? Username
        {
            get { return customer.Username; }
            set
            {
                ValidateProperty(value);
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
                customer.Password = value;
                OnPropertyChanged();
            }
        }
        public string? Country
        {
            get { return customer.Country; }
            set
            {
                ValidateProperty(value);
                customer.Country = value;
                OnPropertyChanged();
            }
        }
        public string? Region
        {
            get { return customer.Region; }
            set
            {
                ValidateProperty(value);
                customer.Region = value;
                OnPropertyChanged();
            }
        }
        public string? City
        {
            get { return customer.City; }
            set
            {
                ValidateProperty(value);
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
                ValidateProperty(value);
                customer.LastUpdate = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<BankAccount> BankAccount
        {
            get { return customer.BankAccount; }
            set
            {
                ValidateProperty(value);
                customer.BankAccount = value;
                OnPropertyChanged();
            }
        }

        public string? PlaceholderID
        {
            get { return this._PlaceholderID; }
            set
            {
                ValidateProperty(value);
                this._PlaceholderID = value;
            }
        }
        #endregion
        public CustomerVM()
        {
            this.Errors = new Dictionary<string, List<string>>();
            this.ValidationRules = new Dictionary<string, List<ValidationRule>>();
            customer = new Customer();
            PlaceholderID = null;
            // Crea un diccionario con las reglas de validaciones.
            // Cada propiedad validada se puede mapear a uno o maws ValidationRule.
            this.ValidationRules.Add(nameof(this.PlaceholderID), new List<ValidationRule>() { new CustomerIDValidationRule() });
            this.ValidationRules.Add(nameof(this.FirstName), new List<ValidationRule>() { new CustomerFirstNameValidationRule() });
            this.ValidationRules.Add(nameof(this.LastName), new List<ValidationRule>() { new CustomerLastNameValidationRule() });
            this.ValidationRules.Add(nameof(this.Username), new List<ValidationRule>() { new CustomerUsernameValidationRule() });
            this.ValidationRules.Add(nameof(this.Password), new List<ValidationRule>() { new CustomerPasswordValidationRule() });
            this.ValidationRules.Add(nameof(this.Country), new List<ValidationRule>() { new CustomerCountryValidationRule() });
            this.ValidationRules.Add(nameof(this.Region), new List<ValidationRule>() { new CustomerRegionValidationRule() });
            this.ValidationRules.Add(nameof(this.City), new List<ValidationRule>() { new CustomerCityValidationRule() });
            this.ValidationRules.Add(nameof(this.Address), new List<ValidationRule>() { new CustomerAddressValidationRule() });

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


            if (this.ValidationRules.TryGetValue(propertyName, out List<ValidationRule>? propertyValidationRules))
            {
                // Aplica las validaciones relacionadas con la propiedad y valida su valor
                propertyValidationRules
                  .Select(validationRule => validationRule.Validate(propertyValue, CultureInfo.CurrentCulture))
                  .Where(result => !result.IsValid)
                  .ToList()
                  .ForEach(invalidResult => AddError(propertyName, (string)invalidResult.ErrorContent));

                return !PropertyHasErrors(propertyName);
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

        // Devuelve 'true' si alguna propiedad que forma parte de la infromación del cliente contiene errores
        public bool CustomerInformationHasErrors()
        {
            return this.Errors.Where(property => property.Key != nameof(this.ID)).Any();
        }
        #endregion

        #region Funciones sobre instancia

        public bool CustomerInfoNotNull()
        {
            return this.FirstName != null && this.LastName != null && this.Username != null && this.Password != null && this.Country != null && this.Region != null && this.City != null && this.Address != null;
        }


        ///<summary>
        ///Devuelve una tuple, que contiene 'true' si existe el o se produjo un error y un string con el mensaje correspondiente, o un 'false' y un string vacii de no ser asi.
        ///</summary>
        public static Tuple<bool, string> CheckUsernameAlreadyExist(string connString, string? Username, int? ID)
        {
            // Lanza un error si Username es null
            if (Username == null)
            {
                throw new ArgumentNullException(Username);
            }
            string checkUsername;

            if (ID != null)
            {
                checkUsername = @"SELECT * FROM Customer WHERE username = @username and id != @id";
            }
            else
            {
                checkUsername = @"SELECT * FROM Customer WHERE username = @username";
            }

            using SqlConnection connection = new(connString);

            using SqlCommand checkUsernamecmd = new(checkUsername, connection);

            checkUsernamecmd.Parameters.AddWithValue("@username", Username);

            if (ID != null)
            {
                checkUsernamecmd.Parameters.AddWithValue("@id", ID);
            }

            try
            {
                connection.Open();
                using SqlDataReader reader = checkUsernamecmd.ExecuteReader();
                if (reader.HasRows)
                {
                    return new Tuple<bool, string>(true, "Nombre de usuario ya en uso.");
                }
                else
                {
                    return new Tuple<bool, string>(false, "");
                }

            }
            catch (Exception ex)
            {
                return new Tuple<bool, string>(true, $"Se ha producido un error inesperado.\r\n{ex}");
            }


        }
        public bool UpdateBankAccountsList()
        {
            string conStr = ConfigurationManager.ConnectionStrings["bankapp"].ToString();

            using SqlConnection conn = new(conStr);

            SqlCommand command = new("SELECT * FROM Account WHERE customer_id=@id", conn);

            command.Parameters.AddWithValue("@id", customer.ID);

            try
            {
                conn.Open();
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

        #endregion

        #region Acciones botones
        public ICommand SearchCustomer
        {
            get
            {
                if (_SearchCustomerAction == null)
                {
                    _SearchCustomerAction = new RelayCommand(param => SearchCustomerOnDB(), param => CanSearchCustomerOnDB());
                }
                return _SearchCustomerAction;
            }
        }

        public void SearchCustomerOnDB()
        {

            _ = int.TryParse((string?)this.PlaceholderID, out int number);

            string conStr = ConfigurationManager.ConnectionStrings["bankapp"].ToString();
           
            using SqlConnection conn = new(conStr);

            conn.Open();
            SqlCommand command = new("SELECT * from Customer where id=@id", conn);

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

                MessageBox.Show("Cliente cargado.");
            }
            else
            {
                MessageBox.Show("Cliente no encontrado.");
            }
        }

        public bool CanSearchCustomerOnDB()
        {
            return !String.IsNullOrEmpty(this.PlaceholderID) && !PropertyHasErrors(nameof(this.PlaceholderID));
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

            Tuple<bool, string> userExist = CheckUsernameAlreadyExist(conStr, this.Username, null);
            if (userExist.Item1)
            {
                AddError(nameof(this.Username), userExist.Item2);
                return;
            }

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
                this.Errors.Clear();
            }
            else
            {
                MessageBox.Show("No se ha podido crear el cliente.");
            }
        }

        public bool CanCreateCustomerOnDB()
        {
            return CustomerInfoNotNull() && !CustomerInformationHasErrors();
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

            Tuple<bool,string> userExist = CheckUsernameAlreadyExist(conStr, this.Username,this.ID);
            if (userExist.Item1)
            {
                AddError(nameof(this.Username), userExist.Item2);
                return;
            }

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

            string queryUpdate = checkExist + @"
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

            using SqlCommand queryUpdatecmd = new(queryUpdate, connection);

            queryUpdatecmd.Parameters.AddWithValue("@id", this.ID);
            queryUpdatecmd.Parameters.AddWithValue("@first_name", this.FirstName);
            queryUpdatecmd.Parameters.AddWithValue("@last_name", this.LastName);
            queryUpdatecmd.Parameters.AddWithValue("@username", this.Username);
            queryUpdatecmd.Parameters.AddWithValue("@password", this.Password);
            queryUpdatecmd.Parameters.AddWithValue("@country", this.Country);
            queryUpdatecmd.Parameters.AddWithValue("@region", this.Region);
            queryUpdatecmd.Parameters.AddWithValue("@city", this.City);
            queryUpdatecmd.Parameters.AddWithValue("@address", this.Address);
            if (this.LastUpdate != null)
            {
                queryUpdatecmd.Parameters.AddWithValue("@last_update", this.LastUpdate);
            }

            connection.Open();

            int result = queryUpdatecmd.ExecuteNonQuery();

            if (result > 0)
            {
                MessageBox.Show("Cliente actualizado.");
                ResetCustomer();
                this.Errors.Clear();
            }
            else
            {
                MessageBox.Show("No se ha podido actualizar el cliente.");
            }

        }

        public bool CanUpdateCustomerOnDB()
        {
            return this.ID != null && CustomerInfoNotNull() && !CustomerInformationHasErrors();
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
        #endregion
    }

}


