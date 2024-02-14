using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public float hunger;
    public float score;
    public float hungerMultiplier;

    public Image bloodScreen;
    public TextMeshProUGUI youDied;
    [HideInInspector] public bool dead;

    private void Awake()
    {
        score = 0;
        hunger = 100;
        dead = false;
    }

    private void Update()
    {
        hunger -= Time.deltaTime * hungerMultiplier;

        if (dead)
            Die();

        if (bloodScreen.color.a >= 0.4f)
        {
            Invoke("ReloadScene", 2);
        }
    }

    public void Die()
    {
        var tempColor = bloodScreen.color;
        tempColor.a = 0.5f;
        bloodScreen.color = Color.Lerp(bloodScreen.color, tempColor, 1 * Time.deltaTime);

        var tempText = youDied.color;
        tempText.a = 1f;
        youDied.color = Color.Lerp(youDied.color, tempColor, 1 * Time.deltaTime);

    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
