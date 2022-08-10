using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
public class MainMenu_Main : MonoBehaviour
{
    [SerializeField] private GameObject cutscene;
    public CanvasGroup mainMenuUIGroup;
    [SerializeField] private Text versionText;
    [HideInInspector] public bool fadeIn = false;
    [HideInInspector] public bool fadeOut = false;
    [SerializeField] private Slider slider;
    [SerializeField] private TextAsset forbiddenWords;
    [SerializeField] private InputField inf;
    [SerializeField] private Text youScore;
    string[] words;
    [SerializeField] string URL;
    [HideInInspector] public string latestVersion;

    IEnumerator GetNewestVersion(string url)
    {
#pragma warning disable CS0618 // Type or member is obsolete
        WWW www = new WWW(url);
#pragma warning restore CS0618 // Type or member is obsolete
        yield return www;
        latestVersion = www.text;
        if (latestVersion != Application.version)
        {
            versionText.text += " - new version available!";
        }
    }

    private void Awake()
    {
        versionText.text = "Version " + Application.version;
        slider.value = PlayerPrefs.GetFloat("vol");
        mainMenuUIGroup.alpha = 0;
        fadeIn = true;
        StartCoroutine(GetNewestVersion(URL));
        words = forbiddenWords.text.Split(",");
        youScore.text = PlayerPrefs.GetInt("roomsCleared").ToString();
        string[] splitted = PlayerPrefs.GetString("name").Split('#');
        if (PlayerPrefs.GetString("name") != null || PlayerPrefs.GetString("name") != "") inf.text = splitted[0];
    }

    private void FixedUpdate()
    {
        if (fadeIn == true)
        {
            if (mainMenuUIGroup.alpha < 1)
            {
                mainMenuUIGroup.alpha += Time.deltaTime * 0.5f;
                if (mainMenuUIGroup.alpha >= 1)
                {
                    fadeIn = false;
                }
            }
        }
        if (fadeOut == true)
        {
            if (mainMenuUIGroup.alpha >= 0)
            {
                var seconds = A_Timer.a_timer.a_s * 0.1f;
                mainMenuUIGroup.alpha -= Time.deltaTime * seconds;
                //Debug.Log(seconds);
                if (AudioManager.am.startTimerCB == false)
                {
                    fadeOut = false;
                    this.gameObject.SetActive(false);
                    A_Timer.a_timer.a_s = 0;
                }
            }
        }
    }
    
    private void OnEnable()
    {
        mainMenuUIGroup.alpha = 1f;
    }

    public void StartGame()
    {
        FindObjectOfType<EventSystem>().enabled = false;
        GameState.gs.introFinished = false;
        AudioManager.am.startTimerCB = true;
        fadeOut = true;
        AudioManager.am.GetComponent<A_MusicCallBack>().FMODIntroDoOnce = true;
        AudioManager.am.FMOD_InGameState(); //nao mexer que esta ligado as variaveis call back
        FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.uiClick);
        this.enabled = false;
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
}
