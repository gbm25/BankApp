using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Configuration;
using System.Data.SqlClient;

namespace BankApp
{
    /// <summary>
    /// Lógica de interacción para LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = this.usernameTextbox.Text.Trim();
            string password = this.passwordTextbox.Password.ToString().Trim();


            if (username.Length > 0 && password.Length > 0)
            {
                bool loginStatus = CheckLogin(username, password);

                if (loginStatus)
                {
                    MessageBox.Show("Identificación correcta!");
                    CustomerWindow CustomerWindow = new();
                    App.Current.MainWindow = CustomerWindow;
                    this.Close();
                    CustomerWindow.Show();
                }
                else
                {
                    MessageBox.Show("Login incorrecto!");
                }
            }
            else
            {
                MessageBox.Show("Debe cubrir todos los campos para poder iniciar sesión");
            }
        }

        private static bool CheckLogin(string username, string password)
        {

            string conStr = ConfigurationManager.ConnectionStrings["bankapp"].ToString();

            SqlConnection conn = new(conStr); ;
            
            SqlCommand command = new("Select id from Customer where username= @username and     password=@password" , conn);


            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);

           
            conn.Open();
            var user_Id = command.ExecuteScalar();
            conn.Close();

            if (user_Id != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
