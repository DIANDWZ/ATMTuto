using System;
using System.Data.SqlClient;

namespace InsertData
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("正在连接数据库...");
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\24097\OneDrive\Documents\ATMDb.mdf;Integrated Security=True;Connect Timeout=30";
            
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                Console.WriteLine("数据库连接成功！");

                Console.WriteLine("正在清空 AnomalyLog 表...");
                using (SqlCommand cmd = new SqlCommand("DELETE FROM AnomalyLog", conn))
                {
                    cmd.ExecuteNonQuery();
                }

                Console.WriteLine("正在插入示例数据...");

                object[][] data = new object[][]
                {
                    new object[] { "2241301009", "存款", 111, "Suspicious", "检测到可疑行为，请确认交易", "非工作时间交易（凌晨0-6点）; 短时间内频繁交易（间隔少于5分钟）", "2026-05-12 03:15:00" },
                    new object[] { "2241301009", "存款", 111, "Warning", "检测到轻微异常，请确认交易", "非工作时间交易（凌晨0-6点）", "2026-05-12 03:10:00" },
                    new object[] { "2241301009", "取款", 1000, "Warning", "检测到轻微异常，请确认交易", "非工作时间交易（凌晨0-6点）", "2026-05-12 03:05:00" },
                    new object[] { "2241301010", "取款", 25000, "Critical", "检测到严重异常！请联系客服核实", "交易金额异常（Z-score: 3.50，超过3倍标准差）; 非工作时间交易（凌晨0-6点）; 接近每日取款限额（已取: 0，本次: 25000，限额: 20000）", "2026-05-12 02:30:00" },
                    new object[] { "2241301011", "转账", 18000, "Warning", "检测到轻微异常，请确认交易", "接近每日取款限额（已取: 15000，本次: 18000，限额: 20000）", "2026-05-12 14:20:00" },
                    new object[] { "2241301012", "存款", 60000, "Warning", "检测到轻微异常，请确认交易", "大额交易提醒（金额: 60000）", "2026-05-12 10:45:00" },
                    new object[] { "2241301013", "取款", 500, "Warning", "检测到轻微异常，请确认交易", "30分钟内连续交易次数过多（5次以上）", "2026-05-12 16:30:00" },
                    new object[] { "2241301014", "存款", 10000, "Normal", "交易正常", "", "2026-05-12 09:00:00" },
                    new object[] { "2241301015", "取款", 3000, "Suspicious", "检测到可疑行为，请确认交易", "短时间内频繁交易（间隔少于5分钟）; 30分钟内连续交易次数过多（5次以上）", "2026-05-12 11:15:00" },
                    new object[] { "2241301016", "转账", 5000, "Warning", "检测到轻微异常，请确认交易", "短时间内频繁交易（间隔少于5分钟）", "2026-05-12 15:40:00" }
                };

                int count = 0;
                foreach (object[] row in data)
                {
                    using (SqlCommand cmd = new SqlCommand(
                        "INSERT INTO AnomalyLog (AccNum, TransactionType, Amount, AnomalyLevel, Message, DetectionRules, LogTime) VALUES (@AccNum, @Type, @Amount, @Level, @Msg, @Rules, @Time)", conn))
                    {
                        cmd.Parameters.AddWithValue("@AccNum", row[0]);
                        cmd.Parameters.AddWithValue("@Type", row[1]);
                        cmd.Parameters.AddWithValue("@Amount", row[2]);
                        cmd.Parameters.AddWithValue("@Level", row[3]);
                        cmd.Parameters.AddWithValue("@Msg", row[4]);
                        cmd.Parameters.AddWithValue("@Rules", row[5]);
                        cmd.Parameters.AddWithValue("@Time", row[6]);
                        cmd.ExecuteNonQuery();
                    }
                    count++;
                    Console.Write($"\r已插入 {count} 条数据...");
                }

                Console.WriteLine($"\n\n成功插入 {count} 条示例数据！");

                Console.WriteLine("\n验证数据:");
                using (SqlCommand cmd = new SqlCommand("SELECT LogId, AccNum, TransactionType, Message FROM AnomalyLog ORDER BY LogId", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"  {reader["LogId"]}: {reader["AccNum"]} - {reader["TransactionType"]} - {reader["Message"]}");
                        }
                    }
                }
            }

            Console.WriteLine("\n完成！按任意键退出...");
            Console.ReadKey();
        }
    }
}