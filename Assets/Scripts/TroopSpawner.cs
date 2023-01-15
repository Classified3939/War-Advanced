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
    // Start is called before the first frame update
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
