/*Copyright (c) 2023, Classified39
All rights reserved.

This source code is licensed under the BSD-style license found in the
LICENSE file in the root directory of this source tree.*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackMenuController : MonoBehaviour
{
    [SerializeField] UnitSettingsVariable blueSettings;
    [SerializeField] UnitSettingsVariable redSettings;
    [SerializeField] TerrainVariable terrainSettings;
    public GameObject defendWinText;
    public GameObject attackWinText;
    public GameObject tieText;
    public GameObject drawText;
    // Start is called before the first frame update
    void Start()
    {
        if (WinLossVariable.blueLoss && WinLossVariable.redLoss)
        {
            tieText.SetActive(true);
        }
        else if (WinLossVariable.blueLoss)
        {
            attackWinText.SetActive(true);
        }
        else if (WinLossVariable.redLoss)
        {
            defendWinText.SetActive(true);
        }
        else
        {
            drawText.SetActive(true);
        }
    }

    public void showAgainPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void restartPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
        WinLossVariable.blueLoss = false;
        WinLossVariable.redLoss = false;
    }
}
