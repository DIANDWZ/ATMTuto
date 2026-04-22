namespace ATMTuto
{
    partial class AdminLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.guna2ControlBox1 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.label1 = new System.Windows.Forms.Label();
            this.AdminLoginBtn = new Guna.UI2.WinForms.Guna2Button();
            this.label7 = new System.Windows.Forms.Label();
            this.AdminPassTb = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.AdminUserTb = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            this.BackLbl = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkCyan;
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.guna2ControlBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1280, 189);
            this.panel1.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(395, 119);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(490, 46);
            this.label9.TabIndex = 4;
            this.label9.Text = "ATM SIMULATION SYSTEM";
            // 
            // guna2ControlBox1
            // 
            this.guna2ControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ControlBox1.FillColor = System.Drawing.Color.DarkCyan;
            this.guna2ControlBox1.IconColor = System.Drawing.Color.White;
            this.guna2ControlBox1.Location = new System.Drawing.Point(1204, 0);
            this.guna2ControlBox1.Name = "guna2ControlBox1";
            this.guna2ControlBox1.Size = new System.Drawing.Size(76, 69);
            this.guna2ControlBox1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(395, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(490, 94);
            this.label1.TabIndex = 1;
            this.label1.Text = "ATM模拟系统";
            // 
            // AdminLoginBtn
            // 
            this.AdminLoginBtn.AutoRoundedCorners = true;
            this.AdminLoginBtn.BorderColor = System.Drawing.Color.Transparent;
            this.AdminLoginBtn.BorderThickness = 1;
            this.AdminLoginBtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.AdminLoginBtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.AdminLoginBtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.AdminLoginBtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.AdminLoginBtn.FillColor = System.Drawing.Color.DarkCyan;
            this.AdminLoginBtn.Font = new System.Drawing.Font("微软雅黑", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AdminLoginBtn.ForeColor = System.Drawing.Color.White;
            this.AdminLoginBtn.HoverState.BorderColor = System.Drawing.Color.DarkCyan;
            this.AdminLoginBtn.Location = new System.Drawing.Point(641, 629);
            this.AdminLoginBtn.Name = "AdminLoginBtn";
            this.AdminLoginBtn.Size = new System.Drawing.Size(244, 67);
            this.AdminLoginBtn.TabIndex = 13;
            this.AdminLoginBtn.Text = "登录<Log in>";
            this.AdminLoginBtn.Click += new System.EventHandler(this.AdminLoginBtn_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.Color.DarkCyan;
            this.label7.Location = new System.Drawing.Point(316, 574);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(184, 46);
            this.label7.TabIndex = 12;
            this.label7.Text = "Password";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // AdminPassTb
            // 
            this.AdminPassTb.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AdminPassTb.Location = new System.Drawing.Point(559, 518);
            this.AdminPassTb.Name = "AdminPassTb";
            this.AdminPassTb.PasswordChar = '*';
            this.AdminPassTb.Size = new System.Drawing.Size(386, 62);
            this.AdminPassTb.TabIndex = 11;
            this.AdminPassTb.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AdminPassTb_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.Color.DarkCyan;
            this.label6.Location = new System.Drawing.Point(306, 454);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(194, 46);
            this.label6.TabIndex = 10;
            this.label6.Text = "Username";
            // 
            // AdminUserTb
            // 
            this.AdminUserTb.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AdminUserTb.Location = new System.Drawing.Point(559, 398);
            this.AdminUserTb.Name = "AdminUserTb";
            this.AdminUserTb.Size = new System.Drawing.Size(386, 62);
            this.AdminUserTb.TabIndex = 9;
            this.AdminUserTb.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AdminUserTb_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.DarkCyan;
            this.label5.Location = new System.Drawing.Point(329, 512);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(171, 62);
            this.label5.TabIndex = 8;
            this.label5.Text = "密码：";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.DarkCyan;
            this.label4.Location = new System.Drawing.Point(329, 392);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(171, 62);
            this.label4.TabIndex = 7;
            this.label4.Text = "账号：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.DarkCyan;
            this.label3.Location = new System.Drawing.Point(264, 281);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(838, 46);
            this.label3.TabIndex = 6;
            this.label3.Text = "Please input your admin account and password";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.DarkCyan;
            this.label2.Location = new System.Drawing.Point(360, 221);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(555, 62);
            this.label2.TabIndex = 5;
            this.label2.Text = "请输入管理员账号和密码";
            // 
            // guna2Panel2
            // 
            this.guna2Panel2.BackColor = System.Drawing.Color.DarkCyan;
            this.guna2Panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.guna2Panel2.Location = new System.Drawing.Point(0, 777);
            this.guna2Panel2.Name = "guna2Panel2";
            this.guna2Panel2.Size = new System.Drawing.Size(1280, 23);
            this.guna2Panel2.TabIndex = 16;
            // 
            // BackLbl
            // 
            this.BackLbl.AutoSize = true;
            this.BackLbl.Font = new System.Drawing.Font("微软雅黑", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BackLbl.ForeColor = System.Drawing.Color.DarkCyan;
            this.BackLbl.Location = new System.Drawing.Point(12, 733);
            this.BackLbl.Name = "BackLbl";
            this.BackLbl.Size = new System.Drawing.Size(350, 41);
            this.BackLbl.TabIndex = 17;
            this.BackLbl.Text = "用户登录<User login>";
            this.BackLbl.Click += new System.EventHandler(this.BackLbl_Click);
            // 
            // AdminLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 800);
            this.Controls.Add(this.BackLbl);
            this.Controls.Add(this.guna2Panel2);
            this.Controls.Add(this.AdminLoginBtn);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.AdminPassTb);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.AdminUserTb);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AdminLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AdminLogin";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label9;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox1;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2Button AdminLoginBtn;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox AdminPassTb;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox AdminUserTb;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
        private System.Windows.Forms.Label BackLbl;
    }
}