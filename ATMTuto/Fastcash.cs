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
    public partial class Fastcash : Form
    {
        public Fastcash()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;
AttachDbFilename=C:\Users\24097\OneDrive\Documents\ATMDb.mdf;
Integrated Security=True;Connect Timeout=30");
        string Acc = Login.AccNumber;
        int bal;
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
        private void addtransaction1()
        {
            string TrType = "取款";
            try
            {
                Con.Open();
                string query = "insert into TransactionTbl values('" + Acc + "', '" + TrType + "', '" + 100 + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "')";
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
        private void addtransaction2()
        {
            string TrType = "取款";
            try
            {
                Con.Open();
                string query = "insert into TransactionTbl values('" + Acc + "', '" + TrType + "', '" + 500 + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "')";
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
        private void addtransaction3()
        {
            string TrType = "取款";
            try
            {
                Con.Open();
                string query = "insert into TransactionTbl values('" + Acc + "', '" + TrType + "', '" + 1000 + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "')";
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
        private void addtransaction4()
        {
            string TrType = "取款";
            try
            {
                Con.Open();
                string query = "insert into TransactionTbl values('" + Acc + "', '" + TrType + "', '" + 2000 + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "')";
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
        private void addtransaction5()
        {
            string TrType = "取款";
            try
            {
                Con.Open();
                string query = "insert into TransactionTbl values('" + Acc + "', '" + TrType + "', '" + 5000 + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "')";
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
        private void addtransaction6()
        {
            string TrType = "取款";
            try
            {
                Con.Open();
                string query = "insert into TransactionTbl values('" + Acc + "', '" + TrType + "', '" + 10000 + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "')";
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

        private void Fastcash_Load(object sender, EventArgs e)
        {
            getBalance();
        }

        private void label21_Click_1(object sender, EventArgs e)
        {
            HOME home = new HOME();
            home.Show();
            this.Hide();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (bal < 100)
            {
                MessageBox.Show("余额不足");
            }
            else
            {
                int newbalance = bal - 100;
                try
                {
                    Con.Open();
                    string query = "update AccountTbl set Balance = " + newbalance + "where AccNum = '" + Acc + "'";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("取款交易成功！");
                    Con.Close();
                    addtransaction1();
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

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (bal < 500)
            {
                MessageBox.Show("余额不足");
            }
            else
            {
                int newbalance = bal - 500;
                try
                {
                    Con.Open();
                    string query = "update AccountTbl set Balance = " + newbalance + "where AccNum = '" + Acc + "'";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("取款交易成功！");
                    Con.Close();
                    addtransaction2();
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

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (bal < 1000)
            {
                MessageBox.Show("余额不足");
            }
            else
            {
                int newbalance = bal - 1000;
                try
                {
                    Con.Open();
                    string query = "update AccountTbl set Balance = " + newbalance + "where AccNum = '" + Acc + "'";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("取款交易成功！");
                    Con.Close();
                    addtransaction3();
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

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (bal < 2000)
            {
                MessageBox.Show("余额不足");
            }
            else
            {
                int newbalance = bal - 2000;
                try
                {
                    Con.Open();
                    string query = "update AccountTbl set Balance = " + newbalance + "where AccNum = '" + Acc + "'";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("取款交易成功！");
                    Con.Close();
                    addtransaction4();
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

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            if (bal < 5000)
            {
                MessageBox.Show("余额不足");
            }
            else
            {
                int newbalance = bal - 5000;
                try
                {
                    Con.Open();
                    string query = "update AccountTbl set Balance = " + newbalance + "where AccNum = '" + Acc + "'";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("取款交易成功！");
                    Con.Close();
                    addtransaction5();
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

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            if (bal < 10000)
            {
                MessageBox.Show("余额不足");
            }
            else
            {
                int newbalance = bal - 10000;
                try
                {
                    Con.Open();
                    string query = "update AccountTbl set Balance = " + newbalance + "where AccNum = '" + Acc + "'";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("取款交易成功！");
                    Con.Close();
                    addtransaction6();
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
    }
}