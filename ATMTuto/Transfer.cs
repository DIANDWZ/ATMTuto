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
                cmd.Parameters.AddWithValue("@Amt", AESHelper.EncryptAmount(amount));
                cmd.Parameters.AddWithValue("@DateTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.ExecuteNonQuery();

                if (type == "转账")
                {
                    query = "insert into TransactionTbl (AccNum, Type, Amount, TDate) values(@Acc2, @TrType2, @Amt2, @DateTime2)";
                    SqlCommand cmd2 = new SqlCommand(query, Con);
                    cmd2.Parameters.AddWithValue("@Acc2", recipientAcc);
                    cmd2.Parameters.AddWithValue("@TrType2", "转账收款");
                    cmd2.Parameters.AddWithValue("@Amt2", AESHelper.EncryptAmount(amount));
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
                oldBalance = AESHelper.DecryptAmount(dt.Rows[0][0].ToString());
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
            string query = "select Amount from TransactionTbl where AccNum = @Acc and (Type = '取款' or Type = '转账') and convert(date, TDate) = convert(date, getdate())";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.Parameters.AddWithValue("@Acc", Acc);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string encryptedAmount = reader["Amount"].ToString();
                dailyAmount += AESHelper.DecryptAmount(encryptedAmount);
            }
            reader.Close();
            Con.Close();
            return dailyAmount;
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
                MessageBox.Show("余额不足，当前余额为：" + oldBalance);
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
                if (dailyWithdrawLimit > 0 && totalWithdraw > dailyWithdrawLimit)
                {
                    MessageBox.Show("超过每日限额（取款+转账）限额￥" + dailyWithdrawLimit + "\n今日已取款/转账：￥" + dailyWithdrawAmount);
                    return;
                }

                var anomalyResult = AnomalyDetection.DetectTransactionAnomaly(Acc, "转账", transferAmount, DateTime.Now);
                AnomalyDetection.LogAnomaly(Acc, "转账", transferAmount, anomalyResult);
                
                if (anomalyResult.Level != AnomalyDetection.AnomalyLevel.Normal)
                {
                    MessageBoxIcon icon = anomalyResult.Level >= AnomalyDetection.AnomalyLevel.Suspicious ? 
                        MessageBoxIcon.Exclamation : MessageBoxIcon.Warning;
                    
                    DialogResult confirmResult = MessageBox.Show(
                        $"【异常交易检测】\n\n检测级别: {anomalyResult.Level}\n\n{anomalyResult.Message}\n\n" +
                        "是否确认继续此转账操作？",
                        "异常交易提醒",
                        MessageBoxButtons.YesNo,
                        icon);
                    if (confirmResult == DialogResult.No)
                    {
                        return;
                    }
                }

                DialogResult result = MessageBox.Show(
                    "转账确认\n\n" +
                    "付款账号：" + Acc + "\n" +
                    "收款账号：" + RecipientAccTb.Text + "\n" +
                    "转账金额：￥" + transferAmount + "\n" +
                    "当前余额：￥" + oldBalance + "\n\n" +
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
                        cmd.Parameters.AddWithValue("@newbalance", AESHelper.EncryptAmount(newBalance));
                        cmd.Parameters.AddWithValue("@Acc", Acc);
                        cmd.ExecuteNonQuery();

                        query = "select Balance from AccountTbl where AccNum = @RecipientAcc";
                        SqlCommand cmd2 = new SqlCommand(query, Con);
                        cmd2.Parameters.AddWithValue("@RecipientAcc", RecipientAccTb.Text);
                        string recipientEncryptedBalance = cmd2.ExecuteScalar().ToString();
                        int recipientBalance = AESHelper.DecryptAmount(recipientEncryptedBalance);
                        int newRecipientBalance = recipientBalance + transferAmount;

                        query = "update AccountTbl set Balance = @RecipientBalance where AccNum = @RecipientAcc";
                        SqlCommand cmd3 = new SqlCommand(query, Con);
                        cmd3.Parameters.AddWithValue("@RecipientBalance", AESHelper.EncryptAmount(newRecipientBalance));
                        cmd3.Parameters.AddWithValue("@RecipientAcc", RecipientAccTb.Text);
                        cmd3.ExecuteNonQuery();

                        Con.Close();

                        addtransaction("转账", transferAmount, RecipientAccTb.Text);

                        MessageBox.Show("转账成功！转账金额：" + transferAmount + "元");
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

        private void Label8_Click(object sender, EventArgs e)
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
            int dailyWithdrawAmount = getDailyWithdrawAmount();
            int remainingDailyLimit = Math.Max(0, dailyWithdrawLimit - dailyWithdrawAmount);
            BalanceLbl.Text = "余额：￥" + oldBalance;
            DailyLimitLbl.Text = "今日剩余取款限额：￥" + remainingDailyLimit;
            SingleLimitLbl.Text = "单次转账限额：￥" + singleWithdrawLimit;
        }
    }
}