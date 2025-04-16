using UnityEngine;

public class OffsetAudio : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        float randomStartTime = Random.Range(0f, audioSource.clip.length);
        audioSource.time = randomStartTime;
        audioSource.Play();
    }
}
