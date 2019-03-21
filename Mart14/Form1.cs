using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mart14
{
    //@Author Yunus Emre Ertürk=>>> yemrerturk@gmail.com / www.muhendiserturk.com
    public partial class Form1 : Form
    {
        bool a=true;
        DateTime baslangic = new DateTime();
        Stopwatch stopwatch = new Stopwatch();
        double zaman = 0;
        SqlConnection Conn = new SqlConnection(
            ConfigurationManager.ConnectionStrings["NorthwindConnectionString"].
            ConnectionString);
        public Form1()
        {
            InitializeComponent();
            timer1.Interval=10000;
        }

        private void btnEntity_Click(object sender, EventArgs e)
        {
            stopwatch.Restart();
            NorthwindEntities db = new NorthwindEntities();            
            
            if (a)
            {
                lbEntity.DataSource = db.Orders.Select(x => x.OrderID).ToList();
                a = false;
                label3.Text = "ORders";
            }
            else
            {
                label3.Text = "ORder Details";
                a = true;
                lbEntity.DataSource = db.Order_Details.Select(x => x.OrderID).ToList();
            }


            stopwatch.Stop();
            label6.Text = stopwatch.ElapsedMilliseconds.ToString();
            
            

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            zaman++;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Interval = 1;
            timer1.Enabled=true;
            baslangic = DateTime.Now;
            
        }

        private void btnDisconnected_Click(object sender, EventArgs e)
        {
            stopwatch.Restart();
            //timer1.Start();          
            SqlCommand cmd = new SqlCommand("Select OrderID,CustomerID from Orders",Conn);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            foreach (DataRow item in dt.Rows)
            {
                lbDisconnected.Items.Add(item[0]+" "+ item[1]);     
                
            }
            //label5.Text = zaman.ToString();
            //timer1.Stop();
            //zaman = 0;
            stopwatch.Stop();
            label5.Text = stopwatch.ElapsedMilliseconds.ToString();


        }

        private void btnConnected_Click(object sender, EventArgs e)
        {
            
            stopwatch.Restart();
            //timer1.Start();
            SqlCommand cmd = new SqlCommand("Select OrderID from Orders",Conn);
            Conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    lbConnected.Items.Add(reader.GetInt32(0));
                }
            }
            else
            {
                Console.WriteLine("No rows found.");
            }
            reader.Close();
            Conn.Close();
            //label4.Text = zaman.ToString();
            //timer1.Stop();
            //zaman = 0;
            stopwatch.Stop();
            label4.Text = stopwatch.ElapsedMilliseconds.ToString();

        }
    }
}
//@Author Yunus Emre Ertürk=>>> yemrerturk@gmail.com / www.muhendiserturk.com