using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayParticle : MonoBehaviour
{
    public GameObject Particle_1;
    public GameObject Particle_2;
    
    void Start()
    {
        Particle_1.SetActive(true);
        Particle_1.GetComponent<ParticleSystem>().Play();
        Particle_2.SetActive(true);
        Particle_2.GetComponent<ParticleSystem>().Play();
    }
}
