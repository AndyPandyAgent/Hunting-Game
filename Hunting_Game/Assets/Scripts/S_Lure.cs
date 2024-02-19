using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class S_Lure : MonoBehaviour
{
    public GameObject targetObject;
    public List<GameObject> gameObjectList;

    void Awake()
    {
        gameObjectList.AddRange(GameObject.FindGameObjectsWithTag("Animal"));
        FindClosestObject();
        
    }

    private void Update()
    {
        if(targetObject != null)
            targetObject.GetComponent<S_AnimalBehaviour>().attractor = gameObject;
    }

    void FindClosestObject()
    {
        if (gameObjectList.Count == 0)
        {
            return;
        }

        float closestDistance = Mathf.Infinity;
        GameObject closestObject = null;

        foreach (GameObject obj in gameObjectList)
        {
            float distance = Vector3.Distance(transform.position, obj.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = obj;
            }
        }
        targetObject = closestObject;
    }
}
