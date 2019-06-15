using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyMaster : MonoBehaviour
{
    private DifficultyVaraibles difVariables;

    public float startSimpleCarCooldown = 6;
    public float startHouseCooldown = 20;
    public float startTruckCooldown = 14;
    public float startCowCooldown = 40;
    public float startBuilding1Cooldown = 50;
    public float startFactoryCooldown = 60;

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
        difVariables.TruckSpawnCooldown = startTruckCooldown;
        difVariables.CowSpawnCooldown = startCowCooldown;
        difVariables.Building1SpawnCooldown = startBuilding1Cooldown;
        difVariables.FactorySpawnCooldown = startFactoryCooldown;
    }
}
