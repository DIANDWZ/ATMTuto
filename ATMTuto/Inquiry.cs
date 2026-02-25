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
    public partial class Inquiry : Form
    {
        public Inquiry()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;
AttachDbFilename=C:\Users\24097\OneDrive\Documents\ATMDb.mdf;
Integrated Security=True;Connect Timeout=30");
        string Acc = Login.AccNumber;

        private void populate()
        {
            Con.Open();
            string query = "select * from TransactionTbl where AccNum = '" + Acc + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            transactionDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void Inquiry_Load(object sender, EventArgs e)
        {
            populate();
            transactionDGV.Columns["Tid"].HeaderText = "业务流水号";
            transactionDGV.Columns["AccNum"].HeaderText = "账号";
            transactionDGV.Columns["Type"].HeaderText = "业务类型";
            transactionDGV.Columns["Amount"].HeaderText = "交易金额";
            transactionDGV.Columns["Tdate"].HeaderText = "交易时间";
        }

        private void label21_Click(object sender, EventArgs e)
        {
            HOME hOME = new HOME();
            hOME.Show();
            this.Hide();
        }
    }
}
