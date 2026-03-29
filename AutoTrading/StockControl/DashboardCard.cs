using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;

namespace StockControl
{
    // 차트 타입을 정의하는 열거형
    public enum IndicatorChartType
    {
        Sparkline,
        BarChart,
        AreaChart,
        Gauge
    }

    // --- [핵심] 게이지 데이터를 담는 모델 클래스 ---
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class GaugeData
    {
        [Description("게이지 하단에 표시될 텍스트입니다.")]
        public string Label { get; set; } = "RSI";

        [Description("바늘이 가리킬 값 (0 ~ 100)")]
        public float Value { get; set; } = 50f;

        [Description("게이지 배경 아크의 시작 색상(좌측)")]
        public Color StartColor { get; set; } = Color.FromArgb(180, 60, 60); // Dark Red

        [Description("게이지 배경 아크의 종료 색상(우측)")]
        public Color EndColor { get; set; } = Color.FromArgb(60, 180, 80); // Dark Green
    }

    public partial class DashboardCard : UserControl
    {
        // --- Properties ---
        [Category("Indicator Data")]
        public string Title { get; set; } = "주요 기술적 지표의 긍정적 시그널";

        [Category("Indicator Data")]
        public string Description { get; set; } = "보조지표가 과매수 구간에 진입했습니다.";

        [Category("Indicator Data")]
        public string BadgeStatusText { get; set; } = "Strong";

        [Category("Indicator Data")]
        public string BadgeValueText { get; set; } = "85";

        [Category("Indicator Data")]
        public Color BadgeColor { get; set; } = Color.FromArgb(28, 62, 45); // Dark Green

        [Category("Indicator Data")]
        public Color BadgeTextColor { get; set; } = Color.FromArgb(135, 215, 150); // Light Green

        // 스파크라인, 바, 에어리어 차트용 데이터
        [Category("Indicator Data")]
        public List<float> ChartData { get; set; } = new List<float> { 10, 8, 15, 12, 22, 18, 28, 25, 38, 35, 45, 40, 55 };

