namespace AutoTrading.Controls.Shell.SideBar
{
    /// <summary>
    /// 네비게이션 항목 선택 변경 이벤트 인자
    /// </summary>
    public sealed class NavigationSelectionChangedEventArgs : EventArgs
    {
        /// <summary>
        /// 이전 선택 키와 현재 선택 키를 지정하여 생성
        /// </summary>
        public NavigationSelectionChangedEventArgs(string? previousKey, string currentKey)
        {
            PreviousKey = previousKey;
            CurrentKey = currentKey;
        }

        /// <summary>이전에 선택되어 있던 항목의 키 (최초 선택 시 null)</summary>
        public string? PreviousKey { get; }

        /// <summary>새로 선택된 항목의 키</summary>
        public string CurrentKey { get; }
    }
}
