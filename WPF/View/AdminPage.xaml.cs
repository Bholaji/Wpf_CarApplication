using Microsoft.Win32;
using Services.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
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
using ProductVM = Models.Product;
using Services.Implementaions;
using EFCoreData.Interface;

namespace WPF.View
{
    /// <summary>
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Window
    {
        private readonly IProductEFCoreRepositories productServiceEF;
        private string imagePath;
        public AdminPage()
        {
            productServiceEF = new ProductServiceEFCore();
            InitializeComponent();
        }

        private void SelectImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new();
            openFileDialog.Filter = "Image files |*.jpg;*.jpeg;*.png;*.gif;*.bmp";

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    BitmapImage bitmap = new();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(openFileDialog.FileName);
                    bitmap.EndInit();
                    imgPreview.Source = bitmap;
                    imagePath = openFileDialog.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnSubmit(object sender, RoutedEventArgs e)
        {
            string carName = txtCarName.Text;
            string carPrice = txtCarPrice.Text;
            string carDetails = txtCarDetails.Text;
            string carImagePath = imagePath;
            /*string carImagePath = utilities.UploadImageAndGetUrl(imagePath);*/

            var product = new ProductVM
            {
                ProductName = carName,
                ProductPrice = carPrice,
                ProductDetail = carDetails,
                ProductImage = carImagePath
            };

            try
            {
                //productService.StoreProductToDB(product);
                productServiceEF.StoreProduct(product);
                MessageBox.Show("Product added successsfully");

                txtCarName.Text = "";
                txtCarPrice.Text = "";
                txtCarDetails.Text = "";
                imagePath = "";
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
    }
}
