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
    public partial class Deposit : Form
    {
        public Deposit()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;
AttachDbFilename=C:\Users\24097\OneDrive\Documents\ATMDb.mdf;
Integrated Security=True;Connect Timeout=30");

        int oldBalance, newbalance;
        string Acc = Login.AccNumber;
        private void addtransaction()
        {
            string TrType = "存款";
            try
            {
                Con.Open();
                string query = "insert into TransactionTbl values('" + Acc + "', '" + TrType + "', '" + DepoAmtTb.Text + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "')";
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
            oldBalance = Convert.ToInt32(dt.Rows[0][0].ToString());
            Con.Close();
        }

        private void DepoBtn_Click(object sender, EventArgs e)
        {
            if (DepoAmtTb.Text == "" || Convert.ToInt32(DepoAmtTb.Text) <= 0)
            {
                MessageBox.Show("请输入存款金额");
            }
            else
            {
                newbalance = oldBalance + Convert.ToInt32(DepoAmtTb.Text);
                try
                {
                    Con.Open();
                    string query = "update AccountTbl set Balance = " + newbalance + "where AccNum = '" + Acc + "'";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("存款交易成功！");
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

        private void Deposit_Load(object sender, EventArgs e)
        {
            getBalance();
        }
    }
}