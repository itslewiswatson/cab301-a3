﻿using System;
using System.Collections.Generic;

class RegisterMemberHandler : Handler
{
    public RegisterMemberHandler(MemberCollection memberCollection, MovieCollection movieCollection) : base(memberCollection, movieCollection)
    {
    }

    protected override void Execute(List<string> values)
    {
        string firstName = values[0];
        string lastName = values[1];
        string contactNumber = values[2];
        string pin = values[3];

        Member newMember = new Member(firstName, lastName, contactNumber, pin);

        memberCollection.Add(newMember);

        Console.WriteLine(string.Format("You just added {0} {1} as a member", firstName, lastName));
        Console.WriteLine();
    }
}