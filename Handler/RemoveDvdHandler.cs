using System;
using System.Collections.Generic;

class RemoveDvdHandler : Handler
{
    public RemoveDvdHandler(MemberCollection memberCollection, MovieCollection movieCollection) : base(memberCollection, movieCollection)
    {
    }

    protected override bool Execute(List<string> values)
    {
        string movieName = values[0];
        int quantity = int.Parse(values[1]); // Guaranteed int here

        Movie movie = (Movie)movieCollection.Search(movieName);

        if (movie == null)
        {
            Console.WriteLine("This movie does not exist in the system");
            Console.WriteLine("");
            return false;
        }

        if (movie.AvailableCopies != movie.TotalCopies)
        {
            if (quantity > movie.AvailableCopies)
            {
                Console.WriteLine("You cannot remove {0} DVDs as there are only {1}/{2} available", quantity.ToString(), movie.AvailableCopies.ToString(), movie.TotalCopies.ToString());
                Console.WriteLine("");
                return false;
            }
        }
        else
        {
            if (quantity == movie.TotalCopies)
            {
                Console.WriteLine("Removing movie from collection as there are no copies");
                Console.WriteLine("");

                movieCollection.Delete(movie);
                return true;
            }

            if (quantity > movie.AvailableCopies)
            {
                Console.WriteLine("You cannot remove {0} DVDs as there are only {1}/{2} available", quantity.ToString(), movie.AvailableCopies.ToString(), movie.TotalCopies.ToString());
                Console.WriteLine("");
                return false;
            }
        }

        movie.AvailableCopies = movie.AvailableCopies - quantity;
        movie.TotalCopies = movie.TotalCopies - quantity;

        Console.WriteLine("Removed {0} DVDs from {1} ({2} left)", quantity, movieName, movie.AvailableCopies);
        Console.WriteLine("");
        return true;
    }
}
