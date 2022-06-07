using System;

class ClassificationValidator : InputValidator
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
            throw new InputInvalidException("Classification must be numeric");
        }
    }

    private void ValidateRange(string input)
    {
        int option = int.Parse(input);

        if (option < 1 || option > 5)
        {
            throw new InputInvalidException("Classification must be a selection between 1 and 5");
        }
    }
}
