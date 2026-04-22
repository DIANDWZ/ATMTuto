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
    public partial class UserManagement : Form
    {
        public UserManagement()
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
            string query = "select * from AccountTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            userDGV.DataSource = ds.Tables[0];
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
                    string query = "update AccountTbl set Name=@Name, LaName=@LaName, Phone=@Phone, Address=@Address, Occupation=@Occupation where AccNum=@AccNum";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.Parameters.AddWithValue("@AccNum", AccNumTb.Text);
                    cmd.Parameters.AddWithValue("@Name", AccNameTb.Text);
                    cmd.Parameters.AddWithValue("@LaName", LaNameTb.Text);
                    cmd.Parameters.AddWithValue("@Phone", PhoneTb.Text);
                    cmd.Parameters.AddWithValue("@Address", AddressTb.Text);
                    cmd.Parameters.AddWithValue("@Occupation", OccupationTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("用户信息更新成功！");
                    Con.Close();
                    populate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void userDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = userDGV.Rows[e.RowIndex];
                AccNumTb.Text = row.Cells[0].Value.ToString();
                AccNameTb.Text = row.Cells[1].Value.ToString();
                LaNameTb.Text = row.Cells[2].Value.ToString();
                PhoneTb.Text = row.Cells[4].Value.ToString();
                AddressTb.Text = row.Cells[5].Value.ToString();
                OccupationTb.Text = row.Cells[7].Value.ToString();
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {
            AdminHome adminHome = new AdminHome();
            FormTransitionHelper.SwitchForm(this, adminHome);
        }
    }
}