namespace ATMTuto
{
    partial class AnomalyLogViewer
    {
        private System.ComponentModel.IContainer components = null;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox1;
        private Guna.UI2.WinForms.Guna2DataGridView anomalyDGV;
        private Guna.UI2.WinForms.Guna2Button SearchBtn;
        private Guna.UI2.WinForms.Guna2Button ClearBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox AccNumTb;
        private System.Windows.Forms.ComboBox AnomalyLevelCb;
        private System.Windows.Forms.Label label4;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.guna2ControlBox1 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            this.label21 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.anomalyDGV = new Guna.UI2.WinForms.Guna2DataGridView();
            this.AccNumTb = new System.Windows.Forms.TextBox();
            this.AnomalyLevelCb = new System.Windows.Forms.ComboBox();
            this.SearchBtn = new Guna.UI2.WinForms.Guna2Button();
            this.ClearBtn = new Guna.UI2.WinForms.Guna2Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.anomalyDGV)).BeginInit();
            this.SuspendLayout();
            //
            // guna2Panel1
            //
            this.guna2Panel1.BackColor = System.Drawing.Color.DarkCyan;
            this.guna2Panel1.Controls.Add(this.label9);
            this.guna2Panel1.Controls.Add(this.label1);
            this.guna2Panel1.Controls.Add(this.guna2ControlBox1);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(1280, 211);
            this.guna2Panel1.TabIndex = 0;
            //
            // label9
            //
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(395, 129);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(490, 46);
            this.label9.TabIndex = 6;
            this.label9.Text = "ATM SIMULATION SYSTEM";
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(395, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(490, 94);
            this.label1.TabIndex = 5;
            this.label1.Text = "ATM模拟系统";
            //
            // guna2ControlBox1
            //
            this.guna2ControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ControlBox1.FillColor = System.Drawing.Color.DarkCyan;
            this.guna2ControlBox1.IconColor = System.Drawing.Color.White;
            this.guna2ControlBox1.Location = new System.Drawing.Point(1204, 0);
            this.guna2ControlBox1.Name = "guna2ControlBox1";
            this.guna2ControlBox1.Size = new System.Drawing.Size(76, 69);
            this.guna2ControlBox1.TabIndex = 4;
            //
            // guna2Panel2
            //
            this.guna2Panel2.BackColor = System.Drawing.Color.DarkCyan;
            this.guna2Panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.guna2Panel2.Location = new System.Drawing.Point(0, 777);
            this.guna2Panel2.Name = "guna2Panel2";
            this.guna2Panel2.Size = new System.Drawing.Size(1280, 23);
            this.guna2Panel2.TabIndex = 41;
            //
            // label21
            //
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("微软雅黑", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label21.ForeColor = System.Drawing.Color.DarkCyan;
            this.label21.Location = new System.Drawing.Point(560, 734);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(201, 41);
            this.label21.TabIndex = 42;
            this.label21.Text = "返回<Back>";
            this.label21.Click += new System.EventHandler(this.Label8_Click);
            //
            // label3
            //
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.DarkCyan;
            this.label3.Location = new System.Drawing.Point(513, 276);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(248, 46);
            this.label3.TabIndex = 44;
            this.label3.Text = "Anomaly Log";
            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.DarkCyan;
            this.label2.Location = new System.Drawing.Point(480, 214);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(315, 62);
            this.label2.TabIndex = 43;
            this.label2.Text = "异常日志查看";
            //
            // anomalyDGV
            //
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            this.anomalyDGV.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.anomalyDGV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.anomalyDGV.ColumnHeadersHeight = 30;
            this.anomalyDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.anomalyDGV.DefaultCellStyle = dataGridViewCellStyle6;
            this.anomalyDGV.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.anomalyDGV.Location = new System.Drawing.Point(0, 329);
            this.anomalyDGV.Name = "anomalyDGV";
            this.anomalyDGV.ReadOnly = true;
            this.anomalyDGV.RowHeadersVisible = false;
            this.anomalyDGV.RowHeadersWidth = 62;
            this.anomalyDGV.RowTemplate.Height = 30;
            this.anomalyDGV.Size = new System.Drawing.Size(1280, 318);
            this.anomalyDGV.TabIndex = 45;
            this.anomalyDGV.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.anomalyDGV.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.anomalyDGV.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.anomalyDGV.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.anomalyDGV.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.anomalyDGV.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.anomalyDGV.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.anomalyDGV.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.anomalyDGV.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.anomalyDGV.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.anomalyDGV.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.anomalyDGV.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.anomalyDGV.ThemeStyle.HeaderStyle.Height = 30;
            this.anomalyDGV.ThemeStyle.ReadOnly = true;
            this.anomalyDGV.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.anomalyDGV.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.anomalyDGV.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.anomalyDGV.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.anomalyDGV.ThemeStyle.RowsStyle.Height = 30;
            this.anomalyDGV.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.anomalyDGV.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            //
            // AccNumTb
            //
            this.AccNumTb.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AccNumTb.Location = new System.Drawing.Point(457, 661);
            this.AccNumTb.Name = "AccNumTb";
            this.AccNumTb.Size = new System.Drawing.Size(150, 35);
            this.AccNumTb.TabIndex = 47;
            //
            // AnomalyLevelCb
            //
            this.AnomalyLevelCb.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AnomalyLevelCb.FormattingEnabled = true;
            this.AnomalyLevelCb.Items.AddRange(new object[] {
            "",
            "Normal",
            "Warning",
            "Suspicious",
            "Critical"});
            this.AnomalyLevelCb.Location = new System.Drawing.Point(767, 664);
            this.AnomalyLevelCb.Name = "AnomalyLevelCb";
            this.AnomalyLevelCb.Size = new System.Drawing.Size(150, 32);
            this.AnomalyLevelCb.TabIndex = 49;
            //
            // SearchBtn
            //
            this.SearchBtn.AutoRoundedCorners = true;
            this.SearchBtn.BorderColor = System.Drawing.Color.Transparent;
            this.SearchBtn.BorderThickness = 1;
            this.SearchBtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.SearchBtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.SearchBtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.SearchBtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.SearchBtn.FillColor = System.Drawing.Color.DarkCyan;
            this.SearchBtn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SearchBtn.ForeColor = System.Drawing.Color.White;
            this.SearchBtn.HoverState.BorderColor = System.Drawing.Color.DarkCyan;
            this.SearchBtn.Location = new System.Drawing.Point(972, 696);
            this.SearchBtn.Name = "SearchBtn";
            this.SearchBtn.Size = new System.Drawing.Size(120, 40);
            this.SearchBtn.TabIndex = 50;
            this.SearchBtn.Text = "查询";
            this.SearchBtn.Click += new System.EventHandler(this.SearchBtn_Click);
            //
            // ClearBtn
            //
            this.ClearBtn.AutoRoundedCorners = true;
            this.ClearBtn.BorderColor = System.Drawing.Color.Transparent;
            this.ClearBtn.BorderThickness = 1;
            this.ClearBtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.ClearBtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.ClearBtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.ClearBtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.ClearBtn.FillColor = System.Drawing.Color.DarkCyan;
            this.ClearBtn.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ClearBtn.ForeColor = System.Drawing.Color.White;
            this.ClearBtn.HoverState.BorderColor = System.Drawing.Color.DarkCyan;
            this.ClearBtn.Location = new System.Drawing.Point(1122, 696);
            this.ClearBtn.Name = "ClearBtn";
            this.ClearBtn.Size = new System.Drawing.Size(120, 40);
            this.ClearBtn.TabIndex = 51;
            this.ClearBtn.Text = "清空";
            this.ClearBtn.Click += new System.EventHandler(this.ClearBtn_Click);
            //
            // label4
            //
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.DarkCyan;
            this.label4.Location = new System.Drawing.Point(365, 660);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 31);
            this.label4.TabIndex = 46;
            this.label4.Text = "账号：";
            //
            // label5
            //
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.DarkCyan;
            this.label5.Location = new System.Drawing.Point(627, 660);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(134, 31);
            this.label5.TabIndex = 48;
            this.label5.Text = "异常级别：";
            //
            // AnomalyLogViewer
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 800);
            this.Controls.Add(this.ClearBtn);
            this.Controls.Add(this.SearchBtn);
            this.Controls.Add(this.AnomalyLevelCb);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.AccNumTb);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.anomalyDGV);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.guna2Panel2);
            this.Controls.Add(this.guna2Panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AnomalyLogViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "异常日志查看";
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.anomalyDGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label label5;
    }
}