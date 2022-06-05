class PinValidator : InputValidator
{
    public override void Validate(string input)
    {
        bool isValid = IMember.IsValidPin(input);

        if (!isValid) throw new InputInvalidException("Pin number is invalid");
    }
}

