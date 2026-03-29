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

namespace AutoTrading.Controls.Cards
{
    public partial class EmptyCard : UserControl
    {
        private Color _cardBackColor = Color.FromArgb(26, 28, 30);

        public EmptyCard()
        {
            InitializeComponent();
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.ResizeRedraw |
                ControlStyles.OptimizedDoubleBuffer, true);

            DoubleBuffered = true;
            Size = new Size(300, 150);
            BackColor = Color.Transparent;
        }

        [Category("Appearance")]
        [Description("카드 배경 색상")]
        public Color CardBackColor
        {
            get => _cardBackColor;
            set { _cardBackColor = value; Invalidate(); }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            DrawBackground(g);
        }

        private void DrawBackground(Graphics g)
        {
            using var path = CreateRoundRect(new Rectangle(0, 0, Width - 1, Height - 1), 8);
            using var brush = new SolidBrush(_cardBackColor);
            g.FillPath(brush, path);
        }

        private static GraphicsPath CreateRoundRect(Rectangle rect, int radius)
        {
            var path = new GraphicsPath();
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
