using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHighscores : MonoBehaviour 
{
    List  <Text> rScores = new List<Text>();
    HighScores myScores;
    [SerializeField] private GameObject textParent;

    private void Awake()
    {
        foreach(Transform child in textParent.transform)
        {
            rScores.Add(child.GetComponent<Text>());
        }
    }
    void Start() //Fetches the Data at the beginning
    {
        for (int i = 0; i < rScores.Count;i ++)
        {
            rScores[i].text = i + 1 + ". Fetching...";
        }
        myScores = GetComponent<HighScores>();
        StartCoroutine("RefreshHighscores");
    }
    public void SetScoresToMenu(PlayerScore[] highscoreList) //Assigns proper name and score for each text value
    {
        for (int i = 0; i < rScores.Count; i ++)
        {
            rScores[i].text = i + 1 + ". ";
            if (highscoreList.Length > i)
            {
                rScores[i].text = (i  + 1) + ". " + highscoreList[i].username + " " + highscoreList[i].score.ToString();
            }
        }
    }
    IEnumerator RefreshHighscores() //Refreshes the scores every 30 seconds
    {
        while(true)
        {
            myScores.DownloadScores();
            yield return new WaitForSeconds(30);
        }
    }
}
