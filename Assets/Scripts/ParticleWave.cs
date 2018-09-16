using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleWave : MonoBehaviour 
{
    private ParticleSystem PS;
    private ParticleSystem.Particle[] particles;
    /*
     ParticleSystem system
     {
         get
         {
             if (_CachedSystem == null)
                 _CachedSystem = GetComponent<ParticleSystem>();
             return _CachedSystem;
         }
     }
     private ParticleSystem _CachedSystem;

     //private ParticleSystem PS;
     private ParticleSystem.Particle[] particles;
     //private ParticleClass[] particleAttr;
     public int particleNum = 10;
     private float speed = 0.1f, r = 0;

     private int frameid = 0;

     public class ParticleClass 
     {
         public float radius = 0.0f;
         public float angle = 0.0f;
         public ParticleClass(float _radius, float _angle) {
             radius = _radius;
             angle = _angle;
         }
     }

     private void Update()
     {
         ++frameid;
         if (frameid == 100)
         {
             r += speed;

             UpdateParticles(r);

             frameid = 0;
         }
     }

     private void Create()
     {
         particles = new ParticleSystem.Particle[particleNum];
     }

     private void UpdateParticles(float radius)
     {
         for (int i = 0; i < particleNum; i++)
         {
             float angle = 360f / particleNum * i;//2f * Mathf.PI / particleNum * i;
             //particles[i].position = Random.insideUnitSphere * universeSize;
             //particles[i].startSize = Random.Range(0.05f, 0.05f);
             //particles[i].startColor = new Color(1, 1, 1, 1);
             particles[i].position = new Vector3(radius * Mathf.Cos(angle), radius * Mathf.Sin(angle), 0.0f);
             particles[i].rotation = angle;
         }
         system.SetParticles(particles, particleNum);
         //system.Play();
     }

     // Use this for initialization
     void Start () 
     {

         //PS = new ParticleSystem();//this.GetComponent<ParticleSystem>();
         //particleNum = PS.main.maxParticles;
         //particles = new ParticleSystem.Particle[particleNum];
         //PS.main.maxParticles = particleNum;
         //PS.Emit(particleNum); PS.GetParticles(particles);
         for (int i = 0; i < particleNum; i++)
         {
             //radius = 5f;
             float angle = 360f / particleNum * i;//2f * Mathf.PI / particleNum * i;
             //particleAttr[i] = new ParticleClass(radius, angle);
             particles[i].position = new Vector3(radius * Mathf.Cos(angle), radius * Mathf.Sin(angle), 0.0f);
             particles[i].rotation = angle;
         }
         //PS.SetParticles(particles, particleNum);
         //PS.GetParticles(particles);
         //Debug.Log(particles[500].rotation);
         //Debug.Break();

         system.Play();
     }
     */

    private void Start()
    {

    }

    void OnParticleCollision(GameObject other)
    {
        Debug.Log(other.name);

        NeonController Neon = other.GetComponent<NeonController>();
        if (Neon != null)
            Neon.LightUp();
        else
            Debug.Log("not neon");
    }

    
}
