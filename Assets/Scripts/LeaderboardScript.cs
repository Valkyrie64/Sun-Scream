using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class LeaderboardScript : MonoBehaviour
{
    public GridLayout gridLayout;
    public List<GameObject> leaderboardCells;
    // Start is called before the first frame update
    void Start()
    {
        string checkSavedTime = PlayerPrefs.GetFloat("BestTime").ToString();
        for (int i = leaderboardCells.Count - 1; i >= 0; i--)
        {
            var playerTime = leaderboardCells[i].GetComponent<TMP_Text>();
            playerTime.text = checkSavedTime;
            var timeCheck = playerTime.text;
            float time = float.Parse(timeCheck);
            var checkAboveTime = leaderboardCells[i - 1].GetComponent<TMP_Text>();
            var aboveTime = checkAboveTime.text;
            float above = float.Parse(aboveTime);
            if (time < above)
            {
                var tempAbove = aboveTime;
                playerTime.text = tempAbove;
                checkAboveTime.text = checkSavedTime;
                Debug.Log("PlayerTime is better than above");
            }

            if (time > above)
            {
                playerTime.color = Color.green;
                i = 0;
                Debug.Log("PlayerTime is worse than above");
            }
        }
    }

    public void SetLeaderboard()
    {
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
