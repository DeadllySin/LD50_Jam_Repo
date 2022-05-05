using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button firstButton;
    [SerializeField] private GameObject cutscene;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(.4f);
        firstButton.Select();
    }

    public void StartGame()
    {
        cutscene.GetComponent<Cutscene_01_Door>().StartCutscene();
        cutscene.GetComponent<Animator>().SetTrigger("play");
        this.gameObject.SetActive(false);
        GameState.gs.introFinished = false;
        AudioManager.am.GetComponent<A_MusicCallBack>().FMODIntroDoOnce = true;
        AudioManager.am.FMOD_InGameState();
        AudioManager.am.FMOD_LoadInGameInstances();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
