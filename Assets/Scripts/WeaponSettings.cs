using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct WeaponSettings
{
    public WeaponSettings(bool pierce = false, bool explode = false, int damage = 0, int init = 0, int shots = 0)
    {
        this.isPiercing = pierce;
        this.isExplosive = explode;
        this.damage = damage;
        this.initiative = init;
        this.shots = shots;
    }

    public bool isPiercing { get; }
    public bool isExplosive { get; }
    public int damage { get; }
    public int initiative { get; }
    public int shots { get; }
}
