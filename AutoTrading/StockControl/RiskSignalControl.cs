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

namespace StockControl
{
    /// <summary>
    /// RiskLevel 열거형 정의
    /// </summary>
    public enum RiskLevel
    {
        High,
        Mid,
        Low
    }

    /// <summary>
    /// 개별 리스크 항목 데이터 구조
    /// </summary>
    public class RiskItem
    {
        public string Label { get; set; }
        public RiskLevel Level { get; set; }

        public RiskItem(string label, RiskLevel level)
        {
            Label = label;
            Level = level;
        }
    }

    /// <summary>
    /// 리스크 신호(Risk Signal) 커스텀 UserControl
    /// </summary>
    public partial class RiskSignalControl : UserControl
    {
        // UI 색상 상수 정의 (Dark Theme)
        private readonly Color _bgColor = Color.FromArgb(26, 28, 32);
        private readonly Color _borderColor = Color.FromArgb(45, 48, 54);
        private readonly Color _treeColor = Color.FromArgb(75, 80, 87);
        private readonly Color _labelColor = Color.FromArgb(180, 185, 195);
        private readonly Color _titleColor = Color.FromArgb(220, 225, 230);
        private readonly Color _judgmentColor = Color.FromArgb(249, 115, 22); // Orange

        public List<RiskItem> Risks { get; set; }
        public string Judgment { get; set; } = "단기 과열 주의";

        public RiskSignalControl()
        {
            this.DoubleBuffered = true; // 깜빡임 방지 (Flicker Prevention)
            this.Size = new Size(240, 220);
            this.BackColor = _bgColor;

            // 기본 데이터 초기화
            Risks = new List<RiskItem>
            {
                new RiskItem("과열", RiskLevel.High),
                new RiskItem("변동성", RiskLevel.Mid),
                new RiskItem("저항대", RiskLevel.Mid),
                new RiskItem("이벤트", RiskLevel.Low)
            };
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias; // 안티앨리어싱 활성화
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            // 1. 배경 및 테두리 렌더링
            using (GraphicsPath path = GetRoundedRectanglePath(this.ClientRectangle, 8))
            {
                g.FillPath(new SolidBrush(_bgColor), path);
                g.DrawPath(new Pen(_borderColor, 1), path);
            }

            // 2. 헤더(타이틀) 렌더링
            Font titleFont = new Font("Malgun Gothic", 10, FontStyle.Bold);
            g.DrawString("리스크 신호", titleFont, new SolidBrush(_titleColor), 16, 16);

            // 3. 리스크 항목 및 트리 브래킷 렌더링
            int startX = 35;
            int startY = 55;
            int itemHeight = 28;
            Font itemFont = new Font("Malgun Gothic", 9, FontStyle.Regular);
            Pen treePen = new Pen(_treeColor, 1);

            for (int i = 0; i < Risks.Count; i++)
            {
                int currentY = startY + (i * itemHeight);
                bool isLast = (i == Risks.Count - 1);

                // 트리 라인 브래킷 그리기 (Tree-line Connector)
                g.DrawLine(treePen, startX - 10, currentY - (i == 0 ? 0 : itemHeight / 2), startX - 10, currentY + (isLast ? 0 : itemHeight / 2));
                g.DrawLine(treePen, startX - 10, currentY, startX - 2, currentY);

                // 라벨 텍스트
                g.DrawString(Risks[i].Label, itemFont, new SolidBrush(_labelColor), startX, currentY - 7);

                // 상태 점(Dot) 및 레벨 텍스트
                Color statusColor = GetColorByLevel(Risks[i].Level);
                g.FillEllipse(new SolidBrush(statusColor), this.Width - 65, currentY - 3, 7, 7);
                g.DrawString(Risks[i].Level.ToString(), new Font("Consolas", 8.5f, FontStyle.Bold), new SolidBrush(statusColor), this.Width - 53, currentY - 7);
            }

            // 4. 하단 판단(Judgment) 섹션
            int separatorY = this.Height - 55;
            g.DrawLine(new Pen(Color.FromArgb(40, _borderColor), 1), 15, separatorY, this.Width - 15, separatorY);

            Font subTitleFont = new Font("Malgun Gothic", 8, FontStyle.Italic);
            string subTitle = "현재 판단";
            float subTitleWidth = g.MeasureString(subTitle, subTitleFont).Width;
            g.DrawString(subTitle, subTitleFont, new SolidBrush(Color.Gray), (this.Width - subTitleWidth) / 2, separatorY + 8);

            Font judgmentFont = new Font("Malgun Gothic", 9, FontStyle.Bold);
            float judgmentWidth = g.MeasureString(Judgment, judgmentFont).Width;
            g.DrawString(Judgment, judgmentFont, new SolidBrush(_judgmentColor), (this.Width - judgmentWidth) / 2, separatorY + 24);
        }

        private Color GetColorByLevel(RiskLevel level)
        {
            switch (level)
            {
                case RiskLevel.High: return Color.FromArgb(239, 68, 68); // Red-500
                case RiskLevel.Mid: return Color.FromArgb(245, 158, 11);  // Amber-500
                case RiskLevel.Low: return Color.FromArgb(59, 130, 246);   // Blue-500
                default: return Color.Gray;
            }
        }

        private GraphicsPath GetRoundedRectanglePath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;
            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
