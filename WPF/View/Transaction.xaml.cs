using SendGrid.Helpers.Mail;
using SendGrid;
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
using serviceUtility = Services.Utilities.Utilities;

namespace WPF.View
{
    /// <summary>
    /// Interaction logic for Transaction.xaml
    /// </summary>
    public partial class Transaction : Window
    {
        private readonly IUtilities utilities;
        public Transaction()
        {
            utilities = new serviceUtility();
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
            var product = new Product();
            product.Show();
            this.Hide();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e) { }

        private void btnCheckout_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string userEmail = txtEmail.Text.Trim();
                /*utilities.SendConfirmationEmail(userEmail);*/
                Task.Run(() => utilities.SendEmailAsync());
                MessageBox.Show("Your order is being processed now!");
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        
    }
}
