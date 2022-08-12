using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outside_Particles : MonoBehaviour
{

    [SerializeField] public ParticleSystem sandParticle;

    float menuDensity = 0.5f;
    float TRXDensity = 5f;
    float outsideDensity = 2f;

    //density - emission rate over time / Force Over Lifetime / 
    //shape interesting random direction randomization 
    // Particle Force Field idea: create dunes with the terrain, add empty objects to dynamically change sandstorm to 
    //be stronger simulating the wind. (External Forces module)
    //Noise module Scroll Speed, Strengh, frequency, size amount adds life
    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
