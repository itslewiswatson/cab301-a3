using System;

class QuantityValidator : InputValidator
{
    public override void Validate(string input)
    {
        try
        {
            int.Parse(input);
        }
        catch (Exception)
        {
            throw new InputInvalidException("Must be a number");
        }

        int qty = int.Parse(input);
        if (qty <= 0)
        {
            throw new InputInvalidException("Numbers must be greater than zero");
        }
    }
}
