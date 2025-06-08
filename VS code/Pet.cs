using System;



public class Pet
{
    
    public delegate void PetStatusHandler(string message, PetStat stat, int newValue);
    public event PetStatusHandler OnStatChanged;

    public delegate void PetDeathHandler(string message);
    public event PetDeathHandler OnDeath;


    



    
    public string Name { get; set; }
    
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

public void DecreaseStats()
    {
        if (!IsAlive) return;

        Hunger--;
        OnStatChanged?.Invoke($"{Name}'s hunger decreased to {Hunger}", PetStat.Hunger, Hunger);

        Sleep--;
        OnStatChanged?.Invoke($"{Name}'s sleep decreased to {Sleep}", PetStat.Sleep, Sleep);

        Fun--;
        OnStatChanged?.Invoke($"{Name}'s fun decreased to {Fun}", PetStat.Fun, Fun);

        if (!IsAlive)
        {
            OnDeath?.Invoke($"{Name} has died!");
        }
    }

    public void IncreaseStat(PetStat stat, int amount)
    {
        switch (stat)
        {
            case PetStat.Hunger:
                Hunger = Math.Min(100, Hunger + amount);
                OnStatChanged?.Invoke($"{Name}'s hunger increased to {Hunger}", PetStat.Hunger, Hunger);
                break;
            case PetStat.Sleep:
                Sleep = Math.Min(100, Sleep + amount);
                OnStatChanged?.Invoke($"{Name}'s sleep increased to {Sleep}", PetStat.Sleep, Sleep);
                break;
            case PetStat.Fun:
                Fun = Math.Min(100, Fun + amount);
                OnStatChanged?.Invoke($"{Name}'s fun increased to {Fun}", PetStat.Fun, Fun);
                break;
        }
    }


    public override string ToString()
    {
        return $"{Name} the {Type} [H:{Hunger} S:{Sleep} F:{Fun}] {(IsAlive ? "" : "(Dead)")}";
    }
}
