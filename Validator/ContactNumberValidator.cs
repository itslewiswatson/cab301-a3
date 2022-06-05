class ContactNumberValidator : InputValidator
{
    public override void Validate(string input)
    {
        bool isValid = IMember.IsValidContactNumber(input);

        if (!isValid) throw new InputInvalidException("Contact number is invalid");
    }
}
