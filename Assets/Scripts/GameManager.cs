using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public class SpawnedObjects
    {
        public GameObject Object;
        public float spawnRate;
    }

    
    public Transform spawnPoint;
    public float SpawnCD;
    public float minSpawnTime;
    public float DifficultyPerSec;
    public float MinOffset;
    public float MaxOffset;

    public SpawnedObjects[] Spawner;

    void Start()
    {
        StartCoroutine(SpawnObj());
    }

    void FixedUpdate()
    {
        if (SpawnCD >= minSpawnTime)
        {
            SpawnCD -= DifficultyPerSec * Time.deltaTime;
        }
    }



    public IEnumerator SpawnObj()
    {
        while(true)
        {
            yield return new WaitForSeconds(SpawnCD);
            Instantiate(Spawner[Random.Range(0, Spawner.Length)].Object, new Vector3(Random.Range(MinOffset, MaxOffset), spawnPoint.position.y, spawnPoint.position.z), Quaternion.identity);
        }
    }


    
}
