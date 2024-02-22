using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public WinData winData;
    public GameObject winText;
    public S_Skill skill;

    private bool countTimer;
    public float timer;
    public float score;
    public float kills;
    public float shotPrecent;
    

    [HideInInspector] public List<float> allDist;

    public float allShots;
    public float allHits;

    public Image bloodScreen;
    public TextMeshProUGUI youDied;
    [HideInInspector] public bool dead;


    public Canvas pauseCanvas;
    public bool isPasued;
    public GameObject skills;

    private void Awake()
    {
        countTimer = true;
        score = 0;
        timer = 0;
        dead = false;
    }

    private void Update()
    {
        if(countTimer)
            timer += Time.deltaTime;

        if (dead)
            Die();

        if (bloodScreen.color.a >= 0.4f)
        {
            winData.winCanvas.gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseCanvas.gameObject.active = !pauseCanvas.gameObject.active;
            skills.GetComponent<S_Skill>().UISwitch();

            if(Time.timeScale == 1)
            {
                Time.timeScale = 0;
            }
            else if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
        }

        
    }

    public void Die()
    {
        SetData();

        var tempColor = bloodScreen.color;
        tempColor.a = 0.5f;
        bloodScreen.color = Color.Lerp(bloodScreen.color, tempColor, 1 * Time.deltaTime);

        var tempText = youDied.color;
        tempText.a = 1f;
        youDied.color = Color.Lerp(youDied.color, tempColor, 1 * Time.deltaTime);

    }

    public void Win()
    {
        SetData();
        winData.winCanvas.gameObject.SetActive(true);
        winText.gameObject.SetActive(true);
        skill.UISwitch();
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void SetData()
    {
        countTimer = false;

        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);
        int milliseconds = Mathf.FloorToInt((timer * 1000) % 1000);

        winData.time.text = "Your time: " + string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
        winData.hearts.text = "Hearts collected: " + score.ToString();
        winData.slimes.text = "Kills: " + kills.ToString();
        winData.longestDist.text = "Longest kill: " + Mathf.Max(allDist.ToArray()).ToString() + "m";
        winData.shotPrecent.text = "Hit precentage: " + ((allHits / allShots) * 100).ToString() + "%";
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            dead = true;
        }

        if (other.gameObject.tag == "Heart")
        {

            score++;
            Destroy(other.gameObject);
            if (GameObject.FindGameObjectWithTag("Absorb") != null)
            {
                GameObject absorb = GameObject.FindGameObjectWithTag("Absorb");
                if (absorb.GetComponent<S_Absorb>().hearts.Contains(other.gameObject))
                {
                    absorb.GetComponent<S_Absorb>().hearts.Remove(other.gameObject);
                }
            }
        }
    }
}
