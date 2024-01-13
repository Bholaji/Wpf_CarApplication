using Models;
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
using Services.Implementaions;
using Services.Utilities;
using Util = Services.Utilities.Utilities;
using EFCoreData;
using EFCoreData.Interface;

namespace WPF.View
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private readonly IUserEFCoreRepositories serviceEFCore;
        public Login()
        {
            serviceEFCore = new UserServiceEFCore();
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            string Email = txtEmail.Text;
            string Password = txtPassword.Password;
            var user = new User { Email = Email, Password = Password, Role= Role.Regular };

            try
            {
                if(!string.IsNullOrEmpty(Email) || !string.IsNullOrEmpty(Password))
                {
                    bool isLoggedIn = serviceEFCore.LoginUser(user);
                        if (isLoggedIn)
                        {
                            var navigation = new Navigation();
                            navigation.Show();
                            this.Hide();
                            MessageBox.Show("Login successful");
                        }
                        else
                        {
                            // Failed login
                            MessageBox.Show("Invalid email or password");
                        }
                    
                }else
                {
                    MessageBox.Show($"Fill in the appropraite fields");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            var mainView = new MainWindow();
            mainView.Show();
            this.Hide();
        }
    }
}
