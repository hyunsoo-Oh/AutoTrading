using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl
{
    /// <summary>
    /// 금융 대시보드 스타일의 커스텀 TabControl입니다.
    /// GDI+ 기반의 UserPaint를 사용하여 고성능 렌더링 및 DoubleBuffering을 지원합니다.
    /// </summary>
    public class DarkTabControl : TabControl
    {
        // --- 디자인 토큰 (제공된 이미지의 색상 값 기준) ---
        private readonly Color _bgColor = Color.FromArgb(13, 14, 18);          // 전체 배경색
        private readonly Color _tabActiveColor = Color.FromArgb(26, 33, 46);   // 활성화된 탭 상단 색상
        private readonly Color _tabInactiveColor = Color.FromArgb(22, 24, 29); // 비활성화된 탭 색상
        private readonly Color _accentColor = Color.FromArgb(0, 212, 255);     // 네온 블루 액센트 (상단 라인)
        private readonly Color _borderColor = Color.FromArgb(40, 42, 48);      // 경계선 색상
        private readonly Color _textColor = Color.FromArgb(200, 200, 200);     // 기본 텍스트 색상
        private readonly Color _activeTextColor = Color.White;                // 활성 탭 텍스트 색상

        public DarkTabControl()
        {
            // 커스텀 페인팅 및 플리커(Flicker) 방지를 위한 제어 스타일 설정
            SetStyle(ControlStyles.AllPaintingInWmPaint |  // WM_ERASEBKGND 무시하여 깜빡임 감소
                     ControlStyles.UserPaint |             // OS 기본 렌더링 대신 직접 그리기
                     ControlStyles.ResizeRedraw |          // 크기 조정 시 다시 그리기
                     ControlStyles.OptimizedDoubleBuffer, true); // 메모리 버퍼를 사용한 이중 버퍼링

            this.DoubleBuffered = true;
            this.SizeMode = TabSizeMode.Fixed;             // 탭 크기를 고정하여 레이아웃 일관성 유지
            this.ItemSize = new Size(120, 40);             // 탭 헤더 기본 사이즈
            this.Padding = new Point(20, 0);               // 내부 여백
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // 렌더링 품질 설정 (안티앨리어싱 및 텍스트 선명도)
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            // 1. 전체 배경 렌더링
            using (SolidBrush bgBrush = new SolidBrush(_bgColor))
            {
                g.FillRectangle(bgBrush, this.ClientRectangle);
            }

            // 2. 탭 헤더(Tab Headers) 렌더링
            for (int i = 0; i < TabCount; i++)
            {
                Rectangle tabRect = GetTabRect(i);
                bool isActive = (SelectedIndex == i);

                // 탭 배경 처리
                if (isActive)
                {
                    // 활성 탭: 상단에서 하단으로 어두워지는 선형 그라데이션 적용
                    using (LinearGradientBrush lgb = new LinearGradientBrush(
                        tabRect, _tabActiveColor, Color.FromArgb(17, 20, 27), LinearGradientMode.Vertical))
                    {
                        g.FillRectangle(lgb, tabRect);
                    }

                    // 상단 네온 블루 액센트 라인 (2px)
                    using (Pen accentPen = new Pen(_accentColor, 2))
                    {
                        g.DrawLine(accentPen, tabRect.Left, tabRect.Top, tabRect.Right, tabRect.Top);
                    }

                    // 은은한 글로우(Glow) 효과 시뮬레이션
                    using (SolidBrush glowBrush = new SolidBrush(Color.FromArgb(40, _accentColor)))
                    {
                        g.FillRectangle(glowBrush, new Rectangle(tabRect.Left, tabRect.Top, tabRect.Width, 4));
                    }
                }
                else
                {
                    // 비활성 탭: 단색 배경
                    using (SolidBrush inactiveBrush = new SolidBrush(_tabInactiveColor))
                    {
                        g.FillRectangle(inactiveBrush, tabRect);
                    }
                }

                // 탭 좌우 경계선 렌더링
                using (Pen borderPen = new Pen(_borderColor))
                {
                    g.DrawLine(borderPen, tabRect.Left, tabRect.Top, tabRect.Left, tabRect.Bottom);
                    g.DrawLine(borderPen, tabRect.Right, tabRect.Top, tabRect.Right, tabRect.Bottom);
                }

                // 탭 텍스트 렌더링 (GDI+ TextRenderer 사용으로 가독성 확보)
                string tabText = TabPages[i].Text;
                TextRenderer.DrawText(g, tabText, Font, tabRect,
                    isActive ? _activeTextColor : Color.Gray,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }

            // 3. 콘텐츠 영역 보더(Border) 및 구분선 렌더링
            if (TabCount > 0)
            {
                Rectangle contentRect = this.DisplayRectangle;
                contentRect.Inflate(1, 1); // 보더가 잘리지 않도록 미세 조정

                using (Pen borderPen = new Pen(_borderColor))
                {
                    g.DrawRectangle(borderPen, contentRect);
                }

                // 탭 헤더와 본문 사이를 구분하는 수평선
                using (Pen borderPen = new Pen(_borderColor))
                {
                    g.DrawLine(borderPen, 0, ItemSize.Height, Width, ItemSize.Height);
                }
            }
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            if (e.Control is TabPage page)
            {
                // 추가되는 TabPage의 배경색과 글자색을 대시보드 테마에 맞게 자동 설정
                page.BackColor = Color.FromArgb(17, 20, 27); // 본문 영역 배경색
                page.ForeColor = _textColor;
            }
        }
    }
}
