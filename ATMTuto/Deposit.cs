﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿using System;
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

        private const string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;
AttachDbFilename=C:\Users\24097\OneDrive\Documents\ATMDb.mdf;
Integrated Security=True;Connect Timeout=30";

        int oldBalance, newbalance, dailyDepositLimit, singleDepositLimit;
        string Acc = Login.AccNumber;
        private void addtransaction()
        {
            string TrType = "存款";
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    string query = "insert into TransactionTbl (AccNum, Type, Amount, TDate) values(@Acc, @TrType, @DepoAmt, @DateTime)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Acc", Acc);
                    cmd.Parameters.AddWithValue("@TrType", TrType);
                    cmd.Parameters.AddWithValue("@DepoAmt", AESHelper.EncryptAmount(int.Parse(DepoAmtTb.Text)));
                    cmd.Parameters.AddWithValue("@DateTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void getBalance()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                string query = "select Balance, DailyDepositLimit, SingleDepositLimit from AccountTbl where AccNum = @Acc";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                sda.SelectCommand.Parameters.AddWithValue("@Acc", Acc);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    oldBalance = AESHelper.DecryptAmount(dt.Rows[0][0].ToString());
                    dailyDepositLimit = dt.Rows[0][1] != DBNull.Value ? Convert.ToInt32(dt.Rows[0][1].ToString()) : 50000;
                    singleDepositLimit = dt.Rows[0][2] != DBNull.Value ? Convert.ToInt32(dt.Rows[0][2].ToString()) : 10000;
                }
                else
                {
                    oldBalance = 0;
                    dailyDepositLimit = 50000;
                    singleDepositLimit = 10000;
                }
            }
        }
        private int getDailyDepositAmount()
        {
            int dailyAmount = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                string query = "select Amount from TransactionTbl where AccNum = @Acc and Type = '存款' and convert(date, TDate) = convert(date, getdate())";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Acc", Acc);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string encryptedAmount = reader["Amount"].ToString();
                        dailyAmount += AESHelper.DecryptAmount(encryptedAmount);
                    }
                }
            }
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
                int depositAmount = Convert.ToInt32(DepoAmtTb.Text);
                int dailyDepositAmount = getDailyDepositAmount();
                int totalDeposit = dailyDepositAmount + depositAmount;
                if (dailyDepositLimit > 0 && totalDeposit > dailyDepositLimit)
                {
                    MessageBox.Show("超过每日存款限额￥" + dailyDepositLimit);
                    return;
                }

                var anomalyResult = AnomalyDetection.DetectTransactionAnomaly(Acc, "存款", depositAmount, DateTime.Now);
                AnomalyDetection.LogAnomaly(Acc, "存款", depositAmount, anomalyResult);
                
                if (anomalyResult.Level != AnomalyDetection.AnomalyLevel.Normal)
                {
                    string iconType = anomalyResult.Level >= AnomalyDetection.AnomalyLevel.Suspicious ? 
                        MessageBoxIcon.Exclamation.ToString() : MessageBoxIcon.Warning.ToString();
                    MessageBoxIcon icon = anomalyResult.Level >= AnomalyDetection.AnomalyLevel.Suspicious ? 
                        MessageBoxIcon.Exclamation : MessageBoxIcon.Warning;
                    
                    DialogResult confirmResult = MessageBox.Show(
                        $"【异常交易检测】\n\n检测级别: {anomalyResult.Level}\n\n{anomalyResult.Message}\n\n" +
                        "是否确认继续此存款操作？",
                        "异常交易提醒",
                        MessageBoxButtons.YesNo,
                        icon);
                    if (confirmResult == DialogResult.No)
                    {
                        return;
                    }
                }

                DialogResult result = MessageBox.Show(
                    "存款确认\n\n" +
                    "账号：" + Acc + "\n" +
                    "存款金额：￥" + depositAmount + "\n" +
                    "当前余额：￥" + oldBalance + "\n\n" +
                    "请确认以上信息是否正确？",
                    "存款确认",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                    {
                        newbalance = oldBalance + depositAmount;
                        try
                        {
                            using (SqlConnection con = new SqlConnection(ConnectionString))
                            {
                                con.Open();
                                string query = "update AccountTbl set Balance = @newbalance where AccNum = @Acc";
                                SqlCommand cmd = new SqlCommand(query, con);
                                cmd.Parameters.AddWithValue("@newbalance", AESHelper.EncryptAmount(newbalance));
                                cmd.Parameters.AddWithValue("@Acc", Acc);
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("存款交易成功！");
                            }
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
            int dailyDepositAmount = getDailyDepositAmount();
            int remainingDailyLimit = Math.Max(0, dailyDepositLimit - dailyDepositAmount);
            BalanceLbl.Text = "余额：￥" + oldBalance;
            DailyLimitLbl.Text = "今日剩余存款限额：￥" + remainingDailyLimit;
            SingleLimitLbl.Text = "单次存款限额：￥" + singleDepositLimit;
        }
    }
}