namespace AutoTrading.Controls.Shell.SideBar
{
    /// <summary>
    /// 네비게이션 항목 정의 (고유 키와 표시 텍스트)
    /// </summary>
    /// <param name="Key">항목 식별 키</param>
    /// <param name="Text">화면에 표시되는 텍스트</param>
    public sealed record NavigationItemDefinition(string Key, string Text);
}
