using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PickupSkill : MonoBehaviour
{
    public GameObject skillManager;
    public GameObject skill;


    private void OnTriggerEnter(Collider other)
    {
        skillManager.GetComponent<S_Skill>().TurnOnSkill(skill);
        Destroy(gameObject);
    }
}
