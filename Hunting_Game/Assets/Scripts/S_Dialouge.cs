using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S_Dialouge : MonoBehaviour
{
    public List<TextMeshProUGUI> startTexts;
    public TextMeshProUGUI currentText;
    public GameObject[] buttons;

    public bool isUI;

    private void RandomText()
    {
        currentText = startTexts[Random.Range(0, startTexts.Count)];
    }

    private void EnableUI()
    {
        currentText.enabled = true;
        foreach (var button in buttons)
        {
            button.SetActive(true);
        }
    }

    public void SacrificeButton()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().Suicide();
    }

    public void PayRent()
    {
        S_WorldStateManager world = GetComponent<S_WorldStateManager>();
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().score -= world.cost;
    }

    public void StartDialouge()
    {
        RandomText();
        EnableUI();
    }

    private void Update()
    {
        if (isUI)
        {
            StartDialouge();
        }
    }
}
