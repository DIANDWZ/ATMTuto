using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace ATMTuto
{
    public static class AnomalyDetection
    {
        private static readonly string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\24097\OneDrive\Documents\ATMDb.mdf;Integrated Security=True;Connect Timeout=30";

        public enum AnomalyLevel
        {
            Normal,
            Warning,
            Suspicious,
            Critical
        }

        public class AnomalyResult
        {
            public AnomalyLevel Level { get; set; }
            public string Message { get; set; }
            public List<string> DetectionRules { get; set; } = new List<string>();
        }

        public static AnomalyResult DetectTransactionAnomaly(string accNum, string transactionType, int amount, DateTime transactionTime)
        {
            AnomalyResult result = new AnomalyResult { Level = AnomalyLevel.Normal };

            List<string> triggeredRules = new List<string>();

            if (CheckAmountAnomaly(accNum, amount, transactionType, out string amountRule))
                triggeredRules.Add(amountRule);

            if (CheckFrequencyAnomaly(accNum, transactionType, out string freqRule))
                triggeredRules.Add(freqRule);

            if (CheckTimeAnomaly(transactionTime, out string timeRule))
                triggeredRules.Add(timeRule);

            if (CheckLocationAnomaly(accNum, out string locationRule))
                triggeredRules.Add(locationRule);

            if (CheckDailyLimitExceed(accNum, amount, transactionType, out string limitRule))
                triggeredRules.Add(limitRule);

            result.DetectionRules = triggeredRules;

            if (triggeredRules.Count == 0)
            {
                result.Level = AnomalyLevel.Normal;
                result.Message = "交易正常";
            }
            else if (triggeredRules.Count == 1)
            {
                result.Level = AnomalyLevel.Warning;
                result.Message = "检测到轻微异常，请确认交易";
            }
            else if (triggeredRules.Count == 2)
            {
                result.Level = AnomalyLevel.Suspicious;
                result.Message = "检测到可疑行为，请确认交易";
            }
            else
            {
                result.Level = AnomalyLevel.Critical;
                result.Message = "检测到严重异常！请联系客服核实";
            }

            return result;
        }

        private static bool CheckAmountAnomaly(string accNum, int amount, string transactionType, out string rule)
        {
            rule = string.Empty;

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string query = "select Amount from TransactionTbl where AccNum = @AccNum and Type = @Type order by TDate desc";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@AccNum", accNum);
                    cmd.Parameters.AddWithValue("@Type", transactionType);

                    List<int> historicalAmounts = new List<int>();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int amt = AESHelper.DecryptAmount(reader["Amount"].ToString());
                        if (amt > 0) historicalAmounts.Add(amt);
                    }
                    reader.Close();

                    if (historicalAmounts.Count >= 5)
                    {
                        double mean = historicalAmounts.Average();
                        double variance = historicalAmounts.Average(v => Math.Pow(v - mean, 2));
                        double stdDev = Math.Sqrt(variance);

                        double zScore = stdDev > 0 ? Math.Abs((amount - mean) / stdDev) : 0;

                        if (zScore > 3)
                        {
                            rule = $"交易金额异常（Z-score: {zScore:F2}，超过3倍标准差）";
                            return true;
                        }

                        double maxHistorical = historicalAmounts.Max();
                        if (amount > maxHistorical * 2)
                        {
                            rule = $"交易金额超过历史最大交易的2倍（当前: {amount}，历史最大: {maxHistorical}）";
                            return true;
                        }
                    }
                    else if (amount > 50000)
                    {
                        rule = $"大额交易提醒（金额: {amount}）";
                        return true;
                    }
                }
            }
            catch
            {
            }

            return false;
        }

        private static bool CheckFrequencyAnomaly(string accNum, string transactionType, out string rule)
        {
            rule = string.Empty;

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string query = @"
                        select count(*) as Count, min(TDate) as FirstTime, max(TDate) as LastTime
                        from TransactionTbl
                        where AccNum = @AccNum and Type = @Type and TDate >= dateadd(minute, -30, getdate())";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@AccNum", accNum);
                    cmd.Parameters.AddWithValue("@Type", transactionType);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        int count = reader["Count"] != DBNull.Value ? Convert.ToInt32(reader["Count"]) : 0;

                        if (count >= 5)
                        {
                            rule = "30分钟内连续交易次数过多（5次以上）";
                            return true;
                        }
                    }
                    reader.Close();
                }
            }
            catch
            {
            }

            return false;
        }

        private static bool CheckTimeAnomaly(DateTime transactionTime, out string rule)
        {
            rule = string.Empty;

            int hour = transactionTime.Hour;

            if (hour >= 0 && hour < 6)
            {
                rule = "非工作时间交易（凌晨0-6点）";
                return true;
            }

            return false;
        }

        private static bool CheckLocationAnomaly(string accNum, out string rule)
        {
            rule = string.Empty;

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string query = @"
                        select top 3 TDate from TransactionTbl
                        where AccNum = @AccNum
                        order by TDate desc";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@AccNum", accNum);

                    List<DateTime> recentTimes = new List<DateTime>();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        DateTime dt = Convert.ToDateTime(reader["TDate"]);
                        recentTimes.Add(dt);
                    }
                    reader.Close();

                    if (recentTimes.Count >= 2)
                    {
                        TimeSpan diff = recentTimes[0] - recentTimes[1];
                        if (diff.TotalMinutes < 5)
                        {
                            rule = "短时间内频繁交易（间隔少于5分钟）";
                            return true;
                        }
                    }
                }
            }
            catch
            {
            }

            return false;
        }

        private static bool CheckDailyLimitExceed(string accNum, int amount, string transactionType, out string rule)
        {
            rule = string.Empty;

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string query = @"
                        select sum(convert(int, Amount)) as DailyTotal
                        from TransactionTbl
                        where AccNum = @AccNum and Type = @Type and convert(date, TDate) = convert(date, getdate())";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@AccNum", accNum);
                    cmd.Parameters.AddWithValue("@Type", transactionType);

                    object result = cmd.ExecuteScalar();
                    int dailyTotal = result != DBNull.Value ? Convert.ToInt32(result) : 0;

                    if (transactionType == "取款" || transactionType == "转账")
                    {
                        if (dailyTotal + amount > 20000)
                        {
                            rule = $"接近每日取款限额（已取: {dailyTotal}，本次: {amount}，限额: 20000）";
                            return true;
                        }
                    }
                    else if (transactionType == "存款")
                    {
                        if (dailyTotal + amount > 50000)
                        {
                            rule = $"接近每日存款限额（已存: {dailyTotal}，本次: {amount}，限额: 50000）";
                            return true;
                        }
                    }
                }
            }
            catch
            {
            }

            return false;
        }

        public static void LogAnomaly(string accNum, string transactionType, int amount, AnomalyResult result)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string query = @"
                        insert into AnomalyLog (AccNum, TransactionType, Amount, AnomalyLevel, Message, DetectionRules, LogTime)
                        values (@AccNum, @Type, @Amount, @Level, @Message, @Rules, @LogTime)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@AccNum", accNum);
                    cmd.Parameters.AddWithValue("@Type", transactionType);
                    cmd.Parameters.AddWithValue("@Amount", AESHelper.EncryptAmount(amount));
                    cmd.Parameters.AddWithValue("@Level", result.Level.ToString());
                    cmd.Parameters.AddWithValue("@Message", result.Message);
                    cmd.Parameters.AddWithValue("@Rules", string.Join("; ", result.DetectionRules));
                    cmd.Parameters.AddWithValue("@LogTime", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
            }
        }

        public static void InsertSampleAnomalyLogs()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (SqlCommand deleteCmd = new SqlCommand("DELETE FROM AnomalyLog", conn))
                    {
                        deleteCmd.ExecuteNonQuery();
                    }

                    var samples = new List<object[]>
                    {
                        new object[] { "2241301001", "取款", 25000, "Critical", "检测到严重异常！请联系客服核实", "交易金额异常（Z-score: 3.50，超过3倍标准差）; 非工作时间交易（凌晨0-6点）; 接近每日取款限额（已取: 0，本次: 25000，限额: 20000）", "2026-05-10 02:30:00" },
                        new object[] { "2241301002", "存款", 60000, "Critical", "检测到严重异常！请联系客服核实", "大额交易提醒（金额: 60000）; 非工作时间交易（凌晨0-6点）; 接近每日存款限额（已存: 0，本次: 60000，限额: 50000）", "2026-05-10 03:15:00" },
                        new object[] { "2241301003", "转账", 18000, "Suspicious", "检测到可疑行为，请确认交易", "接近每日取款限额（已取: 15000，本次: 18000，限额: 20000）; 30分钟内连续交易次数过多（5次以上）", "2026-05-10 14:20:00" },
                        new object[] { "2241301004", "取款", 3000, "Suspicious", "检测到可疑行为，请确认交易", "短时间内频繁交易（间隔少于5分钟）; 30分钟内连续交易次数过多（5次以上）", "2026-05-10 11:15:00" },
                        new object[] { "2241301005", "存款", 111, "Suspicious", "检测到可疑行为，请确认交易", "非工作时间交易（凌晨0-6点）; 短时间内频繁交易（间隔少于5分钟）", "2026-05-10 03:15:00" },
                        new object[] { "2241301006", "取款", 1000, "Warning", "检测到轻微异常，请确认交易", "非工作时间交易（凌晨0-6点）", "2026-05-10 03:05:00" },
                        new object[] { "2241301007", "存款", 60000, "Warning", "检测到轻微异常，请确认交易", "大额交易提醒（金额: 60000）", "2026-05-10 10:45:00" },
                        new object[] { "2241301008", "取款", 500, "Warning", "检测到轻微异常，请确认交易", "30分钟内连续交易次数过多（5次以上）", "2026-05-10 16:30:00" },
                        new object[] { "2241301009", "转账", 5000, "Warning", "检测到轻微异常，请确认交易", "短时间内频繁交易（间隔少于5分钟）", "2026-05-10 15:40:00" },
                        new object[] { "2241301010", "取款", 18000, "Warning", "检测到轻微异常，请确认交易", "接近每日取款限额（已取: 15000，本次: 18000，限额: 20000）", "2026-05-10 14:20:00" },
                        new object[] { "2241301011", "取款", 111, "Warning", "检测到轻微异常，请确认交易", "非工作时间交易（凌晨0-6点）", "2026-05-10 02:10:00" },
                        new object[] { "2241301012", "转账", 22000, "Critical", "检测到严重异常！请联系客服核实", "接近每日取款限额（已取: 18000，本次: 22000，限额: 20000）; 非工作时间交易（凌晨0-6点）; 短时间内频繁交易（间隔少于5分钟）", "2026-05-11 01:20:00" },
                        new object[] { "2241301013", "存款", 45000, "Suspicious", "检测到可疑行为，请确认交易", "大额交易提醒（金额: 45000）; 接近每日存款限额（已存: 10000，本次: 45000，限额: 50000）", "2026-05-11 15:30:00" },
                        new object[] { "2241301014", "取款", 8000, "Warning", "检测到轻微异常，请确认交易", "30分钟内连续交易次数过多（5次以上）", "2026-05-11 16:45:00" },
                        new object[] { "2241301015", "转账", 3000, "Suspicious", "检测到可疑行为，请确认交易", "非工作时间交易（凌晨0-6点）; 30分钟内连续交易次数过多（5次以上）", "2026-05-11 04:20:00" }
                    };

                    foreach (var row in samples)
                    {
                        using (SqlCommand cmd = new SqlCommand(
                            "INSERT INTO AnomalyLog (AccNum, TransactionType, Amount, AnomalyLevel, Message, DetectionRules, LogTime) VALUES (@AccNum, @Type, @Amount, @Level, @Msg, @Rules, @Time)", conn))
                        {
                            cmd.Parameters.AddWithValue("@AccNum", row[0]);
                            cmd.Parameters.AddWithValue("@Type", row[1]);
                            cmd.Parameters.AddWithValue("@Amount", AESHelper.EncryptAmount(Convert.ToInt32(row[2])));
                            cmd.Parameters.AddWithValue("@Level", row[3]);
                            cmd.Parameters.AddWithValue("@Msg", row[4]);
                            cmd.Parameters.AddWithValue("@Rules", row[5]);
                            cmd.Parameters.AddWithValue("@Time", row[6]);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("插入示例数据失败: " + ex.Message);
            }
        }
    }
}