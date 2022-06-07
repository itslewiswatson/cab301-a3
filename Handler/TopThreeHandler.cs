using System;
using System.Collections.Generic;

class TopThreeHandler : Handler
{
    public TopThreeHandler(MemberCollection memberCollection, MovieCollection movieCollection) : base(memberCollection, movieCollection)
    {
    }

    protected override bool Execute(List<string> values)
    {
        IMovie[] movies = movieCollection.ToArray();
        int[] largest = GetThreeLargest();

        for (int i = 0; i < largest.Length; i++)
        {
            if (largest[i] == 0) continue; 
            Console.WriteLine("{0}. {1} - borrowed {2} times", (i + 1).ToString(), movies[i].Title.ToString(), movies[i].NoBorrowings.ToString());
        }
        Console.WriteLine();

        return true;
    }

    private int[] GetThreeLargest()
    {
        int first = 0, second = 0, third = 0;

        IMovie[] movies = movieCollection.ToArray();

        for (int i = 0; i < movies.Length; i++)
        {
            if (movies[i].NoBorrowings > first)
            {
                third = second;
                second = first;
                first = movies[i].NoBorrowings;
            }
            else if (movies[i].NoBorrowings > second)
            {
                third = second;
                second = movies[i].NoBorrowings;
            }
            else if (movies[i].NoBorrowings > third)
            {
                third = movies[i].NoBorrowings;
            }
        }

        return new int[3] { first, second, third };
    }
}

