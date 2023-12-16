
using PetPals.Dao;
using PetPals.Entities;
using PetPals.Exceptions;

Console.WriteLine("--------Welcome to PetPals--------");
Console.WriteLine("\nBring home the bundle of joys and make your life happier than ever!");

int loopChoice = 1;
while(loopChoice==1)
{
    Console.WriteLine("Here are your choices:");
    Console.WriteLine("1. User Management");
    Console.WriteLine("2. Donate for a good cause");
    Console.WriteLine("3. Pets Management");
    Console.WriteLine("4. Adoption Event");
    Console.WriteLine("5. Exit");
    Console.Write("\nPlease enter your choice: ");
    int mainChoice=int.Parse(Console.ReadLine());
    switch(mainChoice)
    {
        case 1:
            Console.WriteLine("Welcome to user management portal.");
            Console.WriteLine("Press 1 to become a member of our evergrowing community!");
            Console.WriteLine("Press 2 to view list of all users");
            Console.Write("\nEnter the choice: ");
            int userChoice=int.Parse(Console.ReadLine());
            switch(userChoice)
            {
                case 1:
                    Console.WriteLine("Enter ID: ");
                    int userid=int.Parse(Console.ReadLine());
                    Console.Write("\nTo become a member, please enter your name: ");
                    string username=Console.ReadLine();
                    IPetPalsRepo addUser = new PetPalsRepo();
                    try
                    {
                        addUser.registerUser(userid, username);
                    }
                    catch(UserNotAddedException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;
                case 2:
                    IPetPalsRepo showUsers = new PetPalsRepo();
                    Console.WriteLine("\nHere is a list of all users:");
                    List<User> users = showUsers.displayUsers();
                    foreach(var v in users)
                    {
                        Console.WriteLine(v);
                    }
                    break;
                default:
                    Console.WriteLine("Please enter a valid choice");
                    break;
            }
            break;

        case 2:
            Console.WriteLine("Make a donation");
            Console.WriteLine("You can make a donation in following ways:");
            Console.WriteLine("1. Cash donation");
            Console.WriteLine("2. Item donation");
            Console.Write("\nSelect the type of donation you would like to make: ");
            int donationChoice=int.Parse(Console.ReadLine());
            switch(donationChoice)
            {
                case 1:
                    Console.WriteLine("\nFor the cash donations, we require a minimum amount to be Rs. 1000");
                    Console.Write("\nPlease enter donationID: ");
                    int did=int.Parse(Console.ReadLine());
                    Console.Write("\nPlease enter your name: ");
                    string cname=Console.ReadLine();
                    Console.Write("\nEnter the amount you would like to donate: ");
                    int amount=int.Parse(Console.ReadLine());
                    IPetPalsRepo cashDonation = new PetPalsRepo();
                    try
                    {
                        cashDonation.recordCashDonation(did, cname, amount);
                    }
                    catch(InsufficientExecutionStackException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;
                case 2:
                    Console.Write("\nPlease enter donationID: ");
                    int id = int.Parse(Console.ReadLine());
                    Console.Write("\nPlease enter your name: ");
                    string iname = Console.ReadLine();
                    Console.Write("\nPlease enter the name of the item you would like to donate: ");
                    string item=Console.ReadLine();
                    IPetPalsRepo itemDonation = new PetPalsRepo();
                    itemDonation.recordItemDonation(id, iname, item);
                    break;
                default:
                    Console.WriteLine("Please enter a valid choice");
                    break;
            }
            break;
        case 3:
            Console.WriteLine("\nWelcome to Pet management portal");
            Console.WriteLine("\nTo view available pets for adoption, press 1");
            Console.WriteLine("To add a pet data to our system, press 2");
            Console.WriteLine("To remove a pet data from our system, press 3");
            Console.Write("\nEnter your choice: ");
            int petChoice=int.Parse(Console.ReadLine());
            switch(petChoice)
            {
                case 1:
                    Console.WriteLine("\nHere are all the pets that are currently available for adoption: ");
                    IPetPalsRepo showPets=new PetPalsRepo();
                    List<Pet> pets = showPets.showAvailablePets();
                    foreach(var v in pets)
                    {
                        Console.WriteLine(v);
                    }
                    break;
                case 2:
                    Console.WriteLine("\nTo add details of a pet in our system, please provide following info:");
                    Console.Write("ID: ");
                    int pid=int.Parse(Console.ReadLine());
                    Console.Write("Name: ");
                    string petname=Console.ReadLine();
                    Console.Write("Age: ");
                    int age=int.Parse(Console.ReadLine());
                    Console.Write("Type(E.g. Cat, Dog, etc.): ");
                    string type=Console.ReadLine();
                    Console.WriteLine("Breed");
                    string breed=Console.ReadLine();
                    IPetPalsRepo addPet = new PetPalsRepo();
                    try
                    {
                        addPet.addPet(pid, petname, age, breed, type);
                    }
                    catch (InvalidPetAgeException e)
                    { Console.WriteLine(e.Message); }
                    
                    break;
                case 3:
                    Console.Write("\nTo remove data of a specific pet from our system, please enter the ID of the pet: ");
                    int removePetId = int.Parse(Console.ReadLine());
                    IPetPalsRepo removePet=new PetPalsRepo();
                    try
                    {
                        removePet.removePet(removePetId);
                    }
                    catch (PetNotFoundException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;
                default:
                    Console.WriteLine("Please enter a valid choice");
                    break;
            }
            break;
        case 4:
            Console.WriteLine("\nWelcome to Adoption event!");
            Console.WriteLine("Here is a list of all pets that are currently available for adoption:\n");
            IPetPalsRepo adoptPet = new PetPalsRepo();
            List<Pet> avblPets = adoptPet.showAvailablePets();
            foreach (Pet p in avblPets)
            {
                Console.WriteLine(p);
            }
            Console.Write("\nEnter the ID of the pet that you wish to adopt: ");
            int paid = int.Parse(Console.ReadLine());
            Console.WriteLine("Great, now you are just one step ahead from taking home your furry bundle of joys.");
            Console.Write("Just enter your userID now: ");
            int uid = int.Parse(Console.ReadLine());
            adoptPet.adoptPet(paid, uid);
            Console.WriteLine("\nAnd you are all set!");
            Console.WriteLine($"Thank you for giving our furry baby a new life!");
            Console.WriteLine("We wish you a good day");
            break;
        case 5:
            loopChoice = 0;
            break;
        default:
            Console.WriteLine("Please enter a valid choice");
            break;
    }
}