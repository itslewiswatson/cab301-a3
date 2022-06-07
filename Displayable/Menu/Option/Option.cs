class Option : MenuField
{
    public bool hidden { get; }

    public Option(string displayText, BasicDisplayable childMenu = null, bool hidden = false) : base(displayText, childMenu) { }
}
