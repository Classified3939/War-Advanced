using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HexGrid
{
    private int width;
    private int height;
    private float cellSize;
    private Vector3 origin;
    private int[,] gridArray;
    private TextMesh[,] textArray;
    private GameObject[,] hexArray;
    private bool[,] selectedHexes;
    private const float HEX_VERTICAL_OFFSET = 0.75f;

    private GameObject hexPrefab = Resources.Load<GameObject>("Prefabs/HexMap/HexSprite");
    private GameObject selectedHexPrefab = Resources.Load<GameObject>("Prefabs/HexMap/HexSpriteSelected");

    public HexGrid(int width, int height, float cellSize, Vector3 origin)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.origin = origin;

        gridArray = new int[width, height];
        textArray = new TextMesh[width, height];
        hexArray = new GameObject[width, height];
        selectedHexes = new bool[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                textArray[x, y] = makeWorldText(gridArray[x, y].ToString(), null, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * 0.5f);
                hexArray[x, y] = makeHexagon(null, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * 0.5f);
            }
        }
    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, 0, 0) * cellSize +
        new Vector3(0, y, 0) * cellSize * HEX_VERTICAL_OFFSET +
        ((y % 2) == 1 ? new Vector3(1, 0, 0) * cellSize * .5f : Vector3.zero)
        + origin;
    }

    private void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        int roughX = Mathf.RoundToInt((worldPosition - origin).x / cellSize);
        int roughY = Mathf.RoundToInt((worldPosition - origin).y / cellSize / HEX_VERTICAL_OFFSET);
        bool oddRow = roughY % 2 == 1;
        Vector3Int roughXY = new Vector3Int(roughX, roughY, 0);
        List<Vector3Int> neighborXYList = new List<Vector3Int>{
            roughXY + new Vector3Int(-1,0,0),
            roughXY + new Vector3Int(1,0,0),

            roughXY + new Vector3Int((oddRow ? 1:-1),1,0),
            roughXY + new Vector3Int(0,1,0),

            roughXY + new Vector3Int((oddRow ? 1:-1),-1,0),
            roughXY + new Vector3Int(0,-1,0),
        };
        Vector3Int closestXY = roughXY;
        foreach (Vector3Int neighborXY in neighborXYList)
        {
            if (Vector3.Distance(worldPosition, GetWorldPosition(neighborXY.x, neighborXY.y)) <
                Vector3.Distance(worldPosition, GetWorldPosition(closestXY.x, closestXY.y)))
            {
                //Neighbor is closer to rough position
                closestXY = neighborXY;
            }
        }
        x = closestXY.x;
        y = closestXY.y;
    }

    private TextMesh makeWorldText(string text, Transform parent = null, Vector3 localPosition = default(Vector3))
    {
        GameObject gameObject = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/HexMap/WorldText"), parent);
        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.text = text;
        return textMesh;
    }

    private GameObject makeHexagon(Transform parent = null, Vector3 localPosition = default(Vector3), bool isSelected = false)
    {
        GameObject gameObject;
        if (!isSelected)
        {
            gameObject = GameObject.Instantiate(hexPrefab, parent);
        }
        else
        {
            gameObject = GameObject.Instantiate(selectedHexPrefab, parent);
        }
        gameObject.transform.localScale = new Vector3(cellSize, cellSize);
        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        return gameObject;
    }

    public void SetValue(int x, int y, int value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;
            textArray[x, y].text = gridArray[x, y].ToString();
        }
    }

    public void SetValue(Vector3 worldPosition, int value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetValue(x, y, value);
    }

    public int GetValue(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }
        else
        {
            return -1;
        }
    }

    public int GetValue(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetValue(x, y);
    }

    public void selectHex(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        if (x < 0 || y < 0 || x > width || y > height) return;
        if (!selectedHexes[x, y])
        {
            selectedHexes[x, y] = true;
            GameObject.Destroy(hexArray[x, y]);
            hexArray[x, y] = makeHexagon(null, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * 0.5f, true);
        }
    }

    public void selectHex(int x, int y)
    {
        if (!selectedHexes[x, y])
        {
            selectedHexes[x, y] = true;
            GameObject.Destroy(hexArray[x, y]);
            hexArray[x, y] = makeHexagon(null, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * 0.5f, true);
        }
    }

    public void unselectHex(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        if (x < 0 || y < 0 || x > width || y > height) return;
        selectedHexes[x, y] = false;
        GameObject.Destroy(hexArray[x, y]);
        hexArray[x, y] = makeHexagon(null, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * 0.5f, false);
    }

    public void unselectHex(int x, int y)
    {
        if (x < 0 || y < 0 || x > width || y > height) return;
        selectedHexes[x, y] = false;
        GameObject.Destroy(hexArray[x, y]);
        hexArray[x, y] = makeHexagon(null, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * 0.5f, false);
    }

    public Tuple<int, int> getHexCoords(GameObject gameObject)
    {
        for (int i = 0; i < hexArray.GetLength(0); i++)
        {
            for (int j = 0; j < hexArray.GetLength(1); j++)
            {
                if (hexArray[i, j] == gameObject)
                {
                    return Tuple.Create(i, j);
                }
            }
        }
        return Tuple.Create(-1, -1);
    }

    public GameObject getHexObject(Tuple<int, int> coords)
    {
        int x = coords.Item1;
        int y = coords.Item2;
        if (x < 0 || y < 0 || x > width || y > height) return null;
        return hexArray[x, y];
    }

    public GameObject getHexObject(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return hexArray[x, y];
        }
        else
        {
            return null;
        }
    }
}
