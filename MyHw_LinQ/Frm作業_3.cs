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

        private void button3_Click(object sender, EventArgs e)
        {
            int a = 1;
            MessageBox.Show(((object)a).GetType().ToString());
        }
    }
}
