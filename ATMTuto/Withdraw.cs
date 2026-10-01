﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿using System;
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
        int bal, newbalance, dailyWithdrawLimit, singleWithdrawLimit;
        private void addtransaction()
        {
            string TrType = "取款";
            try
            {
                Con.Open();
                string query = "insert into TransactionTbl (AccNum, Type, Amount, TDate) values(@Acc, @TrType, @WdAmt, @DateTime)";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.Parameters.AddWithValue("@Acc", Acc);
                cmd.Parameters.AddWithValue("@TrType", TrType);
                cmd.Parameters.AddWithValue("@WdAmt", AESHelper.EncryptAmount(int.Parse(WdAmtTb.Text)));
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
            string query = "select Balance, DailyWithdrawLimit, SingleWithdrawLimit from AccountTbl where AccNum = @Acc";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            sda.SelectCommand.Parameters.AddWithValue("@Acc", Acc);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                string encryptedBalance = dt.Rows[0][0].ToString();
                bal = AESHelper.DecryptAmount(encryptedBalance);
                balancelbl.Text = "￥" + bal;
                dailyWithdrawLimit = dt.Rows[0][1] != DBNull.Value ? Convert.ToInt32(dt.Rows[0][1].ToString()) : 20000;
                singleWithdrawLimit = dt.Rows[0][2] != DBNull.Value ? Convert.ToInt32(dt.Rows[0][2].ToString()) : 10000;
            }
            else
            {
                bal = 0;
                balancelbl.Text = "￥0";
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

        private void WdAmtTb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
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
            else if (singleWithdrawLimit > 0 && Convert.ToInt32(WdAmtTb.Text) > singleWithdrawLimit)
            {
                MessageBox.Show("单次取款不可超过￥" + singleWithdrawLimit);
            }
            else if (Convert.ToInt32(WdAmtTb.Text) > bal)
            {
                MessageBox.Show("余额不足");
            }
            else
            {
                int withdrawAmount = Convert.ToInt32(WdAmtTb.Text);
                int dailyWithdrawAmount = getDailyWithdrawAmount();
                int totalWithdraw = dailyWithdrawAmount + withdrawAmount;
                if (dailyWithdrawLimit > 0 && totalWithdraw > dailyWithdrawLimit)
                {
                    MessageBox.Show("超过每日限额（取款+转账）限额￥" + dailyWithdrawLimit + "\n今日已取款/转账：￥" + dailyWithdrawAmount);
                    return;
                }

                var anomalyResult = AnomalyDetection.DetectTransactionAnomaly(Acc, "取款", withdrawAmount, DateTime.Now);
                AnomalyDetection.LogAnomaly(Acc, "取款", withdrawAmount, anomalyResult);
                
                if (anomalyResult.Level != AnomalyDetection.AnomalyLevel.Normal)
                {
                    MessageBoxIcon icon = anomalyResult.Level >= AnomalyDetection.AnomalyLevel.Suspicious ? 
                        MessageBoxIcon.Exclamation : MessageBoxIcon.Warning;
                    
                    DialogResult confirmResult = MessageBox.Show(
                        $"【异常交易检测】\n\n检测级别: {anomalyResult.Level}\n\n{anomalyResult.Message}\n\n" +
                        "是否确认继续此取款操作？",
                        "异常交易提醒",
                        MessageBoxButtons.YesNo,
                        icon);
                    if (confirmResult == DialogResult.No)
                    {
                        return;
                    }
                }

                DialogResult result = MessageBox.Show(
                    "取款确认\n\n" +
                    "账号：" + Acc + "\n" +
                    "取款金额：￥" + withdrawAmount + "\n" +
                    "当前余额：￥" + bal + "\n\n" +
                    "请确认以上信息是否正确？",
                    "取款确认",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    newbalance = bal - withdrawAmount;
                    try
                    {
                        Con.Open();
                        string query = "update AccountTbl set Balance = @newbalance where AccNum = @Acc";
                        SqlCommand cmd = new SqlCommand(query, Con);
                        cmd.Parameters.AddWithValue("@newbalance", AESHelper.EncryptAmount(newbalance));
                        cmd.Parameters.AddWithValue("@Acc", Acc);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("取款交易成功！");
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

        private void Label8_Click(object sender, EventArgs e)
        {
            HOME home = new HOME();
            FormTransitionHelper.SwitchForm(this, home);
        }

        private void Withdraw_Load(object sender, EventArgs e)
        {
            getBalance();
            int dailyWithdrawAmount = getDailyWithdrawAmount();
            int remainingDailyLimit = Math.Max(0, dailyWithdrawLimit - dailyWithdrawAmount);
            DailyLimitLbl.Text = "今日剩余取款限额：￥" + remainingDailyLimit;
            SingleLimitLbl.Text = "单次取款限额：￥" + singleWithdrawLimit;
        }
    }
}