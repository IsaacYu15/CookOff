using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DelyParticleSystemStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //start the particle system at the end of its lifetime (so the screen is pre-filled with particles)
        ParticleSystem particleSystem = GetComponent<ParticleSystem>();
        particleSystem.Simulate(particleSystem.duration);
        particleSystem.Play();
    }


}
