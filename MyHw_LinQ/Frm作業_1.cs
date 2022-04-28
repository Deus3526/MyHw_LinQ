using System;
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
        bool flag = true;
        int skip = 0;
        int take_now;
        int take_old;
        public Frm作業_1()
        {
            InitializeComponent();
            ordersTableAdapter1.Fill(northWindDataSet1.Orders);
            order_DetailsTableAdapter1.Fill(northWindDataSet1.Order_Details);
            productsTableAdapter1.Fill(northWindDataSet1.Products);
            LoadYearIntoCombobox();
        }
        private void LoadYearIntoCombobox()
        {
            // var q1 = from or in northWindDataSet1.Orders where Check_null(or) group or by or.OrderDate.Year into g select new { Year=g.Key};

            var q1 = from or in northWindDataSet1.Orders
                     select new {Year= or.OrderDate.Year };//select  or.OrderDate.Year;

            foreach (var item in q1.Distinct())
            {
                comboBox1.Items.Add(item.Year);//如果上面寫註解那樣，這邊就寫comboBox1.Items.Add(item);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            //this.nwDataSet1.Products.Take(10);//Top 10 Skip(10)

            //Distinct()
            if (skip > northWindDataSet1.Products.Rows.Count)
            {
                return;
            }
            take_now = int.Parse(textBox1.Text);
            if (flag == false)
            {
                flag = true;
                skip += take_old;
            }
            dataGridView1.DataSource = northWindDataSet1.Products.Skip(skip).Take(take_now).ToList();

            skip += take_now;
            take_old = take_now;

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
            var q1 = from or in northWindDataSet1.Orders where Check_null(or)  select or;
            dataGridView1.DataSource = q1.ToList();

            var q2 = from odr in northWindDataSet1.Order_Details where Check_null(odr) select odr;
            dataGridView2.DataSource = q2.ToList();

        }
        private bool  Check_null(DataRow dr)
        {

            foreach (var item in dr.ItemArray)
            {
                if (item is DBNull)
                {
                    return false;
                }
            }
            return true;
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

        private void button1_Click(object sender, EventArgs e)
        {
           try
            {
                int year = int.Parse(comboBox1.Text);
                var q1 =
                    from or in northWindDataSet1.Orders 
                    where Check_null(or) && or.OrderDate.Year==year 
                    select or;

                dataGridView1.DataSource = q1.ToList();

                var q2 = 
                    from odr in northWindDataSet1.Order_Details
                    join or in northWindDataSet1.Orders on odr.OrderID equals or.OrderID 
                    where Check_null(odr) && Check_null(or) && or.OrderDate.Year==year 
                    select odr;

                dataGridView2.DataSource = q2.ToList();
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int orderid = (int)dataGridView1.Rows[e.RowIndex].Cells["OrderID"].Value;
            var q = from odr in northWindDataSet1.Order_Details where odr.OrderID == orderid select odr;
            dataGridView2.DataSource = q.ToList();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            take_now = int.Parse(textBox1.Text);
            if (flag == true)
            {
                flag = false;
                skip -= take_old;
            }
            skip -= take_now;
            take_old = take_now;
            dataGridView1.DataSource = northWindDataSet1.Products.Skip(skip).Take(take_now).ToList();
            if (skip < 0)
            {
                skip = 0;
            }
        }
    }
}
