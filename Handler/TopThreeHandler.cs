using System;
using System.Collections.Generic;

class TopThreeHandler : Handler
{
    public TopThreeHandler(MemberCollection memberCollection, MovieCollection movieCollection) : base(memberCollection, movieCollection)
    {
    }

    protected override bool Execute(List<string> values)
    {
        Movie[] largest = GetThreeLargest();

        for (int i = 0; i < largest.Length; i++)
        {
            if (largest[i] == null) continue; 
            Console.WriteLine("{0}. {1} - borrowed {2} times", (i + 1).ToString(), largest[i].Title.ToString(), largest[i].NoBorrowings.ToString());
        }
        Console.WriteLine();

        return true;
    }

    private Movie[] GetThreeLargest()
    {
        int first = 0, second = 0, third = 0;
        Movie mFirst = null, mSecond = null, mThird = null;

        IMovie[] movies = movieCollection.ToArray();

        for (int i = 0; i < movies.Length; i++)
        {
            if (movies[i].NoBorrowings > first)
            {
                third = second;
                second = first;
                first = movies[i].NoBorrowings;
                mFirst = (Movie)movies[i];
            }
            else if (movies[i].NoBorrowings > second)
            {
                third = second;
                second = movies[i].NoBorrowings;
                mSecond = (Movie)movies[i];
            }
            else if (movies[i].NoBorrowings > third)
            {
                third = movies[i].NoBorrowings;
                mThird = (Movie)movies[i];
            }
        }

        return new Movie[3] { mFirst, mSecond, mThird };
    }
}

