using System;

class GenreValidator : InputValidator
{
    public override void Validate(string input)
    {
        ValidateNumeric(input);
        ValidateRange(input);
    }

    private void ValidateNumeric(string input)
    {
        try
        {
            int.Parse(input);
        }
        catch (Exception)
        {
            throw new InputInvalidException("Genre must be numeric");
        }
    }

    private void ValidateRange(string input)
    {
        int option = int.Parse(input);

        if (option < 1 || option > 4)
        {
            throw new InputInvalidException("Genre must be a selection between 1 and 4");
        }
    }
}
