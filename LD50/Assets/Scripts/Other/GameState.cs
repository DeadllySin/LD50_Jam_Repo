using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public static GameState gs;
    public bool skipCutscene;
    public bool introFinished = false;
    public string overWriteRoom;

    private void Awake()
    {
        if (PlayerPrefs.GetString("lang") == null || PlayerPrefs.GetString("lang") == "") PlayerPrefs.SetString("lang", "en");
        DontDestroyOnLoad(this.gameObject);
        gs = this;
        StartCoroutine(LoadingScreenAsyncOperation());
        if (PlayerPrefs.GetInt("first") == 0)
        {
            PlayerPrefs.SetString("id", "#" + Random.Range(1000,9999).ToString().ToLower());
            PlayerPrefs.SetInt("first", 1);
            PlayerPrefs.SetFloat("vol", 0.6f);
        }
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