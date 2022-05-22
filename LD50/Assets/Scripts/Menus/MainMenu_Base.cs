using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu_Base : MonoBehaviour
{
    [SerializeField] private Button firstButton;
    [SerializeField] private float time = 0.05f;

    private void OnEnable()
    {
        StartCoroutine(sel());
    }

    IEnumerator sel()
    {
        yield return new WaitForSeconds(time);
        firstButton.Select();
    }

    public void changemenu(GameObject menu)
    {
        menu.SetActive(true);
        this.gameObject.SetActive(false);
        FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.uiClick);
    }
}
