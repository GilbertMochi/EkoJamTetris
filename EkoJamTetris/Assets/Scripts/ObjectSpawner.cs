using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    ObjectPooler OP;
    DifficultyVaraibles DifVariables;
    float carSpawnCooldown;
    float houseCooldown;
    float truckCooldown;
    float cowCooldown;
    float factoryCooldown;
    float building1Cooldown;


    // public Transform[] houseSpawnPoints;
    public Transform[] carSpawnPoints = new Transform[2];//0 should be left, 1 should be right

    // Start is called before the first frame update
    void Start()
    {
        OP = ObjectPooler.Instance;
        DifVariables = GameObject.Find("DifficultyVariables").GetComponent<DifficultyVaraibles>();
        carSpawnCooldown = DifVariables.SimpleCarSpawnCooldown;
        houseCooldown = DifVariables.HouseSpawnCooldown;
        truckCooldown = DifVariables.TruckSpawnCooldown;
        cowCooldown = DifVariables.CowSpawnCooldown;
        building1Cooldown = DifVariables.Building1SpawnCooldown;
        factoryCooldown = DifVariables.FactorySpawnCooldown;

        StartCoroutine(StartSpawningSimpleCars());
        StartCoroutine(StartSpawningHouses());
        StartCoroutine(StartSpawningFactories());
        StartCoroutine(StartSpawningBulding1());
        StartCoroutine(StartSpawningTrucks());
        //StartCoroutine(StartSpawningCows());
    }

    // Update is called once per frame
    IEnumerator StartSpawningSimpleCars()
    {
        yield return new WaitForSeconds(2);//wait for 2 seconds
        OP.SpawnFromPool("SimpleCar", carSpawnPoints[Random.Range(0, carSpawnPoints.Length)].position, transform.rotation);//spawn first car

        while (true)//loop infinitely
        {
            yield return new WaitForSeconds(carSpawnCooldown);//wait for the cooldown to be over
            //Debug.Log("simple car spawned");
            OP.SpawnFromPool("SimpleCar", carSpawnPoints[Random.Range(0, carSpawnPoints.Length)].position, transform.rotation);//spawn next car
        }
    }

    IEnumerator StartSpawningHouses()
    {
        //houseSpawnPoints[Random.Range(0, houseSpawnPoints.Length)].position
        yield return new WaitForSeconds(10);//wait for 2 seconds
        OP.SpawnFromPool("House", GetNewHousePos(), transform.rotation);//spawn first house

        while (true)//loop infinitely
        {
            yield return new WaitForSeconds(houseCooldown);//wait for the cooldown to be over

            //spawn the hous randomly to one of the spawn points
            OP.SpawnFromPool("House", GetNewHousePos(), transform.rotation);
        }
    }

    IEnumerator StartSpawningBulding1()
    {
        //houseSpawnPoints[Random.Range(0, houseSpawnPoints.Length)].position
        yield return new WaitForSeconds(18);//wait for seconds
        OP.SpawnFromPool("Building1", GetNewHousePos(), transform.rotation);//spawn first house

        while (true)//loop infinitely
        {
            yield return new WaitForSeconds(building1Cooldown);//wait for the cooldown to be over

            //spawn the hous randomly to one of the spawn points
            OP.SpawnFromPool("Building1", GetNewHousePos(), transform.rotation);
        }
    }

    IEnumerator StartSpawningFactories()
    {
        //houseSpawnPoints[Random.Range(0, houseSpawnPoints.Length)].position
        yield return new WaitForSeconds(20);//wait for 2 seconds
        OP.SpawnFromPool("Factory", GetNewHousePos(), transform.rotation);//spawn first house

        while (true)//loop infinitely
        {
            yield return new WaitForSeconds(factoryCooldown);//wait for the cooldown to be over

            //spawn the hous randomly to one of the spawn points
            OP.SpawnFromPool("Factory", GetNewHousePos(), transform.rotation);
        }
    }

    IEnumerator StartSpawningTrucks()
    {
        yield return new WaitForSeconds(6);//wait for 2 seconds
        OP.SpawnFromPool("Truck", carSpawnPoints[Random.Range(0, carSpawnPoints.Length)].position, transform.rotation);//spawn first car

        while (true)//loop infinitely
        {
            yield return new WaitForSeconds(truckCooldown);//wait for the cooldown to be over
            //Debug.Log("simple car spawned");
            OP.SpawnFromPool("Truck", carSpawnPoints[Random.Range(0, carSpawnPoints.Length)].position, transform.rotation);//spawn next car
        }
    }

    IEnumerator StartSpawningCows()
    {
        yield return new WaitForSeconds(26);//wait for 2 seconds
        OP.SpawnFromPool("Cow", carSpawnPoints[Random.Range(0, carSpawnPoints.Length)].position, transform.rotation);//spawn first car

        while (true)//loop infinitely
        {
            yield return new WaitForSeconds(cowCooldown);//wait for the cooldown to be over
            //Debug.Log("simple car spawned");
            OP.SpawnFromPool("Cow", carSpawnPoints[Random.Range(0, carSpawnPoints.Length)].position, transform.rotation);//spawn next car
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Collided with: " + other.tag);
        if (other.CompareTag("SimpleCar"))
        {
            //Debug.Log("Putting " + other.tag + " back to pool.");
            //make sure that the other.tag is the same as the tag of the pool in the dictionary we want to put the object back to.
            OP.PutSpawnedObjectBackIntoPool(other.gameObject, other.tag);
        }
    }

    Vector2 GetNewHousePos()
    {
        Vector2 posXLeftEdge = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 posXRightEdge = Camera.main.ViewportToWorldPoint(new Vector2(1, 0));
        Vector2 pos = new Vector2(Random.Range(posXLeftEdge.x, posXRightEdge.x), 0);
        Debug.Log("House spawn pos: " + pos);
        return pos;
    }
}
