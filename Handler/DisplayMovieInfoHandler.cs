using System;
using System.Collections.Generic;

class DisplayMovieInfoHandler : Handler
{
    public DisplayMovieInfoHandler(MemberCollection memberCollection, MovieCollection movieCollection) : base(memberCollection, movieCollection)
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

        Console.WriteLine(movie.ToString());
        Console.WriteLine("");

        return true;
    }
}

