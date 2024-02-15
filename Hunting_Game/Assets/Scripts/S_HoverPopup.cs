using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_HoverPopup : MonoBehaviour
{
    public GameObject hoverPopup;

    public void PopUpEnter()
    {
        hoverPopup.SetActive(true);
    }

    public void PopUpExit()
    {
        hoverPopup.SetActive(false);
    }
}
