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
    //[SerializeField] private Text loadText;

    public FMOD.Studio.Bus masterBus;
    public FMOD.Studio.Bus gameplayBus;
    public FMOD.Studio.Bus UIBus;
    [HideInInspector] public FMOD.Studio.EventInstance menuMusicInstance;


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        gs = this;
        StartCoroutine(LoadingScreenAsyncOperation());
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        masterBus = FMODUnity.RuntimeManager.GetBus("bus:/");
        gameplayBus = FMODUnity.RuntimeManager.GetBus("bus:/Gameplay_Bus");
        //UIBus = FMODUnity.RuntimeManager.GetBus("bus:/UI_Bus");
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
