using BankApp.ViewModels;
using System.Windows;

namespace BankApp
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {

        private readonly CustomerVM _customervm;
        public CustomerWindow()
        {

            InitializeComponent();

            _customervm = new CustomerVM();

            DataContext = _customervm;

        }


    }
}
