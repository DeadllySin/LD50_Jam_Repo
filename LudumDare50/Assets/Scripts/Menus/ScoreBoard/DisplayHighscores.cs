using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHighscores : MonoBehaviour
{
    HighScores myScores;
    [SerializeField] private Text leaderboard;
    [SerializeField] private Text lead2;
    [SerializeField] private Text scoreText;
    void Start() //Fetches the Data at the beginning
    {
        myScores = GetComponent<HighScores>();
        StartCoroutine(RefreshHighscores());
    }
    public void SetScoresToMenu(PlayerScore[] highscoreList) //Assigns proper name and score for each text value
    {
        leaderboard.text = null;
        lead2.text = null;
        for (int i = 0; i < 10; i++)
        {
            if (highscoreList.Length > i)
            {
                leaderboard.text += (i + 1) + ". " + highscoreList[i].username + "\n";
                lead2.text += highscoreList[i].score.ToString() + "\n";
            }
        }
    }
    IEnumerator RefreshHighscores() //Refreshes the scores every 30 seconds
    {
        while (true)
        {
            myScores.DownloadScores();
            yield return new WaitForSeconds(30);
        }
    }
}
