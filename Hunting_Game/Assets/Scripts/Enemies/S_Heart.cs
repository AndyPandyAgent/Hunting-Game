using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class S_Heart : MonoBehaviour
{
    private GameObject ownerBehaviour;
    private Rigidbody rb;

    private void Awake()
    {
        ownerBehaviour = transform.parent.GameObject();
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    public void TakeDamage()
    {
        print("Heart");
        ownerBehaviour.GetComponent<S_SlimeAnimator>().isDead = true;
        ownerBehaviour.GetComponent<BoxCollider>().enabled = false;
        gameObject.transform.parent = null;
        rb.useGravity = true;
    }
}
