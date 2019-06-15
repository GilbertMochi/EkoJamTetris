using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    ObjectPooler OP;
    // Start is called before the first frame update
    void Start()
    {
        OP = ObjectPooler.Instance;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        OP.SpawnFromPool("SimpleCar", transform.position, transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Collided with: " + other.tag);
        if (other.CompareTag("SimpleCar"))
        {
            //make sure that the other.tag is the same as the tag of the pool in the dictionary we want to put the object back to.
            OP.PutSpawnedObjectBackIntoPool(other.gameObject, other.tag);
        }
    }
}
