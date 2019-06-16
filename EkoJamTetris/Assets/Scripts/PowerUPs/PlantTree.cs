using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantTree : PowerUpButton
{
    public GameObject tree;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        Cost = 5000;
    }

    public override void DoAction()
    {
        base.DoAction();
      //  GetComponent<ScoreAndTimer>().
      // slow Down Timer by x;
        if(FindObjectOfType<ScoreAndTimer>().score >= Cost)
        {
            FindObjectOfType<BlockSpawnScript>().TreePowerup();
            Vector2 spawnPoint = Camera.main.ViewportToWorldPoint(new Vector2(Random.Range(0.1f, 0.9f), 0.1f));
            
            GameObject newTree = Instantiate(tree, spawnPoint, Quaternion.identity);
            newTree.GetComponent<Animator>().SetTrigger("Appear");
        }
    }
}
