using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using GameExtensions;

[Serializable]
public class Citizen
{
    public string Name { get; }
    public Gender Gender { get; }
    public int Reputation { get; private set; }

    public Citizen()
    {
        if (5.PercentChance())
        {
            Gender = Gender.Nonbinary;
        }
        else if (50.PercentChance())
        {
            Gender = Gender.Female;
        }
        else
        {
            Gender = Gender.Male;
        }
        Name = GetRandomName(Gender);
        Reputation = 0;
    }

    public string GetRandomName(Gender gender)
    {
        List<string> nonbinaryNames = new() { "Lillium", "Robin", "Ash", "Koda", "Riley", "Lux" };
        List<string> femaleNames = new() { "Amanda", "Veronica", "Cera", "Lauren", "Brianna", "Erin" };
        List<string> maleNames = new() { "Adam", "Noah", "Thomas", "Stephan", "Francis", "Benjamin" };

        string chosenName = gender switch
        {
            Gender.Nonbinary => nonbinaryNames.PickRandom(),
            Gender.Female => femaleNames.PickRandom(),
            Gender.Male => maleNames.PickRandom(),
            _ => nonbinaryNames.Concat(femaleNames).Concat(maleNames).ToList().PickRandom(),
        };

        return chosenName;
    }

    public void ChangeReputation(int amount)
    {
        Reputation += amount;
        Reputation = Reputation.Clamp(-1000, 1000);
    }
}