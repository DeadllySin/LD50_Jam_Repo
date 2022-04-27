using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public static GameState gs;
    public bool playTheCutsceneOnlyOnce;

    public bool playIntroMusic = true;
    [HideInInspector] public FMOD.Studio.EventInstance menuMusicInstance;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        gs = this;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
        menuMusicInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Main_Menu");
        menuMusicInstance.start();
        FMODUnity.RuntimeManager.StudioSystem.setParameterByNameWithLabel("Game_State", "Main_Menu");

    }
    private void FixedUpdate()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }
}
