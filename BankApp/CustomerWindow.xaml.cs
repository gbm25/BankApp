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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace BankApp
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {

        public Customer current_customer;
        public CustomerWindow()
        {
            current_customer = new();
            InitializeComponent();
        }

        private void SearchIcon_Click(object sender, RoutedEventArgs e)
        {
            if (codeTextbox.Text.Length > 0)
            {
                ResetCustomer();

                bool IsNumber = int.TryParse(codeTextbox.Text.Trim(), out int number);
                if (IsNumber)
                {
                    bool dataStatus = current_customer.FillCustomerData(number);

                    if (dataStatus)
                    {
                        MessageBox.Show("Cliente cargado.");
                        UpdateFields();
                    }
                    else
                    {
                        MessageBox.Show("Cliente no encontrado.");
                        ClearFields();
                    }
                }
                else
                {
                    MessageBox.Show("El dato introducido debe de ser un número.");
                    ClearFields();
                }

            }

            else
            {
                MessageBox.Show("Debe escribir un número de cliente antes de poder realizar una búsqueda.");
            }

        }
        private void NewCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckValideCustomerName(this.firstNameTextbox.Text))
            {
                MessageBox.Show("El nombre debe contener entre 2 y 50 caracteres y no contener dígitos.");
            }
            else if (!CheckValideCustomerName(this.lastNameTextbox.Text))
            {
                MessageBox.Show("El apellido debe contener entre 2 y 50 caracteres y no contener dígitos.");
            }
            else if (!CheckValideCustomerUsername(this.usernameTextbox.Text))
            {
                MessageBox.Show("El nombre de usuario debe contener entre 3 y 50 caracteres y no contener dígitos.");
            }
            else if (!CheckValideCustomerPass(this.passwordPWbox.Password.ToString()))
            {
                MessageBox.Show("La contraseña debe contener entre 5 y 100 caracteres y contener al menos un dígito.");
            }

            else if (!CheckValideCustomerLocation(this.countryTextbox.Text))
            {
                MessageBox.Show("El pais debe contener entre 1 y 100 caracteres y no contener dígitos.");
            }
            else if (!CheckValideCustomerLocation(this.regionTextbox.Text))
            {
                MessageBox.Show("La Region debe contener entre 1 y 100 caracteres y no contener dígitos.");
            }
            else if (!CheckValideCustomerLocation(this.cityTextbox.Text))
            {
                MessageBox.Show("La ciudad debe contener entre 1 y 100 caracteres y no contener dígitos.");
            }
            else if (!CheckValideCustomerAddress(this.addressTextbox.Text))
            {
                MessageBox.Show("La dirección debe contener entre 1 y 250 caracteres.");
            }
            else
            {
                UpdateCustomerInfo();

                bool insertStatus = current_customer.PushNewCustomer();

                if (insertStatus)
                {
                    MessageBox.Show("Se ha creado un nuevo cliente.");
                }
                else
                {
                    MessageBox.Show("No se ha podido crear un nuevo cliente.");
                }

                ClearFields();
                ResetCustomer();
            }

        }
        private void UpdateCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.current_customer.Id == null)
            {
                MessageBox.Show("Debe buscar un cliente antes de poder modificar su información.");
            }
            else if (!CheckValideCustomerName(this.firstNameTextbox.Text))
            {
                MessageBox.Show("El nombre debe contener entre 2 y 50 caracteres y no contener dígitos.");
            }
            else if (!CheckValideCustomerName(this.lastNameTextbox.Text))
            {
                MessageBox.Show("El apellido debe contener entre 2 y 50 caracteres y no contener dígitos.");
            }
            else if (!CheckValideCustomerUsername(this.usernameTextbox.Text))
            {
                MessageBox.Show("El nombre de usuario debe contener entre 3 y 50 caracteres y no contener dígitos.");
            }
            else if (!CheckValideCustomerPass(this.passwordPWbox.Password.ToString()))
            {
                MessageBox.Show("La contraseña debe contener entre 5 y 100 caracteres y contener al menos un dígito.");
            }

            else if (!CheckValideCustomerLocation(this.countryTextbox.Text))
            {
                MessageBox.Show("El pais debe contener entre 1 y 100 caracteres y no contener dígitos.");
            }
            else if (!CheckValideCustomerLocation(this.regionTextbox.Text))
            {
                MessageBox.Show("La Region debe contener entre 1 y 100 caracteres y no contener dígitos.");
            }
            else if (!CheckValideCustomerLocation(this.cityTextbox.Text))
            {
                MessageBox.Show("La ciudad debe contener entre 1 y 100 caracteres y no contener dígitos.");
            }
            else if (!CheckValideCustomerAddress(this.addressTextbox.Text))
            {
                MessageBox.Show("La dirección debe contener entre 1 y 250 caracteres.");
            }
            else
            {
                UpdateCustomerInfo();

                bool updateStatus = current_customer.UpdateCustomerinfo();

                if (updateStatus)
                {
                    MessageBox.Show("Se ha actualizado el cliente.");
                }
                else
                {
                    MessageBox.Show("No se ha podido actualizar el cliente.");
                }

                ClearFields();
                ResetCustomer();
            }
        }
        private void DeleteCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.current_customer.Id == null)
            {
                MessageBox.Show("Debe buscar un cliente antes de poder eliminarlo");
            }
            else
            {
                UpdateCustomerInfo();

                bool deleteStatus = current_customer.DeleteCustomer();

                if (deleteStatus)
                {
                    MessageBox.Show("Se ha eliminado el cliente !");
                }
                else
                {
                    MessageBox.Show("No se ha podido eliminar el cliente.");
                }

                ClearFields();
                ResetCustomer();
            }
        }
        private void UpdateFields()
        {
            this.firstNameTextbox.Text = current_customer.FirstName;
            this.lastNameTextbox.Text = current_customer.LastName;
            this.usernameTextbox.Text = current_customer.Username;
            this.passwordPWbox.Password = current_customer.Password;
            this.countryTextbox.Text = current_customer.Country;
            this.regionTextbox.Text = current_customer.Region;
            this.cityTextbox.Text = current_customer.City;
            this.addressTextbox.Text = current_customer.Address;

        }

        private void UpdateCustomerInfo()
        {
            current_customer.FirstName = this.firstNameTextbox.Text;
            current_customer.LastName = this.lastNameTextbox.Text;
            current_customer.Username = this.usernameTextbox.Text;
            current_customer.Password = this.passwordPWbox.Password.ToString();
            current_customer.Country = this.countryTextbox.Text;
            current_customer.Region = this.regionTextbox.Text;
            current_customer.City = this.cityTextbox.Text;
            current_customer.Address = this.addressTextbox.Text;
        }

        private void ResetCustomer()
        {
            current_customer = new();
        }

        private void ClearFields()
        {
            this.codeTextbox.Text = "";
            this.firstNameTextbox.Text = "";
            this.lastNameTextbox.Text = "";
            this.usernameTextbox.Text = "";
            this.passwordPWbox.Password = "";
            this.countryTextbox.Text = "";
            this.regionTextbox.Text = "";
            this.cityTextbox.Text = "";
            this.addressTextbox.Text = "";
        }

        private static bool CheckValideCustomerName(string Name)
        {
            return Name.Length >= 2 && Name.Length < 50 && !Name.Any(char.IsDigit);
        }
        private static bool CheckValideCustomerUsername(string Username)
        {
            return Username.Length >= 3 && Username.Length <= 50;
        }
        private static bool CheckValideCustomerPass(string Password)
        {
            return Password.Length >= 4 && Password.Length <= 100 && Password.Any(char.IsDigit);
        }
        private static bool CheckValideCustomerLocation(string Location)
        {
            return Location.Length >= 1 && Location.Length <= 100 && !Location.Any(char.IsDigit);
        }

        private static bool CheckValideCustomerAddress(string Address)
        {
            return Address.Length >= 1 && Address.Length <= 250;
        }

        private void ShowAccountsButton_Click(object sender, RoutedEventArgs e)
        {

            if (current_customer.Id != null)
            {
                bool statusUpdateAccounts = current_customer.UpdateBankAccountsList();
                if (statusUpdateAccounts)
                {
                    if (current_customer.BankAccounts.Count == 0)
                    {
                        MessageBox.Show("El cliente no posee cuentas");
                    }
                    else
                    {
                        BankAccountListWindow bankAccountListWindow = new(current_customer.BankAccounts);
                        bankAccountListWindow.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("Se ha producido un error. La lista de cuentas no se ha actualizado");
                }

            }
            else
            {
                MessageBox.Show("Es necesario buscar a un cliente antes de ver sus cuentas");
            }
        }

    }
}
