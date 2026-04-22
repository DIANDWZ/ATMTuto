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
        int bal, dailyWithdrawLimit, singleWithdrawLimit;
        private void addtransaction(int amount)
        {
            string TrType = "取款";
            try
            {
                Con.Open();
                string query = "insert into TransactionTbl (AccNum, Type, Amount, TDate) values(@Acc, @TrType, @Amt, @DateTime)";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.Parameters.AddWithValue("@Acc", Acc);
                cmd.Parameters.AddWithValue("@TrType", TrType);
                cmd.Parameters.AddWithValue("@Amt", amount);
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
            SqlDataAdapter sda = new SqlDataAdapter("select Balance, DailyWithdrawLimit, SingleWithdrawLimit from AccountTbl where AccNum = '" + Acc + "'", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                balancelbl.Text = "￥" + dt.Rows[0][0].ToString();
                bal = Convert.ToInt32(dt.Rows[0][0].ToString());
                dailyWithdrawLimit = dt.Rows[0][1] != DBNull.Value ? Convert.ToInt32(dt.Rows[0][1].ToString()) : 20000;
                singleWithdrawLimit = dt.Rows[0][2] != DBNull.Value ? Convert.ToInt32(dt.Rows[0][2].ToString()) : 10000;
            }
            else
            {
                bal = 0;
                dailyWithdrawLimit = 20000;
                singleWithdrawLimit = 10000;
            }
            Con.Close();
        }
        private int getDailyWithdrawAmount()
        {
            int dailyAmount = 0;
            Con.Open();
            string query = "select sum(Amount) from TransactionTbl where AccNum = @Acc and (Type = '取款' or Type = '转账') and convert(date, TDate) = convert(date, getdate())";
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
        private int getRemainingDailyLimit()
        {
            int dailyWithdrawAmount = getDailyWithdrawAmount();
            return dailyWithdrawLimit - dailyWithdrawAmount;
        }

        private bool checkLimits(int amount)
        {
            if (singleWithdrawLimit > 0 && amount > singleWithdrawLimit)
            {
                MessageBox.Show("单次取款不可超过￥" + singleWithdrawLimit);
                return false;
            }
            int dailyWithdrawAmount = getDailyWithdrawAmount();
            int totalWithdraw = dailyWithdrawAmount + amount;
            int remainingDailyLimit = dailyWithdrawLimit - dailyWithdrawAmount;
            if (dailyWithdrawLimit > 0 && totalWithdraw > dailyWithdrawLimit)
            {
                MessageBox.Show("超过每日限额（取款+转账）\n每日限额：￥" + dailyWithdrawLimit + "\n今日已取款/转账：￥" + dailyWithdrawAmount + "\n每日剩余额度：￥" + remainingDailyLimit);
                return false;
            }
            return true;
        }

        private void Fastcash_Load(object sender, EventArgs e)
        {
            getBalance();
        }

        private void label21_Click_1(object sender, EventArgs e)
        {
            HOME home = new HOME();
            FormTransitionHelper.SwitchForm(this, home);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            int amount = 100;
            if (bal < amount)
            {
                MessageBox.Show("余额不足，当前余额：￥" + bal);
            }
            else if (!checkLimits(amount))
            {
                return;
            }
            else
            {
                int remainingDailyLimit = getRemainingDailyLimit();
                DialogResult result = MessageBox.Show(
                    "取款确认\n\n" +
                    "账号：" + Acc + "\n" +
                    "取款金额：￥" + amount + "\n" +
                    "当前余额：￥" + bal + "\n" +
                    "每日取款剩余额度：￥" + remainingDailyLimit + "\n\n" +
                    "请确认以上信息是否正确？",
                    "取款确认",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    int newBalance = bal - amount;
                    try
                    {
                        Con.Open();
                        string query = "update AccountTbl set Balance = " + newBalance + " where AccNum = '" + Acc + "'";
                        SqlCommand cmd = new SqlCommand(query, Con);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("取款交易成功！\n取款金额：￥" + amount + "\n剩余额度：￥" + (remainingDailyLimit - amount) + "\n余额：￥" + newBalance);
                        Con.Close();
                        addtransaction(amount);
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

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            int amount = 500;
            if (bal < amount)
            {
                MessageBox.Show("余额不足，当前余额：￥" + bal);
            }
            else if (!checkLimits(amount))
            {
                return;
            }
            else
            {
                int remainingDailyLimit = getRemainingDailyLimit();
                DialogResult result = MessageBox.Show(
                    "取款确认\n\n" +
                    "账号：" + Acc + "\n" +
                    "取款金额：￥" + amount + "\n" +
                    "当前余额：￥" + bal + "\n" +
                    "每日取款剩余额度：￥" + remainingDailyLimit + "\n\n" +
                    "请确认以上信息是否正确？",
                    "取款确认",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    int newBalance = bal - amount;
                    try
                    {
                        Con.Open();
                        string query = "update AccountTbl set Balance = " + newBalance + " where AccNum = '" + Acc + "'";
                        SqlCommand cmd = new SqlCommand(query, Con);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("取款交易成功！\n取款金额：￥" + amount + "\n剩余额度：￥" + (remainingDailyLimit - amount) + "\n余额：￥" + newBalance);
                        Con.Close();
                        addtransaction(amount);
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

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            int amount = 1000;
            if (bal < amount)
            {
                MessageBox.Show("余额不足，当前余额：￥" + bal);
            }
            else if (!checkLimits(amount))
            {
                return;
            }
            else
            {
                int remainingDailyLimit = getRemainingDailyLimit();
                DialogResult result = MessageBox.Show(
                    "取款确认\n\n" +
                    "账号：" + Acc + "\n" +
                    "取款金额：￥" + amount + "\n" +
                    "当前余额：￥" + bal + "\n" +
                    "每日取款剩余额度：￥" + remainingDailyLimit + "\n\n" +
                    "请确认以上信息是否正确？",
                    "取款确认",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    int newBalance = bal - amount;
                    try
                    {
                        Con.Open();
                        string query = "update AccountTbl set Balance = " + newBalance + " where AccNum = '" + Acc + "'";
                        SqlCommand cmd = new SqlCommand(query, Con);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("取款交易成功！\n取款金额：￥" + amount + "\n剩余额度：￥" + (remainingDailyLimit - amount) + "\n余额：￥" + newBalance);
                        Con.Close();
                        addtransaction(amount);
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

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            int amount = 2000;
            if (bal < amount)
            {
                MessageBox.Show("余额不足，当前余额：￥" + bal);
            }
            else if (!checkLimits(amount))
            {
                return;
            }
            else
            {
                int remainingDailyLimit = getRemainingDailyLimit();
                DialogResult result = MessageBox.Show(
                    "取款确认\n\n" +
                    "账号：" + Acc + "\n" +
                    "取款金额：￥" + amount + "\n" +
                    "当前余额：￥" + bal + "\n" +
                    "每日取款剩余额度：￥" + remainingDailyLimit + "\n\n" +
                    "请确认以上信息是否正确？",
                    "取款确认",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    int newBalance = bal - amount;
                    try
                    {
                        Con.Open();
                        string query = "update AccountTbl set Balance = " + newBalance + " where AccNum = '" + Acc + "'";
                        SqlCommand cmd = new SqlCommand(query, Con);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("取款交易成功！\n取款金额：￥" + amount + "\n剩余额度：￥" + (remainingDailyLimit - amount) + "\n余额：￥" + newBalance);
                        Con.Close();
                        addtransaction(amount);
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

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            int amount = 5000;
            if (bal < amount)
            {
                MessageBox.Show("余额不足，当前余额：￥" + bal);
            }
            else if (!checkLimits(amount))
            {
                return;
            }
            else
            {
                int remainingDailyLimit = getRemainingDailyLimit();
                DialogResult result = MessageBox.Show(
                    "取款确认\n\n" +
                    "账号：" + Acc + "\n" +
                    "取款金额：￥" + amount + "\n" +
                    "当前余额：￥" + bal + "\n" +
                    "每日取款剩余额度：￥" + remainingDailyLimit + "\n\n" +
                    "请确认以上信息是否正确？",
                    "取款确认",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    int newBalance = bal - amount;
                    try
                    {
                        Con.Open();
                        string query = "update AccountTbl set Balance = " + newBalance + " where AccNum = '" + Acc + "'";
                        SqlCommand cmd = new SqlCommand(query, Con);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("取款交易成功！\n取款金额：￥" + amount + "\n剩余额度：￥" + (remainingDailyLimit - amount) + "\n余额：￥" + newBalance);
                        Con.Close();
                        addtransaction(amount);
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

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            int amount = 10000;
            if (bal < amount)
            {
                MessageBox.Show("余额不足，当前余额：￥" + bal);
            }
            else if (!checkLimits(amount))
            {
                return;
            }
            else
            {
                int remainingDailyLimit = getRemainingDailyLimit();
                DialogResult result = MessageBox.Show(
                    "取款确认\n\n" +
                    "账号：" + Acc + "\n" +
                    "取款金额：￥" + amount + "\n" +
                    "当前余额：￥" + bal + "\n" +
                    "每日取款剩余额度：￥" + remainingDailyLimit + "\n\n" +
                    "请确认以上信息是否正确？",
                    "取款确认",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    int newBalance = bal - amount;
                    try
                    {
                        Con.Open();
                        string query = "update AccountTbl set Balance = " + newBalance + " where AccNum = '" + Acc + "'";
                        SqlCommand cmd = new SqlCommand(query, Con);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("取款交易成功！\n取款金额：￥" + amount + "\n剩余额度：￥" + (remainingDailyLimit - amount) + "\n余额：￥" + newBalance);
                        Con.Close();
                        addtransaction(amount);
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
    }
}