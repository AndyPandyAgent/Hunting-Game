using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class S_Heart : MonoBehaviour
{
    private GameObject ownerBehaviour;

    private void Awake()
    {
        ownerBehaviour = transform.parent.GameObject();
    }

    public void TakeDamage(float damage)
    {
        print("Heart");
        ownerBehaviour.GetComponent<EnemyBehivour>().TakeDamage(damage);
    }
}
