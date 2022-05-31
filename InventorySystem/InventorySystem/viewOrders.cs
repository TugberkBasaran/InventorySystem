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
    public partial class viewOrders : Form
    {
        public viewOrders()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Tugberk Basaran\Documents\inventorydb.mdf;Integrated Security=True;Connect Timeout=30");
        void populateorders()
        {
            try
            {
                con.Open();
                string Myquery = "select * from OrdersTbl";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                OrdersGv.DataSource = ds.Tables[0];
                con.Close();
            }
            catch
            {

            }
        }
        private void viewOrders_Load(object sender, EventArgs e)
        {
            populateorders();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void OrdersGv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {

                printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Order Summary", new Font("century", 35, FontStyle.Bold),Brushes.Red,new Point(230));
            e.Graphics.DrawString("Order ID:" + OrdersGv.SelectedRows[0].Cells[0].Value.ToString(), new Font("century", 20, FontStyle.Regular), Brushes.Black, new Point(85, 100));
            e.Graphics.DrawString("Customer ID:" + OrdersGv.SelectedRows[0].Cells[1].Value.ToString(), new Font("century", 20, FontStyle.Regular), Brushes.Black, new Point(85, 140));
            e.Graphics.DrawString("Customer Name:" + OrdersGv.SelectedRows[0].Cells[2].Value.ToString(), new Font("century", 20, FontStyle.Regular), Brushes.Black, new Point(85, 180));
            e.Graphics.DrawString("Order Date:" + OrdersGv.SelectedRows[0].Cells[3].Value.ToString(), new Font("century", 20, FontStyle.Regular), Brushes.Black, new Point(85, 220));
            e.Graphics.DrawString("Customer Amount:" + OrdersGv.SelectedRows[0].Cells[4].Value.ToString(), new Font("century", 20, FontStyle.Regular), Brushes.Black, new Point(85, 260));
            e.Graphics.DrawString("Tugberk Basaran", new Font("century", 20, FontStyle.Bold), Brushes.Red, new Point(500,350));
        }
    }
}
