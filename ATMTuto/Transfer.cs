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
    public partial class Transfer : Form
    {
        public Transfer()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;
AttachDbFilename=C:\Users\24097\OneDrive\Documents\ATMDb.mdf;
Integrated Security=True;Connect Timeout=30");

        int oldBalance, dailyWithdrawLimit, singleWithdrawLimit;
        string Acc = Login.AccNumber;

        private void addtransaction(string type, int amount, string recipientAcc)
        {
            try
            {
                Con.Open();
                string query = "insert into TransactionTbl (AccNum, Type, Amount, TDate) values(@Acc, @TrType, @Amt, @DateTime)";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.Parameters.AddWithValue("@Acc", Acc);
                cmd.Parameters.AddWithValue("@TrType", type);
                cmd.Parameters.AddWithValue("@Amt", amount);
                cmd.Parameters.AddWithValue("@DateTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.ExecuteNonQuery();

                if (type == "转账")
                {
                    query = "insert into TransactionTbl (AccNum, Type, Amount, TDate) values(@Acc2, @TrType2, @Amt2, @DateTime2)";
                    SqlCommand cmd2 = new SqlCommand(query, Con);
                    cmd2.Parameters.AddWithValue("@Acc2", recipientAcc);
                    cmd2.Parameters.AddWithValue("@TrType2", "转账收款");
                    cmd2.Parameters.AddWithValue("@Amt2", amount);
                    cmd2.Parameters.AddWithValue("@DateTime2", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd2.ExecuteNonQuery();
                }

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
            string query = "select Balance, DailyWithdrawLimit, SingleWithdrawLimit from AccountTbl where AccNum = @Acc";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            sda.SelectCommand.Parameters.AddWithValue("@Acc", Acc);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                oldBalance = Convert.ToInt32(dt.Rows[0][0].ToString());
                dailyWithdrawLimit = dt.Rows[0][1] != DBNull.Value ? Convert.ToInt32(dt.Rows[0][1].ToString()) : 20000;
                singleWithdrawLimit = dt.Rows[0][2] != DBNull.Value ? Convert.ToInt32(dt.Rows[0][2].ToString()) : 10000;
            }
            else
            {
                oldBalance = 0;
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

        private bool recipientExists(string recipientAcc)
        {
            Con.Open();
            string query = "select count(*) from AccountTbl where AccNum = @RecipientAcc";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.Parameters.AddWithValue("@RecipientAcc", recipientAcc);
            int count = (int)cmd.ExecuteScalar();
            Con.Close();
            return count > 0;
        }

        private void TransferBtn_Click(object sender, EventArgs e)
        {
            if (RecipientAccTb.Text == "" || TransferAmtTb.Text == "")
            {
                MessageBox.Show("请输入完整信息");
            }
            else if (RecipientAccTb.Text == Acc)
            {
                MessageBox.Show("不能向自己的账户转账");
            }
            else if (!recipientExists(RecipientAccTb.Text))
            {
                MessageBox.Show("收款账户不存在");
            }
            else if (Convert.ToInt32(TransferAmtTb.Text) <= 0)
            {
                MessageBox.Show("请输入有效金额");
            }
            else if (Convert.ToInt32(TransferAmtTb.Text) > oldBalance)
            {
                MessageBox.Show("余额不足，当前余额：￥" + oldBalance);
            }
            else if (singleWithdrawLimit > 0 && Convert.ToInt32(TransferAmtTb.Text) > singleWithdrawLimit)
            {
                MessageBox.Show("单次转账不可超过￥" + singleWithdrawLimit);
            }
            else
            {
                int transferAmount = Convert.ToInt32(TransferAmtTb.Text);
                int dailyWithdrawAmount = getDailyWithdrawAmount();
                int totalWithdraw = dailyWithdrawAmount + transferAmount;
                int remainingDailyLimit = dailyWithdrawLimit - dailyWithdrawAmount;
                if (dailyWithdrawLimit > 0 && totalWithdraw > dailyWithdrawLimit)
                {
                    MessageBox.Show("超过每日限额（取款+转账）\n每日限额：￥" + dailyWithdrawLimit + "\n今日已取款/转账：￥" + dailyWithdrawAmount + "\n每日剩余额度：￥" + remainingDailyLimit);
                    return;
                }
                DialogResult result = MessageBox.Show(
                    "转账确认\n\n" +
                    "付款账号：" + Acc + "\n" +
                    "收款账号：" + RecipientAccTb.Text + "\n" +
                    "转账金额：￥" + transferAmount + "\n" +
                    "当前余额：￥" + oldBalance + "\n" +
                    "每日取款剩余额度：￥" + remainingDailyLimit + "\n\n" +
                    "请确认以上信息是否正确？",
                    "转账确认",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    int newBalance = oldBalance - transferAmount;
                    try
                    {
                        Con.Open();
                        string query = "update AccountTbl set Balance = @newbalance where AccNum = @Acc";
                        SqlCommand cmd = new SqlCommand(query, Con);
                        cmd.Parameters.AddWithValue("@newbalance", newBalance);
                        cmd.Parameters.AddWithValue("@Acc", Acc);
                        cmd.ExecuteNonQuery();

                        query = "update AccountTbl set Balance = Balance + @transferAmount where AccNum = @RecipientAcc";
                        SqlCommand cmd2 = new SqlCommand(query, Con);
                        cmd2.Parameters.AddWithValue("@transferAmount", transferAmount);
                        cmd2.Parameters.AddWithValue("@RecipientAcc", RecipientAccTb.Text);
                        cmd2.ExecuteNonQuery();

                        Con.Close();

                        addtransaction("转账", transferAmount, RecipientAccTb.Text);

                        MessageBox.Show("转账成功！\n转账金额：￥" + transferAmount + "\n剩余额度：￥" + (remainingDailyLimit - transferAmount) + "\n余额：￥" + newBalance);
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

        private void TransferAmtTb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void Transfer_Load(object sender, EventArgs e)
        {
            getBalance();
            label7.Text = Acc;
        }
    }
}