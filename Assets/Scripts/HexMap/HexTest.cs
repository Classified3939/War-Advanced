using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTest : MonoBehaviour
{
    private HexGrid grid;
    private GameObject lastObjectSelected;
    void Start()
    {
        grid = new HexGrid(22, 12, 1f, new Vector3(-11, -4.5f));
        //grid = new HexGrid(2, 2, 1f, new Vector3(0, 0));
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            grid.SetValue(mousePosition, grid.GetValue(mousePosition) + 1);
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (lastObjectSelected != null && lastObjectSelected != grid.getHexObject(mousePosition))
            {
                Tuple<int, int> oldPosition = grid.getHexCoords(lastObjectSelected);
                grid.unselectHex(oldPosition.Item1, oldPosition.Item2);
                grid.selectHex(mousePosition);
                lastObjectSelected = grid.getHexObject(mousePosition);
            }
            else
            {
                grid.selectHex(mousePosition);
                lastObjectSelected = grid.getHexObject(mousePosition);
            }


        }
    }
}
