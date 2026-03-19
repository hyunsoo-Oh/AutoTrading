using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrading.Controls.Cards
{
    public class CardStyle
    {
        public Color CardBackColor { get; set; } = Color.FromArgb(10, 27, 102);
        public Color TitleColor { get; set; } = Color.FromArgb(242, 246, 255);
        public Color ValueColor { get; set; } = Color.FromArgb(216, 210, 106);

        public Color BadgeBackColor { get; set; } = Color.FromArgb(9, 23, 68);
        public Color UpColor { get; set; } = Color.FromArgb(90, 200, 120);
        public Color DownColor { get; set; } = Color.FromArgb(255, 76, 102);
        public Color FlatColor { get; set; } = Color.FromArgb(180, 180, 180);

        public Color ChartUpColor { get; set; } = Color.FromArgb(90, 200, 120);
        public Color ChartDownColor { get; set; } = Color.FromArgb(255, 64, 79);
        public Color ChartFlatColor { get; set; } = Color.FromArgb(190, 190, 190);

        public Color TimeLabelColor { get; set; } = Color.FromArgb(150, 215, 227, 255);

        public int CornerRadius { get; set; } = 24;
        public int PaddingSize { get; set; } = 18;
        public int HeaderTop { get; set; } = 18;
        public int ChartHeight { get; set; } = 95;
    }
}
