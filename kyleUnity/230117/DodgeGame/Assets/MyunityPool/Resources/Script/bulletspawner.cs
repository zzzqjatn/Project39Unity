using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletspawner : MonoBehaviour
{
    public GameObject bulletPrefab = default;
    public float SpawnRateMin = 0.5f;
    public float SpawnRateMax = 3.0f;
    public Transform targetTransf = default;

    private float spawnRate = default;
    private float timeAfferSpawn = default;
    // Start is called before the first frame update
    void Start()
    {
        timeAfferSpawn = 0f;
        spawnRate = Random.Range(SpawnRateMin, SpawnRateMax);
    }

    // Update is called once per frame
    void Update()
    {
        timeAfferSpawn = timeAfferSpawn + Time.deltaTime;

        if (spawnRate <= timeAfferSpawn)
        {
            transform.LookAt(targetTransf);
            //Reset Point
            timeAfferSpawn = 0f;
            spawnRate = Random.RandomRange(SpawnRateMin, SpawnRateMax);

            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            bullet.transform.LookAt(targetTransf);
            //일정시간마다 플레이어 향해 총알 발사
        }
    }
}
