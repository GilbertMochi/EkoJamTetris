using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToLocation : MonoBehaviour
{
    public ParticleSystem p;
    public ParticleSystem.Particle[] particles;
    public GameObject Targetobject;
    public float affectDistance;
    float sqrDist;
    Transform thisTransform;
    Transform Target;


    void Start()
    {
        p = GetComponent<ParticleSystem>();
        sqrDist = affectDistance * affectDistance;
    }


    void Update()
    {
        Target = Targetobject.transform;
        particles = new ParticleSystem.Particle[p.particleCount];

        p.GetParticles(particles);

        for (int i = 0; i < particles.GetUpperBound(0); i++)
        {
           // Vector3 pos = new Vector3(particles[i].position.x, particles[i].position.y, particles[i].position.z);
           // Vector3 pos2 = new Vector3(Target.position.x, Target.position.y, Target.position.z);

            //if (Vector3.Distance(Target.position, particles[i].position)<2f )
            //    return;

            float ForceToAdd = (particles[i].startLifetime - particles[i].remainingLifetime) * (0.5f * Vector3.Distance(Target.position, particles[i].position));

            Debug.DrawRay(particles[i].position, (Target.position - particles[i].position).normalized * (ForceToAdd / 10));
            Vector3 direction = new Vector2(Target.position.x - particles[i].position.x, Target.position.y - particles[i].position.y);
            particles[i].velocity = direction.normalized * ForceToAdd;
            //(Target.position - particles[i].position)
             // particles[i].position = Vector3.Lerp (particles [i].position, Target.position, Time.deltaTime / 1f);

        }

        p.SetParticles(particles, particles.Length);

    }
}
