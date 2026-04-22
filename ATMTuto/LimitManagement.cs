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
            populate();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;
AttachDbFilename=C:\Users\24097\OneDrive\Documents\ATMDb.mdf;
Integrated Security=True;Connect Timeout=30");

        private void populate()
        {
            Con.Open();
            string query = "select AccNum, Name, DailyWithdrawLimit, DailyDepositLimit, SingleWithdrawLimit, SingleDepositLimit from AccountTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            limitDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (AccNumTb.Text == "")
            {
                MessageBox.Show("请选择要编辑的用户");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "update AccountTbl set DailyWithdrawLimit=@DailyWithdrawLimit, DailyDepositLimit=@DailyDepositLimit, SingleWithdrawLimit=@SingleWithdrawLimit, SingleDepositLimit=@SingleDepositLimit where AccNum=@AccNum";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.Parameters.AddWithValue("@AccNum", AccNumTb.Text);
                    cmd.Parameters.AddWithValue("@DailyWithdrawLimit", int.Parse(DailyWithdrawTb.Text));
                    cmd.Parameters.AddWithValue("@DailyDepositLimit", int.Parse(DailyDepositTb.Text));
                    cmd.Parameters.AddWithValue("@SingleWithdrawLimit", int.Parse(SingleWithdrawTb.Text));
                    cmd.Parameters.AddWithValue("@SingleDepositLimit", int.Parse(SingleDepositTb.Text));
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("限额更新成功！");
                    Con.Close();
                    populate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void limitDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = limitDGV.Rows[e.RowIndex];
                AccNumTb.Text = row.Cells[0].Value.ToString();
                AccNameTb.Text = row.Cells[1].Value.ToString();
                DailyWithdrawTb.Text = row.Cells[2].Value.ToString();
                DailyDepositTb.Text = row.Cells[3].Value.ToString();
                SingleWithdrawTb.Text = row.Cells[4].Value.ToString();
                SingleDepositTb.Text = row.Cells[5].Value.ToString();
            }
        }

        private void label8_Click(object sender, EventArgs e)
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