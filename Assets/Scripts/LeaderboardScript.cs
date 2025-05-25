using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardScript : MonoBehaviour
{
    //public static LeaderboardScript instance { get; private set; }
    public Movement playerScript;
    public float playerFinishTime;
    public string leaderboardName;
    public bool didTheyWin;
    
    [Header("Leaderboard Attributes")]
    List<GameObject> LeaderboardNumberObjects;
    [SerializeField] GameObject leaderboardScorePrefab;
    [SerializeField] GameObject leaderboardGridLayout;
    [SerializeField] TMP_InputField nameInputField;
    
    //void Awake()
    //{
        //if (instance == null)
        //{
            //instance = this;
            //DontDestroyOnLoad(gameObject);
        //}
        //else
        //{
            //Destroy(gameObject);
        //}
    //}
    // Start is called before the first frame update
    void Start()
    {
        leaderboardName = "Johnny";
    }

    // Update is called once per frame
    void Update()
    {
        playerFinishTime = playerScript.finishTime;
    }
    
    public void NameEntered()
    {
        didTheyWin = true;
        AddToLeaderboard(playerFinishTime, leaderboardName); //nameInputField.text);
    }
    
    public void AddToLeaderboard(float score, string name)
    {
        int amountOfScores = PlayerPrefs.GetInt("LeaderboardAmount", 0);
        int scoreBeatenPoint = 0;
        List<float> scores = new();
        List<string> names = new();
        for (int i = 0; i < amountOfScores+1; i++)
        {
            if (i < 11)
            {
                if (!didTheyWin)
                {
                    int heldScore = PlayerPrefs.GetInt("Pos" + i + "Score", 0);
                    if (heldScore < score)
                    {
                        didTheyWin = true;
                        scoreBeatenPoint = i;
                    }
                }
                else
                {
                    scores.Add(PlayerPrefs.GetInt("Pos" + (i-1) + "Score", 0));
                    names.Add(PlayerPrefs.GetString("Pos" + (i-1) + "Name", "AAA"));
                }
            }
            else
            {
                scoreBeatenPoint = 100;
            }
        }
        PlayerPrefs.SetInt("LeaderboardAmount", amountOfScores < 10 ? amountOfScores + 1 : amountOfScores);
        if (scoreBeatenPoint == 0)
        {
            if (PlayerPrefs.GetFloat("Pos0Score") < score)
            {
                PlayerPrefs.SetFloat("Pos" + (float)scoreBeatenPoint + "Score", score);
                PlayerPrefs.SetString("Pos" + (float)scoreBeatenPoint + "Name", name);
            }
        }
        else
        {
            PlayerPrefs.SetFloat("Pos" + (float)scoreBeatenPoint + "Score", score);
            PlayerPrefs.SetString("Pos" + (float)scoreBeatenPoint + "Name", name);
        }
          
        int index = 0;
        foreach (int oldScore in scores)
        {
            scoreBeatenPoint++;
            PlayerPrefs.SetInt("Pos" + (float)scoreBeatenPoint + "Score", oldScore);
            PlayerPrefs.SetString("Pos" + (float)scoreBeatenPoint + "Name", names[index]);
            index++;
        }
        LoadLeaderboard();
    }

    public void LoadLeaderboard()
    {
        LeaderboardPositionTextSetter[] children = leaderboardGridLayout.GetComponentsInChildren<LeaderboardPositionTextSetter>();
        foreach(var child in children)
        {
            Destroy(child.transform.gameObject);
        }
        int amountOfScores = PlayerPrefs.GetInt("LeaderboardAmount", 0);
        Debug.Log(amountOfScores);
        for (int i = 0; i < amountOfScores; i++)
        {
            var nextScore = Instantiate(leaderboardScorePrefab, leaderboardGridLayout.transform);
            nextScore.GetComponent<LeaderboardPositionTextSetter>().UpdateText(i+1 + ": "+PlayerPrefs.GetString("Pos"+(float)i+"Name","NAME"),PlayerPrefs.GetInt("Pos"+i+"Score",0));
        }
    }
}
