using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ATMTuto
{
    internal static class Program
    {
        private static readonly string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\24097\OneDrive\Documents\ATMDb.mdf;Integrated Security=True;Connect Timeout=30";

        [STAThread]
        static void Main()
        {
            InitializeDatabaseSchema();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());
        }

        private static void InitializeDatabaseSchema()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    
                    UpdateColumnLength(conn, "AccountTbl", "PIN", "nvarchar", 100);
                    UpdateColumnLength(conn, "AccountTbl", "Phone", "nvarchar", 100);
                    UpdateColumnLength(conn, "AccountTbl", "Balance", "nvarchar", 100);
                    UpdateColumnLength(conn, "TransactionTbl", "Amount", "nvarchar", 100);
                    
                    CreateAnomalyLogTable(conn);
                }
            }
            catch
            {
            }
        }

        private static void CreateAnomalyLogTable(SqlConnection conn)
        {
            try
            {
                string createTableQuery = @"
                    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'AnomalyLog')
                    BEGIN
                        CREATE TABLE AnomalyLog (
                            LogId INT IDENTITY(1,1) PRIMARY KEY,
                            AccNum NVARCHAR(50) NOT NULL,
                            TransactionType NVARCHAR(20) NOT NULL,
                            Amount INT NOT NULL,
                            AnomalyLevel NVARCHAR(20) NOT NULL,
                            Message NVARCHAR(500) NOT NULL,
                            DetectionRules NVARCHAR(1000) NOT NULL,
                            LogTime DATETIME NOT NULL DEFAULT GETDATE()
                        )
                    END";
                using (SqlCommand cmd = new SqlCommand(createTableQuery, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
            }
        }

        private static void UpdateColumnLength(SqlConnection conn, string tableName, string columnName, string dataType, int maxLength)
        {
            try
            {
                string checkColumnQuery = @"
                    SELECT CHARACTER_MAXIMUM_LENGTH 
                    FROM INFORMATION_SCHEMA.COLUMNS 
                    WHERE TABLE_NAME = @TableName AND COLUMN_NAME = @ColumnName";
                
                using (SqlCommand cmd = new SqlCommand(checkColumnQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@TableName", tableName);
                    cmd.Parameters.AddWithValue("@ColumnName", columnName);
                    
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        int currentLength = Convert.ToInt32(result);
                        if (currentLength < maxLength)
                        {
                            string alterQuery = $"ALTER TABLE {tableName} ALTER COLUMN {columnName} {dataType}({maxLength}) NULL";
                            using (SqlCommand alterCmd = new SqlCommand(alterQuery, conn))
                            {
                                alterCmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }
    }
}