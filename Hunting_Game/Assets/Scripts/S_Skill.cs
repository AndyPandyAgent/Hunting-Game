using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class S_Skill : MonoBehaviour
{
    public GameObject skillTreeCanvas;
    public bool isSkill;

    [Header("Orb")]
    public GameObject orb;
    public GameObject orbUIYes;


    [Header("Lure")]
    public GameObject lure;
    public GameObject lureUIYes;

    [Header("Absorb")]
    public GameObject absorb;
    public GameObject absorbUIYes;
    private bool hasGivenHearts;


    [Header("Currency")]
    public int currency;
    public int cost = 1;

    private void Awake()
    {
        skillTreeCanvas.SetActive(false);
        hasGivenHearts = false;

        ResetSkills();
    }

    private void ResetSkills()
    {
        orbUIYes.SetActive(false);
        lureUIYes.SetActive(false);

        orb.SetActive(false);
        lure.SetActive(false);
    }

    private void Update()
    {
        if (orbUIYes.activeSelf)
        {
            orb.SetActive(true);
        }

        if (lureUIYes.activeSelf)
        {
            lure.SetActive(true);
        }

        if (absorbUIYes.activeSelf)
        {
            absorb.SetActive(true);
            if (!hasGivenHearts)
            {
                GameObject[] hearts = GameObject.FindGameObjectsWithTag("Heart");
                foreach (GameObject heart in hearts)
                {
                    heart.GetComponent<S_Heart>().absorb = absorb;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            UISwitch();
            skillTreeCanvas.SetActive(!skillTreeCanvas.activeSelf);
            print("Switch");
        }
    }

    public void TurnOnSkill(GameObject skill)
    {
        if(currency >= cost)
        {
            skill.SetActive(true);
            currency -= cost;
            GameObject.FindGameObjectWithTag("Dialouge").GetComponent<S_Dialouge>().ExitDialouge();
            UISwitch();
            skillTreeCanvas.SetActive(!skillTreeCanvas.activeSelf);
        }
    }

    public void UISwitch()
    {
        if(Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else if(Cursor.lockState == CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        Cursor.visible = !Cursor.visible;
        
    }

    public void StartSkill()
    {
        skillTreeCanvas.SetActive(true);
    }
}
