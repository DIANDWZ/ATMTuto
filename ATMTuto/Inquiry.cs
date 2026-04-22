using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
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
            FormTransitionHelper.SwitchForm(this, hOME);
        }

        private void PrintBtn_Click(object sender, EventArgs e)
        {
            PrintDocument printDoc = new PrintDocument();
            printDoc.PrintPage += new PrintPageEventHandler(PrintDoc_PrintPage);

            PrintPreviewDialog previewDialog = new PrintPreviewDialog();
            previewDialog.Document = printDoc;
            previewDialog.ShowDialog();
        }

        private void PrintDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            Font titleFont = new Font("微软雅黑", 20, FontStyle.Bold);
            Font headerFont = new Font("微软雅黑", 12, FontStyle.Bold);
            Font contentFont = new Font("宋体", 10);
            Font smallFont = new Font("宋体", 9);

            float yPos = 50;
            float leftMargin = 50;

            g.DrawString("ATM交易凭条", titleFont, Brushes.Black, new PointF(250, yPos));
            yPos += 50;

            g.DrawString("========================================", contentFont, Brushes.Black, new PointF(leftMargin, yPos));
            yPos += 30;

            g.DrawString("账号：" + Acc, headerFont, Brushes.Black, new PointF(leftMargin, yPos));
            yPos += 30;

            g.DrawString("打印时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), contentFont, Brushes.Black, new PointF(leftMargin, yPos));
            yPos += 40;

            g.DrawString("----------------------------------------", contentFont, Brushes.Black, new PointF(leftMargin, yPos));
            yPos += 25;

            g.DrawString("交易类型          金额            时间", headerFont, Brushes.Black, new PointF(leftMargin, yPos));
            yPos += 25;

            g.DrawString("----------------------------------------", contentFont, Brushes.Black, new PointF(leftMargin, yPos));
            yPos += 20;

            Con.Open();
            string query = "select Type, Amount, TDate from TransactionTbl where AccNum = '" + Acc + "' order by TDate desc";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataReader reader = cmd.ExecuteReader();

            int count = 0;
            while (reader.Read() && count < 10)
            {
                string type = reader["Type"].ToString();
                string amount = reader["Amount"].ToString();
                string date = Convert.ToDateTime(reader["TDate"]).ToString("yyyy-MM-dd HH:mm");

                g.DrawString(type.PadRight(16) + amount.PadLeft(10) + "     " + date, smallFont, Brushes.Black, new PointF(leftMargin, yPos));
                yPos += 20;
                count++;
            }

            Con.Close();

            yPos += 10;
            g.DrawString("========================================", contentFont, Brushes.Black, new PointF(leftMargin, yPos));
            yPos += 30;

            g.DrawString("请妥善保管此凭条", contentFont, Brushes.Black, new PointF(leftMargin, yPos));
        }
    }
}