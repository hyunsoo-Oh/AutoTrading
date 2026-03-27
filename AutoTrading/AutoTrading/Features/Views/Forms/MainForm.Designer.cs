namespace AutoTrading
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel_ConnectState = new ToolStripStatusLabel();
            toolStripStatusLabel_UserName = new ToolStripStatusLabel();
            toolStripStatusLabel_GetStockInfo = new ToolStripStatusLabel();
            toolStripStatusLabel2 = new ToolStripStatusLabel();
            toolStripStatusLabel_Clock = new ToolStripStatusLabel();
            splitContainer2 = new SplitContainer();
            button1 = new Button();
            button_Test = new Button();
            sideNavigationBar = new AutoTrading.Controls.Shell.SideBar.SideNavigationBar();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            systemToolStripMenuItem = new ToolStripMenuItem();
            ToolStripMenuItem_Mock = new ToolStripMenuItem();
            ToolStripMenuItem_Live = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            종목호출ToolStripMenuItem = new ToolStripMenuItem();
            panel_TopBar = new Panel();
            button_sell = new Button();
            button_buy = new Button();
            twoLineTopBar = new AutoTrading.Controls.Shell.TobBar.TwoLineTopBar();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.SuspendLayout();
            menuStrip1.SuspendLayout();
            panel_TopBar.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(24, 24);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel_ConnectState, toolStripStatusLabel_UserName, toolStripStatusLabel_GetStockInfo, toolStripStatusLabel2, toolStripStatusLabel_Clock });
            statusStrip1.Location = new Point(0, 992);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1898, 32);
            statusStrip1.TabIndex = 0;
            statusStrip1.Text = "statusStrip";
            // 
            // toolStripStatusLabel_ConnectState
            // 
            toolStripStatusLabel_ConnectState.Name = "toolStripStatusLabel_ConnectState";
            toolStripStatusLabel_ConnectState.Size = new Size(126, 25);
            toolStripStatusLabel_ConnectState.Text = "○ Disconnect";
            // 
            // toolStripStatusLabel_UserName
            // 
            toolStripStatusLabel_UserName.Name = "toolStripStatusLabel_UserName";
            toolStripStatusLabel_UserName.Size = new Size(0, 25);
            // 
            // toolStripStatusLabel_GetStockInfo
            // 
            toolStripStatusLabel_GetStockInfo.Name = "toolStripStatusLabel_GetStockInfo";
            toolStripStatusLabel_GetStockInfo.Size = new Size(90, 25);
            toolStripStatusLabel_GetStockInfo.Text = "종목 없음";
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            toolStripStatusLabel2.Size = new Size(1667, 25);
            toolStripStatusLabel2.Spring = true;
            // 
            // toolStripStatusLabel_Clock
            // 
            toolStripStatusLabel_Clock.Name = "toolStripStatusLabel_Clock";
            toolStripStatusLabel_Clock.Size = new Size(0, 25);
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.Location = new Point(0, 153);
            splitContainer2.Margin = new Padding(3, 2, 3, 2);
            splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(button1);
            splitContainer2.Panel1.Controls.Add(button_Test);
            splitContainer2.Panel1.Controls.Add(sideNavigationBar);
            splitContainer2.Size = new Size(1898, 839);
            splitContainer2.SplitterDistance = 288;
            splitContainer2.TabIndex = 0;
            // 
            // button1
            // 
            button1.Location = new Point(107, 467);
            button1.Margin = new Padding(3, 4, 3, 4);
            button1.Name = "button1";
            button1.Size = new Size(104, 36);
            button1.TabIndex = 5;
            button1.Tag = "Test2";
            button1.Text = "Test";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button_Test_Click;
            // 
            // button_Test
            // 
            button_Test.Location = new Point(107, 399);
            button_Test.Margin = new Padding(3, 4, 3, 4);
            button_Test.Name = "button_Test";
            button_Test.Size = new Size(104, 36);
            button_Test.TabIndex = 4;
            button_Test.Tag = "Test1";
            button_Test.Text = "Test";
            button_Test.UseVisualStyleBackColor = true;
            button_Test.Click += button_Test_Click;
            // 
            // sideNavigationBar
            // 
            sideNavigationBar.AccentColor = Color.FromArgb(40, 120, 255);
            sideNavigationBar.BackColor = Color.FromArgb(26, 29, 33);
            sideNavigationBar.Dock = DockStyle.Fill;
            sideNavigationBar.ForeColor = Color.FromArgb(225, 226, 229);
            sideNavigationBar.Location = new Point(0, 0);
            sideNavigationBar.Margin = new Padding(3, 4, 3, 4);
            sideNavigationBar.Name = "sideNavigationBar";
            sideNavigationBar.Padding = new Padding(0, 10, 0, 10);
            sideNavigationBar.Size = new Size(288, 839);
            sideNavigationBar.TabIndex = 0;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = Color.FromArgb(51, 55, 71);
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, systemToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1898, 33);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.ForeColor = Color.FromArgb(225, 226, 229);
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(55, 29);
            fileToolStripMenuItem.Text = "File";
            // 
            // systemToolStripMenuItem
            // 
            systemToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { ToolStripMenuItem_Mock, ToolStripMenuItem_Live, toolStripSeparator1, 종목호출ToolStripMenuItem });
            systemToolStripMenuItem.ForeColor = Color.FromArgb(225, 226, 229);
            systemToolStripMenuItem.Name = "systemToolStripMenuItem";
            systemToolStripMenuItem.Size = new Size(87, 29);
            systemToolStripMenuItem.Text = "System";
            // 
            // ToolStripMenuItem_Mock
            // 
            ToolStripMenuItem_Mock.Name = "ToolStripMenuItem_Mock";
            ToolStripMenuItem_Mock.Size = new Size(228, 34);
            ToolStripMenuItem_Mock.Tag = "Mock";
            ToolStripMenuItem_Mock.Text = "모의투자 서버";
            ToolStripMenuItem_Mock.Click += ToolStripMenuItem_Click;
            // 
            // ToolStripMenuItem_Live
            // 
            ToolStripMenuItem_Live.Name = "ToolStripMenuItem_Live";
            ToolStripMenuItem_Live.Size = new Size(228, 34);
            ToolStripMenuItem_Live.Tag = "Live";
            ToolStripMenuItem_Live.Text = "실전투자 서버";
            ToolStripMenuItem_Live.Click += ToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(225, 6);
            // 
            // 종목호출ToolStripMenuItem
            // 
            종목호출ToolStripMenuItem.Name = "종목호출ToolStripMenuItem";
            종목호출ToolStripMenuItem.Size = new Size(228, 34);
            종목호출ToolStripMenuItem.Tag = "GetStockInfo";
            종목호출ToolStripMenuItem.Text = "종목 호출";
            종목호출ToolStripMenuItem.Click += ToolStripMenuItem_Click;
            // 
            // panel_TopBar
            // 
            panel_TopBar.Controls.Add(button_sell);
            panel_TopBar.Controls.Add(button_buy);
            panel_TopBar.Controls.Add(twoLineTopBar);
            panel_TopBar.Dock = DockStyle.Top;
            panel_TopBar.Location = new Point(0, 33);
            panel_TopBar.Margin = new Padding(3, 2, 3, 2);
            panel_TopBar.Name = "panel_TopBar";
            panel_TopBar.Size = new Size(1898, 120);
            panel_TopBar.TabIndex = 1;
            // 
            // button_sell
            // 
            button_sell.Location = new Point(893, 43);
            button_sell.Name = "button_sell";
            button_sell.Size = new Size(112, 34);
            button_sell.TabIndex = 1;
            button_sell.Tag = "Sell";
            button_sell.Text = "Sell";
            button_sell.UseVisualStyleBackColor = true;
            button_sell.Click += button_Click;
            // 
            // button_buy
            // 
            button_buy.Location = new Point(571, 46);
            button_buy.Name = "button_buy";
            button_buy.Size = new Size(112, 34);
            button_buy.TabIndex = 0;
            button_buy.Tag = "Buy";
            button_buy.Text = "buy";
            button_buy.UseVisualStyleBackColor = true;
            button_buy.Click += button_Click;
            // 
            // twoLineTopBar
            // 
            twoLineTopBar.AccountNumber = "";
            twoLineTopBar.AccountPrefix = "계좌번호:";
            twoLineTopBar.BackColor = Color.FromArgb(26, 29, 33);
            twoLineTopBar.ClockFormat = "HH:mm:ss";
            twoLineTopBar.ConnectedText = "서버 연결됨";
            twoLineTopBar.DisconnectedText = "서버 연결 해제";
            twoLineTopBar.Dock = DockStyle.Fill;
            twoLineTopBar.ForeColor = Color.FromArgb(225, 226, 229);
            twoLineTopBar.InvestmentMode = "";
            twoLineTopBar.IsServerConnected = false;
            twoLineTopBar.Location = new Point(0, 0);
            twoLineTopBar.Margin = new Padding(3, 2, 3, 2);
            twoLineTopBar.Name = "twoLineTopBar";
            twoLineTopBar.Size = new Size(1898, 120);
            twoLineTopBar.TabIndex = 0;
            twoLineTopBar.UseRealtimeClock = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(1898, 1024);
            Controls.Add(splitContainer2);
            Controls.Add(panel_TopBar);
            Controls.Add(menuStrip1);
            Controls.Add(statusStrip1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenuStrip = menuStrip1;
            Margin = new Padding(3, 2, 3, 2);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Auto Stock Trading";
            Load += MainForm_Load;
            Shown += MainForm_Shown;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            splitContainer2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            panel_TopBar.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StatusStrip statusStrip1;
        private SplitContainer splitContainer2;
        private ToolStripStatusLabel toolStripStatusLabel_ConnectState;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private ToolStripStatusLabel toolStripStatusLabel_Clock;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem systemToolStripMenuItem;
        private ToolStripMenuItem ToolStripMenuItem_Mock;
        private ToolStripMenuItem ToolStripMenuItem_Live;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripStatusLabel toolStripStatusLabel_UserName;
        private ToolStripMenuItem 종목호출ToolStripMenuItem;
        private Panel panel_TopBar;
        private ToolStripStatusLabel toolStripStatusLabel_GetStockInfo;
        private Button button_Test;
        private Controls.Shell.SideBar.SideNavigationBar sideNavigationBar;
        private Controls.Shell.TobBar.TwoLineTopBar twoLineTopBar;
        private Button button_sell;
        private Button button_buy;
        private Button button1;
    }
}
