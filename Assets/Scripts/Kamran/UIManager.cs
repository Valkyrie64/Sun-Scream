using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; } //This is cool, basically this means the UI instance cant be set by anything else but itself but other scripts can get the values. TLDR: Read only file*/
    public Text pointsText;
    public Text objectivesText;
    public float timeRemaining = 180;
    [SerializeField]private int points = 0;
    private string objectives = "Pick up CUSTOMERS to make BUXS!";



    [Header("Leaderboard Attributes")]
    List<GameObject> LeaderboardNumberObjects;
    [SerializeField] GameObject leaderboardScorePrefab;
    [SerializeField] GameObject leaderboardGridLayout;
    [SerializeField] TMP_InputField nameInputField;



    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        
    }

    void UpdateUI()
    {
        // Update Points
        pointsText.text = "BUXS: " + points.ToString();

        // Update Objectives
        objectivesText.text = "Objectives: " + objectives;
        Debug.Log("Points: " + points);
    }

    public void AddPoints(int amount)
    {
        points += amount;
        UpdateUI();
    }

    public void SetObjectives(string newObjectives)
    {
        objectives = newObjectives;
        UpdateUI();
    }

    public void NameEntered()
    {
        AddToLeaderboard(points, nameInputField.text);
    }

    public void AddToLeaderboard(int score, string name)
    {
        int amountOfScores = PlayerPrefs.GetInt("LeaderboardAmount", 0);
        bool scoreBeaten = false;
        int scoreBeatenPoint = 0;
        List<int> scores = new();
        List<string> names = new();
        for (int i = 0; i < amountOfScores+1; i++)
        {
            if (i < 11)
            {
                if (!scoreBeaten)
                {
                    int heldScore = PlayerPrefs.GetInt("Pos" + i + "Score", 0);
                    if (heldScore < score)
                    {
                        scoreBeaten = true;
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
            if (PlayerPrefs.GetInt("Pos0Score") < score)
            {
                PlayerPrefs.SetInt("Pos" + (float)scoreBeatenPoint + "Score", score);
                PlayerPrefs.SetString("Pos" + (float)scoreBeatenPoint + "Name", name);
            }
        }
        else
        {
            PlayerPrefs.SetInt("Pos" + (float)scoreBeatenPoint + "Score", score);
            PlayerPrefs.SetString("Pos" + (float)scoreBeatenPoint + "Name", name);
        }
          
        int index = 0;
        foreach (var oldScore in scores)
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
