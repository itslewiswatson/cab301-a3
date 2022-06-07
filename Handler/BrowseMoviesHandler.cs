using System;
using System.Collections.Generic;

class BrowseMoviesHandler : Handler
{
    public BrowseMoviesHandler(MemberCollection memberCollection, MovieCollection movieCollection) : base(memberCollection, movieCollection)
    {
    }

    protected override bool Execute(List<string> values)
    {
        IMovie[] movies = movieCollection.ToArray();

        Console.WriteLine("List of movies:");
        for (int i = 0; i < movies.Length; i++)
        {
            Movie movie = (Movie)movies[i];

            if (movie.AvailableCopies > 0)
            {
                Console.WriteLine(" - " + movie.ToString());
            }
        }

        Console.WriteLine("");

        return true;
    }
}

