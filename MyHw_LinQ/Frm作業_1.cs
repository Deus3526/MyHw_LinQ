﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyHw_LinQ
{
    public partial class Frm作業_1 : Form
    {
        public Frm作業_1()
        {
            InitializeComponent();
            ordersTableAdapter1.Fill(northWindDataSet1.Orders);
            order_DetailsTableAdapter1.Fill(northWindDataSet1.Order_Details);
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            //this.nwDataSet1.Products.Take(10);//Top 10 Skip(10)

            //Distinct()
        }

        private void button14_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files = dir.GetFiles();

            var q = from f in files where f.Extension == ".log" select f;

            this.dataGridView1.DataSource = q.ToList();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            // northWindDataSet1.EnforceConstraints = false;
            var q1 = from or in northWindDataSet1.Orders  select or;
            dataGridView1.DataSource = q1.ToList();

            var q2 = from odr in northWindDataSet1.Order_Details select odr;
            dataGridView2.DataSource = q2.ToList();
        }
        private bool  Check_null(DataRow dr)
        {
            bool flag = true;


            return flag;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files = dir.GetFiles();

            var q = from f in files where f.CreationTime.Year==2020 select f;

            this.dataGridView1.DataSource = q.ToList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files = dir.GetFiles();

            var q = from f in files where f.Length > 5000 select f;

            this.dataGridView1.DataSource = q.ToList();
        }
    }
}
