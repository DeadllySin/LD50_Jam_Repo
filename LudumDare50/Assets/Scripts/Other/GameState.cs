using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public static GameState gs;
    public bool skipCutscene;
    public bool introFinished = false;
    public bool startMusic = true;
    public string overWriteRoom;

    private void Awake()
    {
        if(gs != null && gs != this) Destroy(this);
        gs = this;

        if (PlayerPrefs.GetString("lang") == null || PlayerPrefs.GetString("lang") == "") PlayerPrefs.SetString("lang", "en");
        DontDestroyOnLoad(this.gameObject);

        //StartCoroutine(LoadingScreenAsyncOperation());
        if(PlayerPrefs.GetString("id") == null || PlayerPrefs.GetString("id") == "") PlayerPrefs.SetString("id", "#" + Random.Range(1000, 9999).ToString().ToLower());
        if (PlayerPrefs.GetFloat("vol") == 0) PlayerPrefs.SetFloat("vol", 0.6f);
        AudioManager.am.masterBus.setVolume(PlayerPrefs.GetFloat("vol"));
    }

    IEnumerator LoadingScreenAsyncOperation()
    {
        yield return null;
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(1);
    }
    private void FixedUpdate()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}