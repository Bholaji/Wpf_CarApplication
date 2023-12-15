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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_CarApplication.View;

namespace WPF_CarApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnTransaction_Click(object sender, RoutedEventArgs e)
        {
            var transaction = new Transaction();
            transaction.Show();
            this.Close();
        }

        private void btnLoggedHome_Click(object sender, RoutedEventArgs e)
        {
            var loggedHome = new LoggedHome();
            loggedHome.Show();
            this.Close();
        }

        private void btnProduct_Click(object sender, RoutedEventArgs e)
        {
            var product = new Product();
            product.Show();
            this.Close();
        }

        private void btnSetting_Click(object sender, RoutedEventArgs e)
        {
            var setting = new Setting();
            setting.Show();
            this.Close();
        }

        private void CloseApp_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
