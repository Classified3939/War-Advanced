/*Copyright (c) 2023, Classified39
All rights reserved.

This source code is licensed under the BSD-style license found in the
LICENSE file in the root directory of this source tree.*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public void TeamLoss(bool isDefending)
    {
        if (isDefending)
        {
            WinLossVariable.blueLoss = true;
        }
        else
        {
            WinLossVariable.redLoss = true;
        }

        StartCoroutine(endGame());
    }

    public void TeamDraw()
    {
        StartCoroutine(endGame());
    }

    IEnumerator endGame()
    {
        yield return new WaitForSecondsRealtime(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
