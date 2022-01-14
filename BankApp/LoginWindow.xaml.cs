using BankApp.ViewModels;
using System;
using System.Windows;

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
