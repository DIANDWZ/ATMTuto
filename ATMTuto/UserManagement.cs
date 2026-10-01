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
                    string query = "select * from AccountTbl";
                    SqlDataAdapter sda = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    
                    DataTable newDt = new DataTable();
                    newDt.Columns.Add("账号", typeof(string));
                    newDt.Columns.Add("名", typeof(string));
                    newDt.Columns.Add("姓", typeof(string));
                    newDt.Columns.Add("出生日期", typeof(string));
                    newDt.Columns.Add("手机号", typeof(string));
                    newDt.Columns.Add("地址", typeof(string));
                    newDt.Columns.Add("学历", typeof(string));
                    newDt.Columns.Add("职业", typeof(string));
                    newDt.Columns.Add("密码", typeof(string));
                    newDt.Columns.Add("余额", typeof(string));
                    newDt.Columns.Add("单日存款限额", typeof(string));
                    newDt.Columns.Add("单次存款限额", typeof(string));
                    newDt.Columns.Add("单日取款限额", typeof(string));
                    newDt.Columns.Add("单次取款限额", typeof(string));
                    
                    foreach (DataRow row in dt.Rows)
                    {
                        try
                        {
                            string phoneValue = row[4].ToString();
                            int balance = AESHelper.DecryptAmount(row[9].ToString());
                            
                            newDt.Rows.Add(
                                row[0],
                                row[1],
                                row[2],
                                row[3],
                                phoneValue,
                                row[5],
                                row[6],
                                row[7],
                                "***",
                                balance,
                                row[10],
                                row[11],
                                row[12],
                                row[13]
                            );
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"处理用户数据时出错: {ex.Message}");
                        }
                    }
                    
                    userDGV.DataSource = newDt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载用户数据时出错: {ex.Message}");
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
                    using (SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;
AttachDbFilename=C:\Users\24097\OneDrive\Documents\ATMDb.mdf;
Integrated Security=True;Connect Timeout=30"))
                    {
                        con.Open();
                        string query = "update AccountTbl set AccName=@Name, LaName=@LaName, Phone=@Phone, Address=@Address, Occupation=@Occupation where AccNum=@AccNum";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@AccNum", AccNumTb.Text);
                        cmd.Parameters.AddWithValue("@Name", AccNameTb.Text);
                        cmd.Parameters.AddWithValue("@LaName", LaNameTb.Text);
                        cmd.Parameters.AddWithValue("@Phone", AESHelper.MaskPhone(PhoneTb.Text));
                        cmd.Parameters.AddWithValue("@Address", AddressTb.Text);
                        cmd.Parameters.AddWithValue("@Occupation", OccupationTb.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("用户信息更新成功！");
                    }
                    Populate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void UserDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
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

        private void Label8_Click(object sender, EventArgs e)
        {
            AdminHome adminHome = new AdminHome();
            FormTransitionHelper.SwitchForm(this, adminHome);
        }
    }
}