using System.Collections.Generic;

abstract class Menu : BasicDisplayable
{
    public Menu parentMenu { get; set; }
    private List<Menu> childMenus { get; } = new List<Menu>();

    public InputMenu AddInputMenu(string id, string header)
    {
        InputMenu inputMenu = new InputMenu(id, header, this);
        childMenus.Add(inputMenu);
        return inputMenu;
    }

    public OptionMenu AddOptionMenu(string header)
    {
        OptionMenu optionMenu = new OptionMenu(this, header);
        childMenus.Add(optionMenu);
        return optionMenu;
    }

    public abstract void Print();
}