        // --- [핵심] 게이지 전용 다중 데이터 컬렉션 ---
        [Category("Indicator Data")]
        [Description("Gauge 차트를 선택했을 때 표시될 항목들을 정의합니다. (최대 3개 권장)")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<GaugeData> GaugeItems { get; set; } = new List<GaugeData>();

        // --- Chart Style & Appearance Properties ---
        [Category("Indicator Appearance")]
        public Color CardBackColor { get; set; } = Color.FromArgb(30, 33, 40);

        [Category("Indicator Appearance")]
        private IndicatorChartType _chartStyle = IndicatorChartType.Gauge; // 게이지 테스트를 위해 기본값 변경
        public IndicatorChartType ChartStyle
        {
            get { return _chartStyle; }
            set { _chartStyle = value; this.Invalidate(); }
        }

        [Category("Indicator Appearance")]
        public Color ChartColorPrimary { get; set; } = Color.FromArgb(100, 150, 255); // Blue

        [Category("Indicator Appearance")]
        public Color ChartColorSecondary { get; set; } = Color.FromArgb(120, 100, 150, 255);

        // Colors (Dark Theme Palette)
        private Color borderColor = Color.FromArgb(45, 50, 60);
        private Color titleColor = Color.White;
        private Color descColor = Color.FromArgb(150, 155, 165);

        public DashboardCard()
        {
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);

            this.Size = new Size(320, 160);
            this.Padding = new Padding(15);
            this.BackColor = Color.FromArgb(20, 22, 28);

            // 게이지 기본 데이터 세팅
            GaugeItems.Add(new GaugeData { Label = "RSI", Value = 75f });
            GaugeItems.Add(new GaugeData { Label = "MACD", Value = 85f });
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            int pad = 15;
            int badgeW = 60;
            int badgeH = 45;

            Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            Rectangle titleRect = new Rectangle(pad, 15, this.Width - (pad * 2), 25);

            // 게이지 모드일 경우 Description 높이를 0으로 만들어 차트 영역 극대화
            int descMaxH = (ChartStyle == IndicatorChartType.Gauge) ? 0 : 40;
            Rectangle descRect = new Rectangle(pad, this.Height - pad - descMaxH, this.Width - (pad * 2), descMaxH);

            int chartMarginTop = 12;
            int chartMarginBottom = (ChartStyle == IndicatorChartType.Gauge) ? 5 : 12;

            int middleY = titleRect.Bottom + chartMarginTop;
            int middleH = descRect.Top - middleY - chartMarginBottom;

            Rectangle badgeRect = new Rectangle(this.Width - badgeW - pad, middleY, badgeW, badgeH);
            Rectangle chartRect = new Rectangle(pad, middleY, this.Width - badgeW - (pad * 2) - 15, middleH);

            // 1. Draw Background
            using (GraphicsPath path = GetRoundedRect(rect, 12))
            using (SolidBrush bgBrush = new SolidBrush(CardBackColor))
            using (Pen borderPen = new Pen(borderColor, 1.5f))
            {
                g.FillPath(bgBrush, path);
                g.DrawPath(borderPen, path);
            }

            // 2. Draw Title
            using (Font titleFont = new Font("Malgun Gothic", 10.5f, FontStyle.Bold))
            using (SolidBrush titleBrush = new SolidBrush(titleColor))
            {
                StringFormat sf = new StringFormat() { Trimming = StringTrimming.EllipsisCharacter };
                g.DrawString(Title, titleFont, titleBrush, titleRect, sf);
            }

            // 3. Draw Badge
            DrawBadge(g, badgeRect);

            // 4. Draw Chart
            switch (ChartStyle)
            {
                case IndicatorChartType.Sparkline: DrawSparkline(g, chartRect); break;
                case IndicatorChartType.BarChart: DrawBarChart(g, chartRect); break;
                case IndicatorChartType.AreaChart: DrawAreaChart(g, chartRect); break;
                case IndicatorChartType.Gauge: DrawMultiGauge(g, chartRect); break;
            }

            // 5. Draw Description (게이지 모드가 아닐 때만)
            if (ChartStyle != IndicatorChartType.Gauge)
            {
                using (Font descFont = new Font("Malgun Gothic", 8.5f, FontStyle.Regular))
                using (SolidBrush descBrush = new SolidBrush(descColor))
                {
                    StringFormat sf = new StringFormat();
                    sf.Trimming = StringTrimming.EllipsisCharacter;
                    sf.Alignment = StringAlignment.Near;
                    sf.LineAlignment = StringAlignment.Far;

                    g.DrawString(Description, descFont, descBrush, descRect, sf);
                }
            }
        }

