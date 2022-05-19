using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class LanguageManager : MonoBehaviour
{
    [HideInInspector] public List<string> english = new List<string>();
    [HideInInspector] public List<string> german = new List<string>();
    public string lang = "en";
    public static event Action onChange;
    public static LanguageManager lg;
    void Start()
    {
        lg = this;
        foreach (string line in System.IO.File.ReadLines(Application.streamingAssetsPath + "/English.txt"))
        {
            english.Add(line);
        }
        foreach (string line in System.IO.File.ReadLines(Application.streamingAssetsPath + "/German.txt"))
        {
            german.Add(line);
        }
    }

    public void ChangeLang(string lang)
    {
        this.lang = lang;
        onChange?.Invoke();
    }
}
