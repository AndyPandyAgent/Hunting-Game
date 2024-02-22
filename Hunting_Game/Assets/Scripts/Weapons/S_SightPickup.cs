using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_SightPickup : MonoBehaviour
{
    public GameObject ogRifle;
    public GameObject newRifle;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            ogRifle.SetActive(false);
            newRifle.SetActive(true);
            Destroy(gameObject);
        }
    }
}
