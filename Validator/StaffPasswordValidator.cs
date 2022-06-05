class StaffPasswordValidator : InputValidator
{
    public override void Validate(string input)
    {
        bool isValid = input == "today123";

        if (!isValid) throw new InputInvalidException("Incorrect username or password");
    }
}
