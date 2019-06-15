using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantTree : PowerUpButton
{
    // Start is called before the first frame update
    void Start()
    {
        Cost = 5000;
    }

    public override void DoAction()
    {
        base.DoAction();
      //  GetComponent<ScoreAndTimer>().
      // slow Down Timer by x;
    }
}
