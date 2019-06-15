﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheBlock : MonoBehaviour
{
    public Vector3 rotationPoint;
    private float previousTime;
    public float fallTime = 0.8f;
    public static int width = 39;
    public static int height = 21;
    public static Transform[,] grid = new Transform[width, height];

    void Start() 
    {
        if(!ValidMove())
        {
            // Game over
        }
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
                AddToGrid();
                CheckForLines();
                FindObjectOfType<BlockSpawnScript>().SpawnBlock();
                this.enabled = false; 
            }
            previousTime = Time.time;
        }
    }

    bool ValidMove()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if(roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= height)
            {
                return false;
            }
            

            if(grid[roundedX, roundedY] != null)
            {
                return false;
            }
        }

        return true;
    }

    public void CheckForLines()
    {
        for(int i = height - 1; i >= 0; i--)
        {
            if(HasLine(i))
            {
                DeleteLine(i);
                RowDown(i);
            }
        }
    }

    public bool HasLine(int i)
    {
        for(int j = 0; j < width; j++)
        {
            if(grid[j, i] == null)
            {
                return false;
            }
        }

        return true;
    }

    public void DeleteLine(int i)
    {
        for(int j = 0; j < width; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }
    }

    public void RowDown(int i)
    {
        for(int j = 0; j < width; j++)
        {
            int index = i;
            while(grid[j, index-1] != null)
            {
                grid[j, index-1].transform.position += new Vector3(0, 1, 0);
                index--;
            }   
        }

        // for(int y = height; y >= i; y--)
        // {
        //     for(int j = 0; j < width; j++)
        //     {
        //         Debug.Log(y);
        //         if(grid[j,y-1] != null)
        //         {
        //             // grid[j, y] = grid[j, y-1];
        //             // grid[j, y-1] = null;
        //             grid[j, y] = null;
        //             grid[j, y-1].transform.position += new Vector3(0, 1, 0);
        //         }
        //     }
        // }
    }

    public void AddToGrid()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);
            grid[roundedX, roundedY] = children;
        }
    }

    public void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                if(grid[i, j] != null)
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawSphere(grid[i, j].position, 0.5f);
                }
            }
        }
        
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(rotationPoint+transform.position, 0.2f);
    }
}