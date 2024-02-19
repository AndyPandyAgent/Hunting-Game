using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_MenuFunctions : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadScene(string scene)
    {
        print(scene);
        SceneManager.LoadScene(scene);
    }
}
