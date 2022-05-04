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
