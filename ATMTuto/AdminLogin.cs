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
    public partial class AdminLogin : Form
    {
        public AdminLogin()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;
AttachDbFilename=C:\Users\24097\OneDrive\Documents\ATMDb.mdf;
Integrated Security=True;Connect Timeout=30");

        private void AdminLoginBtn_Click(object sender, EventArgs e)
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select count(*) from AdminTbl where AdminUsername = '" + AdminUserTb.Text + "' and AdminPassword = '" + AdminPassTb.Text + "'", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                AdminHome adminHome = new AdminHome();
                FormTransitionHelper.SwitchForm(this, adminHome);
            }
            else
            {
                MessageBox.Show("管理员用户名或密码错误，请重新输入！");
            }
            Con.Close();
        }

        private void BackLbl_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            FormTransitionHelper.SwitchForm(this, log);
        }

        private void AdminUserTb_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 只允许输入自然数和英文字母大小写
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void AdminPassTb_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 只允许输入自然数和英文字母大小写
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}