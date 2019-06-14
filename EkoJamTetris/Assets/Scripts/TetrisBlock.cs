// https://www.youtube.com/watch?v=T5P8ohdxDjo
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    public Vector3 rotationPoint;
    private float previousTime;
    public float fallTime = 0.8f;
    Vector2 topRight;
    Vector2 bottomLeft;
    private static Transform[,] grid;

    private void Start() 
    {
        topRight = Camera.main.ViewportToWorldPoint(new Vector2(1.0f, 1.0f));
        bottomLeft = Camera.main.ViewportToWorldPoint(new Vector2(0.0f, 0.0f));
        grid = new Transform[(int)topRight.x, (int)topRight.y];
    }

    void Update()
    {
        if(Input.GetButtonDown("Right"))
        {
            transform.position += new Vector3(1, 0, 0);
            if(!ValidMove())
            {
                transform.position -= new Vector3(1, 0, 0);
            }
        }

        else if(Input.GetButtonDown("Left"))
        {
            transform.position += new Vector3(-1, 0, 0);
            if(!ValidMove())
            {
                transform.position -= new Vector3(-1, 0, 0);
            }
        }
        else if(Input.GetButtonDown("Rotate"))
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
            if(!ValidMove())
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
            }
        }

        if(Time.time - previousTime > (Input.GetButton("Up") ? fallTime / 10 : fallTime))
        {
            transform.position += new Vector3(0, 1, 0);
            if(!ValidMove())
            {
                transform.position -= new Vector3(0, 1, 0);
                //AddToGrid();
                //CheckForLines();
                this.enabled = false;
                FindObjectOfType<BlockSpawnScript>().SpawnBlock();
            }
            previousTime = Time.time;
        }
    }

    void CheckForLines()
    {
        for(int i = (int)topRight.y - 1; i >= 0; i--)
        {
            if(HasLine(i))
            {
                DeleteLine(i);
                RowDown(i);
            }
        }
    }

    bool HasLine(int i)
    {
        for(int j = 0; j < topRight.x; j++)
        {
            if(grid[j, i] == null)
            {
                return false;
            }
        }

        return true;
    }

    void DeleteLine(int i)
    {
        for(int j = 0; j < topRight.x; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }
    }

    void RowDown(int i)
    {
        for(int y = i; y < topRight.y; y++)
        {
            for(int j = 0; j < topRight.x; j++)
            {
                grid[j, y-1] = grid[j, y];
                grid[j, y] = null;
                grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
            }
        }
    }

    void AddToGrid()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            grid[roundedX, roundedY] = children;
        }
    }

    bool ValidMove()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if(roundedX < bottomLeft.x || roundedX >= topRight.x || roundedY < bottomLeft.y || roundedY >= topRight.y)
            {
                return false;
            }

            // if(grid[roundedX, roundedY] != null)
            // {
            //     return false;
            // }
        }

        return true;
    }
}
