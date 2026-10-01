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
    public partial class LimitManagement : Form
    {
        public LimitManagement()
        {
            InitializeComponent();
            Populate();
        }

        private const string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;
AttachDbFilename=C:\Users\24097\OneDrive\Documents\ATMDb.mdf;
Integrated Security=True;Connect Timeout=30";

        private void Populate()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    string query = "select * from AccountTbl";
                    SqlDataAdapter sda = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    
                    DataTable filteredDt = new DataTable();
                    filteredDt.Columns.Add("账号", typeof(string));
                    filteredDt.Columns.Add("姓", typeof(string));
                    filteredDt.Columns.Add("名", typeof(string));
                    filteredDt.Columns.Add("单日取款限额", typeof(decimal));
                    filteredDt.Columns.Add("单日存款限额", typeof(decimal));
                    filteredDt.Columns.Add("单次取款限额", typeof(decimal));
                    filteredDt.Columns.Add("单次存款限额", typeof(decimal));
                    
                    foreach (DataRow row in dt.Rows)
                    {
                        filteredDt.Rows.Add(
                            row[0],
                            row[2],
                            row[1],
                            row[10],
                            row[11],
                            row[12],
                            row[13]
                        );
                    }
                    
                    limitDGV.DataSource = filteredDt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载限额数据时出错: {ex.Message}");
            }
        }

        private void Guna2Button1_Click(object sender, EventArgs e)
        {
            if (AccNumTb.Text == "")
            {
                MessageBox.Show("请选择要编辑的用户");
            }
            else
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(ConnectionString))
                    {
                        con.Open();
                        string query = "update AccountTbl set DailyWithdrawLimit=@DailyWithdrawLimit, DailyDepositLimit=@DailyDepositLimit, SingleWithdrawLimit=@SingleWithdrawLimit, SingleDepositLimit=@SingleDepositLimit where AccNum=@AccNum";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@AccNum", AccNumTb.Text);
                        cmd.Parameters.AddWithValue("@DailyWithdrawLimit", int.Parse(DailyWithdrawTb.Text));
                        cmd.Parameters.AddWithValue("@DailyDepositLimit", int.Parse(DailyDepositTb.Text));
                        cmd.Parameters.AddWithValue("@SingleWithdrawLimit", int.Parse(SingleWithdrawTb.Text));
                        cmd.Parameters.AddWithValue("@SingleDepositLimit", int.Parse(SingleDepositTb.Text));
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("限额更新成功！");
                    }
                    Populate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void LimitDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = limitDGV.Rows[e.RowIndex];
                AccNumTb.Text = row.Cells[0].Value.ToString();
                string firstName = row.Cells[1].Value.ToString();
                string lastName = row.Cells[2].Value.ToString();
                AccNameTb.Text = firstName + " " + lastName;
                DailyWithdrawTb.Text = row.Cells[3].Value.ToString();
                DailyDepositTb.Text = row.Cells[4].Value.ToString();
                SingleWithdrawTb.Text = row.Cells[5].Value.ToString();
                SingleDepositTb.Text = row.Cells[6].Value.ToString();
            }
        }

        private void Label8_Click(object sender, EventArgs e)
        {
            AdminHome adminHome = new AdminHome();
            FormTransitionHelper.SwitchForm(this, adminHome);
        }

        private void DailyWithdrawTb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void DailyDepositTb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void SingleWithdrawTb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void SingleDepositTb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void AccNumTb_TextChanged(object sender, EventArgs e)
        {

        }
    }
}