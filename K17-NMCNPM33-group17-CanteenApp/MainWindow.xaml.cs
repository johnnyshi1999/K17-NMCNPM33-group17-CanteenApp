using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
		DateTime currentDate { get; set; }
		int currentNumber { get; set; }
		
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

			Binding orderSumBinding = new Binding("OrderSum");
            orderSumBinding.Source = currentOrder;
            // Bind the new data source to the myText TextBlock control's Text dependency property.
            totalAmountDue.SetBinding(TextBlock.TextProperty, orderSumBinding);

            Binding ChangeBinding = new Binding("Change");
            ChangeBinding.Source = currentOrder;
            // Bind the new data source to the myText TextBlock control's Text dependency property.
            changeAmount.SetBinding(TextBlock.TextProperty, ChangeBinding);

            Binding QuantityBinding = new Binding("Quantity");
            QuantityBinding.Source = currentOrder;
            // Bind the new data source to the myText TextBlock control's Text dependency property.
            //QuantityTextBlock.SetBinding(TextBlock.TextProperty, QuantityBinding);

			setSearchProductList(dt);

			db.connection.Close();					  

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
				SumPrice.Padding = new Thickness(0, 0, 10, 0);
				
				OrderList.Children.Add(SumPrice);

                Grid.SetRow(SumPrice, i);
                Grid.SetColumn(SumPrice, 3);

				Button DeleteButton = new Button()
                {
                    Content = new Image
                    {
                        Source = new BitmapImage(new Uri("Images/clear.png", UriKind.Relative)),
                        VerticalAlignment = VerticalAlignment.Center
                    }

                };
                DeleteButton.Background = null;
                DeleteButton.BorderThickness = new Thickness(0, 0, 0, 0);
                DeleteButton.Tag = i;
                DeleteButton.Click += DeleteProduct_Click;
                DeleteButton.Margin = new Thickness(10, 0, 0, 0);

                OrderList.Children.Add(DeleteButton);

                Grid.SetRow(DeleteButton, i);
                Grid.SetColumn(DeleteButton, 4);
				
				OrderList.RowDefinitions.Add(new RowDefinition());
			}

			//Sumary.Children.Clear();

            //TextBlock OrderSum = new TextBlock();
            //OrderSum.Text = currentOrder.Sum.ToString();
            //OrderSum.Style = Resources["LargeText"] as Style;
            //OrderSum.HorizontalAlignment = HorizontalAlignment.Right;
            //OrderSum.Padding = new Thickness(0, 0, 10, 0);

            //Sumary.Children.Add(OrderSum);
            //Grid.SetRow(OrderSum, 0);
            //Grid.SetColumn(OrderSum, 1);

            //TextBlock QuantitySum = new TextBlock();
            //QuantitySum.Text = currentOrder.Quantity.ToString();
            //QuantitySum.Style = Resources["LargeText"] as Style;
            //QuantitySum.HorizontalAlignment = HorizontalAlignment.Right;
            //QuantitySum.Padding = new Thickness(0, 0, 10, 0);

            //Sumary.Children.Add(QuantitySum);
            //Grid.SetRow(QuantitySum, 0);
            //Grid.SetColumn(QuantitySum, 0);

            //TextBox Recieve = new TextBox();
            //Recieve.Text = currentOrder.Receive.ToString();
            //Recieve.Style = Resources["QuantityTextBox"] as Style;
            //Recieve.TextChanged += ChangeRecieve_TextChange;

            //Sumary.Children.Add(Recieve);
            //Grid.SetRow(Recieve, 1);
            //Grid.SetColumn(Recieve, 1);


            //TextBlock OrderChange = new TextBlock();
            //OrderChange.Text = currentOrder.Change.ToString();
            //OrderChange.Style = Resources["LargeText"] as Style;
            //OrderChange.HorizontalAlignment = HorizontalAlignment.Right;
            //OrderChange.Padding = new Thickness(0, 0, 10, 0);

            //Sumary.Children.Add(OrderChange);
            //Grid.SetRow(OrderChange, 2);
            //Grid.SetColumn(OrderChange, 1);

            currentOrder.Notify("Change");
            currentOrder.Notify("OrderSum");
            currentOrder.Notify("Quantity");
		}
		
		private void ChangeRecieve_TextChange(object sender, TextChangedEventArgs e)
        {
            try
            {
                currentOrder.Receive = int.Parse((sender as TextBox).Text);
            }
            catch
            {
                currentOrder.Receive = 0;
                
            }
            finally
            {
                //currentOrder.Notify("Change");
                //SetOrderList();

                currentOrder.Notify("Change");
                currentOrder.Notify("OrderSum");
                currentOrder.Notify("Quantity");
            }
            
        }
            
        

        /// <summary>
        /// cài đặt grid cho phần hiển thị danh sách sản phẩm
        /// </summary>
        /// <param name="dt">bảng DataTable chứa danh sách sản phẩm</param>
        void setSearchProductList(DataTable dt)
        {
			if (productList.Count != 0)
                productList.Clear();						   
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Product product = new Product()
                {
                    ProductID = dt.Rows[i][0].ToString(),
                    ProductName = dt.Rows[i][1].ToString(),
                    type = Product.typeStringToInt(dt.Rows[i][2].ToString()),
                    price = int.Parse(dt.Rows[i][3].ToString()),
                };
                productList.Add(product);
            }

            loadProductListIntoGrid();


            /*ProductListGrid.Children.Clear();
            ProductListGrid.RowDefinitions.Clear();

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

            }*/


        }

        private void loadProductListIntoGrid()
        {

            ProductListGrid.Children.Clear();
            ProductListGrid.RowDefinitions.Clear();

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

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            currentOrder.detail.RemoveAt(int.Parse(button.Tag.ToString()));
            SetOrderList();

            currentOrder.Notify("Change");
            currentOrder.Notify("OrderSum");
            currentOrder.Notify("Quantity");

            
        }																  

        private void ChangeQuantity_TextChange(object sender, TextChangedEventArgs e)
        {
            var textbox = sender as TextBox;
            int index = int.Parse(textbox.Tag.ToString());
			int newQuanity = 0;
			try			
            {
                newQuanity = int.Parse(textbox.Text);
            }
            catch
            {

            }
            finally
            {
                currentOrder.detail[index].quantity = newQuanity;

                TextBlock Number = OrderList.Children[index * 5 + 3] as TextBlock;
                Number.Text = (newQuanity * currentOrder.detail[index].product.price).ToString();

                currentOrder.Notify("Change");
                currentOrder.Notify("OrderSum");
                currentOrder.Notify("Quantity");
            }
		}
		
		private void CurrentNumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                currentNumber = int.Parse(txtBoxCurrentNumber.Text);
            }
            catch
            {
                //currentNumber = 0;
            }
            finally
            {
                currentOrder.number = currentNumber;
            }
        }

        private void SaveOrder_Click(object sender, RoutedEventArgs e)
        {
            string ID = currentDate.Day.ToString() + currentDate.Month.ToString() + currentNumber.ToString();

            if (currentOrder.OrderID == ID)
            {
                return;
            }
            currentOrder.OrderID = ID;
            currentOrder.TimeCreated = currentDate;

            db.connection.Open();
            SqlCommand cmd = new SqlCommand("SP_ThemDonHang", db.connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MaDH", SqlDbType.Char).Value = ID;
            cmd.Parameters.AddWithValue("@STT", SqlDbType.Int).Value = currentOrder.number;
            cmd.Parameters.AddWithValue("@ThoiGianNhap", SqlDbType.DateTime).Value = currentOrder.TimeCreated;
            cmd.Parameters.AddWithValue("@NvNhap", SqlDbType.Char).Value = "SL01";
            cmd.Parameters.AddWithValue("@TongTien", SqlDbType.Int).Value = currentOrder.OrderSum;
            cmd.Parameters.AddWithValue("@TienNhan", SqlDbType.Int).Value = currentOrder.Receive;
            cmd.Parameters.AddWithValue("@TimeCreated", SqlDbType.DateTime).Value = currentOrder.TimeCreated;

            cmd.ExecuteNonQuery();
            //MessageBox.Show("Sales Order Detail Added");

            for (int i = 0; i < currentOrder.detail.Count; i++)
            {
                cmd = new SqlCommand("SP_ThemChiTietDonHang", db.connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaDH", SqlDbType.Char).Value = currentOrder.OrderID;
                cmd.Parameters.AddWithValue("@MaSP", SqlDbType.NVarChar).Value = currentOrder.detail[i].product.ProductID;
                cmd.Parameters.AddWithValue("@GiaBan", SqlDbType.Int).Value = currentOrder.detail[i].product.price;
                cmd.Parameters.AddWithValue("@SL", SqlDbType.Int).Value = currentOrder.detail[i].quantity;
                cmd.ExecuteNonQuery();
            }
            MessageBox.Show("Sales Order Saved");
            db.connection.Close();
        }

        private void NewOrder_Click(object sender, RoutedEventArgs e)
        {
            currentOrder = new Order();
            SetOrderList();
            currentNumber++;
           txtBoxCurrentNumber.Text = currentNumber.ToString();
        }

        private void DeleteOrder_Click(object sender, RoutedEventArgs e)
        {
            currentOrder = new Order();
            SetOrderList();
        }

        private void ChangeNumber_Click(object sender, RoutedEventArgs e)
        {
            //if (sender == DecreaseNumerButton)
            //{
            //    currentNumber--;
                
            //}
            //else
            //{
            //    currentNumber++;
            //}

            //txtBoxCurrentNumber.Text = currentNumber.ToString();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string filepath = $"{AppDomain.CurrentDomain.BaseDirectory}save.txt";
            try
            {
                 var reader = new StreamReader(filepath);
                var firstLine = reader.ReadLine();
                currentNumber = int.Parse(firstLine);

                currentDate = DateTime.Now;
                var Date = reader.ReadLine();
                DateTime ReadDate = DateTime.Parse(Date);


                if (ReadDate.Date != currentDate.Date)
                {
                    currentNumber = 1;

                }
            }
            catch
            {
                currentNumber = 1;
                currentDate = DateTime.Now;
            }
            finally
            {
                txtBoxCurrentNumber.Text = currentNumber.ToString();
                DateTextBlock.Text = currentDate.Day + "/" + currentDate.Month + "/" + currentDate.Year;
            }
            
            
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            const string filename = "save.txt";

            var writer = new StreamWriter(filename);
            // Dong dau tien la luot di hien tai
            writer.WriteLine(currentNumber);

            // Theo sau la ngay hien tai
            writer.WriteLine(currentDate.ToString());


            writer.Close();
        }
		
		private void BtnPay_Click(object sender, RoutedEventArgs e)
        {

        }

        void setSearchResultProductList(DataTable dt)
        {
            RegexOptions options = RegexOptions.Multiline | RegexOptions.IgnoreCase;
            string pattern = txtBoxInputSearch.Text;

            if (productList.Count != 0)
                productList.Clear();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string src = dt.Rows[i][1].ToString();
                if (Regex.IsMatch(src, pattern, options))
                {
                    Product product = new Product()
                    {
                        ProductID = dt.Rows[i][0].ToString(),
                        ProductName = dt.Rows[i][1].ToString(),
                        type = Product.typeStringToInt(dt.Rows[i][2].ToString()),
                        price = int.Parse(dt.Rows[i][3].ToString()),
                    };
                    productList.Add(product);
                }
            }

            loadProductListIntoGrid();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            db = DatabaseHandler.getInstance();

            db.connection.Open();

            productList = new List<Product>();
            currentOrder = new Order();

            SqlCommand cmd = new SqlCommand("SP_DanhSachSP", db.connection);
            cmd.CommandType = CommandType.StoredProcedure;
            
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            
            setSearchResultProductList(dt);

            db.connection.Close();
        }

        private void SearchStatisticButton_Click(object sender, RoutedEventArgs e)
        {
            db = DatabaseHandler.getInstance();

            db.connection.Open();

            productList = new List<Product>();
            currentOrder = new Order();

            SqlCommand cmd = new SqlCommand("SP_DanhSachDonHang", db.connection);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dts = new DataTable();
            dts.Load(dr);

            setSearchResultStatisticList(dts);

            db.connection.Close();
        }

        private void setSearchResultStatisticList(DataTable dts)
        {
            RegexOptions options = RegexOptions.Multiline | RegexOptions.IgnoreCase;
            //string pattern = txtBoxInputSearch.Text;

           

            for (int i = 0; i < dts.Rows.Count; i++)
            {
                string src = dts.Rows[i][1].ToString();
                if (Regex.IsMatch(src, pattern, options))
                {
                    Order order = new Order()
                    {
                        OrderID = dts.Rows[i][0].ToString(),
                        //TimeCreated = dts.Rows[i][1].ToDateTime,
                        Employee = dts.Rows[i][2].ToString(),
                        OrderSum = int.Parse(dts.Rows[i][3].ToString()),
                    };
                    //productList.Add(product);
                }
            }

            //loadProductListIntoGrid();
        }
    }
}
