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
            if (string.IsNullOrEmpty(AccNumTb.Text) || string.IsNullOrEmpty(AccNameTb.Text) || 
                string.IsNullOrEmpty(LaNameTb.Text) || string.IsNullOrEmpty(PhoneTb.Text) || 
                string.IsNullOrEmpty(AddressTb.Text) || string.IsNullOrEmpty(OccupationTb.Text) || 
                string.IsNullOrEmpty(PinTb.Text) || EducationCb.SelectedItem == null)
            {
                MessageBox.Show("请填写完整信息");
                return;
            }

            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select count(*) from AccountTbl where AccNum = @AccNum", Con);
            sda.SelectCommand.Parameters.AddWithValue("@AccNum", AccNumTb.Text);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                MessageBox.Show("账号已存在");
                Con.Close();
                return;
            }

            string query = "insert into AccountTbl values(@AccNum, @AccName, @LaName, @Dob, @Phone, @Address, @Education, @Occupation, @PIN, @Balance, 50000, 10000, 20000, 10000)";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.Parameters.AddWithValue("@AccNum", AccNumTb.Text);
            cmd.Parameters.AddWithValue("@AccName", AccNameTb.Text);
            cmd.Parameters.AddWithValue("@LaName", LaNameTb.Text);
            cmd.Parameters.AddWithValue("@Dob", DobDate.Value.Date);
            cmd.Parameters.AddWithValue("@Phone", AESHelper.MaskPhone(PhoneTb.Text));
            cmd.Parameters.AddWithValue("@Address", AddressTb.Text);
            cmd.Parameters.AddWithValue("@Education", EducationCb.SelectedItem.ToString());
            cmd.Parameters.AddWithValue("@Occupation", OccupationTb.Text);
            cmd.Parameters.AddWithValue("@PIN", AESHelper.Encrypt(PinTb.Text));
            cmd.Parameters.AddWithValue("@Balance", AESHelper.EncryptAmount(0));
            cmd.ExecuteNonQuery();
            Con.Close();
            MessageBox.Show("注册成功");
            Login login = new Login();
            FormTransitionHelper.SwitchForm(this, login);
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