using System;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Runtime.CompilerServices;
using Microsoft.Data.SqlClient;

namespace BankApp
{
    public class CustomerVM : INotifyPropertyChanged
    {

        private Customer customer;

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

       

        public bool FillCustomerData(int id)
        {
            string conStr = ConfigurationManager.ConnectionStrings["bankapp"].ToString();
            SqlConnection conn;


            conn = new SqlConnection(conStr);

            conn.Open();
            SqlCommand command = new("Select * from Customer where id=@id", conn);

            command.Parameters.AddWithValue("@id", id);

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

                conn.Close();
                return true;
            }
            else
            {
                conn.Close();
                return false;
            }

        }
        public bool PushNewCustomer()
        {
            string conStr = ConfigurationManager.ConnectionStrings["bankapp"].ToString();

            using SqlConnection connection = new(conStr);
            string query = "INSERT INTO dbo.Customer(first_name,last_name,username,password,country,region,city,address) VALUES (@first_name,@last_name,@username,@password,@country,@region,@city,@address)";

            using SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@first_name", customer.FirstName);
            command.Parameters.AddWithValue("@last_name", customer.LastName);
            command.Parameters.AddWithValue("@username", customer.Username);
            command.Parameters.AddWithValue("@password", customer.Password);
            command.Parameters.AddWithValue("@country", customer.Country);
            command.Parameters.AddWithValue("@region", customer.Region);
            command.Parameters.AddWithValue("@city", customer.City);
            command.Parameters.AddWithValue("@address", customer.Address);
            try
            {
                connection.Open();
                int result = command.ExecuteNonQuery();

                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }



        }
        public bool UpdateCustomerinfo()
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

            command.Parameters.AddWithValue("@id", customer.ID);
            command.Parameters.AddWithValue("@first_name", customer.FirstName);
            command.Parameters.AddWithValue("@last_name", customer.LastName);
            command.Parameters.AddWithValue("@username", customer.Username);
            command.Parameters.AddWithValue("@password", customer.Password);
            command.Parameters.AddWithValue("@country", customer.Country);
            command.Parameters.AddWithValue("@region", customer.Region);
            command.Parameters.AddWithValue("@city", customer.City);
            command.Parameters.AddWithValue("@address", customer.Address);
            if (this.LastUpdate != null)
            {
                command.Parameters.AddWithValue("@last_update", this.LastUpdate);
            }

            try
            {
                connection.Open();
                int result = command.ExecuteNonQuery();

                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        public bool DeleteCustomer()
        {
            string conStr = ConfigurationManager.ConnectionStrings["bankapp"].ToString();

            using SqlConnection connection = new(conStr);

            string query = @"DELETE FROM Customer WHERE id = @id;";

            using SqlCommand command = new(query, connection);

            command.Parameters.AddWithValue("@id", customer.ID);

            try
            {
                connection.Open();
                int result = command.ExecuteNonQuery();

                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
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

                    BankAccount.Add(account);

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
            customer = new();
        }
    }

}
