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
                string userEmail = txtEmail.Text;
                utilities.SendConfirmationEmail(userEmail);
                 /*SendEmailAsync(userEmail).Wait();*/
                MessageBox.Show("Confirmation EMail sent successfully!");
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        /*public async Task SendEmailAsync(string userEmail)
        {
            try
            {
                string apiKey = "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";
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("bolajijohnson19@gmail.com", "Bolaji");
                var to = new EmailAddress(userEmail);
                var subject = "Confirmation of Your Order";
                var plainTextContent = "Thank you for your order! This is a confirmation email.";
                var htmlContent = "<strong>Thank you for your order!</strong> This is a confirmation email.";

                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(msg);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }*/
    }
}
