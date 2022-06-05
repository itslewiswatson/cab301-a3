using System;
using System.Collections.Generic;

class StaffLoginHandler : Handler
{
    public const string ACCEPTABLE_USERNAME = "staff";
    public const string ACCEPTABLE_PASSWORD = "today123";

    public StaffLoginHandler(MemberCollection memberCollection, MovieCollection movieCollection) : base(memberCollection, movieCollection)
    {
    }

    protected override void Execute(List<string> values)
    {
        string username = values[0];
        string password = values[1];

        if (username == ACCEPTABLE_USERNAME && password == ACCEPTABLE_PASSWORD)
        {
            Console.WriteLine("Welcome, staff member :)");
        }
        else
        {
            Console.WriteLine("Incorrect username or password");
        }

        Console.WriteLine();
    }
}
