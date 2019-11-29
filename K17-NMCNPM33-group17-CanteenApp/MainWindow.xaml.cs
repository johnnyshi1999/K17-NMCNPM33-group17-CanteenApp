using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
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

namespace K17_NMCNPM33_group17_CanteenApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DatabaseHandler db;
        List<Product> productList;

        Order currentOrder;

        public MainWindow()
        {
            InitializeComponent();
            db = DatabaseHandler.getInstance();

            db.connection.Open();
            //string SQLSelect = "Select * From SANPHAM";
            //SqlCommand cmd = new SqlCommand(SQLSelect, db.connection);
            //SqlDataReader dr = cmd.ExecuteReader();
            //DataTable dt = new DataTable();
            //dt.Load(dr);
            //db.connection.Close();


            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    Product product = new Product()
            //    {
            //        ProductID = dt.Rows[i][0].ToString(),
            //        ProductName = dt.Rows[i][1].ToString(),
            //    };

            //}

            //SqlCommand cmd = new SqlCommand("SP_DanhSachSP", db.connection);
            //cmd.CommandType = CommandType.StoredProcedure;

            //SqlDataReader dr = cmd.ExecuteReader();
            //DataTable dt = new DataTable();
            //dt.Load(dr);

            //DataListView.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = dt });

            productList = new List<Product>();
            currentOrder = new Order();
            
            SqlCommand cmd = new SqlCommand("SP_DanhSachSP", db.connection);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Product product = new Product()
                {
                    ProductID = dt.Rows[i][0].ToString(),
                    ProductName = dt.Rows[i][1].ToString(),
                    price = int.Parse(dt.Rows[i][2].ToString()),
                };
                productList.Add(product);
            }

            for (int i = 0; i < productList.Count; i++)
            {
                TextBlock productName = new TextBlock();
                productName.Text = productList[i].ProductName;
                productName.Style = Resources["SmallText"] as Style;

                ProductListGrid.Children.Add(productName);

                Grid.SetRow(productName, i);
                Grid.SetColumn(productName, 0);

                TextBlock productPrice = new TextBlock();
                productPrice.Text = productList[i].price.ToString();
                productPrice.Style = Resources["SmallText"] as Style;
                productPrice.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2413E3"));

                ProductListGrid.Children.Add(productPrice);

                Grid.SetRow(productPrice, i);
                Grid.SetColumn(productPrice, 1);

                Button AddButton = new Button()
                {
                    Content = new Image
                    {
                        Source = new BitmapImage(new Uri("Images/add-button.png", UriKind.Relative)),
                        VerticalAlignment = VerticalAlignment.Center
                    }
                   
                };
                AddButton.Background = null;
                AddButton.BorderThickness = new Thickness(0, 0, 0, 0);
                AddButton.Tag = i;
                AddButton.Click += AddProduct_Click;

                ProductListGrid.Children.Add(AddButton);

                Grid.SetRow(AddButton, i);
                Grid.SetColumn(AddButton, 2);
               
                ProductListGrid.RowDefinitions.Add(new RowDefinition());

            }

        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            OrderDetail detail = new OrderDetail()
            {
                product = productList[int.Parse(button.Tag.ToString())],
                quantity = 1,
            };
            for (int i = 0; i < currentOrder.detail.Count; i++)
            {
                if (currentOrder.detail[i].product.ProductName == detail.product.ProductName)
                {
                    currentOrder.detail[i].quantity++;
                    SetOrderList();
                    return;
                }
            }

            currentOrder.detail.Add(detail);

            SetOrderList();

            
        }

        void SetOrderList()
        {
            OrderList.Children.Clear();
            OrderList.RowDefinitions.Clear();

            for (int i = 0; i < currentOrder.detail.Count; i++)
            {
                TextBlock productName = new TextBlock();
                productName.Text = currentOrder.detail[i].product.ProductName;
               
                productName.Style = Resources["SmallText"] as Style;

                OrderList.Children.Add(productName);

                Grid.SetRow(productName, i);
                Grid.SetColumn(productName, 0);

                TextBlock productPrice = new TextBlock();
                productPrice.Text = currentOrder.detail[i].product.price.ToString();
                productPrice.Style = Resources["SmallText"] as Style;
                productPrice.HorizontalAlignment = HorizontalAlignment.Right;
                productPrice.Padding = new Thickness(0, 0, 10, 0);

                OrderList.Children.Add(productPrice);

                Grid.SetRow(productPrice, i);
                Grid.SetColumn(productPrice, 1);

                TextBox Quantity = new TextBox();
                Quantity.Text = currentOrder.detail[i].quantity.ToString();
                Quantity.Style = Resources["QuantityTextBox"] as Style;
                Quantity.Tag = i;
                Quantity.TextChanged += ChangeQuantity_TextChange;

                OrderList.Children.Add(Quantity);

                Grid.SetRow(Quantity, i);
                Grid.SetColumn(Quantity, 2);

                TextBlock SumPrice = new TextBlock();
                int sumPrice = currentOrder.detail[i].quantity * currentOrder.detail[i].product.price;
                SumPrice.Text = sumPrice.ToString();
                SumPrice.Style = Resources["SmallText"] as Style;
                SumPrice.HorizontalAlignment = HorizontalAlignment.Right;

                OrderList.Children.Add(SumPrice);

                Grid.SetRow(SumPrice, i);
                Grid.SetColumn(SumPrice, 3);

                OrderList.RowDefinitions.Add(new RowDefinition());
            }

            
        }

        private void ChangeQuantity_TextChange(object sender, TextChangedEventArgs e)
        {
            var textbox = sender as TextBox;
            int index = int.Parse(textbox.Tag.ToString());
            int newQuanity = int.Parse(textbox.Text);
            currentOrder.detail[index].quantity = newQuanity;
        }
    }
}
