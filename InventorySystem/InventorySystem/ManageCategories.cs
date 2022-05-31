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
    public partial class ManageCategories : Form
    {
        public ManageCategories()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Tugberk Basaran\Documents\inventorydb.mdf;Integrated Security=True;Connect Timeout=30");

        void populate()
        {
            try
            {
                con.Open();
                string Myquery = "select * from CategoryTbl";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                CategoriesGV.DataSource = ds.Tables[0];
                con.Close();
            }
            catch
            {

            }
        }
            private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into CategoryTbl values(" + CatIdTb.Text + ",'" + CatNameTb.Text + "')", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Category Successfully Added");
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

        private void button3_Click(object sender, EventArgs e)
        {
            if(CatIdTb.Text == "")
            {
                MessageBox.Show("Enter The Category Id");
            }
            else
            {
                con.Open();
                string Myquery = "delete from CategoryTbl where CatId='" + CatIdTb.Text + "';";
                SqlCommand cmd = new SqlCommand(Myquery, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Category Successfully Deleted");
                con.Close();
                populate();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update CategoryTbl set CatName='" + CatNameTb.Text +"'where Catid='" + CatIdTb.Text + "'", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Category Successfully Updated");
                con.Close();
                populate();
            }
            catch
            {

            }
        }

        private void ManageCategories_Load(object sender, EventArgs e)
        {
            populate();
            
        }

        private void CategoriesGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CatIdTb.Text = CategoriesGV.SelectedRows[0].Cells[0].Value.ToString();
            CatNameTb.Text = CategoriesGV.SelectedRows[0].Cells[1].Value.ToString();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }
    }
    }

