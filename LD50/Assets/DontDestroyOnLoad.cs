using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoad : MonoBehaviour
{
    public static DontDestroyOnLoad ddol;
    public bool playTheCutsceneOnlyOnce;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

    }

    private void Start()
    {
        ddol = this;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
