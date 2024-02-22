using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S_Dialouge : MonoBehaviour
{
    [Header("Text")]
    public List<string> startTexts;
    private string currentText;
    public string[] buttonText;
    public GameObject[] buttons;
    public GameObject dialougeCanvas;

    public S_WorldStateManager worldState;
    public TextMeshProUGUI textUI;
    public S_Skill skill;
    private int buttonInt = 0;
    private bool isUI;
    private PlayerManager playerManager;

    private void Awake()
    {
        isUI = false;
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
    }
    private void SetText()
    {
        currentText = startTexts[Random.Range(0, startTexts.Count)];
        textUI.text = currentText;

        foreach (GameObject button in buttons)
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = buttonText[buttonInt];
            buttonInt++;
        }
        buttons[1].GetComponentInChildren<TextMeshProUGUI>().text = buttons[1].GetComponentInChildren<TextMeshProUGUI>().text + ": " + worldState.cost;

    }

    private void EnableUI()
    {
        dialougeCanvas.active = !dialougeCanvas.active;
    }

    private void DisableUI()
    {
        isUI = false;
    }

    public void SacrificeButton()
    {
        print("ded");
        playerManager.dead = true;
        ExitDialouge();
        EnableUI();
        worldState.normalState = true;
    }

    public void PayRent()
    {
        if (playerManager.score >= worldState.cost)
        {
            playerManager.score -= worldState.cost;

            skill.currency++;
            worldState.cost++;
            buttonInt = 0;
            skill.StartSkill();
            EnableUI();
            if(worldState.cost >= 4)
            {
                worldState.normalState = false;
                worldState.startBoss = true;
            }
            else
            {
                worldState.normalState = true;
            }
        }
    }

    public void StartDialouge()
    {
        if (!isUI)
        {
            SetText();
            EnableUI();
        }
    }

    public void ExitDialouge()
    {
        Invoke("DisableUI", 0.1f);
        worldState.chaosState = false;
        worldState.timer = worldState.startTimer;
        worldState.startTimer = worldState.timer;

    }

    private void Update()
    {
        if (worldState.isInPos && worldState.chaosState)
        {
            StartDialouge();
            isUI = true;
        }
    }
}
