using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ParticleTracker : MonoBehaviour
{
    public GameObject animal;

    private void Update()
    {
        Track();
    }
    public void Track()
    {
        if (animal == null)
        {
            gameObject.GetComponent<ParticleSystem>().maxParticles = 0;
            return;
        }

        transform.LookAt(animal.transform.position);
    }
}
