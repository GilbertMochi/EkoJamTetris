using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawn : MonoBehaviour
{
    public Sprite[] Trees;

    // Start is called before the first frame update
    void Start()
    {
        if (Trees.Length < 1)
            return;
        int x = Random.Range(0, Trees.Length);
        GetComponent<SpriteRenderer>().sprite = Trees[x];
    }

    public void KillTree()
    {
        GetComponent<Animator>().SetTrigger("Die");
    }
  
}
