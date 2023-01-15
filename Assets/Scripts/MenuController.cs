using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour
{
    [SerializeField] TerrainVariable terrainSettings;
    [SerializeField] TMPro.TMP_Dropdown terrainDropdown;
    public void StartGame()
    {
        terrainSettings.value = terrainDropdown.value;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
