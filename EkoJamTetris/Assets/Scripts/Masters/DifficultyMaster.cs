using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyMaster : MonoBehaviour
{
    private DifficultyVaraibles difVariables;

    public float startSimpleCarCooldown = 15;
    public float startHouseCooldown = 20;

    public static DifficultyMaster DifMaster { get; private set; }

    void Awake()
    {
        if (DifMaster == null)
        {
            DifMaster = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        difVariables = GameObject.Find("DifficultyVariables").GetComponent<DifficultyVaraibles>();

        difVariables.SimpleCarSpawnCooldown = startSimpleCarCooldown;
        difVariables.HouseSpawnCooldown = startHouseCooldown;
    }
}
