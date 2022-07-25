using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class LanguageManager : MonoBehaviour
{
    [HideInInspector] public List<string> english = new List<string>();
    [HideInInspector] public List<string> german = new List<string>();
    [HideInInspector] public List<string> portu = new List<string>();
    [HideInInspector] public List<string> french = new List<string>();
    [SerializeField] private TextAsset englishText;
    [SerializeField] private TextAsset germanText;
    [SerializeField] private TextAsset portuText;
    [SerializeField] private TextAsset frenchText;
    [HideInInspector] public string lang = "en";
    public static event Action onChange;
    public static LanguageManager lg;
    void Awake()
    {
        lang = PlayerPrefs.GetString("lang");
        lg = this;
        string[] linesInFile = englishText.text.Split('\n');
        foreach (string line in linesInFile)
        {
            if(!line.Contains("#")) english.Add(line);
        }
        string[] linesInFile2 = germanText.text.Split('\n');
        foreach (string line in linesInFile2)
        {
            if (!line.Contains("#")) german.Add(line);
        }
        string[] linesInFile3 = portuText.text.Split('\n');
        foreach (string line in linesInFile3)
        {
            if (!line.Contains("#")) portu.Add(line);
        }
        string[] linesInFile4 = frenchText.text.Split('\n');
        foreach (string line in linesInFile4)
        {
            if (!line.Contains("#")) french.Add(line);
        }
    }

    public void ChangeLang(string lang)
    {
        PlayerPrefs.SetString("lang", lang);
        this.lang = lang;
        onChange?.Invoke();
    }
}
