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
            FormTransitionHelper.SwitchForm(this, log);
        }

        private void balanceBtn_Click(object sender, EventArgs e)
        {
            Balance bal = new Balance();
            FormTransitionHelper.SwitchForm(this, bal);
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
            FormTransitionHelper.SwitchForm(this, depo);
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            ChangePin pin = new ChangePin();
            FormTransitionHelper.SwitchForm(this, pin);
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Withdraw wd = new Withdraw();
            FormTransitionHelper.SwitchForm(this, wd);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Fastcash fastcash = new Fastcash();
            FormTransitionHelper.SwitchForm(this, fastcash);
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            Inquiry inquiry = new Inquiry();
            FormTransitionHelper.SwitchForm(this, inquiry);
        }

        private void TransferBtn_Click(object sender, EventArgs e)
        {
            Transfer transfer = new Transfer();
            FormTransitionHelper.SwitchForm(this, transfer);
        }
    }
}