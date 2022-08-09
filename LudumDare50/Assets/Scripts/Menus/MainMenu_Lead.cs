using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu_Lead : MonoBehaviour
{
    [SerializeField] private TextAsset forbiddenWords;
    [SerializeField] private InputField inf;
    [SerializeField] private Text youScore;
    string[] words;

    private void Start()
    {
        words = forbiddenWords.text.Split(",");
        youScore.text = PlayerPrefs.GetInt("roomsCleared").ToString();
        string[] splitted = PlayerPrefs.GetString("name").Split('#');
        if (PlayerPrefs.GetString("name") != null || PlayerPrefs.GetString("name") != "") inf.text = splitted[0];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (!inf.IsActive()) FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.uiSelect);
        }
    }

    public void Upload()
    {
        for (int i = 0; i < words.Length; i++)
        {
            if (inf.text.ToLower().Contains(words[i]) || PlayerPrefs.GetInt("roomsCleared") == 0) return;
        }
        StartCoroutine(up());
    }

    [System.Obsolete]
    IEnumerator up()
    {
        HighScores.RemoveScore(PlayerPrefs.GetString("name"));
        yield return new WaitForSeconds(.5f);
        PlayerPrefs.SetString("name", inf.text.ToLower() + PlayerPrefs.GetString("id").ToLower());
        HighScores.UploadScore(PlayerPrefs.GetString("name"), PlayerPrefs.GetInt("roomsCleared"));
    }

    public void changemenu(GameObject menu)
    {
        menu.SetActive(true);
        this.gameObject.SetActive(false);
        FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.uiClick);
    }
}