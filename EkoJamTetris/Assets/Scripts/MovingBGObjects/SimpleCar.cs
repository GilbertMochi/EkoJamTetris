using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]//has to have a rigidbody2d attached
public class SimpleCar : Sprite, IPooledObject
{
    Rigidbody2D rb;

    [SerializeField]
    float forceX = 6f;

    float distanceFromLeftSpawnPoint, distanceFromRightSpawnPoint;
    Transform leftSpawnPoint, rightSpawnPoint;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();//get rigidbody from self
    }

    public void OnObjectSpawn()//do this everytime object is spawned from the pool
    {
        //Debug.Log(transform.name + " on object spawn called in simple car script.");
        
        //get the car's right and left spawn points from the object spawner
        leftSpawnPoint =  GameObject.Find("carLeftSpawnPoint").transform;
        rightSpawnPoint = GameObject.Find("carRightSpawnPoint").transform;

        //calculate distances from each spawn point
        distanceFromLeftSpawnPoint = Vector3.Distance(transform.position, leftSpawnPoint.position);
        distanceFromRightSpawnPoint = Vector3.Distance(transform.position, rightSpawnPoint.position);

        //car was spawned to right, move left
        if (distanceFromLeftSpawnPoint > distanceFromRightSpawnPoint)
        {
            rb.velocity = new Vector2(-forceX, 0);
        }

        //car was spawned to left, move right
        if (distanceFromLeftSpawnPoint < distanceFromRightSpawnPoint)
        {
            rb.velocity = new Vector2(forceX, 0);
        }
    }
}
