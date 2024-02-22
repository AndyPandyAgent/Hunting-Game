using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class S_Tracker : MonoBehaviour
{
    public GameObject pointer;
    public GameObject owner;
    public List<GameObject> allAnimals;
    private Transform closetstAnimal;

    private void Awake()
    {
        pointer = GameObject.FindGameObjectWithTag("Pointer");
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (GameObject.FindGameObjectWithTag("Pointer"))
            {
                pointer = GameObject.FindGameObjectWithTag("Pointer");
                GetAllEnemies();
                if (closetstAnimal != null)
                {
                    print(closetstAnimal.name);
                    pointer.GetComponent<S_Pointer>().GetTarget(closetstAnimal.gameObject);
                }
            }
        }
    }

    private void GetAllEnemies()
    {

        allAnimals.AddRange(GameObject.FindGameObjectsWithTag("Animal"));
        allAnimals.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));

        if (allAnimals == null)
            return;

        List<Transform> allAnimalsTransforms = new List<Transform>();



        foreach (GameObject animal in allAnimals)
        {
            allAnimalsTransforms.Add(animal.transform);
        }

        Transform[] allAnimalsTransformsArray = allAnimalsTransforms.ToArray();

        closetstAnimal = GetClosest(allAnimalsTransformsArray);
    }

    private Transform GetClosest(Transform[] animals)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform t in animals)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }

}
