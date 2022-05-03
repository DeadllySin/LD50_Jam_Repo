using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject cutscene;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void StartGame()
    {
        cutscene.SetActive(true);
        this.gameObject.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        AudioManager.am.menuMusicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByNameWithLabel("Game_State", "In_Game");
        AudioManager.am.GetComponent<A_MusicCallBack>().musicInstance.start();
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("SkipIntro", 0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
