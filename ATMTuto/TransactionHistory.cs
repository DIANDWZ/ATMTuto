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
            populate();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;
AttachDbFilename=C:\Users\24097\OneDrive\Documents\ATMDb.mdf;
Integrated Security=True;Connect Timeout=30");

        private void populate()
        {
            Con.Open();
            string query = "select * from TransactionTbl order by TDate desc";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            transDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (AccNumTb.Text == "")
            {
                populate();
            }
            else
            {
                Con.Open();
                string query = "select * from TransactionTbl where AccNum = @AccNum order by TDate desc";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.Parameters.AddWithValue("@AccNum", AccNumTb.Text);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                transDGV.DataSource = dt;
                Con.Close();
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {
            AdminHome adminHome = new AdminHome();
            FormTransitionHelper.SwitchForm(this, adminHome);
        }
    }
}