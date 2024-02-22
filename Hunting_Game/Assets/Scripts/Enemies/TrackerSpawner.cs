using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackerSpawner : MonoBehaviour
{
    public GameObject tracker;
    private GameObject currentTracker;
    public float spawnRate;

    private void Awake()
    {
        Invoke("Spawner", spawnRate);
    }

    private void Spawner()
    {
        var newTracker = Instantiate(tracker, transform.position + new Vector3(0,0, -2), transform.rotation);

        Invoke("Spawner", spawnRate);
    }
}
