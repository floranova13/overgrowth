using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using GameExtensions;

[Serializable]
public class SeekerSlot
{
    public Citizen Citizen { get; }
    public Resource Weapon { get; }
    public Resource Armor { get; }
    public Resource Equipment { get; }
    public Resource Miscellaneous { get; }

    public SeekerSlot(Citizen citizen, Resource weapon, Resource armor, Resource equipment, Resource miscellaneous)
    {
        Citizen = citizen;
        Weapon = weapon;
        Armor = armor;
        Equipment = equipment;
        Miscellaneous = miscellaneous;
    }
}