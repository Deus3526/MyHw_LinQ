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
    public partial class Frm作業_3 : Form
    {
        NorthwindEntities northwindEntities = new NorthwindEntities();
        public Frm作業_3()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AllClear();
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            foreach (int n in nums) GroupToTreeview(n);

            foreach (TreeNode node in treeView1.Nodes) node.Text=node.Text+"       "+node.Nodes.Count;
        }
        delegate object GroupAndOrderby(System.IO.FileInfo file);
        private string DefineGroupNameInt(int n)
        {
            if (n < 3) return "Small";
            else if (n < 7) return "Medium";
            else return "Large";
        }
        private string DefineGroupNameFile(System.IO.FileInfo file)
        {
            if (file.Length < 50000) return "Small";
            else if (file.Length < 100000) return "Medium";
            else return "Large";
        }
        public  void GroupToTreeview(int n)
        {
            string groupName = DefineGroupNameInt(n);
            TreeNode node = null;
            if (treeView1.Nodes[groupName] is null)
            {
                node = treeView1.Nodes.Add(groupName, groupName);
            }
            else node = treeView1.Nodes[groupName];

            node.Nodes.Add(n.ToString());
        }

        private void button38_Click(object sender, EventArgs e)
        {
            Load_LinQ_FileInfo(f => f.Length, f => DefineGroupNameFile(f));
            #region
            //AllClear();
            //System.IO.DirectoryInfo directory = new System.IO.DirectoryInfo(@"C:\windows");
            //System.IO.FileInfo[] files = directory.GetFiles();

            //var q = from f in files
            //        orderby f.Length descending
            //        select f;
            //var q2 = from f in files
            //         orderby f.Length descending
            //         group f by DefineGroupNameFile(f) into g
            //         select new { Key = g.Key, Count = g.Count(), MyGroup = g };
            //dataGridView1.DataSource = q.ToList();
            //dataGridView2.DataSource = q2.ToList();

            //foreach (var g in q2.ToList())
            //{
            //    TreeNode node = treeView1.Nodes.Add(g.Key, g.Key);
            //    foreach (var n in g.MyGroup)
            //    {
            //        node.Nodes.Add(n.ToString());
            //    }
            //}
            #endregion
        }
        void AllClear()
        {
            treeView1.Nodes.Clear();
            dataGridView1.Columns.Clear();
            dataGridView2.Columns.Clear();
        }
        private void Load_LinQ_FileInfo(GroupAndOrderby groupAndOrderby1, GroupAndOrderby groupAndOrderby2)
        {
            AllClear();
            System.IO.DirectoryInfo directory = new System.IO.DirectoryInfo(@"C:\windows");
            System.IO.FileInfo[] files = directory.GetFiles();

            var q = from f in files
                    orderby groupAndOrderby1(f) descending
                    select f;
            var q2 = from f in files
                     orderby groupAndOrderby1(f) descending
                     group f by groupAndOrderby2(f) into g
                     select new { Key = g.Key, Count = g.Count(), MyGroup = g };
            dataGridView1.DataSource = q.ToList();
            dataGridView2.DataSource = q2.ToList();
            foreach (var g in q2.ToList())
            {
                TreeNode node = treeView1.Nodes.Add(g.Key.ToString(), g.Key.ToString());
                foreach (var n in g.MyGroup)
                {
                    node.Nodes.Add(n.ToString());
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Load_LinQ_FileInfo(f => f.CreationTime, f => f.CreationTime.Year);
            #region
            //AllClear();
            //System.IO.DirectoryInfo directory = new System.IO.DirectoryInfo(@"C:\windows");
            //System.IO.FileInfo[] files = directory.GetFiles();

            //var q = from f in files
            //        orderby f.CreationTime descending
            //        select f;
            //var q2 = from f in files
            //         group f by f.CreationTime.Year into g
            //         orderby g.Key descending
            //         select new { Year = g.Key, Count = g.Count(), MyGroup = g };
            //dataGridView1.DataSource = q.ToList();
            //dataGridView2.DataSource = q2.ToList();

            //foreach (var g in q2.ToList())
            //{
            //    TreeNode node = treeView1.Nodes.Add(g.Year.ToString(), g.Year.ToString());
            //    foreach (var n in g.MyGroup)
            //    {
            //        node.Nodes.Add(n.ToString());
            //    }
            //}
            #endregion
        }

        private void button8_Click(object sender, EventArgs e)
        {
            AllClear();
            var q = from p in northwindEntities.Products.AsEnumerable()
                    orderby p.UnitPrice descending
                    select p;
            dataGridView1.DataSource = q.ToList();
                    

            var q2 = from p in northwindEntities.Products.AsEnumerable()
                    orderby p.UnitPrice descending
                    group p by DefineProductPrice(p) into g
            select new { g.Key,Count=g.Count(),MyGroup=g};
            dataGridView2.DataSource = q2.ToList();

            foreach (var g in q2)
            {
                TreeNode node = treeView1.Nodes.Add(g.Key.ToString(), g.Key.ToString());
                foreach (var n in g.MyGroup)
                {
                    node.Nodes.Add($"{n.ProductName,-35}---({n.UnitPrice:c2})");
                }
            }
        }
        string DefineProductPrice(Product p)
        {
            if (p.UnitPrice < 20) return "低價產品";
            else if (p.UnitPrice < 50) return "中價產品";
            else return "高價產品";
        }

        private void button15_Click(object sender, EventArgs e)
        {
            AllClear();
            var q = from o in northwindEntities.Orders.AsEnumerable()
                    orderby o.OrderDate.Value.Year 
                    select o;
            dataGridView1.DataSource = q.ToList();


            var q2 = from o in northwindEntities.Orders.AsEnumerable()
                     orderby o.OrderDate.Value.Year
                     group o by o.OrderDate.Value.Year into g
                     select new { g.Key, Count = g.Count(), MyGroup = g };
            dataGridView2.DataSource = q2.ToList();

            foreach (var g in q2.ToList())
            {
                TreeNode node = treeView1.Nodes.Add(g.Key.ToString(), g.Key.ToString());
                foreach (var n in g.MyGroup)
                {
                    node.Nodes.Add($"{n.CustomerID,-20}---({n.OrderDate})");
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            AllClear();
            var q = from o in northwindEntities.Orders.AsEnumerable()
                    orderby o.OrderDate.Value.Year,o.OrderDate.Value.Month
                    select o;
            dataGridView1.DataSource = q.ToList();


            var q2 = from o in northwindEntities.Orders.AsEnumerable()
                     orderby o.OrderDate.Value.Year,o.OrderDate.Value.Month
                     group o by DefineOrdersYearMonth(o) into g
                     select new { g.Key, Count = g.Count(), MyGroup = g };
            dataGridView2.DataSource = q2.ToList();

            foreach (var g in q2.ToList())
            {
                TreeNode node = treeView1.Nodes.Add(g.Key.ToString(), g.Key.ToString());
                foreach (var n in g.MyGroup)
                {
                    node.Nodes.Add($"{n.CustomerID,-20}---({n.OrderDate})");
                }
            }
        }
        string DefineOrdersYearMonth(Order o)
        {
            return o.OrderDate.Value.Year + "年 " + o.OrderDate.Value.Month + "月";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var q = from od in northwindEntities.Order_Details.AsEnumerable()
                     group od by od.Order.OrderDate.Value.Year into g
                     select new { Year=g.Key,Total=g.Sum(od=>od.UnitPrice * od.Quantity * (decimal)(1 - od.Discount))};
            dataGridView1.DataSource = q.ToList();

            var q2 = from od in northwindEntities.Order_Details.AsEnumerable()
                     group od by true  into g
                     select new {總銷售金額 = g.Sum(od => od.UnitPrice * od.Quantity * (decimal)(1 - od.Discount)) };
            dataGridView2.DataSource = q2.ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var q = from od in northwindEntities.Order_Details.AsEnumerable()
                    group od by od.Order.Employee.FirstName+"  "+od.Order.Employee.LastName/*Customer(od)*/ into g
                    orderby g.Sum(od => od.UnitPrice * od.Quantity * (decimal)(1 - od.Discount)) descending
                    select new { g.Key,Total=$"{g.Sum(od=> od.UnitPrice * od.Quantity * (decimal)(1 - od.Discount)):c2}" };
            dataGridView1.DataSource = q.Take(5).ToList();
        }
        string Customer(Order_Detail od)
        {

            return od.Order.Employee.FirstName + "  " + od.Order.Employee.LastName;
        }
        private void button7_Click(object sender, EventArgs e)
        {
            bool result = northwindEntities.Products.Any(p => p.UnitPrice > 300);
            MessageBox.Show(result.ToString());
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var q = from c in northwindEntities.Categories
                    from p in c.Products
                    orderby p.UnitPrice descending
                    select new { c.CategoryName, p.ProductName, p.UnitPrice };

            dataGridView1.DataSource = q.Take(5).ToList();
        }
    }
}
