using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConnectToMySql
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (DcContext dc = new DcContext())//增
            {
                try
                {
                    dc.SWs.Add(new SW() { Name = "lkw", Price = 26 });
                    dc.SaveChanges();
                }
                catch (Exception ex)
                {

                }
            }

            using (DcContext dc = new DcContext())//删
            {
                try
                {
                    dc.SWs.Where(p => p.Price == 18).ToList<SW>().ForEach((s) => dc.SWs.Remove(s));
                    dc.SaveChanges();
                }
                catch (Exception ex)
                {

                }

            }

            using (DcContext dc = new DcContext())//查和改
            {
                try
                {
                    var aa = dc.SWs.Where(p => p.Price == 26).ToList<SW>();
                    foreach (var a in aa)
                    {
                        a.Price = 16;
                    }
                    dc.SaveChanges();
                }
                catch (Exception ex)
                {

                }
                
            }
        }
    }
}
