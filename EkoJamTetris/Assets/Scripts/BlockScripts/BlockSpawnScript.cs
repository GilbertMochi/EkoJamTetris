using System.Collections;
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
    bool spawning = true;
    Coroutine spawnCO;
    // Set to true to activate tree powerup
    public bool treePowerup;
    public Transform[,] grid;

    // Start is called before the first frame update
    void Start()
    {
        treePowerup = false;
        spawnPointsInUse = 0;
        mainCam = Camera.main;
        spawnCO = StartCoroutine(KeepSpawning());
        //StartCoroutine(SpawnBlock());
        //StartCoroutine(SpawnBlock());
        //SpawnBlock();
    }

    void Update() 
    {
        if(!canSelect || selectableBlocks.Count <= 0 || playerIsPlaying != null)
        {
            return;
        }

        if(Input.GetButtonDown("Right") && canSelect)
        {
            NextSelection();
        }
        else if(Input.GetButtonDown("Left") && canSelect)
        {
            LastSelection();
        }
        else if(Input.GetButtonDown("Up") && canSelect)
        {
            SendBlock();
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            RocketPowerup(5);
        }
    }

    public void GetGrid(Transform[,] theGrid)
    {
        grid = theGrid;
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

    public void TreePowerup(GameObject tree)
    {
        treePowerup = true;
        // Call make tree appear
    }

    public void RocketPowerup(int startX)
    {
        if(startX > grid.GetLength(0)-3)
        {
            startX = grid.GetLength(0)-3;
        }
        for(int x = startX; x < startX + 3; x++)
        {
            for(int y = 0; y < grid.GetLength(1); y++)
            {
                if(grid[x,y] != null)
                {
                    Destroy(grid[x, y].gameObject);
                    grid[x, y] = null;
                }
            }
        }
    }

    public IEnumerator KeepSpawning()
    {
        float additionalTime;
        while(spawning)
        {
            if(treePowerup)
            {
                additionalTime = 5.0f;
            }
            else
            {
                additionalTime = 0.0f;
            }
            yield return new WaitForSeconds(Random.Range(3.0f, 5.0f) + additionalTime);
            StartCoroutine(SpawnBlock());
        }
    }

    public IEnumerator SpawnBlock()
    {
        List<GameObject> childClouds = new List<GameObject>();
        if(spawnPointsInUse >= spawnPoints.Length-1)
        {
            yield break;
        }

        int randomSP = 0;
        randomSP = Random.Range(0, spawnPoints.Length);

        // do
        // {
        //     randomSP = Random.Range(0, spawnPoints.Length);
        // } while(spawnPoints[randomSP].inUse);

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
        randomizedCol.a = 1.0f;
        ParticleSystem.MainModule main = newNewCloud.transform.GetChild(1).GetComponent<ParticleSystem>().main;
        main.startColor = randomizedCol;
        ParticleSystem.MainModule main2 = newNewCloud.transform.GetChild(2).GetComponent<ParticleSystem>().main;
        main2.startColor = randomizedCol;
        randomizedCol.a = 0.1f;
        foreach (Transform child in newBlock.transform)
        {
            childClouds.Add(child.gameObject);
            child.GetComponent<SpriteRenderer>().color = randomizedCol;
            newNewCloud.GetComponent<SetSpawnPoint>().SpawnLocations.Add(child.gameObject);
        }  
        //newNewCloud.transform.SetParent(newBlock.transform);
        newNewCloud.SetActive(true);
        
        yield return new WaitForSeconds(1f);
        randomizedCol.a = 1.0f;
        for(int i = 0; i < childClouds.Count; i++)
        {
            yield return new WaitForSeconds(0.5f);
            GameObject blockCloud = Instantiate(cloud, childClouds[i].transform.position, Quaternion.identity, childClouds[i].transform);
            ParticleSystem.MainModule main3 = blockCloud.GetComponent<ParticleSystem>().main;
            main3.startColor = randomizedCol;
            //Instantiate(trail, childClouds[i].transform.position, Quaternion.identity, childClouds[i].transform);
        }
        spawnPointsInUse++;
        canSelect = true;
    }
}