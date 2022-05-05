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
    public partial class Frm考試 : Form
    {
        NorthwindEntities northwindEntities = new NorthwindEntities();
        public Frm考試()
        {
            InitializeComponent();

            students_scores = new List<Student>()
                                         {
                                            new Student{ Name = "aaa", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Male" },
                                            new Student{ Name = "bbb", Class = "CS_102", Chi = 80, Eng = 80, Math = 100, Gender = "Male" },
                                            new Student{ Name = "ccc", Class = "CS_101", Chi = 60, Eng = 50, Math = 75, Gender = "Female" },
                                            new Student{ Name = "ddd", Class = "CS_102", Chi = 80, Eng = 70, Math = 85, Gender = "Female" },
                                            new Student{ Name = "eee", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Female" },
                                            new Student{ Name = "fff", Class = "CS_102", Chi = 80, Eng = 80, Math = 80, Gender = "Female" },

                                          };
        }

        List<Student> students_scores;

        public class Student
        {
            public string Name { get; set; }
            public string Class { get;  set; }
            public int Chi { get; set; }
            public int Eng { get; internal set; }
            public int Math { get;  set; }
            public string Gender { get; set; }
        }

        private void button36_Click(object sender, EventArgs e)
        {
            AllClear();
            #region 搜尋 班級學生成績

            // 
            // 共幾個 學員成績 ?						
            listBox1.Items.Add("共幾個 學員成績 ?");
            var q1 = from s in students_scores
                     select s;
            var Q1 = q1.ToList();
            listBox1.Items.Add($"共{Q1.Count()}個學員");
            listBox1.Items.Add(" ");
            // 找出 前面三個 的學員所有科目成績		
            listBox1.Items.Add("找出 前面三個 的學員所有科目成績		");
            for(int i=0;i<3;i++)
            {
                listBox1.Items.Add($"第{i + 1}個學員的成績為");
                listBox1.Items.Add($"國文:{Q1[i].Chi}分  英文:{Q1[i].Eng}分  數學:{Q1[i].Math}分");
            }
            listBox1.Items.Add("");
            // 找出 後面兩個 的學員所有科目成績	
            listBox1.Items.Add("找出 後面兩個 的學員所有科目成績	");
            for(int i=Q1.Count()-1;i>=Q1.Count-2;i--)
            {
                listBox1.Items.Add($"倒數第{Q1.Count()-i}個學員的成績為");
                listBox1.Items.Add($"國文:{Q1[i].Chi}分  英文:{Q1[i].Eng}分  數學:{Q1[i].Math}分");
            }
            listBox1.Items.Add("");
            // 找出 Name 'aaa','bbb','ccc' 的學員國文英文科目成績			
            listBox1.Items.Add("找出 Name 'aaa','bbb','ccc' 的學員國文英文科目成績	");
            var q2 = from s in students_scores
                     where s.Name == "aaa" || s.Name == "bbb" || s.Name == "ccc"
                     select s;
            foreach(var q in q2)
            {
                listBox1.Items.Add($"Name為{q.Name}的學員成績為:");
                listBox1.Items.Add($"國文:{q.Chi}分  英文:{q.Eng}分");
            }
            listBox1.Items.Add("");
            //foreach(var )

            // 找出學員 'bbb' 的成績	                          
            listBox1.Items.Add("找出學員 'bbb' 的成績	  ");
            foreach(var q in q1)
            {
                if (q.Name == "bbb")
                {
                    listBox1.Items.Add($"Name為{q.Name}的學員成績為:");
                    listBox1.Items.Add($"國文:{q.Chi}分  英文:{q.Eng}分  數學:{q.Math}分");
                    break;
                }
                else continue;
            }
            listBox1.Items.Add("");
            // 找出除了 'bbb' 學員的學員的所有成績 ('bbb' 退學)	
            listBox1.Items.Add("找出除了 'bbb' 學員的學員的所有成績 ('bbb' 退學)	");
            foreach (var q in q1)
            {
                if (q.Name == "bbb") continue;
                else
                {
                    listBox1.Items.Add($"Name為{q.Name}的學員成績為:");
                    listBox1.Items.Add($"國文:{q.Chi}分  英文:{q.Eng}分  數學:{q.Math}分");
                }
            }
            listBox1.Items.Add("");
            // 找出 'aaa', 'bbb' 'ccc' 學員 國文數學兩科 科目成績  |		
            listBox1.Items.Add("找出 'aaa', 'bbb' 'ccc' 學員 國文數學兩科 科目成績  |	");
            foreach (var q in q2)
            {
                listBox1.Items.Add($"Name為{q.Name}的學員成績為:");
                listBox1.Items.Add($"國文:{q.Chi}分  數學:{q.Math}分");
            }
            listBox1.Items.Add("");
            // 數學不及格 ... 是誰 
            listBox1.Items.Add("數學不及格 ... 是誰 ");
            foreach(var q in q1)
            {
                if(q.Math<60)
                {
                    listBox1.Items.Add($"Name為{q.Name}的學員數學不及格:");
                    listBox1.Items.Add($"數學:{q.Math}分");
                }
            }
            listBox1.Items.Add("");
            #endregion
        }

        private void button37_Click(object sender, EventArgs e)
        {
            AllClear();
            //個人 sum, min, max, avg
            var q1 = from s in students_scores
                     select new
                     {
                         s.Name,
                         Sum=s.Chi+s.Eng+s.Math,
                         Min=Math.Min(Math.Min(s.Chi,s.Eng),s.Math),
                         Max = Math.Max(Math.Max(s.Chi, s.Eng), s.Math),
                         Avg= (s.Chi + s.Eng + s.Math)/3
                     };
            chart3.DataSource = q1.ToList();
            chart3.Series.Add("Sum");
            chart3.Series.Add("Min");
            chart3.Series.Add("Max");
            chart3.Series.Add("Avg");
            foreach(var q in chart3.Series)
            {
                q.XValueMember = "Name";
                q.YValueMembers = q.Name;
                q.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                q.IsValueShownAsLabel = true;
            }
            //各科 sum, min, max, avg
            var q2 = from s in students_scores
                     group s by true into g
                     select new
                     {
                         Sum = g.Sum(s => s.Chi + s.Eng + s.Math),
                         Min = g.Min(s => Math.Min(Math.Min(s.Chi, s.Eng), s.Math)),
                         Max = g.Max(s => Math.Max(Math.Max(s.Chi, s.Eng), s.Math)),
                         Avg = (g.Average(s => (s.Chi + s.Eng + s.Math) / 3)).ToString("f2")
                     };
            var Q2 = q2.ToList();
            chart2.Series.Add("Subject");
            chart2.Series[0].Points.AddXY("Sum", Q2[0].Sum);
            chart2.Series[0].Points.AddXY("Min", Q2[0].Min);
            chart2.Series[0].Points.AddXY("Max", Q2[0].Max);
            chart2.Series[0].Points.AddXY("Avg", Q2[0].Avg);
            chart2.Series[0].IsValueShownAsLabel = true;
        }
        List<Student> score_Random100 = new List<Student>();
        private void button33_Click(object sender, EventArgs e)
        {
            AllClear();
            score_Random100.Clear();
            Random random = new Random(DateTime.Now.Millisecond);
            for(int i=0;i<100;i++)
            {
                int c = random.Next(101, 103);
                Student student = new Student()
                {
                    Class=$"CS_{c}",
                    Name = $"學生{i + 1}號",
                    Chi = random.Next(60, 101),
                    Eng = random.Next(60, 101),
                    Math = random.Next(60, 101),
                };
                score_Random100.Add(student);
            }
            dataGridView1.DataSource = score_Random100;
            // split=> 分成 三群 '待加強'(60~69) '佳'(70~89) '優良'(90~100) 

            var q = from s in score_Random100
                    group s by Spilt_Score(s) into g 
                    orderby OrderGKey(g)descending
                    select new { MyGroup = g, g.Key };
            // print 每一群是哪幾個 ? (每一群 sort by 分數 descending)
            var g0 = q.ToList()[0].MyGroup.ToList();
         
            foreach(var g in q)
            {  
                listBox1.Items.Add($"{g.Key}:");
                foreach (var gg in g.MyGroup)
                {
                    int avg = (gg.Chi + gg.Eng + gg.Math) / 3;
                    listBox1.Items.Add($"{gg.Name}  成績為:");
                    listBox1.Items.Add($"平均:{avg}分  國文:{gg.Chi}分  英文:{gg.Eng}分  數學:{gg.Math}分");
                }
                listBox1.Items.Add("");
            }
            chart1.Series.Add("成績分布");
            foreach(var g in q)
            {
                chart1.Series[0].Points.AddXY($"{g.Key}", g.MyGroup.Count());
            }
            chart1.Series[0].IsValueShownAsLabel = true;

        }

        private object OrderGKey(IGrouping<string, Student> g)
        {
            if (g.Key == "優良") return 3;
            else if (g.Key == "佳") return 2;
            else return 1;
        }

        string Spilt_Score(Student s)
        {
            int avg = (s.Chi + s.Eng + s.Math) / 3;
            if (avg <= 69) return "待加強";
            else if (avg <= 89) return "佳";
            else return "優良";
        }

        private void button35_Click(object sender, EventArgs e)
        {
            chart3.Series.Clear();
            // 統計 :　所有隨機分數出現的次數/比率; sort ascending or descending
            // 63     7.00%
            // 100    6.00%
            // 78     6.00%
            // 89     5.00%
            // 83     5.00%
            // 61     4.00%
            // 64     4.00%
            // 91     4.00%
            // 79     4.00%
            // 84     3.00%
            // 62     3.00%
            // 73     3.00%
            // 74     3.00%
            // 75     3.00%

            List<int> score_Percent = new List<int>();
            foreach(var s in score_Random100)
            {
                score_Percent.Add(s.Chi);
                score_Percent.Add(s.Eng);
                score_Percent.Add(s.Math);
            }
            var q = from s in score_Percent
                    group s by s into g
                    select new { g.Key, Percent=((double)g.Count())/300 };
            chart3.DataSource = q.ToList();
            chart3.Series.Add("所有隨機分數出現的次數/比率");
            chart3.Series[0].XValueMember = "Key";
            chart3.Series[0].YValueMembers = "Percent";
            chart3.Series[0].IsValueShownAsLabel = true;
            chart3.Series[0].Label = "#VAL{P}";


        }

        private void button34_Click(object sender, EventArgs e)
        {
            AllClear();
            // 年度最高銷售金額 年度最低銷售金額
            var q1 = from o in northwindEntities.Orders.AsEnumerable()
                        group o by o.OrderDate.Value.Year into g
                        orderby g.Sum(o => o.Order_Details.Sum(od => od.UnitPrice * od.Quantity * (decimal)(1 - od.Discount))) ascending
                     select new
                        {
                            Key=g.Key,
                            Max = g.Max(o => o.Order_Details.Sum(od => od.UnitPrice * od.Quantity * (decimal)(1 - od.Discount))),
                            Min = g.Min(o => o.Order_Details.Sum(od=> od.UnitPrice * od.Quantity * (decimal)(1 - od.Discount))),
                            Total=g.Sum(o => o.Order_Details.Sum(od => od.UnitPrice * od.Quantity * (decimal)(1 - od.Discount)))
                        };
                dataGridView1.DataSource = q1.ToList();
                chart1.DataSource = q1.ToList();
                chart1.Series.Add("年度最高銷售金額");
                chart1.Series[0].XValueMember = "Key";
                chart1.Series[0].YValueMembers = "Max";
                chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                chart1.Series[0].IsValueShownAsLabel = true;
                chart1.Series.Add("年度最低銷售金額");
                chart1.Series[1].XValueMember = "Key";
                chart1.Series[1].YValueMembers = "Min";
                chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                chart1.Series[1].IsValueShownAsLabel = true;

            // 那一年總銷售最好 ? 那一年總銷售最不好 ?  
            decimal max_Year = q1.ToList().Last().Key; decimal max_Total_Year = q1.ToList().Last().Total;
            decimal min_Year = q1.ToList().First().Key; decimal min_Total_Year = q1.ToList().First().Total;
            listBox1.Items.Add($"{max_Year}年總銷售最好，金額為:{max_Total_Year}");
            listBox1.Items.Add($"{min_Year}年總銷售最不好，金額為:{min_Total_Year}");
            // 那一個月總銷售最好 ? 那一個月總銷售最不好 ?
            var q2 = from o in northwindEntities.Orders.AsEnumerable()
                     group o by o.OrderDate.Value.Year+"年"+o.OrderDate.Value.Month+"月" into g
                     orderby g.Sum(o => o.Order_Details.Sum(od => od.UnitPrice * od.Quantity * (decimal)(1 - od.Discount))) ascending
                     select new
                     {
                         Key = g.Key,
                         Max = g.Max(o => o.Order_Details.Sum(od => od.UnitPrice * od.Quantity * (decimal)(1 - od.Discount))),
                         Min = g.Min(o => o.Order_Details.Sum(od => od.UnitPrice * od.Quantity * (decimal)(1 - od.Discount))),
                         Total = g.Sum(o => o.Order_Details.Sum(od => od.UnitPrice * od.Quantity * (decimal)(1 - od.Discount)))
                     };
            string max_Month = q2.ToList().Last().Key; decimal max_Total_Month = q2.ToList().Last().Total;
            string min_Month = q2.ToList().First().Key; decimal min_Total_Month = q2.ToList().First().Total;
            listBox1.Items.Add($"{max_Month}總銷售最好，金額為:{max_Total_Month}");
            listBox1.Items.Add($"{min_Month}總銷售最不好，金額為:{min_Total_Month}");
            // 每年 總銷售分析 圖
            chart2.DataSource = q1.ToList();
            chart2.Series.Add("每年總銷售分析");
            chart2.Series[0].XValueMember = "Key";
            chart2.Series[0].YValueMembers = "Total";
            chart2.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            // 每月 總銷售分析 圖
            chart3.DataSource = q2.ToList();
            chart3.Series.Add("每月總銷售分析");
            chart3.Series[0].XValueMember = "Key";
            chart3.Series[0].YValueMembers = "Total";
            chart3.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
        }
        void AllClear()
        {
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
            listBox1.Items.Clear();
            chart1.Series.Clear();
            chart2.Series.Clear();
            chart3.Series.Clear();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            AllClear();
            var q1 = from o in northwindEntities.Orders.AsEnumerable()
                     group o by o.OrderDate.Value.Year into g
                     orderby g.Key ascending
                     select new
                     {
                         Key = g.Key,
                         Total = g.Sum(o => o.Order_Details.Sum(od => od.UnitPrice * od.Quantity * (decimal)(1 - od.Discount)))
                     };
            List<Rate_Year> list = new List<Rate_Year>();
            var Q1 = q1.ToList();
            for (int i=1;i<Q1.Count();i++)
            {
                decimal rate = (Q1[i].Total - Q1[i - 1].Total) / Q1[i-1].Total;
                Rate_Year rate_Year = new Rate_Year
                {
                    Year = Q1[i].Key,
                    Rate = rate
                };
                list.Add(rate_Year);
            }
            chart3.DataSource = list;
            chart3.Series.Add("年銷售成長率");
            chart3.Series[0].XValueMember = "Year";
            chart3.Series[0].YValueMembers = "Rate";
            chart3.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.RangeColumn;
            chart3.Series[0].IsValueShownAsLabel = true;
            chart3.Series[0].Label = "#VAL{P}";


            //chart2.Series.Add("年銷售成長率");
            //foreach(var q in list)
            //{
            //    chart2.Series[1].Points.AddXY(q.Year, q.Rate);
            //}
            //chart2.Series[1].XAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            //chart2.Series[1].YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            //chart2.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
          
        }
        class Rate_Year
        {
            public decimal Rate { get; set; }
            public decimal Year { get; set; }
        }
    }
}
