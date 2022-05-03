using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject cutscene;

    public void StartGame()
    {
        cutscene.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
