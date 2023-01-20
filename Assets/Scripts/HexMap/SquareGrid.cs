/*Copyright (c) 2023, Classified39
All rights reserved.

This source code is licensed under the BSD-style license found in the
LICENSE file in the root directory of this source tree.*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareGrid
{
    private int width;
    private int height;
    private int[,] gridArray;

    public SquareGrid(int width, int height)
    {
        this.width = width;
        this.height = height;

        gridArray = new int[width, height];
    }

}

