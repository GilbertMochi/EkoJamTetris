﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SpawnPoint
{
    public Transform point;
    public bool inUse;
}

public class BlockSpawnScript : MonoBehaviour
{
    public GameObject[] blocks;
    public SpawnPoint[] spawnPoints;
    public int spawnPointsInUse;
    public GameObject newCloud;
    public GameObject cloud;
    public GameObject trail;
    Camera mainCam;
    public GameObject sspGO;
    public List<GameObject> selectableBlocks = new List<GameObject>();
    int currentSelection = 0;
    public bool canSelect;
    public TheBlock playerIsPlaying;

    // Start is called before the first frame update
    void Start()
    {
        spawnPointsInUse = 0;
        mainCam = Camera.main;
        StartCoroutine(SpawnBlock());
        StartCoroutine(SpawnBlock());
        //SpawnBlock();
    }

    void Update() 
    {
        if(!canSelect || selectableBlocks.Count <= 0 || playerIsPlaying != null)
        {
            return;
        }

        if(Input.GetButtonDown("Right"))
        {
            NextSelection();
        }
        else if(Input.GetButtonDown("Left"))
        {
            LastSelection();
        }
        else if(Input.GetButtonDown("Up"))
        {
            SendBlock();
        }
    }

    void NextSelection()
    {
        selectableBlocks[currentSelection].GetComponent<BlockFlashing>().enabled = false;
        currentSelection++;
        if(currentSelection >= selectableBlocks.Count)
        {
            currentSelection = 0;
        }
        selectableBlocks[currentSelection].GetComponent<BlockFlashing>().enabled = true;
    }

    void LastSelection()
    {
        selectableBlocks[currentSelection].GetComponent<BlockFlashing>().enabled = false;
        currentSelection--;
        if(currentSelection < 0)
        {
            currentSelection = selectableBlocks.Count-1;
        }
        selectableBlocks[currentSelection].GetComponent<BlockFlashing>().enabled = true;
    }

    void SendBlock()
    {
        selectableBlocks[currentSelection].GetComponent<BlockFlashing>().enabled = false;
        selectableBlocks[currentSelection].GetComponent<TheBlock>().enabled = true;
        playerIsPlaying = selectableBlocks[currentSelection].GetComponent<TheBlock>();
        spawnPointsInUse--;
        selectableBlocks.RemoveAt(currentSelection);
        canSelect = false;
    }

    Vector2 GetSpawnPoint()
    {
        Vector2 spawnPoint = mainCam.ViewportToWorldPoint(new Vector2(Random.Range(0.4f, 0.6f), 0.2f));
        spawnPoint.x = Mathf.RoundToInt(spawnPoint.x);
        spawnPoint.y = Mathf.RoundToInt(spawnPoint.y);
        return spawnPoint;
    }

    public IEnumerator SpawnBlock()
    {
        List<GameObject> childClouds = new List<GameObject>();
        if(spawnPointsInUse >= spawnPoints.Length)
        {
            yield break;
        }

        int randomSP = 0;

        do
        {
            randomSP = Random.Range(0, spawnPoints.Length);
        } while(spawnPoints[randomSP].inUse);

        int randomized = Random.Range(0, blocks.Length);
        GameObject newBlock = Instantiate(blocks[randomized], spawnPoints[randomSP].point.position, 
        Quaternion.identity);
        selectableBlocks.Add(newBlock);
        spawnPoints[randomSP].inUse = true;
        GameObject newNewCloud = Instantiate(newCloud);
        newNewCloud.SetActive(false);
        Color randomizedCol = new Color(
                Random.Range(0.0f, 1.0f),
                Random.Range(0.0f, 1.0f),
                Random.Range(0.0f, 1.0f),
                0.1f
            );
        foreach (Transform child in newBlock.transform)
        {
            childClouds.Add(child.gameObject);
            child.GetComponent<SpriteRenderer>().color = randomizedCol;
            newNewCloud.GetComponent<SetSpawnPoint>().SpawnLocations.Add(child.gameObject);
        }  
        newNewCloud.SetActive(true);
        
        yield return new WaitForSeconds(4f);
        for(int i = 0; i < childClouds.Count; i++)
        {
            yield return new WaitForSeconds(2f);
            Instantiate(cloud, childClouds[i].transform.position, Quaternion.identity, childClouds[i].transform);
            //Instantiate(trail, childClouds[i].transform.position, Quaternion.identity, childClouds[i].transform);
        }
        spawnPointsInUse++;
        canSelect = true;
    }
}