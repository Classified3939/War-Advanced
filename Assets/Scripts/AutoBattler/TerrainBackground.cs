/*Copyright (c) 2023, Classified39
All rights reserved.

This source code is licensed under the BSD-style license found in the
LICENSE file in the root directory of this source tree.*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainBackground : MonoBehaviour
{
    [SerializeField] TerrainVariable settings;
    // Start is called before the first frame update
    void Start()
    {
        if (settings.value == 0)
        {
            gameObject.GetComponent<SpriteRenderer>().material.color = new Color32(172, 255, 163, 255);
        }
        else if (settings.value == 1)
        {
            gameObject.GetComponent<SpriteRenderer>().material.color = new Color32(43, 140, 4, 255);
        }
        else if (settings.value == 2)
        {
            gameObject.GetComponent<SpriteRenderer>().material.color = new Color32(104, 100, 117, 255);
        }
    }
}
