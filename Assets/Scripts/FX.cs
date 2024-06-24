using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FX : MonoBehaviour
{
    [SerializeField] private ParticleSystem cubeExplosionFX;

    ParticleSystem.MainModule cubeExplosionFXMainModule;
    ParticleSystemRenderer cubeExplosionFXRenderer;
    public static FX instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        
        cubeExplosionFXMainModule = cubeExplosionFX.main;
        cubeExplosionFXRenderer = cubeExplosionFX.GetComponent<ParticleSystemRenderer>();
    }

    public void PlayCubeExplosionFX(Vector3 position, Color color)
    {
        cubeExplosionFXMainModule.startColor = new ParticleSystem.MinMaxGradient(color);
        cubeExplosionFX.transform.position = position;
        cubeExplosionFX.Play();
    }

    public void PlayFx(Vector3 position, Color color)
    {
        
        cubeExplosionFX.transform.position = position;

        cubeExplosionFX.Play();
    }
}
