using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace K17_NMCNPM33_group17_CanteenApp
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Account account;

        DatabaseHandler db;

        public Login()
        {
            InitializeComponent();
            db = DatabaseHandler.getInstance();

            db.connection.Open();
            
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender == userName)
                wrongUserName.Text = "";
            else
                wrongPass.Text = "";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(userName.Text))
            {
                wrongUserName.Text = "Something is wrong";
                return;
            }
            if (String.IsNullOrEmpty(password.Password))
            {
                wrongPass.Text = "Something is wrong";
                return;
            }

            string UserName = userName.Text;
            string Password = password.Password;
            

            SqlCommand cmd = new SqlCommand("SP_NVTheoMa", db.connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MaNV", SqlDbType.NVarChar).Value = UserName;

            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);

            if (dt.Rows.Count == 0)
            {
                wrongUserName.Text = "Wrong user name";
                return;
            }

            if (Password != UserName)
            {
                wrongPass.Text = "Wrong password";
                return;
            }

            account = new Account()
            {
                AccountID = dt.Rows[0][0].ToString(),
                Name = dt.Rows[0][1].ToString(),
                Position = dt.Rows[0][2].ToString(),
            };

            db.connection.Close();
            DialogResult = true;
            return;
        }

    }
}
