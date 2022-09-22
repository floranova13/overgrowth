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
        Name = CitizenUtilities.GetRandomName(Gender);
        Reputation = 0;
    }

    public void ChangeReputation(int amount)
    {
        Reputation += amount;
        Reputation = Reputation.Clamp(-1000, 1000);
    }
}