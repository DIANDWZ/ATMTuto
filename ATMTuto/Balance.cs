using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ATMTuto
{
    public partial class Balance : Form
    {
        public Balance()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;
AttachDbFilename=C:\Users\24097\OneDrive\Documents\ATMDb.mdf;
Integrated Security=True;Connect Timeout=30");

        private void getBalance()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select Balance from AccountTbl where AccNum = '"+AccNumLbl.Text+"'",Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            BalanceLbl.Text = "¥ " + dt.Rows[0][0].ToString();
            Con.Close();
        }

        private void Balance_Load(object sender, EventArgs e)
        {
            AccNumLbl.Text = HOME.AccNumber;
            getBalance();

        }

        private void backlbl_Click(object sender, EventArgs e)
        {
            HOME home = new HOME();
            this.Hide();
            home.Show();
        }
    }
}
