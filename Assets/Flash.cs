using UnityEngine;

public class FiretruckFlasher : MonoBehaviour
{
    public Light redLight;
    public float flashRate = 2f; // how fast it flashes (Hz)
    public float delay = 0f; // optional offset to make alternating lights

    void Update()
    {
        if (redLight != null)
        {
            float time = Time.time + delay;
            redLight.enabled = Mathf.FloorToInt(time * flashRate) % 2 == 0;
        }
    }
}