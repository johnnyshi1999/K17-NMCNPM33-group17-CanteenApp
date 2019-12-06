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

        private static string LOGIN_FAILED = "Something is wrong";

        public Login()
        {
            InitializeComponent();
            db = DatabaseHandler.getInstance();

            db.connection.Open();
            
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            wrongUserName.Text = "";
            wrongPass.Text = "";
        }

        private void Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            wrongUserName.Text = "";
            wrongPass.Text = "";
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {

            if (String.IsNullOrEmpty(userName.Text))
            {
                wrongUserName.Text = LOGIN_FAILED;
                return;
            }
            if (String.IsNullOrEmpty(password.Password))
            {
                wrongPass.Text = LOGIN_FAILED;
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

            if (Password != dt.Rows[0][5].ToString())
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

        private void Login_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (DialogResult == null)
            if (MessageBox.Show(this, "Are you sure you want to exit?", "Exit", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
            {
                e.Cancel = true;
                return;
            }
        }
    }
}
