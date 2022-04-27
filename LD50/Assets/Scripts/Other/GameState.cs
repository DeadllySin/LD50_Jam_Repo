using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public static GameState gs;
    public bool playTheCutsceneOnlyOnce;

    public bool playIntroMusic = true;
    [HideInInspector] public FMOD.Studio.EventInstance menuMusic;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        gs = this;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByNameWithLabel("Game_State", "Main_Menu");
        GameManager.gm.menuMusicInstance.start();
    }
    private void FixedUpdate()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }
}
