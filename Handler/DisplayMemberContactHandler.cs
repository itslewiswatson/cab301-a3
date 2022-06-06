using System;
using System.Collections.Generic;

class DisplayMemberContactHandler : Handler
{
    public DisplayMemberContactHandler(MemberCollection memberCollection, MovieCollection movieCollection) : base(memberCollection, movieCollection)
    {
    }

    protected override bool Execute(List<string> values)
    {
        string firstName = values[0];
        string lastName = values[1];

        Member member = (Member)memberCollection.Find(new Member(firstName, lastName));
        if (member == null)
        {
            Console.WriteLine("This member does not exist");
            Console.WriteLine("");
            return false;
        }

        Console.WriteLine("The member's contact number is {0}", member.ContactNumber.ToString());
        Console.WriteLine("");

        return true;
    }
}
