//lets add some target
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Target : MonoBehaviour
{
    public Score scoreManager;
    public AudioClip extinguishedSFX;

    [Header("Particle systems for shrinking")]
    public ParticleSystem[] fireParticles;

    public float timeToExtinguish = 2f;
    public float timeToRecover = 2f;
    public float regenDelay = 5f;

    private float extinguishProgress = 0f;
    // 0 = fire active, 1 = extinguished

    private bool isBeingHitByWater = false;
    private float timeSinceLastWaterHit = 0f;

    private float[] originalSizes;
    private float[] originalRates;

    private void Start()
    {
        originalSizes = new float[fireParticles.Length];
        originalRates = new float[fireParticles.Length];

        for (int i = 0; i < fireParticles.Length; i++)
        {
            var mainModule = fireParticles[i].main;
            var emissionModule = fireParticles[i].emission;

            originalSizes[i] = mainModule.startSize.constant;
            originalRates[i] = emissionModule.rateOverTime.constant;
        }
    }

    private void Update()
    {
        if (isBeingHitByWater)
        {
            timeSinceLastWaterHit = 0f;
        }
        else
        {
            timeSinceLastWaterHit += Time.deltaTime;
        }

        if (isBeingHitByWater)
        {
            extinguishProgress += Time.deltaTime / timeToExtinguish;
        }
        else
        {
            if (timeSinceLastWaterHit >= regenDelay)
            {
                extinguishProgress -= Time.deltaTime / timeToRecover;
            }
        }

        extinguishProgress = Mathf.Clamp01(extinguishProgress);


        for (int i = 0; i < fireParticles.Length; i++)
        {
            var mainModule = fireParticles[i].main;
            var emissionModule = fireParticles[i].emission;

            float newSize = Mathf.Lerp(originalSizes[i], 0f, extinguishProgress);
            float newRate = Mathf.Lerp(originalRates[i], 0f, extinguishProgress);

            mainModule.startSize = newSize;
            emissionModule.rateOverTime = newRate;
        }

        if (extinguishProgress >= 1f)
        {
            if (extinguishedSFX != null)
                AudioSource.PlayClipAtPoint(extinguishedSFX, transform.position);
            scoreManager.AddFirePoint();
            Destroy(gameObject);
        }

        isBeingHitByWater = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Water"))
        {
            isBeingHitByWater = true;
        }
    }
}