class StaffUsernameValidator : InputValidator
{
    public override void Validate(string input)
    {
        bool isValid = input == "staff";

        if (!isValid) throw new InputInvalidException("Username is invalid");
    }
}
