using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisGrid : MonoBehaviour
{
    public Transform[,] grid;
    public int width;
    public int height;
    int plusX;
    int plusY;
    public Vector2 topRight;
    public Vector2 bottomLeft;

    private void Start() 
    {
        //Camera.main.transform.position = new Vector3(13f, 7f, -10f);
        topRight = Camera.main.ViewportToWorldPoint(new Vector2(1.0f, 1.0f));
        bottomLeft = Camera.main.ViewportToWorldPoint(new Vector2(0.0f, 0.0f));
        plusX = Mathf.RoundToInt(Mathf.Sign(bottomLeft.x));
        plusY = Mathf.RoundToInt(Mathf.Sign(bottomLeft.y));
        height = Mathf.RoundToInt(topRight.y);
        width = Mathf.RoundToInt(topRight.x);
        grid = new Transform[width+plusX, height+plusY]; //new Transform[width+plusX, height+plusY];
    }

    public Vector2 roundVec2(Vector2 v) {
    return new Vector2(Mathf.Round(v.x),
                       Mathf.Round(v.y));
    }

    public void CheckForLines(GameObject block)
    {
        for(int i = (height+plusY+plusY) - 1; i >= 0; i--)
        {
            if(HasLine(i, block))
            {
                DeleteLine(i, block);
                RowDown(i, block);
            }
        }
    }

    public bool HasLine(int i, GameObject block)
    {
        for(int j = 0; j < width+plusX; j++)
        {
            if(grid[j, i] == null)
            {
                return false;
            }
        }

        return true;
    }

    public void DeleteLine(int i, GameObject block)
    {
        for(int j = 0; j < width+plusX; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }
    }

    public void RowDown(int i, GameObject block)
    {
        for(int y = i; y < height+plusY; y++)
        {
            for(int j = 0; j < width+plusX; j++)
            {
                if(grid[j,y] != null)
                {
                    grid[j, y-1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }

    public void AddToGrid(GameObject block)
    {
        foreach (Transform children in block.transform)
        {
            int roundedX = Mathf.FloorToInt(children.transform.position.x);
            int roundedY = Mathf.FloorToInt(children.transform.position.y);
            roundedX += plusX;
            roundedY += plusY;
            grid[roundedX, roundedY] = children;
        }
    }

    public bool insideBorder(Vector2 pos) {
    return ((int)pos.x >= 0 &&
            (int)pos.x < width+plusX &&
            (int)pos.y >= 0);
    }

    public void deleteRow(int y) {
    for (int x = 0; x < width+plusX; ++x) {
        Destroy(grid[x, y].gameObject);
        grid[x, y] = null;
        }
    }

    public void decreaseRow(int y) {
    for (int x = 0; x < width+plusX; ++x) {
        if (grid[x, y] != null) {
            // Move one towards bottom
            grid[x, y-1] = grid[x, y];
            grid[x, y] = null;

            // Update Block position
            grid[x, y-1].position += new Vector3(0, -1, 0);
        }
    }
    }

    public void decreaseRowsAbove(int y) {
    for (int i = y; i < height+plusY; ++i)
        decreaseRow(i);
    }

    public bool isRowFull(int y) {
    for (int x = 0; x < width+plusX; ++x)
        if (grid[x, y] == null)
            return false;
    return true;
    }

    public void deleteFullRows() {
    for (int y = 0; y < height+plusY; ++y) {
        if (isRowFull(y)) {
            deleteRow(y);
            decreaseRowsAbove(y+1);
            --y;
        }
    }
    }
}
