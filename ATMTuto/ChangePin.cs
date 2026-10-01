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

        private const string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;
AttachDbFilename=C:\Users\24097\OneDrive\Documents\ATMDb.mdf;
Integrated Security=True;Connect Timeout=30";

        string Acc = Login.AccNumber;

        private void Guna2Button1_Click(object sender, EventArgs e)
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
                        using (SqlConnection con = new SqlConnection(ConnectionString))
                        {
                            con.Open();
                            string query = "select PIN from AccountTbl where AccNum = @AccNum";
                            SqlCommand cmd = new SqlCommand(query, con);
                            cmd.Parameters.AddWithValue("@AccNum", Acc);
                            object result_pin = cmd.ExecuteScalar();

                            if (result_pin != null)
                            {
                                string storedEncryptedPin = result_pin.ToString();
                                string inputEncryptedPin = AESHelper.Encrypt(textBox1.Text);

                                if (storedEncryptedPin == inputEncryptedPin)
                                {
                                    string updateQuery = "update AccountTbl set PIN = @NewPin where AccNum = @AccNum";
                                    SqlCommand updateCmd = new SqlCommand(updateQuery, con);
                                    updateCmd.Parameters.AddWithValue("@NewPin", AESHelper.Encrypt(Pin1Tb.Text));
                                    updateCmd.Parameters.AddWithValue("@AccNum", Acc);
                                    updateCmd.ExecuteNonQuery();
                                    MessageBox.Show("密码修改成功！");
                                    Login log = new Login();
                                    FormTransitionHelper.SwitchForm(this, log);
                                }
                                else
                                {
                                    MessageBox.Show("旧密码输入错误，请重新输入！");
                                }
                            }
                            else
                            {
                                MessageBox.Show("旧密码输入错误，请重新输入！");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void Label8_Click(object sender, EventArgs e)
        {
            HOME home = new HOME();
            FormTransitionHelper.SwitchForm(this, home);
        }

        private void Pin1Tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Pin2Tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Pin1Tb_TextChanged(object sender, EventArgs e)
        {
            if (Pin1Tb.Text.Length > 6)
            {
                Pin1Tb.Text = Pin1Tb.Text.Substring(0, 6);
                Pin1Tb.SelectionStart = Pin1Tb.Text.Length;
                MessageBox.Show("PIN长度不能超过6位！");
            }
        }

        private void Pin2Tb_TextChanged(object sender, EventArgs e)
        {
            if (Pin2Tb.Text.Length > 6)
            {
                Pin2Tb.Text = Pin2Tb.Text.Substring(0, 6);
                Pin2Tb.SelectionStart = Pin2Tb.Text.Length;
                MessageBox.Show("PIN长度不能超过6位！");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 6)
            {
                textBox1.Text = textBox1.Text.Substring(0, 6);
                textBox1.SelectionStart = textBox1.Text.Length;
                MessageBox.Show("PIN长度不能超过6位！");
            }
        }
    }
}