using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATMTuto
{
    public partial class ChangePin : Form
    {
        public ChangePin()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;
AttachDbFilename=C:\Users\24097\OneDrive\Documents\ATMDb.mdf;
Integrated Security=True;Connect Timeout=30");

        string Acc = Login.AccNumber;

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (Pin1Tb.Text == "" || Pin2Tb.Text == "")
            {
                MessageBox.Show("请输入新密码");
            }
            else if (Pin2Tb.Text != Pin1Tb.Text)
            {
                MessageBox.Show("密码输入不一致，请重新输入！");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "update AccountTbl set PIN = " + Pin1Tb.Text + "where AccNum = '" + Acc + "'";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("密码修改成功！");
                    Con.Close();
                    Login log = new Login();
                    log.Show();
                    this.Hide();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {
            HOME home = new HOME();
            home.Show();
            this.Hide();
        }
    }
}
