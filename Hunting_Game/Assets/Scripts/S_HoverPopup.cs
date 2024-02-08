using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_HoverPopup : MonoBehaviour
{
    public GameObject hoverPopup;
    private void OnMouseOver()
    {
        hoverPopup.SetActive(true);
    }
    private void OnMouseExit()
    {
        hoverPopup.SetActive(false);
    }
}
