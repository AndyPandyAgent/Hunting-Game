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
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            Instantiate(tracker, transform.parent = gameObject.transform);
            yield return new WaitForSeconds(spawnRate);
            currentTracker = gameObject.transform.GetChild(0).gameObject;
            currentTracker.transform.parent = null;
            yield return new WaitForSeconds(spawnRate);
        }

    }
}
