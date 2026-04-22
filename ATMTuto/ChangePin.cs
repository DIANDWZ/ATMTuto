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
            if (textBox1.Text == "" || Pin1Tb.Text == "" || Pin2Tb.Text == "")
            {
                MessageBox.Show("请输入旧密码和新密码");
            }
            else if (Pin2Tb.Text != Pin1Tb.Text)
            {
                MessageBox.Show("密码输入不一致，请重新输入！");
            }
            else
            {
                DialogResult result = MessageBox.Show(
                    "修改密码确认\n\n" +
                    "账号：" + Acc + "\n" +
                    "新密码：" + Pin1Tb.Text + "\n\n" +
                    "请确认是否修改密码？",
                    "修改密码确认",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        Con.Open();
                        SqlDataAdapter sda = new SqlDataAdapter("select count(*) from AccountTbl where AccNum = '" + Acc + "' and PIN = '" + textBox1.Text + "'", Con);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        if (dt.Rows[0][0].ToString() == "1")
                        {
                            string query = "update AccountTbl set PIN = '" + Pin1Tb.Text + "' where AccNum = '" + Acc + "'";
                            SqlCommand cmd = new SqlCommand(query, Con);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("密码修改成功！");
                            Con.Close();
                            Login log = new Login();
                            FormTransitionHelper.SwitchForm(this, log);
                        }
                        else
                        {
                            MessageBox.Show("旧密码输入错误，请重新输入！");
                            Con.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        Con.Close();
                    }
                }
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {
            HOME home = new HOME();
            FormTransitionHelper.SwitchForm(this, home);
        }

        private void Pin1Tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 只允许输入自然数和英文字母大小写
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Pin2Tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 只允许输入自然数和英文字母大小写
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 只允许输入自然数和英文字母大小写
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Pin1Tb_TextChanged(object sender, EventArgs e)
        {
            // 限制PIN长度为6位
            if (Pin1Tb.Text.Length > 6)
            {
                Pin1Tb.Text = Pin1Tb.Text.Substring(0, 6);
                Pin1Tb.SelectionStart = Pin1Tb.Text.Length;
                MessageBox.Show("PIN长度不能超过6位！");
            }
        }

        private void Pin2Tb_TextChanged(object sender, EventArgs e)
        {
            // 限制PIN长度为6位
            if (Pin2Tb.Text.Length > 6)
            {
                Pin2Tb.Text = Pin2Tb.Text.Substring(0, 6);
                Pin2Tb.SelectionStart = Pin2Tb.Text.Length;
                MessageBox.Show("PIN长度不能超过6位！");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // 限制PIN长度为6位
            if (textBox1.Text.Length > 6)
            {
                textBox1.Text = textBox1.Text.Substring(0, 6);
                textBox1.SelectionStart = textBox1.Text.Length;
                MessageBox.Show("PIN长度不能超过6位！");
            }
        }
    }
}
