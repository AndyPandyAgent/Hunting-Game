using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleCreator : MonoBehaviour
{
    [SerializeField] private Toggle toggleButton;
    [SerializeField] private Text toggleText;

    public void CreateToggle(string objective)
    {
        toggleText.text = objective;
    }
}
