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
using System.Data.SqlClient;

namespace ATMTuto
{
    public partial class Account : Form
    {
        public Account()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;
AttachDbFilename=C:\Users\24097\OneDrive\Documents\ATMDb.mdf;
Integrated Security=True;Connect Timeout=30");

        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            int bal = 0;
            if (AccNameTb.Text == "" || AccNumTb.Text == "" || LaNameTb.Text == "" || PhoneTb.Text == "" || AddressTb.Text == "" || OccupationTb.Text == "" || PinTb.Text == "")
            {
                MessageBox.Show("信息缺失！");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "insert into AccountTbl values('" + AccNumTb.Text + "', '" + AccNameTb.Text + "', '" + LaNameTb.Text + "', '" + DobDate.Value.Date + "', '" + PhoneTb.Text + "', '" + AddressTb.Text + "', '" + EducationCb.SelectedItem.ToString() + "', '" + OccupationTb.Text + "', '" + PinTb.Text + "', " + bal + ")";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("账户注册成功！！！");
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

        private void LogoutLbl_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Hide();
        }
    }
}
