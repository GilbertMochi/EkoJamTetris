using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomSprite : MonoBehaviour
{
    [SerializeField]
    Sprite[] Clouds;

    [SerializeField]
    GameObject ObjectWithSprite;
    

    private void Awake()
    {
      if(Clouds.Length > 0)
      {
            int x = Random.Range(0, Clouds.Length);
            if(ObjectWithSprite)
            ObjectWithSprite.GetComponent<SpriteRenderer>().sprite = Clouds[x];
      }

    }
}
