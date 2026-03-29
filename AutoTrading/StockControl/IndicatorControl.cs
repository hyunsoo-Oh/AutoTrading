using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Globalization;

namespace StockControl;

[DefaultEvent(nameof(PercentChanged))]
public partial class IndicatorControl : UserControl
{
    private const float DesignWidth = 320f;
    private const float DesignHeight = 260f;
    private const float CenterX = 160f;
    private const float CenterY = 160f;
    private const float OuterRadius = 160f;
    private const float InnerRadius = 80f;
    private const float LabelRadius = 124f;
    private const float NeedleLength = 136f;
    private const float HubRadius = 12f;

    private float _percent = 75f;
    private float _minimum = 0f;
    private float _maximum = 100f;

    private int _gageCount = 3;
    private readonly List<string> _gageTexts = ["Downtrend", "Range", "Uptrend"];
    private readonly List<float> _gageBoundaries = [0f, 33f, 66f, 100f];
    private readonly List<int> _gageColorArgb =
    [
        Color.FromArgb(109, 45, 45).ToArgb(),
        Color.FromArgb(67, 74, 79).ToArgb(),
        Color.FromArgb(21, 97, 50).ToArgb()
    ];

    private string _statusText = "\uC0C1\uC2B9 \uCD94\uC138";
    private float _statusTextY = 220f;
    private bool _autoStatusFromRange = true;
    private Color _needleColor = Color.FromArgb(160, 164, 168);
    private Color _needleHubColor = Color.FromArgb(11, 77, 36);
    private Color _labelTextColor = Color.White;
    private Color _statusTextColor = Color.White;

    public IndicatorControl()
    {
        InitializeComponent();

        SetStyle(
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.OptimizedDoubleBuffer |
            ControlStyles.ResizeRedraw |
            ControlStyles.SupportsTransparentBackColor,
            true);

        BackColor = Color.Transparent;
    }

