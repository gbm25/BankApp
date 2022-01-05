using System;

using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;

namespace BankApp
{
    public class Customer
    {

        public int? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Country { get; set; }
        public string? Region { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public DateTime? LastUpdate { get; set; }
        public ObservableCollection<BankAccount> BankAccounts { get; set; }

    public Customer()
        {
            Id = null;
            FirstName = null;
            LastName = null;
            Username = null;
            Password = null;
            Country = null;
            Region = null;
            City = null;
            Address = null;
            LastUpdate = null;
            BankAccounts = new();

        }
        public Customer(int? id, string? firstName, string? lastName, string? username, string? password, string? country, string? region, string? city, string? address)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Password = password;
            Country = country;
            Region = region;
            City = city;
            Address = address;
            BankAccounts = new();
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
                Id = (int)reader["id"];
                FirstName = (string)reader["first_name"];
                LastName = (string)reader["last_name"];
                Username = (string)reader["username"];
                Password = (string)reader["password"];
                Country = (string)reader["country"];
                Region = (string)reader["region"];
                City = (string)reader["city"];
                Address = (string)reader["address"];
                LastUpdate = reader["last_update"] == DBNull.Value ? null : (DateTime)reader["last_update"];

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
            command.Parameters.AddWithValue("@first_name", FirstName);
            command.Parameters.AddWithValue("@last_name", LastName);
            command.Parameters.AddWithValue("@username", Username);
            command.Parameters.AddWithValue("@password", Password);
            command.Parameters.AddWithValue("@country", Country);
            command.Parameters.AddWithValue("@region", Region);
            command.Parameters.AddWithValue("@city", City);
            command.Parameters.AddWithValue("@address", Address);
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
                checkExist += @"and (last_update IS NULL or last_update = @last_update))";
            }
            else
            {
                checkExist += @"and last_update IS NULL)";
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

            command.Parameters.AddWithValue("@id", this.Id);
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

            command.Parameters.AddWithValue("@id", this.Id);

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
            SqlCommand command = new("Select * from Account where customer_id=@id", conn);

            command.Parameters.AddWithValue("@id", this.Id);

           
            try
            {
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    BankAccount account = new();
                    account.Id = (int)reader["id"];
                    account.CustomerId = (int)reader["customer_id"];
                    account.Number = (string)reader["account_number"];
                    account.Description = reader["description"] == DBNull.Value ? null : (string)reader["description"];

                    BankAccounts.Add(account);

                }
                conn.Close();
                return true;
            }
            catch
            {
                return false;
            }
           


        }
    }
}