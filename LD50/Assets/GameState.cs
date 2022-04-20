using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public static GameState gs;
    public bool playTheCutsceneOnlyOnce;

    public bool playIntroMusic = true;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

    }

    private void Start()
    {
        gs = this;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    private void FixedUpdate()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
