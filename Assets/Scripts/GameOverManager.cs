using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject finishScreen;
    public GameObject splashScreen;
    public GameObject timerGO;
    public GameObject playerGO;
    public Movement sunburnNumGet;
    public Movement finishCheck;
    public TimerManager timerNumGet;
    public GameObject levelGOS;
    public AudioScript audioScript;
    public GameObject startScreen;
    // Start is called before the first frame update
    void Start()
    {
        startScreen.SetActive(true);
        finishScreen.SetActive(false);
        splashScreen.SetActive(false);
        levelGOS.SetActive(false);
        sunburnNumGet = playerGO.GetComponent<Movement>();
        timerNumGet = timerGO.GetComponent<TimerManager>();
        finishCheck = playerGO.GetComponent<Movement>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var burned = sunburnNumGet.sunburnNum;
        var tod = timerNumGet.currentTime;
        if (burned >= 20 || tod <= 0)
        {
            splashScreen.SetActive(true);
            levelGOS.SetActive(false);
            audioScript.LoseGame();
        }

        if (finishCheck.isFinished)
        {
            finishScreen.SetActive(true);
            levelGOS.SetActive(false);
            finishCheck.isFinished = false;
            audioScript.GameWin();

        }
    }

    public void StartGameSplash()
    {
        audioScript.CountdownStart();
        startScreen.SetActive(false);
    }
    
    public void RetryButton()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
    
}
