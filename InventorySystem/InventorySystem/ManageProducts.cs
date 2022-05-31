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
    public partial class ManageProducts : Form
    {
        public ManageProducts()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Tugberk Basaran\Documents\inventorydb.mdf;Integrated Security=True;Connect Timeout=30");

        void populate()
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
        private void ManageProducts_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into ProductTbl values('" + ProdIdTb.Text + "','" + ProdNameTb.Text + "','" + QtyTb.Text + "','" + PriceTb.Text + "')", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product Successfully Added");
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

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update ProductTbl set ProdId ='" + ProdIdTb.Text + "'where ProdQty='" + QtyTb.Text + "' where ProdPrice='" + PriceTb.Text + "' ProdName='" + ProdNameTb.Text + "')", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product Successfully Updated");
                con.Close();
                populate();
            }
            catch
            {

            }
        }

        private void ProductsGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ProdIdTb.Text = ProductsGV.SelectedRows[0].Cells[0].Value.ToString();
            ProdNameTb.Text = ProductsGV.SelectedRows[0].Cells[1].Value.ToString();
            QtyTb.Text = ProductsGV.SelectedRows[0].Cells[2].Value.ToString();
            PriceTb.Text = ProductsGV.SelectedRows[0].Cells[3].Value.ToString();
        }

        private void PriceTb_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update ProductTbl set ProdName='" + ProdNameTb.Text + "'where ProdQty='" + QtyTb.Text + "' where ProdPrice='" + PriceTb.Text + "'", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product Successfully Updated");
                con.Close();
                populate();
            }
            catch
            {
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (QtyTb.Text == "")
            {
                MessageBox.Show("Enter The User Phone Number");
            }
            else
            {
                con.Open();
                string Myquery = "delete from UserTbl where ProdQty='" + QtyTb.Text + "';";
                SqlCommand cmd = new SqlCommand(Myquery, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product Successfully Deleted");
                con.Close();
                populate();


            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }
    }
}