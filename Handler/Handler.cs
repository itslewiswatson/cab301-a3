using System;
using System.Collections.Generic;

abstract class Handler
{
    protected readonly MemberCollection memberCollection;
    protected readonly MovieCollection movieCollection;

    public Handler(MemberCollection memberCollection, MovieCollection movieCollection)
    {
        this.memberCollection = memberCollection;
        this.movieCollection = movieCollection;
    }

    abstract protected bool Execute(List<string> values);

    public bool Handle(List<Input> fields, List<string> values)
    {
        try
        {
            ValidateValues(fields, values);
            return Execute(values);
        }
        catch (Exception e)
        {
            PrintErrorMessage(e.Message);
        }

        return false;
    }

    public static void ValidateValues(List<Input> fields, List<string> values)
    {
        for (int index = 0; index < fields.Count; index++)
        {
            Input field = fields[index];
            var answer = values[index];

            // Skip null answers
            if (answer != null)
            {
                field.ValidateInput(answer);
            }
        }
    }

    public static void PrintErrorMessage(string message)
    {
        Console.WriteLine(message);
        Console.WriteLine();
    }
}
