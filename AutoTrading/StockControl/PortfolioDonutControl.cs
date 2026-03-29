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
    /// <summary>
    /// 목적: 포트폴리오 자산 배분(Asset Allocation) 시각화를 위한 커스텀 컨트롤
    /// 주요 기능: GDI+ 기반 도넛 차트 렌더링, 다크 모드 UI, 실시간 데이터 바인딩
    /// </summary>
    public partial class PortfolioDonutControl : UserControl
    {
        #region Data Models

        /// <summary>
        /// 개별 자산 항목을 정의하는 데이터 모델 클래스
        /// </summary>
        public class PortfolioItem
        {
            /// <summary> 자산명 (예: 주식, 현금, 채권) </summary>
            public string Name { get; set; } = "새 자산";

            /// <summary> 자산 가치 또는 비중 수치 </summary>
            public double Value { get; set; } = 10.0;

            /// <summary> 차트 및 범례에 사용될 테마 색상 </summary>
            public Color ItemColor { get; set; } = Color.FromArgb(0, 192, 239);

            /// <summary>
            /// WinForms Designer의 XML/Code 직렬화(Serialization)를 위해 반드시 필요한 기본 생성자
            /// </summary>
            public PortfolioItem() { }

            public PortfolioItem(string name, double value, Color color)
            {
                Name = name;
                Value = value;
                ItemColor = color;
            }
        }

        #endregion

        #region Fields & Properties

        /// <summary> 내부 자산 리스트 </summary>
        private List<PortfolioItem> _items = new List<PortfolioItem>();

        /// <summary> 디자인 가이드라인에 따른 짙은 무채색 배경 (Anthracite계열) </summary>
        private readonly Color _backColorDark = Color.FromArgb(18, 18, 18);

        /// <summary> 도넛의 두께 비율 (0.45 = 전체 반지름의 45%가 고리의 두께) </summary>
        private float _donutThickness = 0.45f;

        /// <summary>
        /// 외부에서 접근 가능한 자산 아이템 컬렉션
        /// DesignerSerializationVisibility.Content: 내부 리스트의 '내용물'을 디자이너가 인식하도록 설정
        /// </summary>
        [Category("Data")]
        [Description("차트에 표시할 자산 항목 리스트입니다.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<PortfolioItem> Items
        {
            get => _items;
            set { _items = value; Invalidate(); }
        }

        #endregion

        #region Constructor

        public PortfolioDonutControl()
        {
            // [UI Integrity] 컨트롤의 렌더링 무결성을 위한 스타일 설정
            // OptimizedDoubleBuffer: 픽셀 데이터를 메모리 버퍼에서 계산 후 출력하여 깜빡임(Flickering) 방지
            // ResizeRedraw: 컨트롤 크기 변경 시 OnPaint 자동 호출
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint |
                          ControlStyles.DoubleBuffer |
                          ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.ResizeRedraw, true);

            this.BackColor = _backColorDark;
            this.Size = new Size(500, 300);
            this.MinimumSize = new Size(200, 150);

            // 주의: 생성자 내 Items.Add()는 디자이너 직렬화 시 중복 코드 생성을 유발하므로 배제함
        }

        #endregion

        #region Core Rendering Engine (GDI+)

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            // [Rendering Quality] 고품질 그래픽 설정을 위한 안티앨리어싱 적용
            g.SmoothingMode = SmoothingMode.AntiAlias; // 곡선 테두리 평활화
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit; // 서브픽셀 폰트 가독성 향상
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            // [Data Virtualization] 실제 데이터가 없을 경우 디자이너 상에서 '미리보기'를 제공하기 위한 로직
            List<PortfolioItem> displayItems = _items;
            if (_items == null || _items.Count == 0)
            {
                displayItems = new List<PortfolioItem>
                {
                    new PortfolioItem("현금", 100, Color.FromArgb(0, 192, 239)),
                };
            }

            float total = (float)displayItems.Sum(x => x.Value);
            if (total <= 0) total = 1.0f; // 제로 디비전(Zero-division) 방지

            // 1. 레이아웃 기하학(Geometry) 계산
            int padding = 30;
            float chartAreaWidth = this.Width * 0.45f; // 전체 가로 영역 중 45%를 차트 공간으로 할당
            float chartSize = Math.Max(50, Math.Min(chartAreaWidth, this.Height) - (padding * 2));

            // 차트를 컨트롤 왼쪽 중앙에 배치하기 위한 사각형 정의
            RectangleF chartRect = new RectangleF(
                padding,
                (this.Height - chartSize) / 2f,
                chartSize,
                chartSize
            );

            float startAngle = -90f; // 12시 방향(상단)에서 시작하도록 설정

            // 2. 파이 세그먼트 드로잉 (Z-Order: 하단)
            foreach (var item in displayItems)
            {
                float sweepAngle = (float)(item.Value / total) * 360f;
                if (sweepAngle <= 0) continue;

                using (SolidBrush brush = new SolidBrush(item.ItemColor))
                {
                    g.FillPie(brush, chartRect.X, chartRect.Y, chartRect.Width, chartRect.Height, startAngle, sweepAngle);
                }
                startAngle += sweepAngle;
            }

            // 3. 중앙 홀(Hole) 생성 (Z-Order: 중간)
            // 도넛 효과를 위해 중앙을 배경색 원으로 덮어씌움
            float holeSize = chartRect.Width * (1 - _donutThickness);
            RectangleF holeRect = new RectangleF(
                chartRect.X + (chartRect.Width - holeSize) / 2f,
                chartRect.Y + (chartRect.Height - holeSize) / 2f,
                holeSize, holeSize
            );
            using (SolidBrush holeBrush = new SolidBrush(this.BackColor))
            {
                g.FillEllipse(holeBrush, holeRect);
            }

            // 4. 차트 내부 텍스트(%) 드로잉 (Z-Order: 상단)
            startAngle = -90f;
            foreach (var item in displayItems)
            {
                float sweepAngle = (float)(item.Value / total) * 360f;

                // 가독성을 위해 각도가 8도 이상인 세그먼트에만 수치 표시
                if (sweepAngle >= 8)
                {
                    // 삼각함수를 이용한 텍스트 배치 좌표 계산
                    // 텍스트 위치는 도넛 링의 정확한 '중앙 반지름' 지점에 위치함
                    float midAngle = startAngle + (sweepAngle / 2f);
                    double rad = midAngle * Math.PI / 180.0; // 도 단위를 라디안으로 변환
                    float textDist = chartRect.Width * (2 - _donutThickness) / 4f;

                    // 극좌표계를 직교좌표계(X, Y)로 변환
                    float tx = (chartRect.X + chartRect.Width / 2) + (float)(Math.Cos(rad) * textDist);
                    float ty = (chartRect.Y + chartRect.Height / 2) + (float)(Math.Sin(rad) * textDist);

                    string percentText = $"{(item.Value / total * 100):0}%";
                    using (Font innerFont = new Font("Malgun Gothic", 9, FontStyle.Bold))
                    {
                        SizeF tSize = g.MeasureString(percentText, innerFont);
                        // 계산된 좌표에서 텍스트 크기의 절반을 빼서 '중앙 정렬' 수행
                        g.DrawString(percentText, innerFont, Brushes.White, tx - (tSize.Width / 2), ty - (tSize.Height / 2));
                    }
                }
                startAngle += sweepAngle;
            }

            // 5. 우측 범례(Legend) 및 데이터 값 드로잉
            float legendX = chartRect.Right + 40;
            float legendStartY = chartRect.Y + 10;
            float legendEndY = chartRect.Bottom - 10;
            float availableHeight = legendEndY - legendStartY;

            // 아이템 개수에 따라 가용 높이 내에서 등간격(Proportional Spacing) 계산
            float itemSpacing = displayItems.Count > 1 ? availableHeight / (displayItems.Count - 1) : 0;
            if (displayItems.Count == 1) legendStartY = chartRect.Y + (chartRect.Height / 2) - 10;

            for (int i = 0; i < displayItems.Count; i++)
            {
                var item = displayItems[i];
                float currentY = legendStartY + (i * itemSpacing);

                using (Font font = new Font("Malgun Gothic", 12, FontStyle.Bold))
                using (SolidBrush textBrush = new SolidBrush(item.ItemColor))
                {
                    // 자산명 출력
                    g.DrawString(item.Name, font, textBrush, legendX, currentY - 12);

                    // 백분율 수치 우측 정렬(Right Aligned) 처리
                    string valText = $"{(item.Value / total * 100):0}%";
                    SizeF valSize = g.MeasureString(valText, font);
                    g.DrawString(valText, font, textBrush, this.Width - padding - valSize.Width, currentY - 12);
                }
            }
        }

        #endregion

        #region Public Interface

        /// <summary>
        /// 외부 소스에서 데이터를 대량으로 주입(Injection)할 때 사용하는 메서드
        /// </summary>
        /// <param name="newItems">표시할 자산 목록</param>
        public void SetData(IEnumerable<PortfolioItem> newItems)
        {
            this._items = newItems.ToList();
            this.Invalidate(); // UI 갱신 요청
        }

        #endregion
    }
}