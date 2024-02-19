using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_GodBullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<PlayerManager>().Die();
        }
    }
}
