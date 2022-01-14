using System.Collections.ObjectModel;
using System.Windows;

namespace BankApp
{
    /// <summary>
    /// Lógica de interacción para BankAccountListWindow.xaml
    /// </summary>
    public partial class BankAccountListWindow : Window
    {
        public BankAccountListWindow(ObservableCollection<BankAccount> CustomerAccounts)
        {
            InitializeComponent();
            CustomerAccountsLW.ItemsSource = CustomerAccounts;
        }
    }
}
