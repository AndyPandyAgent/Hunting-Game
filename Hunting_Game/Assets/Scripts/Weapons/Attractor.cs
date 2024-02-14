using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    public bool inRange;
    public float range;
    public GameObject[] allAnimals;
    private Transform closetstAnimal;

    private void Update()
    {
        CheckDist();
        GetAllEnemies();
        SetAttract();

    }

    private void Awake()
    {
    }

    private void SetAttract()
    {
        if(closetstAnimal != null)
            closetstAnimal.GetComponent<EnemyBehivour>().attractor = gameObject;
    }


    private void GetAllEnemies()
    {
 
        allAnimals = GameObject.FindGameObjectsWithTag("Animal");

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
        foreach(Transform t in animals)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if(dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }

    private void CheckDist()
    {
        if(closetstAnimal != null)
        {
            float dist = Vector3.Distance(closetstAnimal.position, transform.position);
            if(dist < range)
            {
                inRange = true;
            }
        }
    }
}
