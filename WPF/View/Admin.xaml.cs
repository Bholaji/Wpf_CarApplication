using EFCoreData.Interface;
using Models;
using Services.Implementaions;
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

namespace WPF.View
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        private readonly IUserEFCoreRepositories serviceEFCore;
        public Admin()
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

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            var navigation = new Navigation();
            navigation.Show();
            this.Hide();
        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            string Email = txtEmail.Text;
            string Password = txtPassword.Password;
            var userAdmin = new User { Email = Email, Password = Password, Role = Role.Admin };

            try
            {
                if (!string.IsNullOrEmpty(Email) || !string.IsNullOrEmpty(Password))
                {

                    //bool isAdmin = userService.AdminLogIn(userAdmin);
                    bool isAdmin = serviceEFCore.LoginAdminUser(userAdmin);
                    if (isAdmin)
                    {
                        var adminPage = new AdminPage();
                        adminPage.Show();
                        this.Hide();
                        MessageBox.Show("Login successful");
                    }
                    else
                    {
                        MessageBox.Show("Invalid email or password");
                    }
                }
                else
                {
                    MessageBox.Show($"Fill in the appropraite fields");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
