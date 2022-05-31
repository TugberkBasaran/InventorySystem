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
    public partial class ManageOrders : Form
    {
        public ManageOrders()
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

        void populateproducts()
        {
            try
            {
                con.Open();
                string Myquery = "select * from ProductTbl";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                ProductsGV.DataSource = ds.Tables[0];
                con.Close();
            }
            catch
            {

            }
        }


        private void CustomersGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CustId.Text = CustomersGV.SelectedRows[0].Cells[0].Value.ToString();
            CustName.Text = CustomersGV.SelectedRows[0].Cells[1].Value.ToString();
        }

        void updateproduct()
        {
            con.Open();
            int id = Convert.ToInt32(ProductsGV.SelectedRows[0].Cells[0].Value.ToString());
            int newQty = stock - Convert.ToInt32(QtyTb.Text);
            string query = "update ProductTbl set ProdQty = " + newQty + " where ProdId=" + id + ";";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            populateproducts();
        }
        int num = 0;
        int singleprice, totalprice, quantity;
        string product;
        private void ManageOrders_Load(object sender, EventArgs e)
        {
            populate();
            populateproducts();
        }

        int flag = 0;
        int stock;
        private void ProductsGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            product  = ProductsGV.SelectedRows[0].Cells[1].Value.ToString();
            //qty = Convert.ToInt32(QtyTb.Text);
            stock = Convert.ToInt32(ProductsGV.SelectedRows[0].Cells[2].Value.ToString());
            singleprice = Convert.ToInt32(ProductsGV.SelectedRows[0].Cells[3].Value.ToString());
            //totprice = qty * uprice;
            flag = 1;
        }

        int sum = 0;        
        private void button1_Click(object sender, EventArgs e)
        {
            if (QtyTb.Text == "")
                MessageBox.Show("Enter the Quantity of products");
            else if (flag == 0)
                MessageBox.Show("Select the product");
            else if (Convert.ToInt32(QtyTb.Text) > stock)
                MessageBox.Show("Not Enough Stock Avaliable :(");

            else
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("num", typeof(int));
                dt.Columns.Add("product", typeof(string));
                dt.Columns.Add("quantity", typeof(int));
                dt.Columns.Add("single price", typeof(decimal));
                dt.Columns.Add("total price", typeof(decimal));

                num = num + 1;
                quantity = Convert.ToInt32(QtyTb.Text);
                totalprice = quantity * singleprice;
                dt.Rows.Add(num, product, quantity, singleprice, totalprice);
                OrderGv.DataSource = dt;
                flag = 0;
            }
            sum = sum + totalprice;
            totAmount.Text = sum.ToString() + "  TL";
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void finalmoney_Click(object sender, EventArgs e)
        {

        }

        private void CustId_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if(OrderIdTb.Text =="" || CustId.Text == "" || CustName.Text == "" || totAmount.Text == "")
            {
                MessageBox.Show("Fill the data correctly");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into OrdersTbl values" + OrderIdTb.Text + "," + CustId.Text + ",'" + CustName.Text + "','" + orderdate.Text + "', " + totAmount.Text + ")", con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show(" Order added successfully.");
                    con.Close();
                //populate();

               

                }
                

                
                catch
                {

                }
            }
                
         }

        private void button3_Click(object sender, EventArgs e)
        {
            viewOrders view = new viewOrders();
            view.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
