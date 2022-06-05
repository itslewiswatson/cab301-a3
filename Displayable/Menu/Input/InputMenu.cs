using System;
using System.Collections.Generic;

class InputMenu : Menu, Displayable<Input>
{
    public string id { get; }
    public string header { get; }
    public List<Input> items { get; } = new List<Input>();
    public List<string> answers { get; } = new List<string>();

    public InputMenu(string id, string header, Menu parentMenu)
    {
        this.id = id;
        this.header = header;
        this.parentMenu = parentMenu;
    }

    public Input AddInput(string displayText, InputValidator inputValidator = null, bool nullable = false)
    {
        Input input = new Input(displayText, this, nullable, null, inputValidator);
        items.Add(input);
        return input;
    }

    public override void Print()
    {
        answers.Clear();
        PrintHeader();
        PrintInputs();
        Console.WriteLine();
    }

    private void PrintHeader()
    {
        if (header != null)
        {
            Console.WriteLine("{0}:", header);
        }
        Console.WriteLine();
    }

    private void PrintInputs()
    {
        foreach (Input input in items)
        {
            Console.Write("{0}: ", input.displayText);
            string answer = Console.ReadLine().Trim();

            // Validate as inputted by user if field has an attached validator
            if (input.inputValidator != null)
            {
                // input.ValidateInput(answer);
            }

            answers.Add(answer);
        }
    }
}