    [Category("Gauge")]
    [Description("Needle position value.")]
    [DefaultValue(75f)]
    public float Percent
    {
        get => _percent;
        set
        {
            var clamped = Math.Clamp(value, Minimum, Maximum);
            if (Math.Abs(_percent - clamped) < float.Epsilon)
            {
                return;
            }

            _percent = clamped;
            Invalidate();
            PercentChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    [Category("Gauge")]
    [DefaultValue(0f)]
    public float Minimum
    {
        get => _minimum;
        set
        {
            _minimum = value;
            if (_maximum < _minimum)
            {
                _maximum = _minimum;
            }

            ResizeGaugeBoundaries();
            Percent = _percent;
            Invalidate();
        }
    }

    [Category("Gauge")]
    [DefaultValue(100f)]
    public float Maximum
    {
        get => _maximum;
        set
        {
            _maximum = value;
            if (_maximum < _minimum)
            {
                _minimum = _maximum;
            }

            ResizeGaugeBoundaries();
            Percent = _percent;
            Invalidate();
        }
    }

    [Category("Gauge")]
    [Description("게이지 개수")]
    [DefaultValue(3)]
    public int GageCount
    {
        get => _gageCount;
        set
        {
            if (value < 0)
            {
                value = 0;
            }

            if (_gageCount == value)
            {
                return;
            }

            _gageCount = value;
            ResizeGaugeTextList();
            ResizeGaugeColorList();
            ResizeGaugeBoundaries();
            RebuildGaugeControls();
            Invalidate();
        }
    }

    [Category("Gauge")]
    [Description("각 구간 문구")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public List<string> GageTexts => _gageTexts;

    [Category("Gauge")]
    [Description("각 구간 경계값(개수는 GageCount + 1). 예: 0,33,66,100")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public List<float> GageBoundaries => _gageBoundaries;

    [Category("Gauge")]
    [Description("각 구간 색상 ARGB(int). 예: Color.FromArgb(...).ToArgb() 값")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public List<int> GageColorArgb => _gageColorArgb;

    [Category("Gauge")]
    [Description("Bottom status text.")]
    [DefaultValue("\uC0C1\uC2B9 \uCD94\uC138")]
    public string StatusText
    {
        get => _statusText;
        set
        {
            _statusText = value ?? string.Empty;
            Invalidate();
        }
    }

    [Category("Gauge")]
    [Description("StatusText Y position. Smaller value moves text upward.")]
    [DefaultValue(220f)]
    public float StatusTextY
    {
        get => _statusTextY;
        set
        {
            _statusTextY = value;
            Invalidate();
        }
    }

    [Category("Gauge")]
    [DefaultValue(true)]
    public bool AutoStatusFromRange
    {
        get => _autoStatusFromRange;
        set
        {
            _autoStatusFromRange = value;
            Invalidate();
        }
    }

    [Category("Appearance")]
    [DefaultValue(typeof(Color), "160, 164, 168")]
    public Color NeedleColor
    {
        get => _needleColor;
        set
        {
            _needleColor = value;
            Invalidate();
        }
    }

    [Category("Appearance")]
    [DefaultValue(typeof(Color), "11, 77, 36")]
    public Color NeedleHubColor
    {
        get => _needleHubColor;
        set
        {
            _needleHubColor = value;
            Invalidate();
        }
    }

    [Category("Appearance")]
    [DefaultValue(typeof(Color), "White")]
    public Color LabelTextColor
    {
        get => _labelTextColor;
        set
        {
            _labelTextColor = value;
            Invalidate();
        }
    }

    [Category("Appearance")]
    [DefaultValue(typeof(Color), "White")]
    public Color StatusTextColor
    {
        get => _statusTextColor;
        set
        {
            _statusTextColor = value;
            Invalidate();
        }
    }

    // Backward compatibility (hidden in PropertyGrid)
    [Browsable(false)]
    public string RangeDefinitions
    {
        get => string.Join(",", Enumerable.Range(0, Math.Max(0, GageCount))
            .Select(i => $"{GetBoundaryValue(i).ToString(CultureInfo.InvariantCulture)}-{GetBoundaryValue(i + 1).ToString(CultureInfo.InvariantCulture)}"));
        set
        {
            ParseRangeDefinitions(value);
            RebuildGaugeControls();
            Invalidate();
        }
    }

    [Browsable(false)]
    public string SegmentLabels
    {
        get => string.Join(",", GageTexts);
        set
        {
            _gageTexts.Clear();
            _gageTexts.AddRange(ParseList(value));
            GageCount = _gageTexts.Count;
            Invalidate();
        }
    }

    [Browsable(false)]
    public string SegmentColors
    {
        get => string.Join(",", _gageColorArgb.Select(x => x.ToString(CultureInfo.InvariantCulture)));
        set
        {
            _gageColorArgb.Clear();
            foreach (var token in ParseList(value))
            {
                if (int.TryParse(token, NumberStyles.Integer, CultureInfo.InvariantCulture, out var argb))
                {
                    _gageColorArgb.Add(argb);
                    continue;
                }

                try
                {
                    _gageColorArgb.Add(ColorTranslator.FromHtml(token).ToArgb());
                }
                catch
                {
                    var named = Color.FromName(token);
                    if (named.A > 0 || string.Equals(token, "Transparent", StringComparison.OrdinalIgnoreCase))
                    {
                        _gageColorArgb.Add(named.ToArgb());
                    }
                }
            }

            ResizeGaugeColorList();
            Invalidate();
        }
    }

    [Category("Action")]
    public event EventHandler? PercentChanged;

    protected override void OnPaintBackground(PaintEventArgs pevent)
    {
        if (Parent is null || BackColor.A != 0)
        {
            base.OnPaintBackground(pevent);
            return;
        }

        var state = pevent.Graphics.Save();
        pevent.Graphics.TranslateTransform(-Left, -Top);
        var parentRect = new Rectangle(Point.Empty, Parent.ClientSize);
        using var args = new PaintEventArgs(pevent.Graphics, parentRect);
        InvokePaintBackground(Parent, args);
        InvokePaint(Parent, args);
        pevent.Graphics.Restore(state);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        if (ClientSize.Width <= 0 || ClientSize.Height <= 0)
        {
            return;
        }

        ResizeGaugeTextList();
        ResizeGaugeColorList();
        ResizeGaugeBoundaries();

        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

        var scale = Math.Min(ClientSize.Width / DesignWidth, ClientSize.Height / DesignHeight);
        var offsetX = (ClientSize.Width - (DesignWidth * scale)) / 2f;
        var offsetY = (ClientSize.Height - (DesignHeight * scale)) / 2f;

        var state = e.Graphics.Save();
        e.Graphics.TranslateTransform(offsetX, offsetY);
        e.Graphics.ScaleTransform(scale, scale);

        var segments = BuildSegments();
        DrawSegments(e.Graphics, segments);
        DrawLabels(e.Graphics, segments);
        DrawNeedle(e.Graphics);
        DrawStatus(e.Graphics, segments);

        e.Graphics.Restore(state);
    }

    private sealed record Segment(float Start, float End, string Label, Color Color);

    private List<Segment> BuildSegments()
    {
        var output = new List<Segment>();
        for (var i = 0; i < GageCount; i++)
        {
            var start = Math.Clamp(GetBoundaryValue(i), Minimum, Maximum);
            var end = Math.Clamp(GetBoundaryValue(i + 1), Minimum, Maximum);
            if (end <= start)
            {
                continue;
            }

            var label = i < _gageTexts.Count ? _gageTexts[i] : $"Range {i + 1}";
            var color = i < _gageColorArgb.Count ? Color.FromArgb(_gageColorArgb[i]) : GetFallbackColor(i);
            output.Add(new Segment(start, end, label, color));
        }

        if (output.Count == 0)
        {
            output.Add(new Segment(Minimum, Maximum, "Range", Color.DimGray));
        }

        return output.OrderBy(x => x.Start).ToList();
    }

    private void DrawSegments(Graphics g, List<Segment> segments)
    {
        var center = new PointF(CenterX, CenterY);
        foreach (var segment in segments)
        {
            var startAngle = ValueToAngle(segment.Start);
            var endAngle = ValueToAngle(segment.End);
            if (Math.Abs(endAngle - startAngle) < 0.01f)
            {
                continue;
            }

            using var path = CreateDonutSegment(center, OuterRadius, InnerRadius, startAngle, endAngle);
            using var brush = new SolidBrush(segment.Color);
            g.FillPath(brush, path);
        }
    }

    private void DrawLabels(Graphics g, List<Segment> segments)
    {
        using var brush = new SolidBrush(LabelTextColor);
        using var font = new Font("Arial", 16f, FontStyle.Bold, GraphicsUnit.Pixel);
        using var format = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };

        foreach (var segment in segments)
        {
            var midValue = (segment.Start + segment.End) / 2f;
            var midAngle = ValueToAngle(midValue);
            var point = PointOnCircle(CenterX, CenterY, LabelRadius, midAngle);
            var rotation = midAngle - 270f;

            var state = g.Save();
            g.TranslateTransform(point.X, point.Y);
            g.RotateTransform(rotation);
            g.DrawString(segment.Label, font, brush, 0f, 0f, format);
            g.Restore(state);
        }
    }

    private void DrawNeedle(Graphics g)
    {
        var angle = ValueToAngle(Percent);
        var tip = PointOnCircle(CenterX, CenterY, NeedleLength, angle);
        var baseLeft = PointOnCircle(CenterX, CenterY, 5f, angle - 90f);
        var baseRight = PointOnCircle(CenterX, CenterY, 5f, angle + 90f);

        using var needleBrush = new SolidBrush(NeedleColor);
        using var outlinePen = new Pen(Color.Black, 1f);
        using var hubBrush = new SolidBrush(NeedleHubColor);

        g.FillPolygon(needleBrush, [baseLeft, tip, baseRight]);
        g.DrawPolygon(outlinePen, [baseLeft, tip, baseRight]);
        g.FillEllipse(hubBrush, CenterX - HubRadius, CenterY - HubRadius, HubRadius * 2f, HubRadius * 2f);
    }

    private void DrawStatus(Graphics g, List<Segment> segments)
    {
        var text = AutoStatusFromRange
            ? segments.FirstOrDefault(s => Percent >= s.Start && Percent <= s.End)?.Label ?? StatusText
            : StatusText;

        using var brush = new SolidBrush(StatusTextColor);
        using var font = new Font("Malgun Gothic", 26f, FontStyle.Bold, GraphicsUnit.Pixel);
        using var format = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };

        g.DrawString(text, font, brush, new PointF(CenterX, StatusTextY), format);
    }

    private float ValueToAngle(float value)
    {
        if (Math.Abs(Maximum - Minimum) < float.Epsilon)
        {
            return 180f;
        }

        var normalized = Math.Clamp((value - Minimum) / (Maximum - Minimum), 0f, 1f);
        return 180f + (normalized * 180f);
    }

    private static GraphicsPath CreateDonutSegment(PointF center, float outerRadius, float innerRadius, float startAngle, float endAngle)
    {
        var outerRect = new RectangleF(center.X - outerRadius, center.Y - outerRadius, outerRadius * 2f, outerRadius * 2f);
        var innerRect = new RectangleF(center.X - innerRadius, center.Y - innerRadius, innerRadius * 2f, innerRadius * 2f);
        var sweep = endAngle - startAngle;

        var path = new GraphicsPath();
        path.AddArc(outerRect, startAngle, sweep);
        path.AddLine(path.GetLastPoint(), PointOnCircle(center.X, center.Y, innerRadius, endAngle));
        path.AddArc(innerRect, endAngle, -sweep);
        path.CloseFigure();
        return path;
    }

    private static PointF PointOnCircle(float centerX, float centerY, float radius, float angleDeg)
    {
        var rad = MathF.PI * angleDeg / 180f;
        return new PointF(
            centerX + (radius * MathF.Cos(rad)),
            centerY + (radius * MathF.Sin(rad)));
    }

    private void ParseRangeDefinitions(string? input)
    {
        var tokens = ParseList(input);
        var ranges = new List<(float Start, float End)>();
        var hasPairToken = tokens.Any(x => x.Contains('-') || x.Contains('~') || x.Contains(':'));

        if (hasPairToken)
        {
            foreach (var token in tokens)
            {
                var parts = token.Split(['-', '~', ':'], StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 2 && TryParseFloat(parts[0], out var start) && TryParseFloat(parts[1], out var end))
                {
                    ranges.Add((start, end));
                }
            }
        }
        else
        {
            var points = new List<float>();
            foreach (var token in tokens)
            {
                if (TryParseFloat(token, out var value))
                {
                    points.Add(value);
                }
            }

            for (var i = 0; i < points.Count - 1; i++)
            {
                ranges.Add((points[i], points[i + 1]));
            }
        }

        if (ranges.Count == 0)
        {
            return;
        }

        _gageBoundaries.Clear();
        _gageBoundaries.Add(ranges[0].Start);
        foreach (var range in ranges)
        {
            _gageBoundaries.Add(range.End);
        }

        _gageCount = Math.Max(0, _gageBoundaries.Count - 1);
        ResizeGaugeTextList();
        ResizeGaugeColorList();
    }

    private float GetBoundaryValue(int index)
    {
        if (_gageBoundaries.Count == 0)
        {
            return Minimum;
        }

        if (index <= 0)
        {
            return _gageBoundaries[0];
        }

        if (index >= _gageBoundaries.Count)
        {
            return _gageBoundaries[^1];
        }

        return _gageBoundaries[index];
    }

    private void ResizeGaugeTextList()
    {
        while (_gageTexts.Count < _gageCount)
        {
            _gageTexts.Add(string.Empty);
        }

        while (_gageTexts.Count > _gageCount)
        {
            _gageTexts.RemoveAt(_gageTexts.Count - 1);
        }
    }

    private void ResizeGaugeColorList()
    {
        while (_gageColorArgb.Count < _gageCount)
        {
            _gageColorArgb.Add(GetFallbackColor(_gageColorArgb.Count).ToArgb());
        }

        while (_gageColorArgb.Count > _gageCount)
        {
            _gageColorArgb.RemoveAt(_gageColorArgb.Count - 1);
        }
    }

    private void ResizeGaugeBoundaries()
    {
        var needed = _gageCount + 1;
        if (needed <= 0)
        {
            _gageBoundaries.Clear();
            return;
        }

        while (_gageBoundaries.Count < needed)
        {
            _gageBoundaries.Add(Maximum);
        }

        while (_gageBoundaries.Count > needed)
        {
            _gageBoundaries.RemoveAt(_gageBoundaries.Count - 1);
        }

        if (_gageBoundaries.Count == 1)
        {
            _gageBoundaries[0] = Minimum;
            return;
        }

        var step = (Maximum - Minimum) / _gageCount;
        for (var i = 0; i < _gageBoundaries.Count; i++)
        {
            if (_gageBoundaries[i] < Minimum || _gageBoundaries[i] > Maximum)
            {
                _gageBoundaries[i] = Minimum + (step * i);
            }
        }

        _gageBoundaries[0] = Minimum;
        _gageBoundaries[^1] = Maximum;
        for (var i = 1; i < _gageBoundaries.Count; i++)
        {
            if (_gageBoundaries[i] < _gageBoundaries[i - 1])
            {
                _gageBoundaries[i] = _gageBoundaries[i - 1];
            }
        }
    }

    private void RebuildGaugeControls()
    {
        // Reserved for future child-control gauge layout.
    }

    private static bool TryParseFloat(string input, out float value)
    {
        return float.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out value)
            || float.TryParse(input, NumberStyles.Float, CultureInfo.CurrentCulture, out value);
    }

    private static List<string> ParseList(string? input)
    {
        return (input ?? string.Empty)
            .Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .ToList();
    }

    private static Color GetFallbackColor(int index)
    {
        Color[] fallback =
        [
            Color.FromArgb(109, 45, 45),
            Color.FromArgb(67, 74, 79),
            Color.FromArgb(21, 97, 50),
            Color.FromArgb(70, 70, 120),
            Color.FromArgb(120, 120, 40)
        ];

        return fallback[index % fallback.Length];
    }
}
