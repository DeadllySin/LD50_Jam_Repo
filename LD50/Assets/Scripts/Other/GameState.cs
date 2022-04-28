using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    public static GameState gs;
    public bool playTheCutsceneOnlyOnce;
    [SerializeField] private Image progressBar;
    [SerializeField] private int nextSceneIndex;
    public bool playIntroMusic = true;
    [HideInInspector] public FMOD.Studio.EventInstance menuMusicInstance;
    //[SerializeField] private Text loadText;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        gs = this;
        StartCoroutine(LoadingScreenAsyncOperation());
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //menuMusicInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Main_Menu");
        //menuMusicInstance.start();
        //FMODUnity.RuntimeManager.StudioSystem.setParameterByNameWithLabel("Game_State", "Main_Menu");
    }

    IEnumerator LoadingScreenAsyncOperation()
    {
        yield return null;
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(nextSceneIndex);
        /*
        while (!loadScene.isDone)
        {
            progressBar.fillAmount = loadScene.progress;
            //loadText.text = (loadScene.progress * 100).ToString("F0") + "%";

            if (loadScene.progress >= 0.9f)
            {
                progressBar.fillAmount = 1f;
                //loadText.text = "100%";
            }

            yield return null;
        }*/
    }
    private void FixedUpdate()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
