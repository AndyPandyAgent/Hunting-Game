using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Gun : MonoBehaviour
{
    public Camera fpsCam;
    public LayerMask heartLayer;
    public float damage;



    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print("click");
            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, 999999999999999, heartLayer))
            {
                print("Shoot");
                S_Heart heart = hit.transform.GetComponent<S_Heart>();

                if (heart != null)
                {
                    print("hit");
                    heart.TakeDamage(damage);
                }
            }
        }

    }
}
