using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K17_NMCNPM33_group17_CanteenApp
{
    class DatabaseHandler
    {

        //chuoi connect voi database --- "Data Source": ten may tinh; "Initial Catalog": ten db

        //static string connection_string = @"Data Source=DESKTOP-CR27SAG;Initial Catalog=QLBH;Integrated Security=True";
        //static string connection_string = @"Data Source=LENOVO-YOGA-520;Initial Catalog=QuanLyBanHang;User ID=sa;Password=123;Integrated Security=True";
        static string connection_string = @"Data Source=DESKTOP-CR27SAG;Initial Catalog=QLBH;Integrated Security=True";
        public SqlConnection connection;

        private static DatabaseHandler databaseObject = null;

        public static DatabaseHandler getInstance()
        {
            if (databaseObject == null)
            {
                databaseObject = new DatabaseHandler();
                databaseObject.connection = new SqlConnection(connection_string);
            }
            return databaseObject;
        }

    }

    
}
