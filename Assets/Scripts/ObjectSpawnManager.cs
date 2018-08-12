using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnManager : MonoBehaviour
{
    public Transform spawnObject1;
    public Transform spawnObject2;
    public bool mysteryboxSpawned = false;
    public bool heartSpawned = false;

    void Start()
    {
        StartInvoke();
    }

    void StartInvoke()
    {
        InvokeRepeating("MysteryboxSpawn", Random.Range(5.0f, 15.0f), Random.Range(10.0f, 15.0f));
        InvokeRepeating("HeartSpawn", Random.Range(5.0f, 15.0f), Random.Range(10.0f, 15.0f));
    }

    void MysteryboxSpawn()
    {
        if (!mysteryboxSpawned)
        {
            mysteryboxSpawned = true;
            var position = new Vector3(Random.Range(-2.87f, 2.64f), 0.5f, Random.Range(3.4f, -4.0f));
            Instantiate(spawnObject1, position, Quaternion.identity);
        }
    }

    void HeartSpawn()
    {
        if (!heartSpawned)
        {
            heartSpawned = true;
            var position = new Vector3(Random.Range(-2.87f, 2.64f), 0.5f, Random.Range(3.4f, -4.0f));
            Instantiate(spawnObject2, position, Quaternion.identity);
        }
    }
}
