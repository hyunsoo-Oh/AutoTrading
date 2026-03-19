using AutoTrading.Controls.Cards;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoTrading.Controls
{
    public partial class ChartCard : UserControl
    {
        public enum UpDownDirection
        {
            Up, Down, Flat
        }

        private CardStyle _style = new CardStyle();
        private List<float> _points = new List<float>();

        private string _title = string.Empty;
        private float _value = 0f;
        private float _changeValue = 0f;
        private float _changeRate = 0f;

        private UpDownDirection _direction = UpDownDirection.Flat;

        private string _startTime = "09:00";
        private string _endTime = "15:00";
        public ChartCard()
        {
            InitializeComponent();

            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.SupportsTransparentBackColor, true);

            DoubleBuffered = true;
            Size = new Size(540, 300);
            BackColor = Color.Transparent;

        }

        [Category("Custom")]
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                Invalidate();
            }
        }

        [Category("Custom")]
        public float Value
        {
            get => _value;
            set
            {
                _value = value;
                Invalidate();
            }
        }

        [Category("Custom")]
        public float ChangeValue
        {
            get => _changeValue;
            set
            {
                _changeValue = value;
                Invalidate();
            }
        }

        [Category("Custom")]
        public float ChangeRate
        {
            get => _changeRate;
            set
            {
                _changeRate = value;
                Invalidate();
            }
        }

        [Category("Custom")]
        public UpDownDirection Direction
        {
            get => _direction;
            set
            {
                _direction = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 그래프 데이터
        /// </summary>
        /// <param name="values"></param>
        public void SetPoints(IEnumerable<float> values)
        {
            _points = values.ToList();
            Invalidate();
        }

        /// <summary>
        /// 실시간 데이터 추가
        /// </summary>
        /// <param name="value"></param>
        public void AddPoint(float value)
        {
            _points.Add(value);

            if (_points.Count > 200)
                _points.RemoveAt(0);

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            Rectangle cardRect = new Rectangle(0, 0, Width - 1, Height - 1);

            DrawCard(g, cardRect);
            DrawTexts(g);
            DrawChangeBadge(g);
            DrawChart(g, cardRect);
        }

        /// <summary>
        /// 카드 그리기
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        private void DrawCard(Graphics g, Rectangle rect)
        {
            using GraphicsPath path = CreateRoundRect(rect, _style.CornerRadius);
            using SolidBrush brush = new SolidBrush(_style.CardBackColor);

            g.FillPath(brush, path);
        }

        /// <summary>
        /// 글씨 작성
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        private void DrawTexts(Graphics g)
        {
            int left = 48;
            int top = 34;

            using Font titleFont = new Font("Segoe UI", 18, FontStyle.Bold);
            using Font valueMainFont = new Font("Segoe UI", 34, FontStyle.Bold);
            using Font valueFractionFont = new Font("Segoe UI", 22, FontStyle.Bold);

            using SolidBrush titleBrush = new SolidBrush(_style.TitleColor);
            using SolidBrush valueBrush = new SolidBrush(_style.ValueColor);

            g.DrawString(_title, titleFont, titleBrush, left, top);

            string full = _value.ToString("N2");
            string[] parts = full.Split('.');

            string mainPart = parts[0];
            string fracPart = parts.Length > 1 ? "." + parts[1] : "";

            float valueY = top + 42f;
            g.DrawString(mainPart, valueMainFont, valueBrush, left, valueY);

            SizeF mainSize = g.MeasureString(mainPart, valueMainFont);
            g.DrawString(fracPart, valueFractionFont, valueBrush, left + mainSize.Width - 4f, valueY + 10f);
        }


        /// <summary>
        /// 그래프 그리기
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        private void DrawChart(Graphics g, Rectangle rect)
        {
            if (_points.Count < 2)
                return;

            float min = _points.Min();
            float max = _points.Max();
            float range = Math.Max(1f, max - min);

            // 카드 오른쪽 아래 영역에 길게 배치
            int chartLeft = rect.Width / 2 + 10;
            int chartTop = rect.Height - 115;
            int chartWidth = rect.Width - chartLeft - 24;
            int chartHeight = 80;

            PointF[] pts = new PointF[_points.Count];

            for (int i = 0; i < _points.Count; i++)
            {
                float x = chartLeft + (chartWidth * i / (float)(_points.Count - 1));
                float normalized = (_points[i] - min) / range;
                float y = chartTop + chartHeight - normalized * chartHeight;
                pts[i] = new PointF(x, y);
            }

            using Pen pen = new Pen(GetChartColor(), 3f)
            {
                StartCap = LineCap.Round,
                EndCap = LineCap.Round,
                LineJoin = LineJoin.Round
            };

            if (pts.Length >= 3)
            {
                using GraphicsPath path = new GraphicsPath();
                path.AddCurve(pts, 0.35f);
                g.DrawPath(pen, path);
            }
            else
            {
                g.DrawLines(pen, pts);
            }

            PointF last = pts[^1];

            using SolidBrush haloBrush = new SolidBrush(Color.FromArgb(70, GetChartColor()));
            using SolidBrush dotBrush = new SolidBrush(GetChartColor());

            g.FillEllipse(haloBrush, last.X - 8, last.Y - 8, 16, 16);
            g.FillEllipse(dotBrush, last.X - 3.5f, last.Y - 3.5f, 7, 7);
        }

        private void DrawChangeBadge(Graphics g)
        {
            int left = 72;
            int top = 175;
            int badgeWidth = 185;
            int badgeHeight = 34;

            Rectangle badgeRect = new Rectangle(left, top, badgeWidth, badgeHeight);

            using GraphicsPath path = CreateRoundRect(badgeRect, badgeHeight / 2);
            using SolidBrush bgBrush = new SolidBrush(_style.BadgeBackColor);
            g.FillPath(bgBrush, path);

            Color color = GetChartColor();
            using SolidBrush brush = new SolidBrush(color);
            using Font font = new Font("Segoe UI", 10, FontStyle.Bold);

            Point[] arrow;
            if (_direction == UpDownDirection.Up)
            {
                arrow = new[]
                {
            new Point(left + 14, top + 22),
            new Point(left + 20, top + 12),
            new Point(left + 26, top + 22),
        };
            }
            else if (_direction == UpDownDirection.Down)
            {
                arrow = new[]
                {
            new Point(left + 14, top + 12),
            new Point(left + 20, top + 22),
            new Point(left + 26, top + 12),
        };
            }
            else
            {
                arrow = new[]
                {
            new Point(left + 14, top + 17),
            new Point(left + 26, top + 17),
            new Point(left + 26, top + 19),
            new Point(left + 14, top + 19),
        };
            }

            g.FillPolygon(brush, arrow);

            string text = $"{Math.Abs(_changeValue):N2} ({Math.Abs(_changeRate):N2}%)";
            g.DrawString(text, font, brush, left + 34, top + 8);
        }

        /// <summary>
        /// 차트 색상
        /// </summary>
        /// <returns></returns>
        private Color GetChartColor()
        {
            return _direction switch
            {
                UpDownDirection.Up => _style.ChartUpColor,
                UpDownDirection.Down => _style.ChartDownColor,
                _ => _style.ChartFlatColor
            };
        }

        /// <summary>
        /// RoundRect
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        private GraphicsPath CreateRoundRect(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();

            int d = radius * 2;

            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);

            path.CloseFigure();

            return path;
        }
    }
}
