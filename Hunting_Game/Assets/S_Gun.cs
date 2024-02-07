using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Gun : MonoBehaviour
{
    public Transform fpsCam;
    public Transform endPos;
    public LayerMask heartLayer;
    public float damage;
    public GameObject shotObject;



    public void Update()
    {
        /*if (Input.GetMouseButtonDown(0))
        {

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000))
            {
                print("hit");
                print(hit.transform.gameObject.name);

                S_Heart heart = hit.transform.GetComponent<S_Heart>();
                heart.TakeDamage(damage);
            }
        }*/

    }

}
