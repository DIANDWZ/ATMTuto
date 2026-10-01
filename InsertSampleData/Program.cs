using System;
using System.Data.SqlClient;

namespace InsertSampleData
{
    class Program
    {
        static void Main(string[] args)
        {
            string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\24097\OneDrive\Documents\ATMDb.mdf;Integrated Security=True;Connect Timeout=30";
            
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    Console.WriteLine("数据库连接成功！");
                    
                    string[] samples = {
                        "INSERT INTO AnomalyLog (AccNum, TransactionType, Amount, AnomalyLevel, Message, DetectionRules, LogTime) VALUES ('2241301009', '存款', 111, 'Suspicious', '检测到可疑行为，请确认交易', '非工作时间交易（凌晨0-6点）; 短时间内频繁交易（间隔少于5分钟）', '2026-05-12 03:15:00')",
                        "INSERT INTO AnomalyLog (AccNum, TransactionType, Amount, AnomalyLevel, Message, DetectionRules, LogTime) VALUES ('2241301009', '存款', 111, 'Warning', '检测到轻微异常，请确认交易', '非工作时间交易（凌晨0-6点）', '2026-05-12 03:10:00')",
                        "INSERT INTO AnomalyLog (AccNum, TransactionType, Amount, AnomalyLevel, Message, DetectionRules, LogTime) VALUES ('2241301009', '取款', 1000, 'Warning', '检测到轻微异常，请确认交易', '非工作时间交易（凌晨0-6点）', '2026-05-12 03:05:00')",
                        "INSERT INTO AnomalyLog (AccNum, TransactionType, Amount, AnomalyLevel, Message, DetectionRules, LogTime) VALUES ('2241301010', '取款', 25000, 'Critical', '检测到严重异常！请联系客服核实', '交易金额异常（Z-score: 3.50，超过3倍标准差）; 非工作时间交易（凌晨0-6点）; 接近每日取款限额（已取: 0，本次: 25000，限额: 20000）', '2026-05-12 02:30:00')",
                        "INSERT INTO AnomalyLog (AccNum, TransactionType, Amount, AnomalyLevel, Message, DetectionRules, LogTime) VALUES ('2241301011', '转账', 18000, 'Warning', '检测到轻微异常，请确认交易', '接近每日取款限额（已取: 15000，本次: 18000，限额: 20000）', '2026-05-12 14:20:00')",
                        "INSERT INTO AnomalyLog (AccNum, TransactionType, Amount, AnomalyLevel, Message, DetectionRules, LogTime) VALUES ('2241301012', '存款', 60000, 'Warning', '检测到轻微异常，请确认交易', '大额交易提醒（金额: 60000）', '2026-05-12 10:45:00')",
                        "INSERT INTO AnomalyLog (AccNum, TransactionType, Amount, AnomalyLevel, Message, DetectionRules, LogTime) VALUES ('2241301013', '取款', 500, 'Warning', '检测到轻微异常，请确认交易', '30分钟内连续交易次数过多（5次以上）', '2026-05-12 16:30:00')",
                        "INSERT INTO AnomalyLog (AccNum, TransactionType, Amount, AnomalyLevel, Message, DetectionRules, LogTime) VALUES ('2241301014', '存款', 10000, 'Normal', '交易正常', '', '2026-05-12 09:00:00')",
                        "INSERT INTO AnomalyLog (AccNum, TransactionType, Amount, AnomalyLevel, Message, DetectionRules, LogTime) VALUES ('2241301015', '取款', 3000, 'Suspicious', '检测到可疑行为，请确认交易', '短时间内频繁交易（间隔少于5分钟）; 30分钟内连续交易次数过多（5次以上）', '2026-05-12 11:15:00')",
                        "INSERT INTO AnomalyLog (AccNum, TransactionType, Amount, AnomalyLevel, Message, DetectionRules, LogTime) VALUES ('2241301016', '转账', 5000, 'Warning', '检测到轻微异常，请确认交易', '短时间内频繁交易（间隔少于5分钟）', '2026-05-12 15:40:00')"
                    };

                    int count = 0;
                    foreach (string sql in samples)
                    {
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.ExecuteNonQuery();
                        count++;
                    }
                    
                    Console.WriteLine($"成功插入 {count} 条示例数据！");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("插入失败: " + ex.Message);
            }
            
            Console.WriteLine("按任意键退出...");
            Console.ReadKey();
        }
    }
}