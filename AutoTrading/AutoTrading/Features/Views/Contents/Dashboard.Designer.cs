namespace AutoTrading.Features.Views.Contents
{
    partial class Dashboard
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
            stockCard2 = new AutoTrading.Controls.StockCard();
            stockCard1 = new AutoTrading.Controls.StockCard();
            valueTrackerCard3 = new AutoTrading.Controls.Cards.ValueTrackerCard();
            valueTrackerCard2 = new AutoTrading.Controls.Cards.ValueTrackerCard();
            valueTrackerCard1 = new AutoTrading.Controls.Cards.ValueTrackerCard();
            groupBox1 = new GroupBox();
            dataGridView1 = new DataGridView();
            label1 = new Label();
            stockCard3 = new AutoTrading.Controls.StockCard();
            stockCard4 = new AutoTrading.Controls.StockCard();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // stockCard2
            // 
            stockCard2.BackColor = Color.Transparent;
            stockCard2.BaseLineColor = Color.FromArgb(150, 165, 185);
            stockCard2.CardBackColor = Color.FromArgb(26, 28, 30);
            stockCard2.ChangePrice = 0D;
            stockCard2.ChangeRate = 0D;
            stockCard2.CurrentPrice = 0D;
            stockCard2.DownColor = Color.FromArgb(229, 83, 80);
            stockCard2.IndexName = "S&P 500";
            stockCard2.IsPositive = false;
            stockCard2.Location = new Point(1107, 13);
            stockCard2.Name = "stockCard2";
            stockCard2.PriceFormat = "N2";
            stockCard2.Size = new Size(325, 188);
            stockCard2.SparklineThickness = 2.8F;
            stockCard2.TabIndex = 13;
            stockCard2.UpColor = Color.FromArgb(72, 199, 142);
            // 
            // stockCard1
            // 
            stockCard1.BackColor = Color.Transparent;
            stockCard1.BaseLineColor = Color.FromArgb(150, 165, 185);
            stockCard1.CardBackColor = Color.FromArgb(26, 28, 30);
            stockCard1.ChangePrice = 0D;
            stockCard1.ChangeRate = 0D;
            stockCard1.CurrentPrice = 0D;
            stockCard1.DownColor = Color.FromArgb(229, 83, 80);
            stockCard1.IndexName = "KOSPI";
            stockCard1.IsPositive = false;
            stockCard1.Location = new Point(767, 13);
            stockCard1.Name = "stockCard1";
            stockCard1.PriceFormat = "N2";
            stockCard1.Size = new Size(325, 188);
            stockCard1.SparklineThickness = 2.8F;
            stockCard1.TabIndex = 12;
            stockCard1.UpColor = Color.FromArgb(72, 199, 142);
            // 
            // valueTrackerCard3
            // 
            valueTrackerCard3.BackColor = Color.Transparent;
            valueTrackerCard3.BorderColor = Color.FromArgb(40, 42, 45);
            valueTrackerCard3.CardBackColor = Color.FromArgb(26, 28, 30);
            valueTrackerCard3.ChangeAmount = new decimal(new int[] { 0, 0, 0, 0 });
            valueTrackerCard3.ChangeRate = 0F;
            valueTrackerCard3.CornerRadius = 8;
            valueTrackerCard3.DownColor = Color.FromArgb(229, 83, 80);
            valueTrackerCard3.IsPositive = false;
            valueTrackerCard3.Location = new Point(249, 145);
            valueTrackerCard3.Name = "valueTrackerCard3";
            valueTrackerCard3.Padding = new Padding(15, 10, 15, 10);
            valueTrackerCard3.ShowIndicator = true;
            valueTrackerCard3.Size = new Size(153, 125);
            valueTrackerCard3.TabIndex = 11;
            valueTrackerCard3.TitleColor = Color.FromArgb(170, 170, 170);
            valueTrackerCard3.TitleText = "수익률";
            valueTrackerCard3.TotalValue = new decimal(new int[] { 0, 0, 0, 0 });
            valueTrackerCard3.UpColor = Color.FromArgb(72, 199, 142);
            valueTrackerCard3.ValueColor = Color.White;
            valueTrackerCard3.ValueFormat = "N0";
            // 
            // valueTrackerCard2
            // 
            valueTrackerCard2.BackColor = Color.Transparent;
            valueTrackerCard2.BorderColor = Color.FromArgb(40, 42, 45);
            valueTrackerCard2.CardBackColor = Color.FromArgb(26, 28, 30);
            valueTrackerCard2.ChangeAmount = new decimal(new int[] { 0, 0, 0, 0 });
            valueTrackerCard2.ChangeRate = 0F;
            valueTrackerCard2.CornerRadius = 8;
            valueTrackerCard2.DownColor = Color.FromArgb(229, 83, 80);
            valueTrackerCard2.IsPositive = false;
            valueTrackerCard2.Location = new Point(15, 145);
            valueTrackerCard2.Name = "valueTrackerCard2";
            valueTrackerCard2.Padding = new Padding(15, 10, 15, 10);
            valueTrackerCard2.ShowIndicator = true;
            valueTrackerCard2.Size = new Size(228, 125);
            valueTrackerCard2.TabIndex = 10;
            valueTrackerCard2.TitleColor = Color.FromArgb(170, 170, 170);
            valueTrackerCard2.TitleText = "순 손익";
            valueTrackerCard2.TotalValue = new decimal(new int[] { 0, 0, 0, 0 });
            valueTrackerCard2.UpColor = Color.FromArgb(72, 199, 142);
            valueTrackerCard2.ValueColor = Color.White;
            valueTrackerCard2.ValueFormat = "N0";
            // 
            // valueTrackerCard1
            // 
            valueTrackerCard1.BackColor = Color.Transparent;
            valueTrackerCard1.BorderColor = Color.FromArgb(40, 42, 45);
            valueTrackerCard1.CardBackColor = Color.FromArgb(26, 28, 30);
            valueTrackerCard1.ChangeAmount = new decimal(new int[] { 0, 0, 0, 0 });
            valueTrackerCard1.ChangeRate = 0F;
            valueTrackerCard1.CornerRadius = 8;
            valueTrackerCard1.DownColor = Color.FromArgb(229, 83, 80);
            valueTrackerCard1.IsPositive = true;
            valueTrackerCard1.Location = new Point(15, 13);
            valueTrackerCard1.Name = "valueTrackerCard1";
            valueTrackerCard1.Padding = new Padding(15, 8, 15, 8);
            valueTrackerCard1.ShowIndicator = true;
            valueTrackerCard1.Size = new Size(387, 125);
            valueTrackerCard1.TabIndex = 9;
            valueTrackerCard1.TitleColor = Color.FromArgb(170, 170, 170);
            valueTrackerCard1.TitleText = "총 자산 가치";
            valueTrackerCard1.TotalValue = new decimal(new int[] { 10000000, 0, 0, 0 });
            valueTrackerCard1.UpColor = Color.FromArgb(72, 199, 142);
            valueTrackerCard1.ValueColor = Color.White;
            valueTrackerCard1.ValueFormat = "N0";
            // 
            // groupBox1
            // 
            groupBox1.BackColor = SystemColors.WindowFrame;
            groupBox1.Location = new Point(415, 13);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(333, 257);
            groupBox1.TabIndex = 14;
            groupBox1.TabStop = false;
            groupBox1.Text = "groupBox1";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(15, 329);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(733, 318);
            dataGridView1.TabIndex = 15;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            label1.Location = new Point(15, 297);
            label1.Name = "label1";
            label1.Size = new Size(99, 28);
            label1.TabIndex = 16;
            label1.Text = "보유 종목";
            // 
            // stockCard3
            // 
            stockCard3.BackColor = Color.Transparent;
            stockCard3.BaseLineColor = Color.FromArgb(150, 165, 185);
            stockCard3.CardBackColor = Color.FromArgb(26, 28, 30);
            stockCard3.ChangePrice = 0D;
            stockCard3.ChangeRate = 0D;
            stockCard3.CurrentPrice = 0D;
            stockCard3.DownColor = Color.FromArgb(229, 83, 80);
            stockCard3.IndexName = "VIX 지수 (공포)";
            stockCard3.IsPositive = false;
            stockCard3.Location = new Point(1107, 216);
            stockCard3.Name = "stockCard3";
            stockCard3.PriceFormat = "N2";
            stockCard3.Size = new Size(325, 188);
            stockCard3.SparklineThickness = 2.8F;
            stockCard3.TabIndex = 18;
            stockCard3.UpColor = Color.FromArgb(72, 199, 142);
            // 
            // stockCard4
            // 
            stockCard4.BackColor = Color.Transparent;
            stockCard4.BaseLineColor = Color.FromArgb(150, 165, 185);
            stockCard4.CardBackColor = Color.FromArgb(26, 28, 30);
            stockCard4.ChangePrice = 0D;
            stockCard4.ChangeRate = 0D;
            stockCard4.CurrentPrice = 0D;
            stockCard4.DownColor = Color.FromArgb(229, 83, 80);
            stockCard4.IndexName = "환율";
            stockCard4.IsPositive = false;
            stockCard4.Location = new Point(767, 216);
            stockCard4.Name = "stockCard4";
            stockCard4.PriceFormat = "N2";
            stockCard4.Size = new Size(325, 188);
            stockCard4.SparklineThickness = 2.8F;
            stockCard4.TabIndex = 17;
            stockCard4.UpColor = Color.FromArgb(72, 199, 142);
            // 
            // Dashboard
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(31, 34, 40);
            Controls.Add(stockCard3);
            Controls.Add(stockCard4);
            Controls.Add(label1);
            Controls.Add(dataGridView1);
            Controls.Add(groupBox1);
            Controls.Add(stockCard2);
            Controls.Add(stockCard1);
            Controls.Add(valueTrackerCard3);
            Controls.Add(valueTrackerCard2);
            Controls.Add(valueTrackerCard1);
            ForeColor = Color.FromArgb(225, 226, 228);
            Name = "Dashboard";
            Size = new Size(1448, 659);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Controls.StockCard stockCard2;
        private Controls.StockCard stockCard1;
        private Controls.Cards.ValueTrackerCard valueTrackerCard3;
        private Controls.Cards.ValueTrackerCard valueTrackerCard2;
        private Controls.Cards.ValueTrackerCard valueTrackerCard1;
        private GroupBox groupBox1;
        private DataGridView dataGridView1;
        private Label label1;
        private Controls.StockCard stockCard3;
        private Controls.StockCard stockCard4;
    }
}
