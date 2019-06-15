using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockFlashing : MonoBehaviour
{
    List<SpriteRenderer> childSprites = new List<SpriteRenderer>();
    float alpha;

    private void Start() 
    {
        foreach (Transform child in transform)
        {
            childSprites.Add(child.GetComponent<SpriteRenderer>());
        }    
    }
    // Update is called once per frame
    void Update()
    {
        alpha += Mathf.Sin(Time.time+1) * 0.5f*0.02f;
        Debug.Log(alpha);
        foreach (SpriteRenderer sr in childSprites)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
        }  
    }
}
