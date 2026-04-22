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
    public partial class AdminHome : Form
    {
        public AdminHome()
        {
            InitializeComponent();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            AdminLogin log = new AdminLogin();
            FormTransitionHelper.SwitchForm(this, log);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            UserManagement userManagement = new UserManagement();
            FormTransitionHelper.SwitchForm(this, userManagement);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            LimitManagement limitManagement = new LimitManagement();
            FormTransitionHelper.SwitchForm(this, limitManagement);
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            TransactionHistory transactionHistory = new TransactionHistory();
            FormTransitionHelper.SwitchForm(this, transactionHistory);
        }
    }
}