using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleWave : MonoBehaviour 
{
    /*
    private ParticleSystem PS;
    private ParticleSystem.Particle[] particles;
    private ParticleClass[] particleAttr;
    public int particleNum = 1000;
    public float radius = 0, speed = 0.00001f;

    public class ParticleClass 
    {
        public float radius = 0.0f;
        public float angle = 0.0f;
        public ParticleClass(float _radius, float _angle) {
            radius = _radius;
            angle = _angle;
        }
    }
    */

	// Use this for initialization
	void Start () 
    {
        /*
        PS = this.GetComponent<ParticleSystem>();
        particleNum = PS.main.maxParticles;
        particles = new ParticleSystem.Particle[particleNum];
        //PS.main.maxParticles = particleNum;
        //PS.Emit(particleNum);
        PS.GetParticles(particles);
        for (int i = 0; i < particleNum; i++)
        {//相应初始化操作，为每个粒子设置半径，角度
            //radius = 5f;
            float angle = 360f / particleNum * i;//2f * Mathf.PI / particleNum * i;
            //particleAttr[i] = new ParticleClass(radius, angle);
            particles[i].position = new Vector3(radius * Mathf.Cos(angle), radius * Mathf.Sin(angle), 0.0f);
            particles[i].rotation = angle;
        }
        //设置粒子
        PS.SetParticles(particles, particleNum);
        PS.GetParticles(particles);
        Debug.Log(particles[500].rotation);
        //Debug.Break();

        PS.Play();
        */
	}
	
	// Update is called once per frame
    void Update () 
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
