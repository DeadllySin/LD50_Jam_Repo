using UnityEngine;
using UnityEngine.UI;

public class MainMenu_Main : MonoBehaviour
{
    [SerializeField] private GameObject cutscene;
    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject howToPlay;

    public void StartGame()
    {
        cutscene.GetComponent<Animator>().SetTrigger("play");
        this.gameObject.SetActive(false);
        GameState.gs.introFinished = false;
        AudioManager.am.GetComponent<A_MusicCallBack>().FMODIntroDoOnce = true;
        AudioManager.am.FMOD_InGameState();
        //AudioManager.am.FMOD_LoadInGameInstances();
    }

    public void Credits()
    {
        credits.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void OnValueChanged(Slider sl)
    {
        AudioManager.am.masterBus.setVolume(sl.value);
    }

    public void HowToPlay()
    {
        howToPlay.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
