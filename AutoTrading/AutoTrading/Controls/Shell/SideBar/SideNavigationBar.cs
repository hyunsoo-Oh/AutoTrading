namespace AutoTrading.Controls.Shell.SideBar
{
    public sealed class SideNavigationBar : UserControl
    {
        private const string SettingsKey = "Settings";
        private const string SettingsText = "⚙ Settings";

        private readonly Panel _itemsHost;
        private readonly Panel _bottomHost;
        private readonly SideNavigationItem _settingsItem;
        private readonly List<SideNavigationItem> _items = new();
        private string? _selectedKey;

        public SideNavigationBar()
        {
            Padding = new Padding(0, 8, 0, 8);

            _itemsHost = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(0),
                Margin = new Padding(0)
            };

            _settingsItem = new SideNavigationItem
            {
                Dock = DockStyle.Fill,
                ItemKey = SettingsKey,
                ItemText = SettingsText,
                ShowSelectionIndicator = false
            };
            _settingsItem.ItemInvoked += (_, _) => SettingsInvoked?.Invoke(this, EventArgs.Empty);

            _bottomHost = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = _settingsItem.Height,
                Padding = new Padding(0),
                Margin = new Padding(0)
            };
            _bottomHost.Controls.Add(_settingsItem);

            Controls.Add(_itemsHost);
            Controls.Add(_bottomHost);
        }

        public string? SelectedKey => _selectedKey;

        [System.ComponentModel.Category("Navigation")]
        public Color AccentColor
        {
            get => _settingsItem.AccentColor;
            set
            {
                _settingsItem.AccentColor = value;
                foreach (var item in _items)
                    item.AccentColor = value;
            }
        }

        public event EventHandler<NavigationSelectionChangedEventArgs>? SelectionChanged;
        public event EventHandler? SettingsInvoked;

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);

            if (_itemsHost is null || _bottomHost is null || _settingsItem is null)
                return;

            _itemsHost.BackColor = BackColor;
            _bottomHost.BackColor = BackColor;

            _settingsItem.NormalBackColor = BackColor;

            foreach (var item in _items)
            {
                item.NormalBackColor = BackColor;
            }
        }

        public void SetItems(IEnumerable<NavigationItemDefinition> definitions)
        {
            _itemsHost.SuspendLayout();
            _itemsHost.Controls.Clear();
            _items.Clear();

            foreach (var definition in definitions.Where(x => !IsSettingsItem(x)).Reverse())
            {
                AddItem(definition);
            }

            _itemsHost.ResumeLayout(true);

            if (_items.Count > 0)
            {
                Select(_items[0].ItemKey);
            }
        }

        public void Select(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return;
            }

            var hasMatch = _items.Any(x => x.ItemKey == key);

            if (!hasMatch)
            {
                return;
            }

            if (_selectedKey == key)
            {
                return;
            }

            var previous = _selectedKey;
            _selectedKey = key;

            foreach (var item in _items)
            {
                item.IsSelected = item.ItemKey == key;
            }

            SelectionChanged?.Invoke(this, new NavigationSelectionChangedEventArgs(previous, key));
        }

        private static bool IsSettingsItem(NavigationItemDefinition definition)
        {
            return string.Equals(definition.Key, SettingsKey, StringComparison.Ordinal);
        }

        private void AddItem(NavigationItemDefinition definition)
        {
            var item = new SideNavigationItem
            {
                Dock = DockStyle.Top,
                ItemKey = definition.Key,
                ItemText = definition.Text,
                NormalBackColor = BackColor,
                AccentColor = AccentColor
            };

            item.ItemInvoked += (_, _) => Select(item.ItemKey);
            _items.Add(item);
            _itemsHost.Controls.Add(item);
            item.BringToFront();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            Invalidate();
        }
    }
}
