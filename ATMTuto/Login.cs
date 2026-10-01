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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;
AttachDbFilename=C:\Users\24097\OneDrive\Documents\ATMDb.mdf;
Integrated Security=True;Connect Timeout=30");

        private void Label8_Click(object sender, EventArgs e)
        {
            Account acc = new Account();
            FormTransitionHelper.SwitchForm(this, acc);
        }

        public static String AccNumber;

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(AccNumTb.Text) || string.IsNullOrEmpty(PinTb.Text))
            {
                MessageBox.Show("请输入账号和密码！");
                return;
            }

            Con.Open();
            string query = "select PIN from AccountTbl where AccNum = @AccNum";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.Parameters.AddWithValue("@AccNum", AccNumTb.Text);
            object result = cmd.ExecuteScalar();

            if (result != null)
            {
                string storedEncryptedPin = result.ToString();
                string inputEncryptedPin = AESHelper.Encrypt(PinTb.Text);

                if (storedEncryptedPin == inputEncryptedPin)
                {
                    AccNumber = AccNumTb.Text;
                    HOME hOME = new HOME();
                    FormTransitionHelper.SwitchForm(this, hOME);
                }
                else
                {
                    MessageBox.Show("您输入用户名或密码错误，请重新输入！");
                }
            }
            else
            {
                MessageBox.Show("您输入用户名或密码错误，请重新输入！");
            }
            Con.Close();
        }

        private void AccNumTb_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 只允许输入自然数和英文字母大小写
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void PinTb_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 只允许输入自然数和英文字母大小写
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void PinTb_TextChanged(object sender, EventArgs e)
        {
            // 限制PIN长度为6位
            if (PinTb.Text.Length > 6)
            {
                PinTb.Text = PinTb.Text.Substring(0, 6);
                PinTb.SelectionStart = PinTb.Text.Length;
                MessageBox.Show("PIN长度不能超过6位！");
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {
            AdminLogin adminlogin = new AdminLogin();
            FormTransitionHelper.SwitchForm(this, adminlogin);
        }
    }
}
