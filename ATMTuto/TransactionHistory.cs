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
    public partial class TransactionHistory : Form
    {
        public TransactionHistory()
        {
            InitializeComponent();
            Populate();
        }

        private void Populate()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;
AttachDbFilename=C:\Users\24097\OneDrive\Documents\ATMDb.mdf;
Integrated Security=True;Connect Timeout=30"))
                {
                    con.Open();
                    string query = "select * from TransactionTbl order by TDate desc";
                    SqlDataAdapter sda = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    
                    DataTable newDt = new DataTable();
                    newDt.Columns.Add("交易ID", typeof(int));
                    newDt.Columns.Add("账号", typeof(string));
                    newDt.Columns.Add("交易类型", typeof(string));
                    newDt.Columns.Add("金额", typeof(string));
                    newDt.Columns.Add("交易时间", typeof(string));
                    
                    foreach (DataRow row in dt.Rows)
                    {
                        int amount = AESHelper.DecryptAmount(row[3].ToString());
                        newDt.Rows.Add(
                            row[0],
                            row[1],
                            row[2],
                            amount,
                            row[4]
                        );
                    }
                    
                    transDGV.DataSource = newDt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载交易记录时出错: {ex.Message}");
            }
        }

        private void Guna2Button1_Click(object sender, EventArgs e)
        {
            if (AccNumTb.Text == "")
            {
                Populate();
            }
            else
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;
AttachDbFilename=C:\Users\24097\OneDrive\Documents\ATMDb.mdf;
Integrated Security=True;Connect Timeout=30"))
                    {
                        con.Open();
                        string query = "select * from TransactionTbl where AccNum = @AccNum order by TDate desc";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@AccNum", AccNumTb.Text);
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        
                        DataTable newDt = new DataTable();
                        newDt.Columns.Add("交易ID", typeof(int));
                        newDt.Columns.Add("账号", typeof(string));
                        newDt.Columns.Add("交易类型", typeof(string));
                        newDt.Columns.Add("金额", typeof(string));
                        newDt.Columns.Add("交易时间", typeof(string));
                        
                        foreach (DataRow row in dt.Rows)
                        {
                            int amount = AESHelper.DecryptAmount(row[3].ToString());
                            newDt.Rows.Add(
                                row[0],
                                row[1],
                                row[2],
                                amount,
                                row[4]
                            );
                        }
                        
                        transDGV.DataSource = newDt;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"查询交易记录时出错: {ex.Message}");
                }
            }
        }

        private void Label8_Click(object sender, EventArgs e)
        {
            AdminHome adminHome = new AdminHome();
            FormTransitionHelper.SwitchForm(this, adminHome);
        }
    }
}