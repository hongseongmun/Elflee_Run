using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    const float ORIGIN_SPEED = 1;

    public static float globalSpeed;
    public static float score;
    
    public static bool isPlay;
    public static bool isTitle;
    public static bool isGameOver;

    public static float hsmTimer;

    public UnityEvent PlayerPlay;
    public UnityEvent Scoller;
    public UnityEvent rankingEvent;

    float temporaryTimer;

    public GameObject pauseButton;
    public GameObject obtion;
    public GameObject player;

    public TextMeshProUGUI titleText;
    public GameObject titleImg;

    IEnumerator titleIE;

    private void Start()
    {
        obtion.SetActive(false);
        pauseButton.SetActive(true);
        isPlay = false;
        isGameOver = false;
        isTitle = true;
        hsmTimer = 0.001f;
        score = 0f;
        titleIE = BlinkTitleText();
        StartCoroutine(titleIE);
    }

    // Update is called once per frame
    void Update()
    {
        if (isTitle)
        {
            GameTitle();

            if (Input.anyKeyDown)
            {
                StopCoroutine(titleIE);
                player.SetActive(true);
                pauseButton.SetActive(true);
                titleImg.SetActive(false);
                titleText.text = " ";

                isTitle = false;
                isPlay = true;
            }
        }
        else if (isPlay)
        {
            GamePlay();
        }
        else if (isGameOver)
        {
            rankingEvent.Invoke();
        }

    }

    public void GameTitle()
    {
        player.SetActive(false);
        pauseButton.SetActive(false);
    }

public IEnumerator BlinkTitleText()
    {
        while (true)
        {
            titleText.text = " ";
            yield return new WaitForSeconds(1.0f);
            titleText.text = "Press the key to start the game";
            yield return new  WaitForSeconds(1.0f);
        }
    }
    public void GamePlay()
    {
        hsmTimer += hsmTimer * 0.00005f;
        score += hsmTimer * 10.0f;
        globalSpeed = ORIGIN_SPEED + hsmTimer * 0.1f;
        PlayerPlay.Invoke();
        Scoller.Invoke();
    }
    public void GameOver()
    {
        //uiOver.SetActive(true);
        isPlay = false;
        isGameOver = true;
        hsmTimer = 0.0f;

    }

    public void GamePause()
    {
        obtion.SetActive(true);
        pauseButton.SetActive(false);
        temporaryTimer = hsmTimer;
        hsmTimer = 0.0f;
        Time.timeScale = 0.0f;
    }

    public void GameResume()
    {
        obtion.SetActive(false);
        pauseButton.SetActive(true);
        hsmTimer = temporaryTimer;
        Time.timeScale = 1.0f;
    }

    public void GameReStart()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
        Time.timeScale = 1.0f;
    }
}
