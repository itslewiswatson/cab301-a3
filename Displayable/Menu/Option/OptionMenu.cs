using System;
using System.Collections.Generic;

class OptionMenu : Menu, Displayable<Option>
{
    private const string Option_Separator = ")";

    public string header { get; }
    public List<Option> items { get; } = new List<Option>();

    public OptionMenu(Menu parentMenu, string header)
    {
        this.header = header;
        this.parentMenu = parentMenu;
    }

    public OptionMenu(string header)
    {
        this.header = header;
        this.parentMenu = null;
    }

    public void AddOption(string displayText, BasicDisplayable childMenu = null)
    {
        Option option = new Option(displayText, childMenu);
        items.Add(option);
    }

    public override void Print()
    {
        PrintOptionsHeader();
        PrintOptions();
    }

    private void PrintOptionsHeader()
    {
        if (header != null)
        {
            Console.WriteLine("{0}:", header);
        }
        Console.WriteLine();
    }

    private void PrintOptions()
    {
        for (var index = 0; index < items.Count; index++)
        {
            Option option = items[index];
            Console.WriteLine("{0}{1} {2}", index + 1, Option_Separator, option.displayText);
        }
        Console.WriteLine();
    }
}

