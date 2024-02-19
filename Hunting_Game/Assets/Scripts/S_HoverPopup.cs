using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_HoverPopup : MonoBehaviour
{
    public GameObject hoverPopup;

    private void Awake()
    {
        hoverPopup.SetActive(false);
    }

    public void PopUpEnter()
    {
        hoverPopup.SetActive(true);
    }

    public void PopUpExit()
    {
        hoverPopup.SetActive(false);
    }
}
