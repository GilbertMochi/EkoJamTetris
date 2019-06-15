using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour, IPooledObject
{
    Animator anim;
    public ParticleSystem dust;
    ObjectPooler OP;
    public float lifeTime = 80f;
    float t;

    // Start is called before the first frame update
    void Start()
    {
        OP = ObjectPooler.Instance;
        anim = GetComponent<Animator>();
        //dust = GetComponentInChildren<ParticleSystem>();
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
            anim.ResetTrigger("disappear");
            anim.ResetTrigger("appear");
        }
        else
        {
            anim.ResetTrigger("disappear");
            anim.ResetTrigger("appear");
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
        anim.SetTrigger("appear");
    }

    void MakeHouseDisappear()
    {
        anim.SetTrigger("disappear");
    }

    public void playDust()
    {
        dust.Play();
    }
    public void stopDust()
    {
        dust.Stop();
    }
}
