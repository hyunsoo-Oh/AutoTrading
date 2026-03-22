using System.ComponentModel;

namespace AutoTrading.Controls.Shell.SideBar
{
    public sealed class SideNavigationItem : UserControl
    {
        private readonly Panel _indicator;
        private readonly Label _label;
        private bool _isHovered;
        private bool _isSelected;
        private string _text = string.Empty;

        public SideNavigationItem()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);

            Height = 53;
            MinimumSize = new Size(120, 44);
            Margin = new Padding(0);
            Cursor = Cursors.Hand;
            BackColor = Color.White;

            _indicator = new Panel
            {
                Dock = DockStyle.Left,
                Width = 4,
                BackColor = AccentColor,
                Visible = false
            };

            _label = new Label
            {
                Dock = DockStyle.Fill,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(12, 5, 8, 5),
                Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point),
                ForeColor = NormalForeColor,
                BackColor = Color.Transparent
            };

            Controls.Add(_label);
            Controls.Add(_indicator);

            WireEvents(this);
            WireEvents(_label);

            ApplyVisualState();
        }

        [Category("Navigation")]
        [Description("선택/이벤트 식별에 사용되는 고유 키")]
        public string ItemKey { get; set; } = string.Empty;

        [Category("Navigation")]
        [Description("항목에 표시되는 텍스트")]
        public string ItemText
        {
            get => _text;
            set
            {
                _text = value;
                ApplyVisualState();
            }
        }

        [Category("Navigation")]
        [Description("현재 선택된 상태 여부")]
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                ApplyVisualState();
            }
        }

        [Category("Navigation")]
        [Description("선택 시 왼쪽 인디케이터 바 표시 여부")]
        public bool ShowSelectionIndicator { get; set; } = true;

        [Category("Navigation")]
        [Description("마우스 호버 시 배경 색상")]
        public Color HoverBackColor { get; set; } = Color.FromArgb(60, 62, 70);

        [Category("Navigation")]
        [Description("선택 상태 배경 색상")]
        public Color SelectedBackColor { get; set; } = Color.FromArgb(75, 56, 76);

        private Color _normalBackColor = Color.White;

        [Category("Navigation")]
        [Description("기본 배경 색상")]
        public Color NormalBackColor
        {
            get => _normalBackColor;
            set
            {
                _normalBackColor = value;
                if (_label is not null)
                    ApplyVisualState();
            }
        }

        [Category("Navigation")]
        [Description("선택 인디케이터 및 강조 색상")]
        public Color AccentColor { get; set; } = Color.FromArgb(40, 120, 255);

        [Category("Navigation")]
        [Description("비선택 상태 텍스트 색상")]
        public Color NormalForeColor { get; set; } = Color.FromArgb(225, 226, 229);

        [Category("Navigation")]
        [Description("선택 상태 텍스트 색상")]
        public Color SelectedForeColor { get; set; } = Color.FromArgb(225, 226, 229);

        public event EventHandler? ItemInvoked;

        private void WireEvents(Control control)
        {
            control.MouseEnter += (_, _) =>
            {
                _isHovered = true;
                ApplyVisualState();
            };
            control.MouseLeave += (_, _) =>
            {
                _isHovered = false;
                ApplyVisualState();
            };
            control.Click += (_, _) => ItemInvoked?.Invoke(this, EventArgs.Empty);
        }

        private void ApplyVisualState()
        {
            var showActive = _isHovered || _isSelected;

            var background = _isSelected
                ? SelectedBackColor
                : (_isHovered ? HoverBackColor : NormalBackColor);

            BackColor = background;
            _label.BackColor = background;

            _indicator.Visible = showActive && ShowSelectionIndicator;
            _indicator.BackColor = AccentColor;

            _label.ForeColor = _isSelected ? SelectedForeColor : NormalForeColor;
            _label.Text = showActive ? $" {_text}" : _text;
        }
    }
}
