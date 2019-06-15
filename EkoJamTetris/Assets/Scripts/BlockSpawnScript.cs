using UnityEngine;

public class BlockSpawnScript : MonoBehaviour
{
    public GameObject[] blocks;
    Camera mainCam;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        SpawnBlock();
    }

    Vector2 GetSpawnPoint()
    {
        Vector2 spawnPoint = mainCam.ViewportToWorldPoint(new Vector2(Random.Range(0.2f, 0.8f), 0.2f));
        spawnPoint.x = Mathf.RoundToInt(spawnPoint.x);
        spawnPoint.y = Mathf.RoundToInt(spawnPoint.y);
        return spawnPoint;
    }

    public void SpawnBlock()
    {
        int randomized = Random.Range(0, blocks.Length);
        GameObject obj = Instantiate(blocks[randomized], GetSpawnPoint(), Quaternion.identity);
    }
}