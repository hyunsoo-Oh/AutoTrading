namespace AutoTrading.Controls.Shell.TobBar
{
    partial class TwoLineTopBar
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            flowLayoutPanel1 = new FlowLayoutPanel();
            label1 = new Label();
            label_Clock = new Label();
            flowLayoutPanel2 = new FlowLayoutPanel();
            label_Server = new Label();
            statusLabel = new StatusLabel();
            splitter1 = new Splitter();
            label_Account = new Label();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Outset;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel1.Controls.Add(flowLayoutPanel2, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 56F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 44F));
            tableLayoutPanel1.Size = new Size(1898, 120);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.Controls.Add(flowLayoutPanel1, 1, 0);
            tableLayoutPanel2.Controls.Add(label_Clock, 2, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(5, 5);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(1888, 57);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.Controls.Add(label1);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.Location = new Point(3, 3);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Padding = new Padding(8, 8, 0, 0);
            flowLayoutPanel1.Size = new Size(1876, 51);
            flowLayoutPanel1.TabIndex = 0;
            flowLayoutPanel1.WrapContents = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("맑은 고딕", 14F, FontStyle.Regular, GraphicsUnit.Point, 129);
            label1.Location = new Point(11, 8);
            label1.Name = "label1";
            label1.Size = new Size(340, 38);
            label1.TabIndex = 1;
            label1.Text = "AutoTrading | DashBoard";
            // 
            // label_Clock
            // 
            label_Clock.AutoSize = true;
            label_Clock.Dock = DockStyle.Fill;
            label_Clock.Font = new Font("맑은 고딕", 10F, FontStyle.Regular, GraphicsUnit.Point, 129);
            label_Clock.Location = new Point(1885, 0);
            label_Clock.Name = "label_Clock";
            label_Clock.Size = new Size(1, 57);
            label_Clock.TabIndex = 1;
            label_Clock.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.Controls.Add(label_Server);
            flowLayoutPanel2.Controls.Add(statusLabel);
            flowLayoutPanel2.Controls.Add(splitter1);
            flowLayoutPanel2.Controls.Add(label_Account);
            flowLayoutPanel2.Dock = DockStyle.Fill;
            flowLayoutPanel2.Location = new Point(5, 70);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Padding = new Padding(8, 2, 8, 4);
            flowLayoutPanel2.Size = new Size(1888, 45);
            flowLayoutPanel2.TabIndex = 1;
            flowLayoutPanel2.WrapContents = false;
            // 
            // label_Server
            // 
            label_Server.Font = new Font("맑은 고딕", 10F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label_Server.Location = new Point(11, 2);
            label_Server.Name = "label_Server";
            label_Server.Size = new Size(142, 38);
            label_Server.TabIndex = 6;
            label_Server.Text = "모의투자";
            label_Server.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // statusLabel
            // 
            statusLabel.Location = new Point(159, 5);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(225, 36);
            statusLabel.Status = StatusLabel.StatusEnum.Gray;
            statusLabel.StatusText = "서버 연결 해제";
            statusLabel.TabIndex = 5;
            statusLabel.Tip = "";
            // 
            // splitter1
            // 
            splitter1.Location = new Point(390, 5);
            splitter1.Name = "splitter1";
            splitter1.Size = new Size(1230, 36);
            splitter1.TabIndex = 8;
            splitter1.TabStop = false;
            // 
            // label_Account
            // 
            label_Account.Font = new Font("맑은 고딕", 10F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label_Account.Location = new Point(1626, 2);
            label_Account.Name = "label_Account";
            label_Account.Size = new Size(262, 38);
            label_Account.TabIndex = 7;
            label_Account.Text = "계좌번호: 6757844601";
            label_Account.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // TwoLineTopBar
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightBlue;
            Controls.Add(tableLayoutPanel1);
            Name = "TwoLineTopBar";
            Size = new Size(1898, 120);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            flowLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private FlowLayoutPanel flowLayoutPanel1;
        private Label label1;
        private Label label_Clock;
        private FlowLayoutPanel flowLayoutPanel2;
        private StatusLabel statusLabel;
        private Label label_Server;
        private Splitter splitter1;
        private Label label_Account;
    }
}
