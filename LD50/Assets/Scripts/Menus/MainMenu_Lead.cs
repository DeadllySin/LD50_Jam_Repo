using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu_Lead : MonoBehaviour
{
    private string[] no = {"anal", "anus", "arse", "ass", "ballsack", "balls", "bastard", "bitch", "biatch", "bloody", "blowjob", "blow job", "bollock", "bollok", "boner", "boob", "bugger", "bum", "butt", "buttplug", "clitoris", "cock", "coon", "crap", "cunt", "damn", "dick", "dildo", "dyke", "fag", "feck", "fellate", "fellatio", "felching", "fuck", "f u c k", "fudgepacker", "fudge packer", "flange", "Goddamn", "God damn", "hell", "homo", "jerk", "jizz", "knobend", "knob end", "labia", "lmao", "lmfao", "muff", "nigger", "nigga", "omg", "penis", "piss", "poop", "prick", "pube", "pussy", "queer", "scrotum", "sex", "shit", "s hit", "sh1t", "slut", "smegma", "spunk", "tit", "tosser", "turd", "twat", "vagina", "wank", "whore", "wtf"};
    [SerializeField] private TMP_InputField inf;
    [SerializeField] private Text youScore;

    private void Start()
    {
        youScore.text = "your score:" + PlayerPrefs.GetString("roomsCleared");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.uiSelect);
        }
    }

    public void Upload()
    {
        for(int i = 0; i < no.Length; i++)
        {
            if (inf.text.ToLower().Contains(no[i]) || PlayerPrefs.GetInt("roomsCleared") == null || PlayerPrefs.GetInt("roomsCleared") == 0) return;
        }
        StartCoroutine(up());

    }

    IEnumerator up()
    {
        HighScores.RemoveScore(PlayerPrefs.GetString("name"));
        yield return new WaitForSeconds(.5f);
        PlayerPrefs.SetString("name", inf.text.ToLower() + PlayerPrefs.GetString("id").ToLower());
        HighScores.UploadScore(PlayerPrefs.GetString("name"), PlayerPrefs.GetInt("roomsCleared"));
        inf.text = null;
    }

    public void changemenu(GameObject menu)
    {
        menu.SetActive(true);
        this.gameObject.SetActive(false);
        FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.uiClick);
    }
}