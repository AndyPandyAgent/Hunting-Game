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
    private bool lureBool;

    [Header("Currency")]
    public int currency;
    public int cost = 1;

    private void Awake()
    {
        skillTreeCanvas.SetActive(false);

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

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            UISwitch();
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
        skillTreeCanvas.SetActive(!skillTreeCanvas.activeSelf);
    }

    public void StartSkill()
    {
        skillTreeCanvas.SetActive(true);
    }
}
