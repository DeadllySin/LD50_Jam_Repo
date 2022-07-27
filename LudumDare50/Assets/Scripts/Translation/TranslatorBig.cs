using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TranslatorBig : MonoBehaviour
{
    [SerializeField][Multiline(15)] private string english;
    [SerializeField][Multiline(15)] private string german;
    [SerializeField][Multiline(15)] private string port;
    [SerializeField][Multiline(15)] private string french;
    [SerializeField] private Text text;

    private void OnEnable()
    {
        switch (PlayerPrefs.GetString("lang"))
        {
            case "en":
                text.text = english;
                break;
            case "de":
                text.text = german;
                break;
            case "pt":
                text.text = port;
                break;
            case "fr":
                text.text = port;
                break;
        }

    }
}
