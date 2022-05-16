using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonParticleController : MonoBehaviour
{
    [SerializeField] ParticleSystem playerParticle;

    ParticleSystem.MainModule main;
    ParticleSystem.EmissionModule emission;

    public void Start()
    {
        main = playerParticle.main;
        emission = playerParticle.emission;
    }

    public void StartParticle()
    {
        emission.rateOverTime = 6;
        main.startSpeed = 3f;
    }

    public void StopParticle()
    {
        emission.rateOverTime = 0;
        main.startSpeed = 0;
    }
}
