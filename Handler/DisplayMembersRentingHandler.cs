using System;
using System.Collections.Generic;

class DisplayMembersRentingHandler : Handler
{
    public DisplayMembersRentingHandler(MemberCollection memberCollection, MovieCollection movieCollection) : base(memberCollection, movieCollection)
    {
    }

    protected override bool Execute(List<string> values)
    {
        string movieName = values[0];
        Movie movie = (Movie)movieCollection.Search(movieName);

        if (movie == null)
        {
            Console.WriteLine("This movie does not exist in the system");
            Console.WriteLine("");
            return false;
        }

        Console.WriteLine("List of members borrowing {0}:", movieName);
        Console.Write(movie.Borrowers.ToString());
        Console.WriteLine();

        return true;
    }
}
