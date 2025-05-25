using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class LeaderboardPositionTextSetter : MonoBehaviour
{
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text scoreText;
    public GameObject leaderboardGO;
    public LeaderboardScript leaderboardScript;
    public string playerName;
    public float playerTime;

    public void Start()
    {
        leaderboardGO = GameObject.FindWithTag("Leaderboard");
        leaderboardScript = leaderboardGO.GetComponent<LeaderboardScript>();
        playerName = leaderboardScript.leaderboardName;
        playerTime = leaderboardScript.playerFinishTime;
    }
    
    
    public void UpdateText(string Name, float Score)
    {
        //nameText.text = Name;
        Name = playerName;
        //scoreText.text = Score.ToString();
        Score = playerTime;
    }
}
