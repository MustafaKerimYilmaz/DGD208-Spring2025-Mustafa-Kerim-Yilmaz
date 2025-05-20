using System;



public class Pet
{
    
    public delegate void PetStatusHandler(string message, PetStat stat, int newValue);
    public event PetStatusHandler OnStatChanged;

    public delegate void PetDeathHandler(string message);
    public event PetDeathHandler OnDeath;


    



    // Pet's name ( "Buddy", "Fluffy")
    public string Name { get; set; }
    // Type of the pet ( Dog, Cat)
    public PetType Type { get; set; }
    public int Hunger { get; set; } = 50;
    public int Sleep { get; set; } = 50;
    public int Fun { get; set; } = 50;
    public bool IsAlive => Hunger > 0 && Sleep > 0 && Fun > 0;

    public Pet(string name, PetType type)
    {
        Name = name;
        Type = type;
    }

