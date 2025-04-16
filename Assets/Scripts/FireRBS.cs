using UnityEngine;

public class FireSpawnSystem : MonoBehaviour
{
    public GameObject firePrefab;

    public float spawnInterval = 15f;
    public int maxFires = 5;
    public float spawnRadius = 2f;

    public Transform player;
    public float spawnTriggerDistance = 15f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            GameObject[] fires = GameObject.FindGameObjectsWithTag("Fire");

            if (fires.Length > 0 && fires.Length < maxFires)
            {
                Transform baseFire = fires[0].transform;
                float distanceToPlayer = Vector3.Distance(baseFire.position, player.position);
                if (distanceToPlayer <= spawnTriggerDistance)
                {
                    Vector3 offset = Random.insideUnitSphere * spawnRadius;
                    offset.y = 0f;
                    Vector3 spawnPosition = baseFire.position + offset;
                    Instantiate(firePrefab, spawnPosition, baseFire.rotation);
                }
            }
        }
    }
}