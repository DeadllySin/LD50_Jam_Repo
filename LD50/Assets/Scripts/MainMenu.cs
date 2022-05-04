using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button but;
    [SerializeField] private GameObject cutscene;
    private void Start()
    {
        but.Select();
        StartCoroutine(sel());
    }

    IEnumerator sel()
    {
        yield return new WaitForSeconds(.4f);
        but.Select();
    }

    public void StartGame()
    {
        cutscene.GetComponent<Cutscene_01_Door>().StartCutscene();
        cutscene.GetComponent<Animator>().SetTrigger("play");
        AudioManager.am.menuMusicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByNameWithLabel("Game_State", "In_Game");
        AudioManager.am.GetComponent<A_MusicCallBack>().musicInstance.start();
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("SkipIntro", 0);
        this.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
