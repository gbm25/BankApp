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
using Microsoft.Data.SqlClient;
using BankApp.ViewModels;

namespace BankApp
{
    /// <summary>
    /// Lógica de interacción para LoginWindow.xaml
    /// </summary>
    /// 
   
    public partial class LoginWindow : Window
    {
        private readonly LoginViewModel _loginviewmodel;
        public LoginWindow()
        {
            InitializeComponent();

            _loginviewmodel = new();

            DataContext = _loginviewmodel;
            if (_loginviewmodel.CloseAction == null)
                _loginviewmodel.CloseAction = new Action(() => this.Close());
        }

        private void Close_Window(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
