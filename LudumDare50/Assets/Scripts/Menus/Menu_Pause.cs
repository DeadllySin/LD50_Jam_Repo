using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_Pause : MonoBehaviour
{

    [SerializeField] private Slider sl;
    private void OnEnable()
    {
        sl.value = PlayerPrefs.GetFloat("vol");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) FindObjectOfType<GameManager>().Pause();
    }
}
