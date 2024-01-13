using EFCoreData;
using EFCoreData.Implementations;
using EFCoreData.Interface;
using Models;
using Services.Implementaions;
using Services.Utilities;
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
using serviceUtilities = Services.Utilities.Utilities;

namespace WPF.View
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        private readonly IUtilities utilities;
        private readonly IUserEFCoreRepositories serviceEFCore;
        public SignUp()
        {
            utilities = new serviceUtilities();
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

        private void btnLogin_Click(object sender, RoutedEventArgs e) { }


        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            var mainView = new MainWindow();
            mainView.Show();

            this.Hide();
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            string FirstName = txtFirstName.Text;
            string LastName = txtLastName.Text;
            string Email = txtEmail.Text;
            string Password = txtPassword.Password;
            var user = new User
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Password = Password
            };
            user.Role = Role.Regular;


           

            try
            {
                if (!string.IsNullOrEmpty(FirstName) &&
                    !string.IsNullOrEmpty(LastName) &&
                    !string.IsNullOrEmpty(Email) &&
                    !string.IsNullOrEmpty(Password))
                {
                    bool isEmailValid = utilities.IsValidEmail(Email);
                    bool isEmailExist = serviceEFCore.IsEmailExist(Email);
                    if(!isEmailValid || isEmailExist)
                    {
                        if (!isEmailValid)
                        {
                            txtError.Text = "Email not valid!";
                            txtError.Visibility = Visibility.Visible;
                            
                        }
                        if (isEmailExist)
                        {
                            txtError.Text = "Email already exist!";
                            txtError.Visibility = Visibility.Visible;
                        }
                    }
                    else
                    {
                        serviceEFCore.SignUpUser(user);

                        MessageBox.Show($"Signup Successful");
                        var navigation = new Navigation();
                        navigation.Show();
                        this.Hide();
                    }



                }
                else { MessageBox.Show($"Fill in the appropriate fields"); }
                
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error: " + ex.Message, "Signup Error", MessageBoxButton.OK, MessageBoxImage.Error);
                MessageBox.Show(ex.Message);
            }

        }
    }
}
