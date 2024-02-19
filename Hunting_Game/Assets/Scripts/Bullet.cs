using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 100;

    /*private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Heart")
        {
            collision.gameObject.GetComponent<S_Heart>().TakeDamage(damage);
        }
    }*/
}
