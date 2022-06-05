﻿abstract class MenuField
{
    public string displayText { get; }
    public BasicDisplayable childMenu { get; }

    public MenuField(string displayText, BasicDisplayable childMenu = null)
    {
        this.displayText = displayText;
        this.childMenu = childMenu;
    }
}
