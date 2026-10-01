using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ATMTuto
{
    public partial class AnomalyLogViewer : Form
    {
        private const string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;
AttachDbFilename=C:\Users\24097\OneDrive\Documents\ATMDb.mdf;
Integrated Security=True;Connect Timeout=30";

        public AnomalyLogViewer()
        {
            InitializeComponent();
            PopulateLog();
        }

        private void PopulateLog()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    string query = "select * from AnomalyLog order by LogTime desc";
                    SqlDataAdapter sda = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    DataTable newDt = new DataTable();
                    newDt.Columns.Add("日志ID", typeof(int));
                    newDt.Columns.Add("账号", typeof(string));
                    newDt.Columns.Add("交易类型", typeof(string));
                    newDt.Columns.Add("金额", typeof(string));
                    newDt.Columns.Add("异常级别", typeof(string));
                    newDt.Columns.Add("异常描述", typeof(string));
                    newDt.Columns.Add("检测规则", typeof(string));
                    newDt.Columns.Add("记录时间", typeof(string));

                    foreach (DataRow row in dt.Rows)
                    {
                        string encryptedAmount = row[3].ToString();
                        string decryptedAmount = "";
                        try
                        {
                            decryptedAmount = AESHelper.DecryptAmount(encryptedAmount).ToString();
                        }
                        catch
                        {
                            decryptedAmount = encryptedAmount;
                        }

                        newDt.Rows.Add(
                            row[0],
                            row[1],
                            row[2],
                            decryptedAmount,
                            row[4],
                            row[5],
                            row[6],
                            row[7]
                        );
                    }

                    anomalyDGV.DataSource = newDt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载异常日志时出错: {ex.Message}");
            }
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    string query = "select * from AnomalyLog where 1=1 ";

                    if (!string.IsNullOrEmpty(AccNumTb.Text))
                    {
                        query += "and AccNum like @AccNum ";
                    }
                    if (!string.IsNullOrEmpty(AnomalyLevelCb.Text))
                    {
                        query += "and AnomalyLevel = @Level ";
                    }
                    query += "order by LogTime desc";

                    SqlCommand cmd = new SqlCommand(query, con);
                    if (!string.IsNullOrEmpty(AccNumTb.Text))
                    {
                        cmd.Parameters.AddWithValue("@AccNum", "%" + AccNumTb.Text + "%");
                    }
                    if (!string.IsNullOrEmpty(AnomalyLevelCb.Text))
                    {
                        cmd.Parameters.AddWithValue("@Level", AnomalyLevelCb.Text);
                    }

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    DataTable newDt = new DataTable();
                    newDt.Columns.Add("日志ID", typeof(int));
                    newDt.Columns.Add("账号", typeof(string));
                    newDt.Columns.Add("交易类型", typeof(string));
                    newDt.Columns.Add("金额", typeof(string));
                    newDt.Columns.Add("异常级别", typeof(string));
                    newDt.Columns.Add("异常描述", typeof(string));
                    newDt.Columns.Add("检测规则", typeof(string));
                    newDt.Columns.Add("记录时间", typeof(string));

                    foreach (DataRow row in dt.Rows)
                    {
                        string encryptedAmount = row[3].ToString();
                        string decryptedAmount = "";
                        try
                        {
                            decryptedAmount = AESHelper.DecryptAmount(encryptedAmount).ToString();
                        }
                        catch
                        {
                            decryptedAmount = encryptedAmount;
                        }

                        newDt.Rows.Add(
                            row[0],
                            row[1],
                            row[2],
                            decryptedAmount,
                            row[4],
                            row[5],
                            row[6],
                            row[7]
                        );
                    }

                    anomalyDGV.DataSource = newDt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"查询异常日志时出错: {ex.Message}");
            }
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            AccNumTb.Text = "";
            AnomalyLevelCb.Text = "";
            PopulateLog();
        }

        private void Label8_Click(object sender, EventArgs e)
        {
            AdminHome adminHome = new AdminHome();
            FormTransitionHelper.SwitchForm(this, adminHome);
        }
    }
}