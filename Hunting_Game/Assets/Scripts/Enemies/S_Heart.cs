using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class S_Heart : MonoBehaviour
{
    private GameObject ownerBehaviour;
    public GameObject absorb;

    private void Awake()
    {
        ownerBehaviour = transform.parent.GameObject();
    }

    public void TakeDamage()
    {
        ownerBehaviour.GetComponent<BoxCollider>().enabled = false;
        gameObject.transform.parent = null;

        if (ownerBehaviour.GetComponent<S_SlimeAnimator>() != null)
            ownerBehaviour.GetComponent<S_SlimeAnimator>().isDead = true;


        if(absorb != null)
            absorb.GetComponent<S_Absorb>().GetHeart(gameObject);
    }
}
