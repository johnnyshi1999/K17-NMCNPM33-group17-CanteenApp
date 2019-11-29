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

        public MainWindow()
        {
            InitializeComponent();
            db = DatabaseHandler.getInstance();

            db.connection.Open();
            string SQLSelect = "Select * From SANPHAM";
            SqlCommand cmd = new SqlCommand(SQLSelect, db.connection);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            db.connection.Close();

            List<Product> productList = new List<Product>();
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    Product product = new Product()
            //    {
            //        ProductID = dt.Rows[i][0].ToString(),
            //        ProductName = dt.Rows[i][1].ToString(),
            //    };

            //}

            DataListView.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = dt });
        }
    }
}
