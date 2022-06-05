using System;

class InputInvalidException : Exception
{
    public InputInvalidException(string message) : base(message) { }
}