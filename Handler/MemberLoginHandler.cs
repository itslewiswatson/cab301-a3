using System;
using System.Collections.Generic;

class MemberLoginHandler : Handler
{
    public MemberLoginHandler(MemberCollection memberCollection, MovieCollection movieCollection) : base(memberCollection, movieCollection)
    {
    }

    protected override bool Execute(List<string> values)
    {
        string firstName = values[0];
        string lastName = values[1];
        string pin = values[2];

        Member member = (Member)memberCollection.Find(new Member(firstName, lastName));

        if (member == null || member.Pin != pin)
        {
            Console.WriteLine("No member exists or PIN incorrect");
            return false;
        }

        return true;
    }
}
