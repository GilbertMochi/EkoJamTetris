using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEcoMissile : PowerUpButton
{
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
            FindObjectOfType<BlockSpawnScript>().RocketPowerup();
        }
    }
}


