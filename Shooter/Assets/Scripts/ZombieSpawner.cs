using System.Collections;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombie;
    public GameObject spawnEffect;
    public float spawnDelay = 2f;
    public int maxZombieCount = 1;
    private int currentZombieCount = 0;
    private float currentTime = 0;

    private void FixedUpdate()
    {
        currentTime += Time.deltaTime;

        if (currentTime > spawnDelay && currentZombieCount < maxZombieCount)
        {
            SpawnZombie();
        }
    }

    private void SpawnZombie()
    {
        currentTime = 0;

        StartCoroutine(CreateSpawnEffect());
        Instantiate(zombie, transform.position, Quaternion.identity);
        currentZombieCount++;
    }

    private IEnumerator CreateSpawnEffect()
    {
        var spawn = Instantiate(spawnEffect, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(1);

        Destroy(spawn);
    }
}
