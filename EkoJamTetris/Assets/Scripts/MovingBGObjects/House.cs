using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour, IPooledObject
{
    Animator anim;
    ObjectPooler OP;
    public float lifeTime = 5f;
    float t;

    // Start is called before the first frame update
    void Start()
    {
        OP = ObjectPooler.Instance;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if (t > lifeTime)
        {
            MakeHouseDisappear();
        }
    }

    public void OnObjectSpawn()
    {
        if (anim == null)
        {
            anim = GetComponent<Animator>();
            anim.SetBool("Disappear", false);
            anim.SetBool("Appear", false);
        }
        else
        {
            anim.SetBool("Disappear", false);
            anim.SetBool("Appear", false);
        }
        MakeHouseAppear();
        t = 0f;
    }

    public void ReturnHouseToPool()
    {
        OP.PutSpawnedObjectBackIntoPool(gameObject, transform.tag);
    }

    void MakeHouseAppear()
    {
        anim.SetBool("Appear", true);
        anim.SetBool("Disappear", false);
    }

    void MakeHouseDisappear()
    {
        anim.SetBool("Disappear", true);
        anim.SetBool("Appear", false);
    }
}
