using System; 

abstract class InputValidator
{
    abstract public void Validate(string input);

    public bool IsValid(string input)
    {
        input = input.Trim();

        try
        {
            Validate(input);
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }
}
