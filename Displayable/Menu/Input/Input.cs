class Input : MenuField
{     
    private InputMenu parentMenu { get; }
    public InputValidator inputValidator { get; }
    private bool nullable { get; } = false;

    public Input(string displayText, InputMenu parentMenu, bool nullable, BasicDisplayable childMenu = null,
                    InputValidator inputValidator = null) : base(displayText, childMenu)
    {
        this.parentMenu = parentMenu;
        this.inputValidator = inputValidator;
        this.nullable = nullable;
    }

    public void ValidateInput(string input)
    {
        if (nullable && input.Trim() == "")
        {
            return;
        }
        if (inputValidator != null)
        {
            inputValidator.Validate(input);
        }
    }
}

