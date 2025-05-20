using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
public class StudentInfo
{
    public string Name { get; }
    public string StudentNumber { get; }

    public StudentInfo(string name, string studentNumber)
    {
        Name = name;
        StudentNumber = studentNumber;
    }
}

public class Game
{
    private bool _isRunning;
    private List<Pet> _pets = new List<Pet>();
    private readonly Menu<string> _mainMenu;

    private readonly StudentInfo _studentInfo = new StudentInfo(   
    "Mustafa Kerim Yilmaz",
    "229911278"
    );


    public Game()
    {
        _mainMenu = new Menu<string>("Main Menu", new List<string>
        {
            "Adopt a new pet",
            "View your pets",
            "Use an item on a pet",
            "Show creator info",
            "Exit"
        }, item => item);
    }

    public async Task GameLoop()
    {
        Initialize();

        _isRunning = true;
        while (_isRunning)
        {

            string userChoice = _mainMenu.ShowAndGetSelection();


            await ProcessUserChoice(userChoice);
        }

        Console.WriteLine("Thanks for playing!");
    }

    private void Initialize()
    {
        Console.WriteLine("Welcome to the Pet Simulator!");
        Console.WriteLine("Stats decrease over time. Keep them above 0 to keep your pets alive!");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private async Task ProcessUserChoice(string choice)
    {
        if (choice == null) return;

        if (choice == "Exit")
        {
            _isRunning = false;
        }
        else if (choice == "Adopt a new pet")
        {
            await AdoptPet();
        }
        else if (choice == "View your pets")
        {
            ViewPets();
        }
        else if (choice == "Use an item on a pet")
        {
            await UseItemOnPet();
        }
        else if (choice == "Show creator info")
        {
            ShowCreatorInfo();
        }
    }

    private async Task AdoptPet()
