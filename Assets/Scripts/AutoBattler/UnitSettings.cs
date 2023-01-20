/*Copyright (c) 2023, Classified39
All rights reserved.

This source code is licensed under the BSD-style license found in the
LICENSE file in the root directory of this source tree.*/

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
