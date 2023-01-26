using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTest : MonoBehaviour
{
    private SquareGrid grid;
    void Start()
    {
        grid = new SquareGrid(4, 2, 3f, new Vector3(-5, -5));
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            grid.SetValue(mousePosition, 56);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(grid.GetValue(mousePosition));
        }
    }
}
