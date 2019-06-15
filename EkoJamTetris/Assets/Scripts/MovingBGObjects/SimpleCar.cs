using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]//has to have a rigidbody2d attached
public class SimpleCar : MonoBehaviour, IPooledObject
{
    Rigidbody2D rb;

    [SerializeField]
    float forceX = 6f;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();//get rigidbody from self
    }
    public void OnObjectSpawn()//do this everytime object is spawned from the pool
    {
        //Debug.Log(transform.name + " on object spawn called in simple car script.");
        rb.velocity = new Vector2(forceX, 0);
    }

}
