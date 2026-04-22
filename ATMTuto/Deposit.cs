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

        int oldBalance, newbalance, dailyDepositLimit, singleDepositLimit;
        string Acc = Login.AccNumber;
        private void addtransaction()
        {
            string TrType = "存款";
            try
            {
                Con.Open();
                string query = "insert into TransactionTbl (AccNum, Type, Amount, TDate) values(@Acc, @TrType, @DepoAmt, @DateTime)";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.Parameters.AddWithValue("@Acc", Acc);
                cmd.Parameters.AddWithValue("@TrType", TrType);
                cmd.Parameters.AddWithValue("@DepoAmt", DepoAmtTb.Text);
                cmd.Parameters.AddWithValue("@DateTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.ExecuteNonQuery();
                Con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void getBalance()
        {
            Con.Open();
            string query = "select Balance, DailyDepositLimit, SingleDepositLimit from AccountTbl where AccNum = @Acc";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            sda.SelectCommand.Parameters.AddWithValue("@Acc", Acc);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                oldBalance = Convert.ToInt32(dt.Rows[0][0].ToString());
                dailyDepositLimit = dt.Rows[0][1] != DBNull.Value ? Convert.ToInt32(dt.Rows[0][1].ToString()) : 50000;
                singleDepositLimit = dt.Rows[0][2] != DBNull.Value ? Convert.ToInt32(dt.Rows[0][2].ToString()) : 10000;
            }
            else
            {
                oldBalance = 0;
                dailyDepositLimit = 50000;
                singleDepositLimit = 10000;
            }
            Con.Close();
        }
        private int getDailyDepositAmount()
        {
            int dailyAmount = 0;
            Con.Open();
            string query = "select sum(Amount) from TransactionTbl where AccNum = @Acc and Type = '存款' and convert(date, TDate) = convert(date, getdate())";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.Parameters.AddWithValue("@Acc", Acc);
            object result = cmd.ExecuteScalar();
            if (result != DBNull.Value && result != null)
            {
                dailyAmount = Convert.ToInt32(result);
            }
            Con.Close();
            return dailyAmount;
        }

        private void DepoBtn_Click(object sender, EventArgs e)
        {
            if (DepoAmtTb.Text == "" || Convert.ToInt32(DepoAmtTb.Text) <= 0)
            {
                MessageBox.Show("请输入存款金额");
            }
            else if (singleDepositLimit > 0 && Convert.ToInt32(DepoAmtTb.Text) > singleDepositLimit)
            {
                MessageBox.Show("单次存款不可超过￥" + singleDepositLimit);
            }
            else
            {
                int dailyDepositAmount = getDailyDepositAmount();
                int totalDeposit = dailyDepositAmount + Convert.ToInt32(DepoAmtTb.Text);
                int remainingDailyLimit = dailyDepositLimit - dailyDepositAmount;
                if (dailyDepositLimit > 0 && totalDeposit > dailyDepositLimit)
                {
                    MessageBox.Show("超过每日存款限额￥" + dailyDepositLimit + "\n今日已存：￥" + dailyDepositAmount + "\n每日剩余额度：￥" + remainingDailyLimit);
                    return;
                }
                DialogResult result = MessageBox.Show(
                    "存款确认\n\n" +
                    "账号：" + Acc + "\n" +
                    "存款金额：￥" + DepoAmtTb.Text + "\n" +
                    "当前余额：￥" + oldBalance + "\n" +
                    "每日存款剩余额度：￥" + remainingDailyLimit + "\n\n" +
                    "请确认以上信息是否正确？",
                    "存款确认",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    newbalance = oldBalance + Convert.ToInt32(DepoAmtTb.Text);
                    try
                    {
                        Con.Open();
                        string query = "update AccountTbl set Balance = @newbalance where AccNum = @Acc";
                        SqlCommand cmd = new SqlCommand(query, Con);
                        cmd.Parameters.AddWithValue("@newbalance", newbalance);
                        cmd.Parameters.AddWithValue("@Acc", Acc);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("存款交易成功！\n存款金额：￥" + DepoAmtTb.Text + "\n剩余额度：￥" + (remainingDailyLimit - Convert.ToInt32(DepoAmtTb.Text)));
                        Con.Close();
                        addtransaction();
                        HOME home = new HOME();
                        FormTransitionHelper.SwitchForm(this, home);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {
            HOME home = new HOME();
            FormTransitionHelper.SwitchForm(this, home);
        }

        private void DepoAmtTb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void Deposit_Load(object sender, EventArgs e)
        {
            getBalance();
        }
    }
}