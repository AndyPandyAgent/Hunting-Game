using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Gun : MonoBehaviour
{
    public Transform fpsCam;
    public LayerMask heartLayer;
    public float damage;



    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.DrawRay(fpsCam.transform.position, fpsCam.transform.forward * 10f, Color.red, 1f);

            print("click");
            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, Mathf.Infinity))
            {

                print("hit object" + hit.transform.name);

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
