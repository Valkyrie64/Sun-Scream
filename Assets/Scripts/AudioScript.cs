using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    public GameOverManager gameOverScript;
    public AudioSource musicPlayer;
    public AudioClip[] audioClips;
    public GameObject GOS;
    public bool countdownPlayed;
    public bool lostPlayed;
    public bool winPlayed;
    public bool musicStarted;
    // Start is called before the first frame update
    void Start()
    {
        gameOverScript = GetComponent<GameOverManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!musicPlayer.isPlaying && countdownPlayed)
        {
            GameStart();
            musicStarted = true;
        }
    }

    public void CountdownStart()
    {
        musicPlayer.clip = audioClips[1];
        musicPlayer.loop = false;
        countdownPlayed = true;
        musicPlayer.Play();
        
        
    }

    public void GameStart()
    {
        if (!musicStarted)
        {
            
            GOS.SetActive(true);
            musicPlayer.clip = audioClips[0];
            musicPlayer.loop = true;
            musicPlayer.Play();
        }
    }

    public void LoseGame()
    {
        if (!lostPlayed)
        {
            musicPlayer.clip = audioClips[2];
            musicPlayer.loop = false;
            musicPlayer.Play();
            lostPlayed = true;
        }
    }

    public void GameWin()
    {
        if (!winPlayed)
        {
            musicPlayer.clip = audioClips[3];
            musicPlayer.loop = false;
            musicPlayer.Play();
            winPlayed = true;
        }
    }
}
