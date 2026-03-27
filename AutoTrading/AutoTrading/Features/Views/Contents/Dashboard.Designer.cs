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
            stockCard_SnP = new AutoTrading.Controls.StockCard();
            stockCard_kospi = new AutoTrading.Controls.StockCard();
            valueTrackerCard_Rate = new AutoTrading.Controls.Cards.ValueTrackerCard();
            valueTrackerCard_Deposits = new AutoTrading.Controls.Cards.ValueTrackerCard();
            valueTrackerCard_TotalAsset = new AutoTrading.Controls.Cards.ValueTrackerCard();
            groupBox1 = new GroupBox();
            dataGridView1 = new DataGridView();
            label1 = new Label();
            stockCard_InterestRate = new AutoTrading.Controls.StockCard();
            stockCard_ExchangeRate = new AutoTrading.Controls.StockCard();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // stockCard_SnP
            // 
            stockCard_SnP.BackColor = Color.Transparent;
            stockCard_SnP.BaseLineColor = Color.FromArgb(150, 165, 185);
            stockCard_SnP.CardBackColor = Color.FromArgb(26, 28, 30);
            stockCard_SnP.ChangePrice = 0D;
            stockCard_SnP.ChangeRate = 0D;
            stockCard_SnP.CurrentPrice = 0D;
            stockCard_SnP.DownColor = Color.FromArgb(229, 83, 80);
            stockCard_SnP.IndexName = "S&P 500";
            stockCard_SnP.IsPositive = false;
            stockCard_SnP.Location = new Point(1230, 16);
            stockCard_SnP.Margin = new Padding(3, 4, 3, 4);
            stockCard_SnP.Name = "stockCard_SnP";
            stockCard_SnP.PriceFormat = "N2";
            stockCard_SnP.Size = new Size(361, 235);
            stockCard_SnP.SparklineThickness = 2.8F;
            stockCard_SnP.TabIndex = 13;
            stockCard_SnP.UpColor = Color.FromArgb(72, 199, 142);
            // 
            // stockCard_kospi
            // 
            stockCard_kospi.BackColor = Color.Transparent;
            stockCard_kospi.BaseLineColor = Color.FromArgb(150, 165, 185);
            stockCard_kospi.CardBackColor = Color.FromArgb(26, 28, 30);
            stockCard_kospi.ChangePrice = 0D;
            stockCard_kospi.ChangeRate = 0D;
            stockCard_kospi.CurrentPrice = 0D;
            stockCard_kospi.DownColor = Color.FromArgb(229, 83, 80);
            stockCard_kospi.IndexName = "KOSPI";
            stockCard_kospi.IsPositive = false;
            stockCard_kospi.Location = new Point(852, 16);
            stockCard_kospi.Margin = new Padding(3, 4, 3, 4);
            stockCard_kospi.Name = "stockCard_kospi";
            stockCard_kospi.PriceFormat = "N2";
            stockCard_kospi.Size = new Size(361, 235);
            stockCard_kospi.SparklineThickness = 2.8F;
            stockCard_kospi.TabIndex = 12;
            stockCard_kospi.UpColor = Color.FromArgb(72, 199, 142);
            // 
            // valueTrackerCard_Rate
            // 
            valueTrackerCard_Rate.BackColor = Color.Transparent;
            valueTrackerCard_Rate.BorderColor = Color.FromArgb(40, 42, 45);
            valueTrackerCard_Rate.CardBackColor = Color.FromArgb(26, 28, 30);
            valueTrackerCard_Rate.ChangeAmount = new decimal(new int[] { 0, 0, 0, 0 });
            valueTrackerCard_Rate.ChangeRate = 0F;
            valueTrackerCard_Rate.CornerRadius = 8;
            valueTrackerCard_Rate.DownColor = Color.FromArgb(229, 83, 80);
            valueTrackerCard_Rate.IsPositive = false;
            valueTrackerCard_Rate.Location = new Point(277, 181);
            valueTrackerCard_Rate.Margin = new Padding(3, 4, 3, 4);
            valueTrackerCard_Rate.Name = "valueTrackerCard_Rate";
            valueTrackerCard_Rate.Padding = new Padding(17, 12, 17, 12);
            valueTrackerCard_Rate.ShowIndicator = true;
            valueTrackerCard_Rate.Size = new Size(170, 156);
            valueTrackerCard_Rate.TabIndex = 11;
            valueTrackerCard_Rate.TitleColor = Color.FromArgb(170, 170, 170);
            valueTrackerCard_Rate.TitleText = "수익률";
            valueTrackerCard_Rate.TotalValue = new decimal(new int[] { 0, 0, 0, 0 });
            valueTrackerCard_Rate.UpColor = Color.FromArgb(72, 199, 142);
            valueTrackerCard_Rate.ValueColor = Color.White;
            valueTrackerCard_Rate.ValueFormat = "N0";
            // 
            // valueTrackerCard_Deposits
            // 
            valueTrackerCard_Deposits.BackColor = Color.Transparent;
            valueTrackerCard_Deposits.BorderColor = Color.FromArgb(40, 42, 45);
            valueTrackerCard_Deposits.CardBackColor = Color.FromArgb(26, 28, 30);
            valueTrackerCard_Deposits.ChangeAmount = new decimal(new int[] { 0, 0, 0, 0 });
            valueTrackerCard_Deposits.ChangeRate = 0F;
            valueTrackerCard_Deposits.CornerRadius = 8;
            valueTrackerCard_Deposits.DownColor = Color.FromArgb(229, 83, 80);
            valueTrackerCard_Deposits.IsPositive = false;
            valueTrackerCard_Deposits.Location = new Point(17, 181);
            valueTrackerCard_Deposits.Margin = new Padding(3, 4, 3, 4);
            valueTrackerCard_Deposits.Name = "valueTrackerCard_Deposits";
            valueTrackerCard_Deposits.Padding = new Padding(17, 12, 17, 12);
            valueTrackerCard_Deposits.ShowIndicator = true;
            valueTrackerCard_Deposits.Size = new Size(253, 156);
            valueTrackerCard_Deposits.TabIndex = 10;
            valueTrackerCard_Deposits.TitleColor = Color.FromArgb(170, 170, 170);
            valueTrackerCard_Deposits.TitleText = "예수금";
            valueTrackerCard_Deposits.TotalValue = new decimal(new int[] { 0, 0, 0, 0 });
            valueTrackerCard_Deposits.UpColor = Color.FromArgb(72, 199, 142);
            valueTrackerCard_Deposits.ValueColor = Color.White;
            valueTrackerCard_Deposits.ValueFormat = "N0";
            // 
            // valueTrackerCard_TotalAsset
            // 
            valueTrackerCard_TotalAsset.BackColor = Color.Transparent;
            valueTrackerCard_TotalAsset.BorderColor = Color.FromArgb(40, 42, 45);
            valueTrackerCard_TotalAsset.CardBackColor = Color.FromArgb(26, 28, 30);
            valueTrackerCard_TotalAsset.ChangeAmount = new decimal(new int[] { 0, 0, 0, 0 });
            valueTrackerCard_TotalAsset.ChangeRate = 0F;
            valueTrackerCard_TotalAsset.CornerRadius = 8;
            valueTrackerCard_TotalAsset.DownColor = Color.FromArgb(229, 83, 80);
            valueTrackerCard_TotalAsset.IsPositive = true;
            valueTrackerCard_TotalAsset.Location = new Point(17, 16);
            valueTrackerCard_TotalAsset.Margin = new Padding(3, 4, 3, 4);
            valueTrackerCard_TotalAsset.Name = "valueTrackerCard_TotalAsset";
            valueTrackerCard_TotalAsset.Padding = new Padding(17, 10, 17, 10);
            valueTrackerCard_TotalAsset.ShowIndicator = true;
            valueTrackerCard_TotalAsset.Size = new Size(430, 156);
            valueTrackerCard_TotalAsset.TabIndex = 9;
            valueTrackerCard_TotalAsset.TitleColor = Color.FromArgb(170, 170, 170);
            valueTrackerCard_TotalAsset.TitleText = "총 자산 가치";
            valueTrackerCard_TotalAsset.TotalValue = new decimal(new int[] { 10000000, 0, 0, 0 });
            valueTrackerCard_TotalAsset.UpColor = Color.FromArgb(72, 199, 142);
            valueTrackerCard_TotalAsset.ValueColor = Color.White;
            valueTrackerCard_TotalAsset.ValueFormat = "N0";
            // 
            // groupBox1
            // 
            groupBox1.BackColor = SystemColors.WindowFrame;
            groupBox1.Location = new Point(461, 16);
            groupBox1.Margin = new Padding(3, 4, 3, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 4, 3, 4);
            groupBox1.Size = new Size(370, 321);
            groupBox1.TabIndex = 14;
            groupBox1.TabStop = false;
            groupBox1.Text = "groupBox1";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(17, 411);
            dataGridView1.Margin = new Padding(3, 4, 3, 4);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(814, 398);
            dataGridView1.TabIndex = 15;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            label1.Location = new Point(17, 371);
            label1.Name = "label1";
            label1.Size = new Size(118, 32);
            label1.TabIndex = 16;
            label1.Text = "보유 종목";
            // 
            // stockCard_InterestRate
            // 
            stockCard_InterestRate.BackColor = Color.Transparent;
            stockCard_InterestRate.BaseLineColor = Color.FromArgb(150, 165, 185);
            stockCard_InterestRate.CardBackColor = Color.FromArgb(26, 28, 30);
            stockCard_InterestRate.ChangePrice = 0D;
            stockCard_InterestRate.ChangeRate = 0D;
            stockCard_InterestRate.CurrentPrice = 0D;
            stockCard_InterestRate.DownColor = Color.FromArgb(229, 83, 80);
            stockCard_InterestRate.IndexName = "한국 금리";
            stockCard_InterestRate.IsPositive = false;
            stockCard_InterestRate.Location = new Point(1230, 270);
            stockCard_InterestRate.Margin = new Padding(3, 4, 3, 4);
            stockCard_InterestRate.Name = "stockCard_InterestRate";
            stockCard_InterestRate.PriceFormat = "N2";
            stockCard_InterestRate.Size = new Size(361, 235);
            stockCard_InterestRate.SparklineThickness = 2.8F;
            stockCard_InterestRate.TabIndex = 18;
            stockCard_InterestRate.UpColor = Color.FromArgb(72, 199, 142);
            // 
            // stockCard_ExchangeRate
            // 
            stockCard_ExchangeRate.BackColor = Color.Transparent;
            stockCard_ExchangeRate.BaseLineColor = Color.FromArgb(150, 165, 185);
            stockCard_ExchangeRate.CardBackColor = Color.FromArgb(26, 28, 30);
            stockCard_ExchangeRate.ChangePrice = 0D;
            stockCard_ExchangeRate.ChangeRate = 0D;
            stockCard_ExchangeRate.CurrentPrice = 0D;
            stockCard_ExchangeRate.DownColor = Color.FromArgb(229, 83, 80);
            stockCard_ExchangeRate.IndexName = "환율";
            stockCard_ExchangeRate.IsPositive = false;
            stockCard_ExchangeRate.Location = new Point(852, 270);
            stockCard_ExchangeRate.Margin = new Padding(3, 4, 3, 4);
            stockCard_ExchangeRate.Name = "stockCard_ExchangeRate";
            stockCard_ExchangeRate.PriceFormat = "N2";
            stockCard_ExchangeRate.Size = new Size(361, 235);
            stockCard_ExchangeRate.SparklineThickness = 2.8F;
            stockCard_ExchangeRate.TabIndex = 17;
            stockCard_ExchangeRate.UpColor = Color.FromArgb(72, 199, 142);
            // 
            // Dashboard
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(31, 34, 40);
            Controls.Add(stockCard_InterestRate);
            Controls.Add(stockCard_ExchangeRate);
            Controls.Add(label1);
            Controls.Add(dataGridView1);
            Controls.Add(groupBox1);
            Controls.Add(stockCard_SnP);
            Controls.Add(stockCard_kospi);
            Controls.Add(valueTrackerCard_Rate);
            Controls.Add(valueTrackerCard_Deposits);
            Controls.Add(valueTrackerCard_TotalAsset);
            ForeColor = Color.FromArgb(225, 226, 228);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Dashboard";
            Size = new Size(1609, 824);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Controls.StockCard stockCard_SnP;
        private Controls.StockCard stockCard_kospi;
        private Controls.Cards.ValueTrackerCard valueTrackerCard_Rate;
        private Controls.Cards.ValueTrackerCard valueTrackerCard_Deposits;
        private Controls.Cards.ValueTrackerCard valueTrackerCard_TotalAsset;
        private GroupBox groupBox1;
        private DataGridView dataGridView1;
        private Label label1;
        private Controls.StockCard stockCard_InterestRate;
        private Controls.StockCard stockCard_ExchangeRate;
    }
}
