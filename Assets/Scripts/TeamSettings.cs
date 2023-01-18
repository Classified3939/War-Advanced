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

    void Start()
    {
        UnityEngine.UI.Toggle[] toggles = inputPanel.GetComponentsInChildren<UnityEngine.UI.Toggle>();

        if (unitSettings.value != null)
        {
            if (!unitSettings.value.isInfantry)
            {
                toggles[1].isOn = true;
            }

            UnityEngine.CanvasGroup[] panels = inputPanel.GetComponentsInChildren<UnityEngine.CanvasGroup>();

            UnityEngine.CanvasGroup unitPanel;
            UnityEngine.CanvasGroup primaryWeaponPanel;
            UnityEngine.CanvasGroup secondaryWeaponPanel;

            if (unitSettings.value.isInfantry)
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

            TMPro.TMP_InputField input = unitPanel.GetComponentInChildren<TMPro.TMP_InputField>();
            input.text = unitSettings.value.armor.ToString();

            setWeaponPanel(primaryWeaponPanel, true);
            setWeaponPanel(secondaryWeaponPanel, false);
        }
    }

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

        TMPro.TMP_InputField input = unitPanel.GetComponentInChildren<TMPro.TMP_InputField>();
        Int32.TryParse(input.text, out int armor);

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

    private void setWeaponPanel(UnityEngine.CanvasGroup weaponPanel, bool isPrimary)
    {
        UnityEngine.UI.Toggle[] effects = weaponPanel.GetComponentsInChildren<UnityEngine.UI.Toggle>();
        TMPro.TMP_InputField[] inputs = weaponPanel.GetComponentsInChildren<TMPro.TMP_InputField>();
        if (isPrimary)
        {
            effects[0].isOn = unitSettings.value.primary.isPiercing;
            effects[1].isOn = unitSettings.value.primary.isExplosive;
            effects[2].isOn = unitSettings.value.primary.isAssault;
            effects[3].isOn = unitSettings.value.primary.isIncendiary;

            inputs[0].text = unitSettings.value.primary.damage.ToString();
            inputs[1].text = unitSettings.value.primary.initiative.ToString();
            inputs[2].text = unitSettings.value.primary.shots.ToString();
        }
        else
        {
            effects[0].isOn = unitSettings.value.secondary.isPiercing;
            effects[1].isOn = unitSettings.value.secondary.isExplosive;
            effects[2].isOn = unitSettings.value.secondary.isAssault;
            effects[3].isOn = unitSettings.value.secondary.isIncendiary;

            inputs[0].text = unitSettings.value.secondary.damage.ToString();
            inputs[1].text = unitSettings.value.secondary.initiative.ToString();
            inputs[2].text = unitSettings.value.secondary.shots.ToString();
        }
    }
}
