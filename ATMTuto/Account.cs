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
                DialogResult result = MessageBox.Show(
                    "开户确认\n\n" +
                    "账号：" + AccNumTb.Text + "\n" +
                    "户名：" + AccNameTb.Text + " " + LaNameTb.Text + "\n" +
                    "手机号：" + PhoneTb.Text + "\n" +
                    "职业：" + OccupationTb.Text + "\n" +
                    "PIN码：" + PinTb.Text + "\n\n" +
                    "请确认以上信息是否正确？",
                    "开户确认",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        Con.Open();
                        string query = @"insert into AccountTbl values(@AccNum, @AccName, @LaName, @Dob, @Phone, @Address, @Education, @Occupation, @Pin, @bal)";
                        SqlCommand cmd = new SqlCommand(query, Con);
                        cmd.Parameters.AddWithValue("@AccNum", AccNumTb.Text);
                        cmd.Parameters.AddWithValue("@AccName", AccNameTb.Text);
                        cmd.Parameters.AddWithValue("@LaName", LaNameTb.Text);
                        cmd.Parameters.AddWithValue("@Dob", DobDate.Value.Date);
                        cmd.Parameters.AddWithValue("@Phone", PhoneTb.Text);
                        cmd.Parameters.AddWithValue("@Address", AddressTb.Text);
                        cmd.Parameters.AddWithValue("@Education", EducationCb.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@Occupation", OccupationTb.Text);
                        cmd.Parameters.AddWithValue("@Pin", PinTb.Text);
                        cmd.Parameters.AddWithValue("@bal", bal);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("账户注册成功！！！");
                        Con.Close();
                        Login log = new Login();
                        FormTransitionHelper.SwitchForm(this, log);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void LogoutLbl_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            FormTransitionHelper.SwitchForm(this, log);
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
    }
}
