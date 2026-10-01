using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace ATMTuto
{
    public static class AnomalyDetectionHelper
    {
        private static readonly string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\24097\OneDrive\Documents\ATMDb.mdf;Integrated Security=True;Connect Timeout=30";

        public enum AnomalyType
        {
            None,
            UnusualAmount,
            FrequentTransactions,
            LargeTransaction,
            UnusualTime,
            HighFrequency,
            CrossLimitAlert
        }

        public class AnomalyResult
        {
            public bool IsAnomalous { get; set; }
            public AnomalyType Type { get; set; }
            public string Message { get; set; }
            public double Score { get; set; }
        }

        public static AnomalyResult DetectAnomaly(string accountNumber, int transactionAmount, string transactionType)
        {
            List<AnomalyResult> results = new List<AnomalyResult>();

            var amountResult = DetectUnusualAmount(accountNumber, transactionAmount, transactionType);
            if (amountResult.IsAnomalous) results.Add(amountResult);

            var freqResult = DetectFrequentTransactions(accountNumber);
            if (freqResult.IsAnomalous) results.Add(freqResult);

            var timeResult = DetectUnusualTimeTransaction();
            if (timeResult.IsAnomalous) results.Add(timeResult);

            var highFreqResult = DetectHighFrequency(accountNumber);
            if (highFreqResult.IsAnomalous) results.Add(highFreqResult);

            if (results.Count == 0)
            {
                return new AnomalyResult { IsAnomalous = false, Type = AnomalyType.None, Message = "交易正常", Score = 0 };
            }

            var mostSevere = results.OrderByDescending(r => r.Score).First();
            return mostSevere;
        }

        public static AnomalyResult DetectUnusualAmount(string accountNumber, int transactionAmount, string transactionType)
        {
            List<int> historicalAmounts = GetHistoricalTransactionAmounts(accountNumber, transactionType);
            
            if (historicalAmounts.Count < 5)
            {
                return new AnomalyResult { IsAnomalous = false, Type = AnomalyType.None, Message = "交易记录不足，跳过检测", Score = 0 };
            }

            double mean = historicalAmounts.Average();
            double variance = historicalAmounts.Average(v => Math.Pow(v - mean, 2));
            double stdDev = Math.Sqrt(variance);

            if (stdDev == 0)
            {
                return new AnomalyResult { IsAnomalous = false, Type = AnomalyType.None, Message = "交易模式单一", Score = 0 };
            }

            double zScore = Math.Abs((transactionAmount - mean) / stdDev);

            if (zScore > 3)
            {
                return new AnomalyResult
                {
                    IsAnomalous = true,
                    Type = AnomalyType.UnusualAmount,
                    Message = $"异常交易金额检测：当前交易金额({transactionAmount})与历史平均金额({mean:F2})偏差超过3倍标准差(z-score={zScore:F2})",
                    Score = zScore
                };
            }

            if (transactionAmount > mean * 5)
            {
                return new AnomalyResult
                {
                    IsAnomalous = true,
                    Type = AnomalyType.LargeTransaction,
                    Message = $"大额交易提醒：当前交易金额({transactionAmount})是历史平均金额({mean:F2})的5倍以上",
                    Score = 4.5
                };
            }

            return new AnomalyResult { IsAnomalous = false, Type = AnomalyType.None, Message = "金额正常", Score = 0 };
        }

        public static AnomalyResult DetectFrequentTransactions(string accountNumber)
        {
            DateTime startTime = DateTime.Now.AddHours(-1);
            int recentTransactionCount = GetTransactionCountSince(accountNumber, startTime);

            if (recentTransactionCount >= 5)
            {
                return new AnomalyResult
                {
                    IsAnomalous = true,
                    Type = AnomalyType.FrequentTransactions,
                    Message = $"频繁交易检测：最近1小时内发生了{recentTransactionCount}笔交易，请确认是否为本人操作",
                    Score = 3.5
                };
            }

            return new AnomalyResult { IsAnomalous = false, Type = AnomalyType.None, Message = "交易频率正常", Score = 0 };
        }

        public static AnomalyResult DetectUnusualTimeTransaction()
        {
            int currentHour = DateTime.Now.Hour;

            if (currentHour >= 0 && currentHour < 6)
            {
                return new AnomalyResult
                {
                    IsAnomalous = true,
                    Type = AnomalyType.UnusualTime,
                    Message = $"非工作时间交易提醒：当前时间为{currentHour}:00，属于凌晨时段，请确认是否为本人操作",
                    Score = 3.0
                };
            }

            return new AnomalyResult { IsAnomalous = false, Type = AnomalyType.None, Message = "交易时间正常", Score = 0 };
        }

        public static AnomalyResult DetectHighFrequency(string accountNumber)
        {
            DateTime startTime = DateTime.Now.AddMinutes(-5);
            int recentTransactionCount = GetTransactionCountSince(accountNumber, startTime);

            if (recentTransactionCount >= 3)
            {
                return new AnomalyResult
                {
                    IsAnomalous = true,
                    Type = AnomalyType.HighFrequency,
                    Message = $"高频交易检测：最近5分钟内发生了{recentTransactionCount}笔交易，疑似异常操作",
                    Score = 4.0
                };
            }

            return new AnomalyResult { IsAnomalous = false, Type = AnomalyType.None, Message = "交易频率正常", Score = 0 };
        }

        private static List<int> GetHistoricalTransactionAmounts(string accountNumber, string transactionType)
        {
            List<int> amounts = new List<int>();

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT TOP 20 Amount FROM TransactionTbl WHERE AccNum = @AccNum AND Type = @Type ORDER BY TDate DESC";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@AccNum", accountNumber);
                    cmd.Parameters.AddWithValue("@Type", transactionType);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string encryptedAmount = reader["Amount"].ToString();
                            int amount = AESHelper.DecryptAmount(encryptedAmount);
                            amounts.Add(amount);
                        }
                    }
                }
            }
            catch
            {
            }

            return amounts;
        }

        private static int GetTransactionCountSince(string accountNumber, DateTime startTime)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM TransactionTbl WHERE AccNum = @AccNum AND TDate >= @StartTime";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@AccNum", accountNumber);
                    cmd.Parameters.AddWithValue("@StartTime", startTime);

                    return (int)cmd.ExecuteScalar();
                }
            }
            catch
            {
                return 0;
            }
        }
    }
}