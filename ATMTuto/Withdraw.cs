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
    public partial class Withdraw : Form
    {
        public Withdraw()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;
AttachDbFilename=C:\Users\24097\OneDrive\Documents\ATMDb.mdf;
Integrated Security=True;Connect Timeout=30");
        string Acc = Login.AccNumber;
        int bal, newbalance;
        private void addtransaction()
        {
            string TrType = "取款";
            try
            {
                Con.Open();
                string query = "insert into TransactionTbl values('" + Acc + "', '" + TrType + "', '" + WdAmtTb.Text + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "')";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                //MessageBox.Show("账户注册成功！！！");
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
        private void getBalance()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select Balance from AccountTbl where AccNum = '" + Acc + "'", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            balancelbl.Text = "¥ " + dt.Rows[0][0].ToString();
            bal = Convert.ToInt32(dt.Rows[0][0].ToString());
            Con.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (WdAmtTb.Text == "")
            {
                MessageBox.Show("请输入取款金额");
            }
            else if (Convert.ToInt32(WdAmtTb.Text) <= 0)
            {
                MessageBox.Show("请输入有效金额");
            }
            else if (Convert.ToInt32(WdAmtTb.Text) > bal)
            {
                MessageBox.Show("余额不足");
            }
            else
            {
                newbalance = bal - Convert.ToInt32(WdAmtTb.Text);
                try
                {
                    Con.Open();
                    string query = "update AccountTbl set Balance = " + newbalance + "where AccNum = '" + Acc + "'";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("取款交易成功！");
                    Con.Close();
                    addtransaction();
                    HOME home = new HOME();
                    home.Show();
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

        private void Withdraw_Load(object sender, EventArgs e)
        {
            getBalance();
        }
    }
}
