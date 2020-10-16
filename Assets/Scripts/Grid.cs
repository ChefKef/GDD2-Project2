using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    private int width;
    private int height;
    private float cellSize;
    private int[,] gridArray;

    private Vector3 Offset = new Vector3(10.5f, 4.5f, 0);

    //public GameObject TestBlock;

    public Grid(int width, int height, float cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        gridArray = new int[width, height];

        for (int x=0; x<gridArray.GetLength(0); x++)        //can also just use width
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)        //can also just use width
            {
                //GameObject instanceBlock;
                //instanceBlock = Instantiate(TestBlock, GetWorldPosition(x,y), Quaternion.identity);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
            }
        }

        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return (new Vector3(x, y) * cellSize) - Offset;
    }
}
