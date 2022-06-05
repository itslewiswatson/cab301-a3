using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // ADTs
        MemberCollection memberCollection = new MemberCollection(100);
        MovieCollection movieCollection = new MovieCollection();

        // Handlers
        StaffLoginHandler staffLoginHandler = new StaffLoginHandler(memberCollection, movieCollection);
        RegisterMemberHandler registerMemberHandler = new RegisterMemberHandler(memberCollection, movieCollection);

        // Validators
        PinValidator pinValidator = new PinValidator();
        ContactNumberValidator contactNumberValidator = new ContactNumberValidator();

        // Menus
        OptionMenu mainMenu = new OptionMenu("Enter a number to select from the list");

        InputMenu staffLoginMenu = mainMenu.AddInputMenu("StaffLogin", "Enter your staff credentials");
        mainMenu.AddOption("Staff Login", staffLoginMenu);
        staffLoginMenu.AddInput("Username", new StaffUsernameValidator());
        staffLoginMenu.AddInput("Password", new StaffPasswordValidator());

        OptionMenu staffOptionMenu = new OptionMenu(mainMenu, "Select what you would like to do");

        InputMenu addDvdMenu = staffOptionMenu.AddInputMenu("AddDVDs", "Add new DVDs of a new movie to the system");
        staffOptionMenu.AddOption("Add new DVDs of a new movie to the system", addDvdMenu);

        InputMenu removeDvdMenu = staffOptionMenu.AddInputMenu("RemoveDVDs", "Remove DVDs of a movie from the system");
        staffOptionMenu.AddOption("Remove DVDs of a movie from the system", removeDvdMenu);

        InputMenu registerMemberMenu = staffOptionMenu.AddInputMenu("RegisterMember", "Register a new member with the system");
        staffOptionMenu.AddOption("Register a new member with the system", registerMemberMenu);
        registerMemberMenu.AddInput("First Name");
        registerMemberMenu.AddInput("Last Name");
        registerMemberMenu.AddInput("Contact Number", contactNumberValidator);
        registerMemberMenu.AddInput("PIN", pinValidator);

        InputMenu removeMemberMenu = staffOptionMenu.AddInputMenu("RemoveMember", "Remove a registered member from the system");
        staffOptionMenu.AddOption("Remove a registered member from the system", removeMemberMenu);

        InputMenu displayContactMenu = staffOptionMenu.AddInputMenu("DisplayMemberContact", "Display a member's contact phone number, given their name");
        staffOptionMenu.AddOption("Display a member's contact phone number, given their name", displayContactMenu);

        InputMenu displayMembersRentingMenu = staffOptionMenu.AddInputMenu("DisplayMembersRenting", "Display all members who are currently renting a particular movie");
        staffOptionMenu.AddOption("Display all members who are currently renting a particular movie", displayMembersRentingMenu);

        InputMenu goToParentMenu = staffOptionMenu.AddInputMenu("GoToParentMenu", "Return to the main menu");
        staffOptionMenu.AddOption("Return to the main menu", goToParentMenu);

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
                    case "StaffLogin":
                        // We don't need to execute this line but we should because it performs validation we need
                        // staffLoginHandler.Handle(fields, values);

                        string username = values[0];
                        string password = values[1];

                        if (username == StaffLoginHandler.ACCEPTABLE_USERNAME && password == StaffLoginHandler.ACCEPTABLE_PASSWORD)
                        {
                            Console.WriteLine("Welcome, staff member :)");
                            Console.WriteLine();
                            currentDisplay = staffOptionMenu;
                            break;
                        }
                        Console.WriteLine("Incorrect username or password");
                        Console.WriteLine();
                        currentDisplay = currentMenu.parentMenu;
                        break;
                    case "RegisterMember":
                        registerMemberHandler.Handle(fields, values);
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
}