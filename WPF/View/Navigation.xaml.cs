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
    /// Interaction logic for Navigation.xaml
    /// </summary>
    public partial class Navigation : Window
    {
        public Navigation()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void AdminPage(object sender, MouseButtonEventArgs e)
        {
            var admin = new Admin();
            admin.Show();
            this.Close();
        }


        private void ProductPage(object sender, MouseButtonEventArgs e)
        {
            var product = new Product();
            product.Show();
            this.Close();
        }

        private void LogOut(object sender, MouseButtonEventArgs e)
        {
            var mainView = new MainWindow();
            mainView.Show();
            this.Close();
        }

        private void CloseApp_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }
}
