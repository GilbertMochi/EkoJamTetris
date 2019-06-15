using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    ObjectPooler OP;
    DifficultyVaraibles DifVariables;
    float carSpawnCooldown;

    // Start is called before the first frame update
    void Start()
    {
        OP = ObjectPooler.Instance;
        DifVariables = GameObject.Find("DifficultyVariables").GetComponent<DifficultyVaraibles>();
        carSpawnCooldown = DifVariables.SimpleCarSpawnCooldown;

        StartCoroutine(StartSpawningSimpleCars());
    }

    // Update is called once per frame
    IEnumerator StartSpawningSimpleCars()
    {
        yield return new WaitForSeconds(2);//wait for 2 seconds
        OP.SpawnFromPool("SimpleCar", transform.position, transform.rotation);//spawn first car

        while (true)//loop infinitely
        {
            yield return new WaitForSeconds(carSpawnCooldown);//wait for the cooldown to be over
            //Debug.Log("simple car spawned");
            OP.SpawnFromPool("SimpleCar", transform.position, transform.rotation);//spawn next car
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
}
