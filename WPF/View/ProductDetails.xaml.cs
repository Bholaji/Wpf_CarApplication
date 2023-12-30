using Repository.Implementations;
using Repository.Interfaces;
using Services.Implementaions;
using Services.Interfaces;
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
using ProductVM = Models.Product;

namespace WPF.View
{
    /// <summary>
    /// Interaction logic for ProductDetails.xaml
    /// </summary>
    public partial class ProductDetails : Window
    {
        private readonly IUserService userService;
        public ProductDetails(Guid selectedProductId)
        {
            userService = new UserService();
            InitializeComponent();
            var product = new ProductVM()
            {
                ProductId = selectedProductId
            };
            ProductVM selectedProduct = userService.LoadProductById(product);

            txtProductname.Text = selectedProduct.ProductName;
            txtProductDetails.Text = selectedProduct.ProductDetail;
            txtProductPrice.Text = selectedProduct.ProductPrice;
          

            string imagePath = selectedProduct.ProductImage;

            ImageSourceConverter converter = new();
            ImageSource imageSource = (ImageSource)converter.ConvertFromString(imagePath);

            txtProductImage.ImageSource = imageSource;
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

        private void btnCheckout(object sender, RoutedEventArgs e)
        {
            var transaction = new Transaction();
            transaction.Show();
            this.Hide();
        }
    }
}
