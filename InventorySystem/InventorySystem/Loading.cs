﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventorySystem
{
    public partial class Loading : Form
    {
        public Loading()
        {
            InitializeComponent();
        }

        
        private void guna2CircleProgressBar1_ValueChanged(object sender, EventArgs e)
        {
           
        }

        private void Loading_Load(object sender, EventArgs e)
        {
            timer1.Start();

        }
        int startpoint = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            startpoint += 1;
            progress.Value = startpoint;
            if (progress.Value == 100)
            {
                progress.Value = 0;
                timer1.Stop();
                Form1 login = new Form1();
                this.Hide();
                login.Show();
            }
        }
    }
}
