using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyHw_LinQ
{
    public partial class Frm作業_2 : Form
    {
        public Frm作業_2()
        {
            InitializeComponent();
            productTableAdapter1.Fill(adventureWorksDataSet1.Product);
            productPhotoTableAdapter1.Fill(adventureWorksDataSet1.ProductPhoto);
            productProductPhotoTableAdapter1.Fill(adventureWorksDataSet1.ProductProductPhoto);
            LoadYearIntoComboBox();
        }
        void LoadYearIntoComboBox()
        {
            var q = from y in adventureWorksDataSet1.Product
                    select y.SellStartDate.Year;
            comboBox3.DataSource = q.Distinct().ToList();
        }
        private void button11_Click(object sender, EventArgs e)
        {
            LoadDatagridview(p => true);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DateTime date_Statr = dateTimePicker1.Value;
            DateTime date_End = dateTimePicker2.Value;
            LoadDatagridview(p => p.SellStartDate >= date_Statr && p.SellStartDate < date_End);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            LoadDatagridview(p => p.SellStartDate.Year == int.Parse(comboBox3.Text));
        }

        private void button10_Click(object sender, EventArgs e)
        {
            LoadDatagridview(p=>p.SellStartDate.Year== int.Parse(comboBox3.Text)&&(p.SellStartDate.Month/4)==comboBox2.SelectedIndex);
            MessageBox.Show($"共有{dataGridView1.Rows.Count}筆");
        }
        private void LoadDatagridview(System.Func<MyHw_LinQ.AdventureWorksDataSet.ProductRow,bool> condition)
        {
            var q = adventureWorksDataSet1.Product.
                Where(condition /*p=>condition(p)*/).
                Select(p=>new { p.ProductID, p.Name, p.ProductNumber, p.SellStartDate });
            dataGridView1.DataSource = q.ToList();
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.ColumnHeader);
        }
        delegate bool Condition(AdventureWorksDataSet.ProductRow pr);

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex > dataGridView1.RowCount) return;
            int productID = (int)dataGridView1.Rows[e.RowIndex].Cells["ProductID"].Value;
            var q1 = from ppp in adventureWorksDataSet1.ProductProductPhoto where ppp.ProductID == productID select ppp.ProductPhotoID;
            int photoID = q1.ToList()[0];

            var q2 = from pp in adventureWorksDataSet1.ProductPhoto where pp.ProductPhotoID == photoID select pp.LargePhoto;
            byte[] bytes = q2.ToList()[0];
            MemoryStream ms = new MemoryStream(bytes);
            pictureBox1.Image = Image.FromStream(ms);
        }
    }
}
