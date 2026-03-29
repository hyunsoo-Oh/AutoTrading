namespace AutoTrading.Features.Views.Contents
{
    partial class Orders
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
            emptyCard1 = new AutoTrading.Controls.Cards.EmptyCard();
            darkTabControl1 = new StockControl.DarkTabControl();
            tabPage_day = new TabPage();
            emptyCard2 = new AutoTrading.Controls.Cards.EmptyCard();
            tabPage_month = new TabPage();
            tabPage_quarter = new TabPage();
            tabPage_half = new TabPage();
            tabPage_year = new TabPage();
            transparentskControl1 = new ScottPlot.WinForms.TransparentSKControl();
            transparentskControl2 = new ScottPlot.WinForms.TransparentSKControl();
            transparentskControl3 = new ScottPlot.WinForms.TransparentSKControl();
            transparentskControl4 = new ScottPlot.WinForms.TransparentSKControl();
            transparentskControl5 = new ScottPlot.WinForms.TransparentSKControl();
            darkTabControl1.SuspendLayout();
            tabPage_day.SuspendLayout();
            tabPage_month.SuspendLayout();
            tabPage_quarter.SuspendLayout();
            tabPage_half.SuspendLayout();
            tabPage_year.SuspendLayout();
            SuspendLayout();
            // 
            // emptyCard1
            // 
            emptyCard1.BackColor = Color.Transparent;
            emptyCard1.CardBackColor = Color.FromArgb(26, 28, 30);
            emptyCard1.Location = new Point(17, 9);
            emptyCard1.Name = "emptyCard1";
            emptyCard1.Size = new Size(1584, 68);
            emptyCard1.TabIndex = 0;
            // 
            // darkTabControl1
            // 
            darkTabControl1.Controls.Add(tabPage_day);
            darkTabControl1.Controls.Add(tabPage_month);
            darkTabControl1.Controls.Add(tabPage_quarter);
            darkTabControl1.Controls.Add(tabPage_half);
            darkTabControl1.Controls.Add(tabPage_year);
            darkTabControl1.ItemSize = new Size(120, 40);
            darkTabControl1.Location = new Point(17, 87);
            darkTabControl1.Name = "darkTabControl1";
            darkTabControl1.Padding = new Point(20, 0);
            darkTabControl1.SelectedIndex = 0;
            darkTabControl1.Size = new Size(1164, 451);
            darkTabControl1.SizeMode = TabSizeMode.Fixed;
            darkTabControl1.TabIndex = 1;
            // 
            // tabPage_day
            // 
            tabPage_day.BackColor = Color.FromArgb(17, 20, 27);
            tabPage_day.Controls.Add(transparentskControl1);
            tabPage_day.ForeColor = Color.FromArgb(200, 200, 200);
            tabPage_day.Location = new Point(4, 44);
            tabPage_day.Name = "tabPage_day";
            tabPage_day.Padding = new Padding(3);
            tabPage_day.Size = new Size(1156, 403);
            tabPage_day.TabIndex = 0;
            tabPage_day.Text = "1 Day";
            // 
            // emptyCard2
            // 
            emptyCard2.BackColor = Color.Transparent;
            emptyCard2.CardBackColor = Color.FromArgb(26, 28, 30);
            emptyCard2.Location = new Point(1191, 87);
            emptyCard2.Name = "emptyCard2";
            emptyCard2.Size = new Size(410, 451);
            emptyCard2.TabIndex = 2;
            // 
            // tabPage_month
            // 
            tabPage_month.BackColor = Color.FromArgb(17, 20, 27);
            tabPage_month.Controls.Add(transparentskControl2);
            tabPage_month.ForeColor = Color.FromArgb(200, 200, 200);
            tabPage_month.Location = new Point(4, 44);
            tabPage_month.Name = "tabPage_month";
            tabPage_month.Size = new Size(1156, 403);
            tabPage_month.TabIndex = 1;
            tabPage_month.Text = "1 Month";
            // 
            // tabPage_quarter
            // 
            tabPage_quarter.BackColor = Color.FromArgb(17, 20, 27);
            tabPage_quarter.Controls.Add(transparentskControl3);
            tabPage_quarter.ForeColor = Color.FromArgb(200, 200, 200);
            tabPage_quarter.Location = new Point(4, 44);
            tabPage_quarter.Name = "tabPage_quarter";
            tabPage_quarter.Size = new Size(1156, 403);
            tabPage_quarter.TabIndex = 2;
            tabPage_quarter.Text = "3 Month";
            // 
            // tabPage_half
            // 
            tabPage_half.BackColor = Color.FromArgb(17, 20, 27);
            tabPage_half.Controls.Add(transparentskControl4);
            tabPage_half.ForeColor = Color.FromArgb(200, 200, 200);
            tabPage_half.Location = new Point(4, 44);
            tabPage_half.Name = "tabPage_half";
            tabPage_half.Size = new Size(1156, 403);
            tabPage_half.TabIndex = 3;
            tabPage_half.Text = "6 Month";
            // 
            // tabPage_year
            // 
            tabPage_year.BackColor = Color.FromArgb(17, 20, 27);
            tabPage_year.Controls.Add(transparentskControl5);
            tabPage_year.ForeColor = Color.FromArgb(200, 200, 200);
            tabPage_year.Location = new Point(4, 44);
            tabPage_year.Name = "tabPage_year";
            tabPage_year.Size = new Size(1156, 403);
            tabPage_year.TabIndex = 4;
            tabPage_year.Text = "1 Year";
            // 
            // transparentskControl1
            // 
            transparentskControl1.Dock = DockStyle.Fill;
            transparentskControl1.Location = new Point(3, 3);
            transparentskControl1.Name = "transparentskControl1";
            transparentskControl1.Size = new Size(1150, 397);
            transparentskControl1.TabIndex = 0;
            transparentskControl1.Text = "transparentskControl1";
            // 
            // transparentskControl2
            // 
            transparentskControl2.Dock = DockStyle.Fill;
            transparentskControl2.Location = new Point(0, 0);
            transparentskControl2.Name = "transparentskControl2";
            transparentskControl2.Size = new Size(1156, 403);
            transparentskControl2.TabIndex = 1;
            transparentskControl2.Text = "transparentskControl2";
            // 
            // transparentskControl3
            // 
            transparentskControl3.Dock = DockStyle.Fill;
            transparentskControl3.Location = new Point(0, 0);
            transparentskControl3.Name = "transparentskControl3";
            transparentskControl3.Size = new Size(1156, 403);
            transparentskControl3.TabIndex = 1;
            transparentskControl3.Text = "transparentskControl3";
            // 
            // transparentskControl4
            // 
            transparentskControl4.Dock = DockStyle.Fill;
            transparentskControl4.Location = new Point(0, 0);
            transparentskControl4.Name = "transparentskControl4";
            transparentskControl4.Size = new Size(1156, 403);
            transparentskControl4.TabIndex = 1;
            transparentskControl4.Text = "transparentskControl4";
            // 
            // transparentskControl5
            // 
            transparentskControl5.Dock = DockStyle.Fill;
            transparentskControl5.Location = new Point(0, 0);
            transparentskControl5.Name = "transparentskControl5";
            transparentskControl5.Size = new Size(1156, 403);
            transparentskControl5.TabIndex = 1;
            transparentskControl5.Text = "transparentskControl5";
            // 
            // Orders
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(51, 54, 60);
            Controls.Add(emptyCard2);
            Controls.Add(darkTabControl1);
            Controls.Add(emptyCard1);
            ForeColor = Color.FromArgb(225, 226, 228);
            Name = "Orders";
            Size = new Size(1614, 883);
            darkTabControl1.ResumeLayout(false);
            tabPage_day.ResumeLayout(false);
            tabPage_month.ResumeLayout(false);
            tabPage_quarter.ResumeLayout(false);
            tabPage_half.ResumeLayout(false);
            tabPage_year.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Controls.Cards.EmptyCard emptyCard1;
        private StockControl.DarkTabControl darkTabControl1;
        private TabPage tabPage_day;
        private TabPage tabPage_month;
        private TabPage tabPage_quarter;
        private TabPage tabPage_half;
        private TabPage tabPage_year;
        private Controls.Cards.EmptyCard emptyCard2;
        private ScottPlot.WinForms.TransparentSKControl transparentskControl1;
        private ScottPlot.WinForms.TransparentSKControl transparentskControl2;
        private ScottPlot.WinForms.TransparentSKControl transparentskControl3;
        private ScottPlot.WinForms.TransparentSKControl transparentskControl4;
        private ScottPlot.WinForms.TransparentSKControl transparentskControl5;
    }
}
