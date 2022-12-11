using System;
using UnityEngine;
using UnityEngine.UI;

public class TranslationManager : MonoBehaviour
{
    public static TranslationManager instance;
    public static event Action onChange;
    public Language[] languages;

    private void Awake()
    {
        instance = this;
        for (int i = 0; i < languages.Length; i++)
        {
            string[] linesInFile = languages[i].languages.text.Split('\n');
            foreach (string line in linesInFile) if (!line.Contains("#") || line == "") languages[i].stuff.Add(line);
        }
    }

    public void SetUIText(Text text, string tag)
    {
        text.text = "";
        string[] translatedText = GetTextArray(tag);
        for (int i = 0; i < translatedText.Length; i++) text.text += translatedText[i] + "\n";
    }

    public void UpdateLanguage()
    {
        onChange?.Invoke();
        Debug.Log("Updated Language");
    }

    public string[] GetTextArray(string tag)
    {
        return GetText(tag).Split("/");
    }

    public string GetText(string tag)
    {
        for (int i = 0; i < TranslationManager.instance.languages[PlayerPrefs.GetInt("lang")].stuff.Count; i++)
        {
            string[] translation = TranslationManager.instance.languages[PlayerPrefs.GetInt("lang")].stuff[i].Split('=');
            translation[0].Replace(" ", "");
            if (translation[0] == tag) return translation[1];
        }
        Debug.LogError("Text with tag: " + tag + " not found");
        return "";
    }
}
