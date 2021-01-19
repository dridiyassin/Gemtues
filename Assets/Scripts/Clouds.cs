using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{
    public float SpawnRate;
    public GameObject[] clouds;
    void Start()
    {
        StartCoroutine(SpawnCloud());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnCloud()
    {
            while (true)
            {
                yield return new WaitForSeconds(SpawnRate);
                GameObject Cloud = Instantiate(clouds[Random.Range(0, clouds.Length)], new Vector3(-12, Random.Range(0, 6.5f), 0f), Quaternion.identity);
                Destroy(Cloud, 30f);

            }
        
    }
}
