using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class S_Skill : MonoBehaviour
{
    public GameObject skillTreeCanvas;

    [Header("Orb")]
    public GameObject orb;
    public GameObject orbUIYes;
    private bool orbBool;

    [Header("Particle")]
    public GameObject particle;
    public GameObject particleUIYes;
    private bool particleBool;

    [Header("Lure")]
    public GameObject lure;
    public GameObject lureUIYes;
    private bool lureBool;

    [Header("Currency")]
    public int currency;

    private void Awake()
    {
        skillTreeCanvas.SetActive(false);

        ResetSkills();
    }

    private void ResetSkills()
    {
        orbUIYes.SetActive(false);
        particleUIYes.SetActive(false);
        lureUIYes.SetActive(false);

        orb.SetActive(false);
        particle.SetActive(false);
        lure.SetActive(false);
    }

    private void Update()
    {
        if (orbUIYes.activeSelf)
        {
            orb.SetActive(true);
        }
        if (particleUIYes.activeSelf)
        {
            particle.SetActive(true);
        }
        if (lureUIYes.activeSelf)
        {
            lure.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            UISwitch();
        }


    }

    public void TurnOnSkill(GameObject skill)
    {
        skill.SetActive(true);
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
}
