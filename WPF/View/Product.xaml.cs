using EFCoreData.Interface;
using Services.Implementaions;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ProductVM = Models.Product;

namespace WPF.View
{
    /// <summary>
    /// Interaction logic for Product.xaml
    /// </summary>
    public partial class Product : Window
    {
        private readonly IProductEFCoreRepositories productServiceEF;
        private List<ProductVM> ProductList;
        public Product()
        {
            ProductList = new List<ProductVM>();
            productServiceEF = new ProductServiceEFCore();
            InitializeComponent();

            
            LoadProducts();
        }

        private void LoadProducts()
        {
            //ProductList = productService.LoadProduct();
            ProductList = productServiceEF.LoadProducts();
            

            if(ProductList.Count > 0)
            {
                foreach (var p in ProductList)
                {
                    string imagePath = p.ProductImage;

                    ImageSourceConverter converter = new();
                    ImageSource imageSource = (ImageSource)converter.ConvertFromString(imagePath);

                    // Create an Image control
                    Image productImage = new();
                    productImage.Source = imageSource;
                    productImage.Width = 230;
                    productImage.Height = 230;

                    // Create a TextBlock for product name
                    TextBlock productName = new();
                    productName.Text = p.ProductName;
                    productName.Margin = new(0,-10,0,10);

                    // Create a StackPanel to hold each product's image and name
                    StackPanel stackPanel = new();
                    stackPanel.Orientation = Orientation.Vertical;
                    stackPanel.Margin = new Thickness(0,-10,10,0); 
                    stackPanel.Children.Add(productImage);
                    stackPanel.Children.Add(productName);

                    //Pass the productid from the mouseclick event
                    productImage.MouseLeftButtonDown += (sender, e) =>
                    {
                        HandleProductClick(p.ProductId);
                    };

                    // Add the stackPanel to the WrapPanel
                    productsWrapPanel.Children.Add(stackPanel);
                }
            }
            else
            {
                MessageBox.Show("No availble products");
            }
        }

        private void HandleProductClick(Guid ProductId)
        {
            Guid selectedProductId = ProductId;
            var productDetails = new ProductDetails(selectedProductId);
            productDetails.Show();
            this.Hide();
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

        private void btnLogin_Click(object sender, RoutedEventArgs e) { }

        
    }
}
