using UnityEngine;
using UnityEngine.UI;

public class MainMenu_Main : MonoBehaviour
{
    [SerializeField] private GameObject cutscene;
    [SerializeField] private GameObject credits;

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
        //Is Between 0 and 100. Use this value to change the volume
        Debug.Log(sl.value);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
