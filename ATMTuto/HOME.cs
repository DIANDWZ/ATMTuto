using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATMTuto
{
    public partial class HOME : Form
    {
        public HOME()
        {
            InitializeComponent();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Hide();
        }

        private void balanceBtn_Click(object sender, EventArgs e)
        {
            Balance bal = new Balance();
            this.Hide();
            bal.Show();
        }

        public static String AccNumber;

        private void HOME_Load(object sender, EventArgs e)
        {
            AccNumLbl.Text = "账号：" + Login.AccNumber;
            AccNumber = Login.AccNumber;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Deposit depo = new Deposit();
            depo.Show();
            this.Hide();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            ChangePin pin = new ChangePin();
            pin.Show();
            this.Hide();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Withdraw wd = new Withdraw();
            wd.Show();
            this.Hide();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Fastcash fastcash = new Fastcash();
            fastcash.Show();
            this.Hide();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            Inquiry inquiry = new Inquiry();
            inquiry.Show();
            this.Hide();
        }
    }
}
