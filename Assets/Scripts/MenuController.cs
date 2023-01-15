/*Copyright (c) 2023, Classified39
All rights reserved.

This source code is licensed under the BSD-style license found in the
LICENSE file in the root directory of this source tree.*/

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
