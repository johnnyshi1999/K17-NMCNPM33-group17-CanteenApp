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
        List<Order> StatisticList;
        Account currentAccount;

        Order currentOrder;
		DateTime currentDate { get; set; }
		int currentNumber { get; set; }
        private bool orderSaved;

        bool isLoggedIn = false;
        bool[] searchTypeTicked;

        public MainWindow()
        {
            InitializeComponent();

            Login login = new Login();
            if (login.ShowDialog() != true)
            {
                this.Close();
                return;
            }
            else
            {
                isLoggedIn = true;
                currentAccount = login.account;
            }

            searchTypeTicked = new bool[3];

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
             StatisticList = new List<Order>();
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

            /*Binding curNumberBinding = new Binding();
            curNumberBinding.Source = currentNumber;
            txtBoxCurrentNumber.SetBinding(TextBlock.TextProperty, curNumberBinding);*/

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
            currentOrder.Notify("Change");
            currentOrder.Notify("OrderSum");
            currentOrder.Notify("Quantity");
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
                DeleteButton.Width = 35;
                DeleteButton.Height = 35;
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
		
		private void ChangeReceive_TextChange(object sender, TextChangedEventArgs e)
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
                    AvatarLink = dt.Rows[i][4].ToString(),
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
                Image avatar = new Image();
                avatar.Source = new BitmapImage(new Uri($"{AppDomain.CurrentDomain.BaseDirectory}{productList[i].AvatarLink}"));

                ProductListGrid.Children.Add(avatar);

                Grid.SetRow(avatar, i);
                Grid.SetColumn(avatar, 0);

                TextBlock productName = new TextBlock();
                productName.Text = productList[i].ProductName;
                productName.Style = Resources["SmallText"] as Style;
                productName.Margin = new Thickness(10, 0, 0, 0);

                ProductListGrid.Children.Add(productName);

                Grid.SetRow(productName, i);
                Grid.SetColumn(productName, 1);

                TextBlock productPrice = new TextBlock();
                productPrice.Text = productList[i].price.ToString();
                productPrice.HorizontalAlignment = HorizontalAlignment.Right;
                productPrice.Margin = new Thickness(0, 0, 10, 0);
                productPrice.Style = Resources["SmallText"] as Style;
                productPrice.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2413E3"));

                ProductListGrid.Children.Add(productPrice);

                Grid.SetRow(productPrice, i);
                Grid.SetColumn(productPrice, 2);

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
                AddButton.Height = 65;
                AddButton.Width = 65;
                AddButton.Tag = i;
                AddButton.Click += AddProduct_Click;

                ProductListGrid.Children.Add(AddButton);

                Grid.SetRow(AddButton, i);
                Grid.SetColumn(AddButton, 3);

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

            if (orderSaved == true)
            {
                MessageBox.Show("Order đã được thanh toán. Xin tạo order mới");
                return;
            }

            if (currentOrder.detail.Count == 0)
            {
                MessageBox.Show("Order trống, không thể thanh toán");
                return;
            }

            if (currentOrder.Change < 0)
            {
                MessageBox.Show("Nhận chưa đủ tiền, không thể thanh toán");
                return;
            }

            string ID = currentDate.Day.ToString() + currentDate.Month.ToString() + currentNumber.ToString();

            currentOrder.OrderID = ID;
            currentOrder.TimeCreated = currentDate;

            db.connection.Open();
            SqlCommand cmd = new SqlCommand("SP_ThemDonHang", db.connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MaDH", SqlDbType.Char).Value = ID;
            cmd.Parameters.AddWithValue("@STT", SqlDbType.Int).Value = currentOrder.number;
            cmd.Parameters.AddWithValue("@ThoiGianNhap", SqlDbType.DateTime).Value = currentOrder.TimeCreated;
            cmd.Parameters.AddWithValue("@NvNhap", SqlDbType.Char).Value = currentAccount.AccountID;
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
            MessageBox.Show("Order lưu thành công");

            currentOrder.Notify("Change");
            currentOrder.Notify("OrderSum");
            currentOrder.Notify("Quantity");
            db.connection.Close();
            orderSaved = true;
        }

        private void NewOrder_Click(object sender, RoutedEventArgs e)
        {

            if (orderSaved != true)
            {
                MessageBox.Show("Hãy lưu order hiện tại trước tiên");
                return;
            }
            currentOrder = new Order();
            SetOrderList();
            currentNumber++;
            txtBoxCurrentNumber.Text = currentNumber.ToString();
            currentOrder.number = currentNumber;
            Binding orderSumBinding = new Binding("OrderSum");
            orderSumBinding.Source = currentOrder;
            // Bind the new data source to the myText TextBlock control's Text dependency property.
            totalAmountDue.SetBinding(TextBlock.TextProperty, orderSumBinding);

            Binding ChangeBinding = new Binding("Change");
            ChangeBinding.Source = currentOrder;
            // Bind the new data source to the myText TextBlock control's Text dependency property.
            changeAmount.SetBinding(TextBlock.TextProperty, ChangeBinding);

            receivedAmount.Text = "";

            Binding QuantityBinding = new Binding("Quantity");
            QuantityBinding.Source = currentOrder;
            orderSaved = false;
        }

        private void DeleteOrder_Click(object sender, RoutedEventArgs e)
        {
            if (orderSaved != true)
            {
                currentOrder = new Order();
                SetOrderList();
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
            }
            else
            {
                MessageBox.Show("Order đã được lưu, không thể xóa");
            }
            
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
                reader.Close();


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
                currentOrder.number = currentNumber;
                DateTextBlock.Text = currentDate.Day + "/" + currentDate.Month + "/" + currentDate.Year;
                
            }
            
            
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (isLoggedIn)
            {
                if (MessageBox.Show(this, "Bạn có thực sự muốn thoát chương trình?", "Exit", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                {
                    e.Cancel = true;
                    return;
                }

                const string filename = "save.txt";

                var writer = new StreamWriter(filename);
                // Dong dau tien la luot di hien tai

                //neu order hien tai da duoc luu, so thu tu se duoc +1 de lan sau mo app so thu tu se khong bi trung
                if (orderSaved == true)
                    writer.WriteLine(currentNumber + 1);
                else
                    writer.WriteLine(currentNumber);
                // Theo sau la ngay hien tai
                writer.WriteLine(currentDate.ToString());

                writer.Close();
            }
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
                
                int tmp = Product.typeStringToInt(dt.Rows[i][2].ToString());
                if (searchTypeTicked[tmp - 1])
                {
                    if (Regex.IsMatch(src, pattern, options))
                    {
                        Product product = new Product()
                        {
                            
                            ProductID = dt.Rows[i][0].ToString(),
                            ProductName = dt.Rows[i][1].ToString(),
                            type = tmp,
                            price = int.Parse(dt.Rows[i][3].ToString()),
                            AvatarLink = dt.Rows[i][4].ToString(),
                        };

                        productList.Add(product);
                    }
                }
            }

            loadProductListIntoGrid();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            searchTypeTicked[0] = (bool)chkBoxCourse.IsChecked;
            searchTypeTicked[1] = (bool)chkBoxDrink.IsChecked;
            searchTypeTicked[2] = (bool)chkBoxSnack.IsChecked;

            db = DatabaseHandler.getInstance();

            db.connection.Open();

            
            SqlCommand cmd = new SqlCommand("SP_DanhSachSP", db.connection);
            cmd.CommandType = CommandType.StoredProcedure;
            
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            
            setSearchResultProductList(dt);

            db.connection.Close();
        }

        private void TxtBoxInputSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SearchButton_Click(null, null);
            }
        }

        private void SearchStatisticButton_Click(object sender, RoutedEventArgs e)
        {
            db = DatabaseHandler.getInstance();

            db.connection.Open();

            StatisticList = new List<Order>();
         

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
            string pattern = statisticDate.Text;


            int sum = 0;
            for (int i = 0; i < dts.Rows.Count; i++)
            {
                
                DateTime time = (DateTime)dts.Rows[i][2];
                string src = time.ToString("dd-MM-yyyy");
                int f = int.Parse(dts.Rows[i][4].ToString());
                if (Regex.IsMatch(src, pattern, options))
                {
                    sum += int.Parse(dts.Rows[i][4].ToString());
                    Order order = new Order()
                    {
                        OrderID = dts.Rows[i][0].ToString(),
                        TimeCreated = (DateTime)dts.Rows[i][2],
                        Employee = dts.Rows[i][3].ToString(),
                        orderSum = int.Parse(dts.Rows[i][4].ToString()),
                       
                    };
                    StatisticList.Add(order);
                }
            }
            totalSumStatistic.Text = sum.ToString();
          
            loadStatisticListIntoGrid();
        }
        private void loadStatisticListIntoGrid()
        {

            statisticList.Children.Clear();
            statisticList.RowDefinitions.Clear();
            totalAmountStatistic.Text = StatisticList.Count.ToString();
            for (int i = 0; i < StatisticList.Count; i++)
            {
                TextBlock orderID = new TextBlock();
                orderID.Text = StatisticList[i].OrderID.ToString();
                orderID.Style = Resources["SmallText"] as Style;

                statisticList.Children.Add(orderID);

                Grid.SetRow(orderID, i);
                Grid.SetColumn(orderID, 0);

                TextBlock employee = new TextBlock();
                employee.Text = StatisticList[i].Employee;
                employee.Style = Resources["SmallText"] as Style;
               

                statisticList.Children.Add(employee);

                Grid.SetRow(employee, i);
                Grid.SetColumn(employee, 1);

                TextBlock oderSum = new TextBlock();
                oderSum.Text = StatisticList[i].OrderSum.ToString();
                Debug.WriteLine(StatisticList[i].OrderSum);
                oderSum.Style = Resources["SmallText"] as Style;

                statisticList.Children.Add(oderSum);

                Grid.SetRow(oderSum, i);
                Grid.SetColumn(oderSum, 2);

                TextBlock timeCreated = new TextBlock();
                timeCreated.Text = StatisticList[i].TimeCreated.ToString("dd MMMM yyyy hh:mm:ss tt");
                timeCreated.Style = Resources["SmallText"] as Style;

                statisticList.Children.Add(timeCreated);

                Grid.SetRow(timeCreated, i);
                Grid.SetColumn(timeCreated, 3);
                statisticList.RowDefinitions.Add(new RowDefinition());

            }
        }
        private void AccountInfo_Click(object sender, RoutedEventArgs e)
        {

            MessageBox.Show($"Mã nhân viên: {currentAccount.AccountID}\n" +
                $"Tên: {currentAccount.Name}\n");
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            isLoggedIn = false;
            Login login = new Login();
            if (login.ShowDialog() == true)
            {
                isLoggedIn = true;
                currentAccount = login.account;
            }
            else
            {
                this.Close();
            }
        }

        private void PrintBill_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Order đã được in!!!");
        }
    }
}
