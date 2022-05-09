using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu_Base : MonoBehaviour
{
    [SerializeField] private Button firstButton;

    private void Start()
    {
        StartCoroutine(sel());
    }

    IEnumerator sel()
    {
        yield return new WaitForSeconds(.05f);
        firstButton.Select();
    }
}