        private void DrawMultiGauge(Graphics g, Rectangle bounds)
        {
            if (GaugeItems == null || GaugeItems.Count == 0) return;

            // 최대 3개까지만 제한
            int count = Math.Min(GaugeItems.Count, 3);

            // [Fix 1] 게이지 간의 넉넉한 여백(Spacing)을 추가
            float spacing = 25f;
            float totalSpacing = spacing * (count - 1);
            float itemWidth = (bounds.Width - totalSpacing) / count;
            float penWidth = 8f;

            for (int i = 0; i < count; i++)
            {
                GaugeData item = GaugeItems[i];

                // 할당된 Bounding Box 영역 (spacing 포함)
                RectangleF itemBox = new RectangleF(bounds.X + i * (itemWidth + spacing), bounds.Y, itemWidth, bounds.Height);

                // [Fix 2] 펜 두께(Bleeding)를 고려하여 지름의 한계치를 더욱 안전하게 보수적으로 계산
                float safeWidth = itemBox.Width - (penWidth * 2);
                float safeHeight = (itemBox.Height - 25) * 2 - (penWidth * 2); // 25px는 라벨 여백

                float diameter = Math.Min(safeWidth, safeHeight);
                if (diameter <= 0) continue;

                float radius = diameter / 2f;

                float cx = itemBox.X + (itemBox.Width / 2f);
                float cy = itemBox.Bottom - 25;

                RectangleF arcBox = new RectangleF(cx - radius, cy - radius, diameter, diameter);

                // [Fix 3] 그라데이션 브러시 영역 확장
                // LinearGradientBrush가 arcBox 사이즈에 딱 맞으면 둥근 캡(Round Cap)이 박스 밖으로 
                // 나갈 때 색이 타일링되며 뚝 끊기는 현상이 발생합니다. 펜 두께만큼 브러시 영역을 확장합니다.
                RectangleF brushRect = arcBox;
                brushRect.Inflate(penWidth, penWidth);

                using (LinearGradientBrush brush = new LinearGradientBrush(brushRect, item.StartColor, item.EndColor, LinearGradientMode.Horizontal))
                using (Pen bgPen = new Pen(brush, penWidth))
                {
                    bgPen.StartCap = LineCap.Round;
                    bgPen.EndCap = LineCap.Round;
                    g.DrawArc(bgPen, arcBox, 180, 180);
                }

                // --- 2. 바늘(Needle) 렌더링 ---
                float value = Math.Max(0, Math.Min(100, item.Value));
                float angleDeg = 180f + (value / 100f) * 180f;
                float angleRad = (float)(angleDeg * Math.PI / 180.0);

                // 바늘 끝이 곡선 밖으로 튀어나가지 않도록 길이 안전 확보
                float needleLen = radius - penWidth - 2f;
                float needleBase = 4f;

                PointF tip = new PointF(cx + needleLen * (float)Math.Cos(angleRad), cy + needleLen * (float)Math.Sin(angleRad));
                PointF leftBase = new PointF(cx + needleBase * (float)Math.Cos(angleRad - Math.PI / 2), cy + needleBase * (float)Math.Sin(angleRad - Math.PI / 2));
                PointF rightBase = new PointF(cx + needleBase * (float)Math.Cos(angleRad + Math.PI / 2), cy + needleBase * (float)Math.Sin(angleRad + Math.PI / 2));

                using (SolidBrush needleBrush = new SolidBrush(Color.White))
                {
                    g.FillPolygon(needleBrush, new PointF[] { tip, rightBase, leftBase });
                }

                using (SolidBrush pivotBrush = new SolidBrush(Color.White))
                {
                    g.FillEllipse(pivotBrush, cx - 4f, cy - 4f, 8f, 8f);
                }

                // --- 3. 하단 라벨 렌더링 ---
                using (Font labelFont = new Font("Arial", 9.5f, FontStyle.Bold))
                using (SolidBrush labelBrush = new SolidBrush(Color.White))
                {
                    StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Near };
                    g.DrawString(item.Label, labelFont, labelBrush, new PointF(cx, cy + 8), sf);
                }
            }
        }

        private void DrawBadge(Graphics g, Rectangle bounds)
        {
            using (GraphicsPath badgePath = GetRoundedRect(bounds, 6))
            using (SolidBrush badgeBg = new SolidBrush(BadgeColor))
            {
                g.FillPath(badgeBg, badgePath);
            }

            using (Font statusFont = new Font("Arial", 8f, FontStyle.Regular))
            using (SolidBrush statusBrush = new SolidBrush(BadgeTextColor))
            {
                StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                RectangleF statusRect = new RectangleF(bounds.X, bounds.Y + 4, bounds.Width, bounds.Height / 2f);
                g.DrawString(BadgeStatusText, statusFont, statusBrush, statusRect, sf);
            }

            using (Font valueFont = new Font("Arial", 11f, FontStyle.Bold))
            using (SolidBrush valueBrush = new SolidBrush(Color.White))
            {
                StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                RectangleF valueRect = new RectangleF(bounds.X, bounds.Y + 20, bounds.Width, bounds.Height / 2f);
                g.DrawString(BadgeValueText, valueFont, valueBrush, valueRect, sf);
            }
        }

