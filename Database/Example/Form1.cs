using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace _2019_CSDLNC_TH02_1712501
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(ConnectionString.constring);
        public Form1()
        {
            InitializeComponent();
        }

        private void Label8_Click(object sender, EventArgs e)
        {

        }

        private void Label25_Click(object sender, EventArgs e)
        {

        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label16_Click(object sender, EventArgs e)
        {

        }

        private void TextBox17_TextChanged(object sender, EventArgs e)
        {

        }

        private void Bt_SOH_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("THEMHD_ORDHEADER", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SalesOrderID", SqlDbType.Int).Value = textBox1.Text.Trim();
            cmd.Parameters.AddWithValue("@RevisionNumber", SqlDbType.Int).Value = textBox2.Text.Trim();
            cmd.Parameters.AddWithValue("@OrderDate", SqlDbType.Date).Value = dateTimePicker1.Value;
            cmd.Parameters.AddWithValue("@DueDate", SqlDbType.Date).Value = dateTimePicker2.Value;
            cmd.Parameters.AddWithValue("@ShipDate", SqlDbType.Date).Value = dateTimePicker3.Value;
            cmd.Parameters.AddWithValue("@Status", SqlDbType.TinyInt).Value = textBox3.Text.Trim();
            cmd.Parameters.AddWithValue("@OnlineOrderFlag", SqlDbType.Bit).Value = textBox4.Text.Trim();
            cmd.Parameters.AddWithValue("@SalesOrderNumber", SqlDbType.VarChar).Value = textBox5.Text.Trim();
            cmd.Parameters.AddWithValue("@PurchaseOrderNumber", SqlDbType.NVarChar).Value = textBox6.Text.Trim();
            cmd.Parameters.AddWithValue("@AccountNumber", SqlDbType.NVarChar).Value = textBox7.Text.Trim();
            cmd.Parameters.AddWithValue("@CustomerID", SqlDbType.Int).Value = textBox8.Text.Trim();
            cmd.Parameters.AddWithValue("@SalesPersonID", SqlDbType.Int).Value = textBox9.Text.Trim();
            cmd.Parameters.AddWithValue("@TerritoryID", SqlDbType.Int).Value = textBox10.Text.Trim();
            cmd.Parameters.AddWithValue("@BillToAddressID", SqlDbType.Int).Value = textBox11.Text.Trim();
            cmd.Parameters.AddWithValue("@ShipToAddressID", SqlDbType.Int).Value = textBox12.Text.Trim();
            cmd.Parameters.AddWithValue("@ShipMethodID", SqlDbType.Int).Value = textBox13.Text.Trim();
            cmd.Parameters.AddWithValue("@CreditCardID", SqlDbType.Int).Value = textBox14.Text.Trim();
            cmd.Parameters.AddWithValue("@CreditCardApprovalCode", SqlDbType.VarChar).Value = textBox15.Text.Trim();
            cmd.Parameters.AddWithValue("@CurrencyRateID", SqlDbType.Int).Value = textBox16.Text.Trim();
            cmd.Parameters.AddWithValue("@SubTotal", SqlDbType.Money).Value = textBox17.Text.Trim();
            cmd.Parameters.AddWithValue("@TaxAmt", SqlDbType.Money).Value = textBox18.Text.Trim();
            cmd.Parameters.AddWithValue("@Freight", SqlDbType.Money).Value = textBox19.Text.Trim();
            cmd.Parameters.AddWithValue("@TotalDue", SqlDbType.Money).Value = textBox20.Text.Trim();
            cmd.Parameters.AddWithValue("@Comment", SqlDbType.NVarChar).Value = textBox21.Text.Trim();
            cmd.Parameters.AddWithValue("@rowguid", Guid.Parse(textBox22.Text.Trim()));
            cmd.Parameters.AddWithValue("@ModifiedDate", SqlDbType.Date).Value = dateTimePicker4.Value;
            cmd.ExecuteNonQuery();
            MessageBox.Show("Sales Order Header Added");
            con.Close();
        }

        private void Bt_SOD_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("THEMHD_ORDDETAIL", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SalesOrderID", SqlDbType.Int).Value = textBox23.Text.Trim();
            cmd.Parameters.AddWithValue("@SalesOrderDetailID", SqlDbType.Int).Value = textBox24.Text.Trim();
            cmd.Parameters.AddWithValue("@CarrierTrackingNumber", SqlDbType.NVarChar).Value = textBox25.Text.Trim();
            cmd.Parameters.AddWithValue("@OrderQty", SqlDbType.SmallInt).Value = textBox26.Text.Trim();
            cmd.Parameters.AddWithValue("@ProductID", SqlDbType.Int).Value = textBox27.Text.Trim();
            cmd.Parameters.AddWithValue("@SpecialOfferID", SqlDbType.Int).Value = textBox28.Text.Trim();
            cmd.Parameters.AddWithValue("@UnitPrice", SqlDbType.Money).Value = textBox29.Text.Trim();
            cmd.Parameters.AddWithValue("@UnitPriceDiscount", SqlDbType.Money).Value = textBox30.Text.Trim();
            cmd.Parameters.AddWithValue("@LineTotal", SqlDbType.Decimal).Value = textBox31.Text.Trim();
            cmd.Parameters.AddWithValue("@rowguid", Guid.Parse(textBox32.Text.Trim()));
            cmd.Parameters.AddWithValue("@ModifiedDate", SqlDbType.Date).Value = dateTimePicker5.Value;
            cmd.ExecuteNonQuery();
            MessageBox.Show("Sales Order Detail Added");
            con.Close();
        }

        private void Bt_search_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("TIMIDVANGAY", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PersonID", SqlDbType.Int).Value = textBox33.Text.Trim();
            cmd.Parameters.AddWithValue("@DATE_START", SqlDbType.Date).Value = dateTimePicker6.Value;
            cmd.Parameters.AddWithValue("@DATE_END", SqlDbType.Date).Value = dateTimePicker7.Value;
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            con.Open();
            string SQLSelect = "Select * From n_SalesOrderDetail";
            SqlCommand cmd = new SqlCommand(SQLSelect, con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView1.DataSource = dt;
            con.Close();
        }
    }
}
