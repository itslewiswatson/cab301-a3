using System;
using System.Collections.Generic;

class Program
{
    static MemberCollection memberCollection = new MemberCollection(100);
    static MovieCollection movieCollection = new MovieCollection();

    static void Main()
    {
        // Global state
        Member loggedInMember = null;
        string movieToAdd = null;

        // Test data, please ignore
        /*
        Member lewis = new Member("Lewis", "Watson", "0404925759", "1234");
        Member grant = new Member("Grant", "Smith", "0404925759", "1234");
        Member kanye = new Member("Kanye", "West", "0404925759", "1234");
        memberCollection.Add(lewis);
        memberCollection.Add(grant);
        memberCollection.Add(kanye);

        Movie gf = new Movie("Goodfellas", MovieGenre.Action, MovieClassification.G, 100, 10);
        Movie cas = new Movie("Casino", MovieGenre.Action, MovieClassification.G, 100, 10);
        gf.AddBorrower(kanye);
        gf.AddBorrower(grant);
        cas.AddBorrower(lewis);
        cas.AddBorrower(grant);
        cas.AddBorrower(kanye);
        movieCollection.Insert(gf);
        movieCollection.Insert(cas);
        lewis.Movies.Insert(cas);
        */
        
        // Handlers
        StaffLoginHandler staffLoginHandler = new StaffLoginHandler(memberCollection, movieCollection);
        RegisterMemberHandler registerMemberHandler = new RegisterMemberHandler(memberCollection, movieCollection);
        MemberLoginHandler memberLoginHandler = new MemberLoginHandler(memberCollection, movieCollection);
        RemoveMemberHandler removeMemberHandler = new RemoveMemberHandler(memberCollection, movieCollection);
        DisplayMemberContactHandler displayMemberContactHandler = new DisplayMemberContactHandler(memberCollection, movieCollection);
        DisplayMembersRentingHandler displayMembersRentingHandler = new DisplayMembersRentingHandler(memberCollection, movieCollection);
        RemoveDvdHandler removeDvdHandler = new RemoveDvdHandler(memberCollection, movieCollection);
        BrowseMoviesHandler browseMoviesHandler = new BrowseMoviesHandler(memberCollection, movieCollection);
        DisplayMovieInfoHandler displayMovieInfoHandler = new DisplayMovieInfoHandler(memberCollection, movieCollection);
        TopThreeHandler topThreeHandler = new TopThreeHandler(memberCollection, movieCollection);

        // Validators
        PinValidator pinValidator = new PinValidator();
        ContactNumberValidator contactNumberValidator = new ContactNumberValidator();
        QuantityValidator quantityValidator = new QuantityValidator();
        ClassificationValidator classificationValidator = new ClassificationValidator();
        GenreValidator genreValidator = new GenreValidator();

        // Menus
        OptionMenu mainMenu = new OptionMenu("Enter a number to select from the list");

        // Member
        InputMenu memberLoginMenu = mainMenu.AddInputMenu("MemberLogin", "Enter your membership credentials");
        mainMenu.AddOption("Member Login", memberLoginMenu);
        memberLoginMenu.AddInput("First Name");
        memberLoginMenu.AddInput("Last Name");
        memberLoginMenu.AddInput("PIN", pinValidator);

        OptionMenu memberOptionMenu = new OptionMenu(mainMenu, "Select what you would like to do");

        InputMenu browseMovieMenu = memberOptionMenu.AddInputMenu("BrowseMovies", null);
        memberOptionMenu.AddOption("Browse all the movies", browseMovieMenu);

        InputMenu displayMovieInfoMenu = memberOptionMenu.AddInputMenu("DisplayMovieInfo", "Display movie info");
        memberOptionMenu.AddOption("Display all information about a movie, given its title", displayMovieInfoMenu);
        displayMovieInfoMenu.AddInput("Movie");

        InputMenu borrowMovieMenu = memberOptionMenu.AddInputMenu("BorrowMovie", null);
        memberOptionMenu.AddOption("Borrow a movie DVD", borrowMovieMenu);
        borrowMovieMenu.AddInput("Movie");

        InputMenu returnMovieMenu = memberOptionMenu.AddInputMenu("ReturnMovie", null);
        memberOptionMenu.AddOption("Return a movie DVD", returnMovieMenu);
        returnMovieMenu.AddInput("Movie");

        InputMenu currentBorrowingMenu = memberOptionMenu.AddInputMenu("CurrentBorrowing", null);
        memberOptionMenu.AddOption("List current borrowing movies", currentBorrowingMenu);

        InputMenu topThreeMenu = memberOptionMenu.AddInputMenu("Top3", null);
        memberOptionMenu.AddOption("Display the top 3 movies rented by members", topThreeMenu);

        InputMenu memberGoToParentMenu = memberOptionMenu.AddInputMenu("GoToParentMenu", null);
        memberOptionMenu.AddOption("Return to the main menu", memberGoToParentMenu);

        // Staff
        InputMenu staffLoginMenu = mainMenu.AddInputMenu("StaffLogin", "Enter your staff credentials");
        mainMenu.AddOption("Staff Login", staffLoginMenu);
        staffLoginMenu.AddInput("Username", new StaffUsernameValidator());
        staffLoginMenu.AddInput("Password", new StaffPasswordValidator());

        OptionMenu staffOptionMenu = new OptionMenu(mainMenu, "Select what you would like to do");

        InputMenu addDvdMenu = staffOptionMenu.AddInputMenu("AddDVDs", "Add new DVDs of a new movie to the system");
        staffOptionMenu.AddOption("Add new DVDs of a new movie to the system", addDvdMenu);
        addDvdMenu.AddInput("Movie");

        InputMenu addDvdFullMenu = addDvdMenu.AddInputMenu("AddDVDsFull", "We haven't seen that movie before, let's get some more info");
        addDvdFullMenu.AddInput("Genre (1 = Action, 2 = Comedy, 3 = History, 4 = Drama, 5 = Western)", genreValidator, false);
        addDvdFullMenu.AddInput("Classification (1 = G, 2 = PG, 3 = M, 4 = M15+)", classificationValidator, false);
        addDvdFullMenu.AddInput("Duration", quantityValidator, false);
        addDvdFullMenu.AddInput("Copies", quantityValidator, false);

        InputMenu addDvdQuantityMenu = addDvdMenu.AddInputMenu("AddDVDsQty", "This movie already exists, what would you like to do?");
        addDvdQuantityMenu.AddInput("Quantity", quantityValidator);

        OptionMenu addDvdNotFoundOption = addDvdMenu.AddOptionMenu("We haven't seen that movie before...");
        addDvdNotFoundOption.AddOption("Continue to add", addDvdFullMenu);
        addDvdNotFoundOption.AddOption("Stop, take me back!", staffOptionMenu);

        OptionMenu addDvdFoundOption = addDvdMenu.AddOptionMenu("This movie already exists, what would you like to do?");
        addDvdFoundOption.AddOption("Specify quantity and continue to add", addDvdQuantityMenu);
        addDvdFoundOption.AddOption("Stop, take me back!", staffOptionMenu);

        InputMenu removeDvdMenu = staffOptionMenu.AddInputMenu("RemoveDVDs", "Remove DVDs of a movie from the system");
        staffOptionMenu.AddOption("Remove DVDs of a movie from the system", removeDvdMenu);
        removeDvdMenu.AddInput("Movie");
        removeDvdMenu.AddInput("Quantity", quantityValidator);

        InputMenu registerMemberMenu = staffOptionMenu.AddInputMenu("RegisterMember", "Register a new member with the system");
        staffOptionMenu.AddOption("Register a new member with the system", registerMemberMenu);
        registerMemberMenu.AddInput("First Name");
        registerMemberMenu.AddInput("Last Name");
        registerMemberMenu.AddInput("Contact Number", contactNumberValidator);
        registerMemberMenu.AddInput("PIN", pinValidator);

        InputMenu removeMemberMenu = staffOptionMenu.AddInputMenu("RemoveMember", "Remove a registered member from the system");
        staffOptionMenu.AddOption("Remove a registered member from the system", removeMemberMenu);
        removeMemberMenu.AddInput("First Name");
        removeMemberMenu.AddInput("Last Name");

        InputMenu displayContactMenu = staffOptionMenu.AddInputMenu("DisplayMemberContact", "Display a member's contact phone number, given their name");
        staffOptionMenu.AddOption("Display a member's contact phone number, given their name", displayContactMenu);
        displayContactMenu.AddInput("First Name");
        displayContactMenu.AddInput("Last Name");

        InputMenu displayMembersRentingMenu = staffOptionMenu.AddInputMenu("DisplayMembersRenting", "Display all members who are currently renting a particular movie");
        staffOptionMenu.AddOption("Display all members who are currently renting a particular movie", displayMembersRentingMenu);
        displayMembersRentingMenu.AddInput("Movie");

        InputMenu staffGoToParentMenu = staffOptionMenu.AddInputMenu("GoToParentMenu", null);
        staffOptionMenu.AddOption("Return to the main menu", staffGoToParentMenu);

        // Menu handling
        BasicDisplayable currentDisplay = mainMenu;
        currentDisplay.Print();

        // Initial input grabber
        Console.Write("$ ");
        string input = Console.ReadLine();

        do
        {
            // Handle worst case scenario of no menu being present
            if (currentDisplay == null)
            {
                Console.WriteLine("Critical error - taking you back to the main menu");
                currentDisplay = mainMenu;
            }

            // Display option menu
            if (currentDisplay.GetType() == typeof(OptionMenu))
            {
                OptionMenu currentMenu = (OptionMenu)currentDisplay;
                InputValidator optionSelectionValidator = new OptionSelectionValidator(currentMenu.items);

                // Try to parse selected option and navigate to child menu
                try
                {
                    optionSelectionValidator.Validate(input);

                    int selectedValue = int.Parse(input) - 1; // Account for alternate indexing

                    //Option selectedOption = currentMenu.items.ElementAt(selectedValue);
                    Option selectedOption = currentMenu.items[selectedValue];
                    currentDisplay = selectedOption.childMenu;
                }
                catch (Exception e)
                {
                    // Don't input an error message for action keys or a blank input
                    if (input != "")
                    {
                        Console.WriteLine(e.Message);
                        Console.WriteLine();
                    }
                }

                // Input menus usually result from option menus
                // Attempt to catch errors as input is passed to field validators
                try
                {
                    currentDisplay.Print();
                }
                catch (Exception e)
                {
                    // Write friendly message
                    Console.WriteLine();
                    Console.WriteLine(e.Message);
                    Console.WriteLine();

                    // Take them back to the main menu and let them know where they are at
                    currentDisplay = mainMenu;
                    currentDisplay.Print();
                }
            }

            // Display input menu
            if (currentDisplay.GetType() == typeof(InputMenu))
            {
                InputMenu currentMenu = (InputMenu)currentDisplay;
                List<Input> fields = currentMenu.items;
                List<string> values = currentMenu.answers;

                // Match current input menu to the appropriate handler
                switch (currentMenu.id)
                {
                    case "GoToParentMenu":
                        currentDisplay = mainMenu;
                        break;

                    case "StaffLogin":
                        bool staffLoginSuccess = staffLoginHandler.Handle(fields, values);
                        currentDisplay = staffLoginSuccess ? staffOptionMenu : currentMenu.parentMenu;
                        break;

                    case "MemberLogin":
                        bool memberLoginSuccess = memberLoginHandler.Handle(fields, values);
                        currentDisplay = memberLoginSuccess ? memberOptionMenu : currentMenu.parentMenu;
                        if (memberLoginSuccess)
                        {
                            loggedInMember = (Member)memberCollection.Find(new Member(values[0], values[1]));
                        }
                        break;

                    case "RegisterMember":
                        registerMemberHandler.Handle(fields, values);
                        currentDisplay = currentMenu.parentMenu;
                        break;

                    case "RemoveMember":
                        removeMemberHandler.Handle(fields, values);
                        currentDisplay = currentMenu.parentMenu;
                        break;

                    case "DisplayMemberContact":
                        displayMemberContactHandler.Handle(fields, values);
                        currentDisplay = currentMenu.parentMenu;
                        break;

                    case "DisplayMembersRenting":
                        displayMembersRentingHandler.Handle(fields, values);
                        currentDisplay = currentMenu.parentMenu;
                        break;

                    case "AddDVDs":
                        try
                        {
                            Handler.ValidateValues(fields, values);
                        }
                        catch (Exception e)
                        {
                            Handler.PrintErrorMessage(e.Message);
                            break;
                        }

                        string movieName = values[0];

                        if (movieName == "")
                        {
                            Console.WriteLine("Movie name cannot be blank");
                            Console.WriteLine();
                            currentDisplay = currentMenu.parentMenu;
                            break;
                        }

                        IMovie _addMovie = movieCollection.Search(movieName);
                        IMovie addMovie = (Movie)_addMovie;

                        movieToAdd = movieName;

                        if (addMovie != null)
                        {
                            currentDisplay = addDvdFoundOption;
                            break;
                        }

                        currentDisplay = addDvdNotFoundOption;

                        break;

                    case "AddDVDsFull":
                        try
                        {
                            Handler.ValidateValues(fields, values);
                        }
                        catch (Exception e)
                        {
                            Handler.PrintErrorMessage(e.Message);
                            currentDisplay = staffOptionMenu;
                            break;
                        }

                        if (movieToAdd == null)
                        {
                            Console.WriteLine("Error");
                            Console.WriteLine();
                            currentDisplay = staffOptionMenu;
                            break;
                        }

                        string rawGenre = values[0];
                        string rawClassification = values[1];
                        string rawDuration = values[2];
                        string rawCopies = values[3];

                        MovieGenre genre = (MovieGenre)int.Parse(rawGenre);
                        MovieClassification classification = (MovieClassification)int.Parse(rawClassification);
                        int duration = int.Parse(rawDuration);
                        int copies = int.Parse(rawCopies);

                        Movie newMovie = new Movie(movieToAdd, genre, classification, duration, copies);
                        bool didInsert = movieCollection.Insert(newMovie);

                        if (didInsert)
                        {
                            Console.WriteLine("Successfully added {0} to the collection", movieToAdd);
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine("Could not add {0} to the collection", movieToAdd);
                            Console.WriteLine();
                        }

                        movieToAdd = null;
                        currentDisplay = staffOptionMenu;
                        break;

                    case "AddDVDsQty":
                        try
                        {
                            Handler.ValidateValues(fields, values);
                        } catch (Exception e)
                        {
                            Handler.PrintErrorMessage(e.Message);
                            currentDisplay = staffOptionMenu;
                            break;
                        }

                        if (movieToAdd == null)
                        {
                            Console.WriteLine("Error");
                            Console.WriteLine();
                            currentDisplay = staffOptionMenu;
                            break;
                        }

                        string _qtyToAdd = values[0];
                        int qtyToAdd = int.Parse(_qtyToAdd);

                        Movie existingMovie = (Movie)movieCollection.Search(movieToAdd);
                        if (existingMovie == null)
                        {
                            Console.WriteLine("Error");
                            Console.WriteLine();
                            movieToAdd = null;
                            break;
                        }

                        existingMovie.TotalCopies = existingMovie.TotalCopies + qtyToAdd;

                        Console.WriteLine("There are now {0} total copies ({1} available)", existingMovie.TotalCopies.ToString(), existingMovie.AvailableCopies.ToString());
                        Console.WriteLine();

                        movieToAdd = null;
                        currentDisplay = staffOptionMenu;
                        break;

                    case "RemoveDVDs":
                        removeDvdHandler.Handle(fields, values);
                        currentDisplay = currentMenu.parentMenu;
                        break;

                    case "BrowseMovies":
                        browseMoviesHandler.Handle(fields, values);
                        currentDisplay = currentMenu.parentMenu;
                        break;

                    case "DisplayMovieInfo":
                        displayMovieInfoHandler.Handle(fields, values);
                        currentDisplay = currentMenu.parentMenu;
                        break;

                    case "CurrentBorrowing":
                        if (loggedInMember == null)
                        {
                            Console.WriteLine("Error");
                            Console.WriteLine();
                            currentDisplay = currentMenu.parentMenu;
                            break;
                        }

                        Console.WriteLine("Movies currently being borrowed by member:");
                        IMovie[] moviesBorrowed = loggedInMember.Movies.ToArray();
                        for (int i = 0; i < moviesBorrowed.Length; i++)
                        {
                            Console.WriteLine("- {0}", moviesBorrowed[i].Title);
                        }
                        Console.WriteLine();
                        currentDisplay = currentMenu.parentMenu;
                        break;

                    case "BorrowMovie":
                        try
                        {
                            Handler.ValidateValues(fields, values);
                        }
                        catch (Exception e)
                        {
                            Handler.PrintErrorMessage(e.Message);
                            currentDisplay = currentMenu.parentMenu;
                            break;
                        }

                        if (loggedInMember == null)
                        {
                            Console.WriteLine("Error - no logged in user detected");
                            Console.WriteLine();
                            currentDisplay = currentMenu.parentMenu;
                            break;
                        }

                        Movie movieToBorrow = FindMovie(values[0]);
                        if (movieToBorrow == null)
                        {
                            Console.WriteLine("The movie '{0}' does not exist in the system", values[0].ToString());
                            Console.WriteLine("");
                            currentDisplay = currentMenu.parentMenu;
                            break;
                        }

                        bool currentlyBorrowing = movieToBorrow.Borrowers.Search(loggedInMember);
                        if (currentlyBorrowing)
                        {
                            Console.WriteLine("You are already borrowing this DVD");
                            Console.WriteLine("");
                            currentDisplay = currentMenu.parentMenu;
                            break;
                        }

                        bool isBorrowing = movieToBorrow.AddBorrower(loggedInMember);
                        if (!isBorrowing)
                        {
                            Console.WriteLine("Could not borrow this DVD");
                            Console.WriteLine("");
                            currentDisplay = currentMenu.parentMenu;
                            break;
                        }
                        loggedInMember.Movies.Insert(movieToBorrow);

                        Console.WriteLine("You have borrowed a copy of {0}", movieToBorrow.Title.ToString());
                        Console.WriteLine("");
                        currentDisplay = currentMenu.parentMenu;
                        break;

                    case "ReturnMovie":
                        try
                        {
                            Handler.ValidateValues(fields, values);
                        }
                        catch (Exception e)
                        {
                            Handler.PrintErrorMessage(e.Message);
                            currentDisplay = currentMenu.parentMenu;
                            break;
                        }

                        if (loggedInMember == null)
                        {
                            Console.WriteLine("Error - no logged in user detected");
                            Console.WriteLine();
                            currentDisplay = currentMenu.parentMenu;
                            break;
                        }

                        Movie movie = FindMovie(values[0]);
                        if (movie == null)
                        {
                            Console.WriteLine("The movie '{0}' does not exist in the system", values[0].ToString());
                            Console.WriteLine("");
                            currentDisplay = currentMenu.parentMenu;
                            break;
                        }

                        if (!movie.Borrowers.Search(loggedInMember))
                        {
                            Console.WriteLine("You are not currently borrowing this DVD");
                            Console.WriteLine("");
                            currentDisplay = currentMenu.parentMenu;
                            break;
                        }

                        bool returned = movie.RemoveBorrower(loggedInMember);
                        if (!returned)
                        {
                            Console.WriteLine("Could not return this DVD");
                            Console.WriteLine("");
                            currentDisplay = currentMenu.parentMenu;
                            break;
                        }

                        loggedInMember.Movies.Delete(movie);
                        Console.WriteLine("You have returned your copy of {0}", movie.Title.ToString());
                        Console.WriteLine("");
                        currentDisplay = currentMenu.parentMenu;
                        break;

                    case "Top3":
                        topThreeHandler.Handle(fields, values);
                        currentDisplay = currentMenu.parentMenu;
                        break;

                    default:
                        Console.WriteLine("Something has gone wrong.");
                        Console.WriteLine();
                        currentDisplay = currentMenu.parentMenu;
                        break;
                }

                // currentDisplay = currentMenu.parentMenu;
                currentDisplay.Print();
            }

            Console.Write("$ ");
            input = Console.ReadLine();
        } while (input != null);

        Environment.Exit(0);
    }

    static Movie FindMovie(string movieName)
    {
        return (Movie)movieCollection.Search(movieName);
    }
}
