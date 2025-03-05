using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Enemy : MonoBehaviour
{
    private float countdown = 5f;
    private WaveSystem waveSystem;

    private void Start()
    {
        waveSystem = GetComponentInParent<WaveSystem>();
    }

    void Update()
    {
        if(countdown <= 0)
        {
            waveSystem.waves[waveSystem.currentWaveIndex].enemiesLeft--;
        } 
    }
}
