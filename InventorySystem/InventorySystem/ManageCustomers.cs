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

namespace InventorySystem
{
    public partial class ManageCustomers : Form
    {
        public ManageCustomers()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Tugberk Basaran\Documents\inventorydb.mdf;Integrated Security=True;Connect Timeout=30");
        void populate()
        {
            try
            {
                con.Open();
                string Myquery = "select * from CustomerTbl";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                CustomersGV.DataSource = ds.Tables[0];
                con.Close();
            }
            catch
            {

            }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into CustomerTbl values(" + Customerid.Text + ",'" + CustomernameTb.Text + "','" + CustomerPhoneTb.Text + "')", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Customer Successfully Added");
                con.Close();
                populate();
            }
            catch
            {

            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ManageCustomers_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Customerid.Text == "")
            {
                MessageBox.Show("Enter The Customer Id");
            }
            else
            {
                con.Open();
                string Myquery = "delete from CustomerTbl where CustId='" + Customerid.Text + "';";
                SqlCommand cmd = new SqlCommand(Myquery, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Customer Successfully Deleted");
                con.Close();
                populate();

            }
        }

        private void CustomersGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Customerid.Text = CustomersGV.SelectedRows[0].Cells[0].Value.ToString();
            CustomernameTb.Text = CustomersGV.SelectedRows[0].Cells[1].Value.ToString();
            CustomerPhoneTb.Text = CustomersGV.SelectedRows[0].Cells[2].Value.ToString();
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select Count (*) from OrdersTbl where CustId = " + Customerid.Text + "", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            OrderLabel.Text = dt.Rows[0][0].ToString();

            SqlDataAdapter sda1 = new SqlDataAdapter("select Sum (TotalAmt) from OrdersTbl where CustId = " + Customerid.Text + "", con);                                                                
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            AmountLabel.Text = dt1.Rows[0][0].ToString();

            SqlDataAdapter sda2 = new SqlDataAdapter("select Max(OrderDate) from OrdersTbl where CustId = " + Customerid.Text + "", con);
            DataTable dt2 = new DataTable();
            sda2.Fill(dt2);
            label9.Text = dt2.Rows[0][0].ToString();
            con.Close();
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update CustomerTbl set CustName='" + CustomernameTb.Text + "',CustPhone='" + CustomerPhoneTb.Text + "'where Custid='" + Customerid.Text + "'", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Customer Successfully Updated");
                con.Close();
                populate();
            }
            catch
            {

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
   
    
}
