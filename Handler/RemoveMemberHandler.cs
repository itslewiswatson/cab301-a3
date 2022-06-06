using System;
using System.Collections.Generic;
using System.Text;

class RemoveMemberHandler : Handler
{
    public RemoveMemberHandler(MemberCollection memberCollection, MovieCollection movieCollection) : base(memberCollection, movieCollection)
    {
    }

    protected override bool Execute(List<string> values)
    {
        string firstName = values[0];
        string lastName = values[1];

        Member memberToDelete = new Member(firstName, lastName);
        bool memberExists = memberCollection.Search(memberToDelete);
        if (!memberExists)
        {
            Console.WriteLine("This member does not exist");
            Console.WriteLine("");
            return false;
        }

        bool borrowing = false;
        IMovie[] movies = movieCollection.ToArray();

        for (int i = 0; i < movies.Length; i++)
        {
            IMovie movie = movies[i];

            if (movie.Borrowers.Search(memberToDelete))
            {
                borrowing = true;
                break;
            }
        }

        if (borrowing)
        {
            Console.WriteLine("The member cannot be deleted as they are still borrowing a DVD");
            Console.WriteLine("");
            return false;
        }

        memberCollection.Delete(memberToDelete);
        Console.WriteLine("The member has been deleted");
        Console.WriteLine("");

        return true;
    }
}
