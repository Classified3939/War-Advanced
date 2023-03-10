/*Copyright (c) 2023, Classified39
All rights reserved.

This source code is licensed under the BSD-style license found in the
LICENSE file in the root directory of this source tree.*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopSpawner : MonoBehaviour
{
    public UnitSettingsVariable settings;
    public TerrainVariable terrainSettings;
    public bool isDefending;
    public Vector2 spawnLocation;
    public Vector2 enemyLocation;
    public GameObject infantryTemplate;
    public GameObject vehicleTemplate;
    private GameObject troopInstance;

    void Start()
    {
        if (settings.value.isInfantry)
        {
            troopInstance = Instantiate(infantryTemplate, spawnLocation, Quaternion.identity);
        }
        else
        {
            troopInstance = Instantiate(vehicleTemplate, spawnLocation, Quaternion.identity);
        }

        troopInstance.GetComponent<Troop>().setSettings(settings, terrainSettings, isDefending);
        troopInstance.GetComponent<Troop>().setTarget(enemyLocation);
    }

}
