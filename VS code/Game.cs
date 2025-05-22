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
{
        var petTypeMenu = new Menu<PetType>("Select Pet Type",
            Enum.GetValues(typeof(PetType)).Cast<PetType>().ToList(),
            pt => pt.ToString());

        var selectedType = petTypeMenu.ShowAndGetSelection();
        if (selectedType == default) return;

        Console.Clear();
        Console.Write("Enter your pet's name: ");
        string name = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Invalid name. Press any key to continue...");
            Console.ReadKey();
            return;
        }

        var newPet = new Pet(name, selectedType);

  // For pet events
        newPet.OnStatChanged += (message, stat, value) =>
        {
            Console.WriteLine($"[STAT CHANGE] {message}");
            if (value <= 20)  // Warning: when stats are low
            {
                Console.WriteLine($"Warning: {newPet.Name}'s {stat} is getting low!");
            }
        };

        newPet.OnDeath += (message) =>
        {
            Console.WriteLine($"[ALERT] {message}");
            _pets.Remove(newPet);
            Console.WriteLine($"{newPet.Name} has been removed from your pets.");
        };

        _pets.Add(newPet);

        Console.WriteLine($"You've adopted {name} the {selectedType}!");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();

        _ = StartPetStatDecay(newPet);
    }

    private async Task StartPetStatDecay(Pet pet)
    {
        while (pet.IsAlive && _isRunning)
        {
            await Task.Delay(3000);
            pet.DecreaseStats();

            if (!pet.IsAlive)
            {
                Console.WriteLine("Press any key to continue...");

            }
        }
    }