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
        alpha = Mathf.Abs(Mathf.Sin(Time.time*3.0f)) * 0.3f;
        //Debug.Log(alpha);
        foreach (SpriteRenderer sr in childSprites)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
            if(sr.transform.childCount > 0)
            {
                ParticleSystem.MainModule main = sr.transform.GetChild(0).GetComponent<ParticleSystem>().main;
                main.startColor = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
            }
        }  
    }

    void OnDisable() 
    {
        foreach (SpriteRenderer sr in childSprites)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.3f);
            if(sr.transform.childCount > 0)
            {
                ParticleSystem.MainModule main = sr.transform.GetChild(0).GetComponent<ParticleSystem>().main;
                main.startColor = new Color(sr.color.r, sr.color.g, sr.color.b, 0.3f);
            }
        }  
    }
}
