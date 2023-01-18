/*Copyright (c) 2023, Classified39
All rights reserved.

This source code is licensed under the BSD-style license found in the
LICENSE file in the root directory of this source tree.*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamSettings : MonoBehaviour
{
    public UnityEngine.CanvasGroup inputPanel;
    public UnitSettingsVariable unitSettings;

    public void SetTeam()
    {
        UnityEngine.UI.Toggle toggle = inputPanel.GetComponentInChildren<UnityEngine.UI.Toggle>();
        UnityEngine.CanvasGroup[] panels = inputPanel.GetComponentsInChildren<UnityEngine.CanvasGroup>();


        UnityEngine.CanvasGroup unitPanel;
        UnityEngine.CanvasGroup primaryWeaponPanel;
        UnityEngine.CanvasGroup secondaryWeaponPanel;

        if (toggle.isOn)
        {
            unitPanel = Array.Find(panels, p => p.name == "InfantryPanel");
            primaryWeaponPanel = Array.Find(panels, p => p.name == "TeamWeaponPanel");
            secondaryWeaponPanel = Array.Find(panels, p => p.name == "LeaderWeaponPanel");
        }
        else
        {
            unitPanel = Array.Find(panels, p => p.name == "VehiclePanel");
            primaryWeaponPanel = Array.Find(panels, p => p.name == "VehicleLightPanel");
            secondaryWeaponPanel = Array.Find(panels, p => p.name == "VehicleHeavyPanel");
        }

        TMPro.TMP_InputField inputs = unitPanel.GetComponentInChildren<TMPro.TMP_InputField>();
        Int32.TryParse(inputs.text, out int armor);

        WeaponSettings primaryWeapon = makeWeapon(primaryWeaponPanel);
        WeaponSettings secondaryWeapon = makeWeapon(secondaryWeaponPanel);

        unitSettings.value = new UnitSettings(armor, toggle.isOn, primaryWeapon, secondaryWeapon);
    }

    private WeaponSettings makeWeapon(UnityEngine.CanvasGroup weaponPanel)
    {
        UnityEngine.UI.Toggle[] effects = weaponPanel.GetComponentsInChildren<UnityEngine.UI.Toggle>();
        bool pierce = effects[0].isOn;
        bool explode = effects[1].isOn;
        bool assault = effects[2].isOn;
        bool incen = effects[3].isOn;

        TMPro.TMP_InputField[] inputs = weaponPanel.GetComponentsInChildren<TMPro.TMP_InputField>();
        Int32.TryParse(inputs[0].text, out int damage);
        Int32.TryParse(inputs[1].text, out int init);
        Int32.TryParse(inputs[2].text, out int shots);

        return new WeaponSettings(pierce, explode, assault, incen, damage, init, shots);
    }
}
