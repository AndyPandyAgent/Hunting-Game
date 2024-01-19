using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_LureSpawner : MonoBehaviour
{
    public GameObject lure;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SpawnLure();
        }
    }

    private void SpawnLure()
    {
        Instantiate(lure, transform.position - new Vector3(0,0.2f,0), Quaternion.identity);
    }
}
