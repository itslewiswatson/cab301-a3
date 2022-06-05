using System;
using System.Collections.Generic;

class OptionSelectionValidator : InputValidator
{
    private List<Option> options;

    public OptionSelectionValidator(List<Option> options)
    {
        this.options = options;
    }

    private void ValidateNumeric(string input)
    {
        try
        {
            int.Parse(input);
        }
        catch (Exception)
        {
            throw new InputInvalidException(string.Format("Input of '{0}' is not a valid option selection", input));
        }
    }

    private void ValidateRange(string input)
    {
        int selectedOption = int.Parse(input);
        for (int index = 0; index < options.Count; index++)
        {
            if (index + 1 == selectedOption)
            {
                return;
            }
        }
        throw new InputInvalidException(string.Format("Your selected option of '{0}' is not valid for this menu", input));
    }

    public override void Validate(string input)
    {
        ValidateNumeric(input);
        ValidateRange(input);
    }
}