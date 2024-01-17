using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class NoFog : MonoBehaviour
{
    bool doWeHaveFogScene;

    private void Start()
    {
        doWeHaveFogScene = RenderSettings.fog;
    }

    private void OnPreRender()
    {
        RenderSettings.fog = false;
    }
    private void OnPostRender()
    {
        RenderSettings.fog = false;
    }
}
