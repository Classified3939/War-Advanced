using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSettings
{
    public int armor;
    public bool isInfantry;
    public WeaponSettings primary;
    public WeaponSettings secondary;

    public UnitSettings(int armor, bool isInfantry, WeaponSettings primary, WeaponSettings secondary)
    {
        this.armor = armor;
        this.isInfantry = isInfantry;
        this.primary = primary;
        this.secondary = secondary;
    }
}