        private void DrawSparkline(Graphics g, Rectangle bounds)
        {
            if (ChartData == null || ChartData.Count < 2) return;

            float minY = ChartData.Min();
            float maxY = ChartData.Max();
            float rangeY = Math.Max(1, maxY - minY);
            float maxX = ChartData.Count - 1;

            PointF[] points = new PointF[ChartData.Count];
            float renderMargin = 2f;
            float renderH = bounds.Height - (renderMargin * 2);

            for (int i = 0; i < ChartData.Count; i++)
            {
                float px = bounds.X + (i / maxX) * bounds.Width;
                float py = bounds.Y + renderMargin + renderH - ((ChartData[i] - minY) / rangeY) * renderH;
                points[i] = new PointF(px, py);
            }

            using (Pen linePen = new Pen(ChartColorPrimary, 1.5f))
            {
                linePen.LineJoin = LineJoin.Round;
                g.DrawCurve(linePen, points, 0.3f);
            }
        }

        private void DrawAreaChart(Graphics g, Rectangle bounds)
        {
            if (ChartData == null || ChartData.Count < 2) return;

            float minY = ChartData.Min();
            float maxY = ChartData.Max();
            float rangeY = Math.Max(1, maxY - minY);
            float maxX = ChartData.Count - 1;

            PointF[] points = new PointF[ChartData.Count];
            float renderMargin = 2f;
            float renderH = bounds.Height - renderMargin;

            for (int i = 0; i < ChartData.Count; i++)
            {
                float px = bounds.X + (i / maxX) * bounds.Width;
                float py = bounds.Y + renderMargin + renderH - ((ChartData[i] - minY) / rangeY) * renderH;
                points[i] = new PointF(px, py);
            }

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddCurve(points, 0.3f);
                path.AddLine(points.Last().X, points.Last().Y, bounds.Right, bounds.Bottom);
                path.AddLine(bounds.Right, bounds.Bottom, bounds.Left, bounds.Bottom);
                path.AddLine(bounds.Left, bounds.Bottom, points.First().X, points.First().Y);

                Color colorTop = ChartColorSecondary;
                Color colorBottom = Color.FromArgb(0, ChartColorSecondary.R, ChartColorSecondary.G, ChartColorSecondary.B);
                RectangleF gradRect = new RectangleF(bounds.X, bounds.Y, bounds.Width, Math.Max(1, bounds.Height));

                using (LinearGradientBrush areaBrush = new LinearGradientBrush(gradRect, colorTop, colorBottom, LinearGradientMode.Vertical))
                {
                    g.FillPath(areaBrush, path);
                }
            }

            using (Pen linePen = new Pen(ChartColorPrimary, 1.5f))
            {
                linePen.LineJoin = LineJoin.Round;
                g.DrawCurve(linePen, points, 0.3f);
            }
        }

        private void DrawBarChart(Graphics g, Rectangle bounds)
        {
            if (ChartData == null || ChartData.Count == 0) return;

            float minY = ChartData.Min();
            float maxY = ChartData.Max();
            float rangeY = Math.Max(1, maxY - minY);

            float spacing = 1f;
            float barWidth = ((float)bounds.Width / ChartData.Count) - spacing;

            using (SolidBrush barBrush = new SolidBrush(ChartColorPrimary))
            {
                for (int i = 0; i < ChartData.Count; i++)
                {
                    float px = bounds.X + i * (barWidth + spacing);
                    float barH = ((ChartData[i] - Math.Min(0, minY)) / rangeY) * bounds.Height;
                    if (barH < 1) barH = 1;
                    float py = bounds.Y + bounds.Height - barH;

                    g.FillRectangle(barBrush, px, py, barWidth, barH);
                }
            }
        }

        private GraphicsPath GetRoundedRect(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(bounds.Location, size);
            GraphicsPath path = new GraphicsPath();

            if (radius == 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            path.AddArc(arc, 180, 90);
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }
    }
}