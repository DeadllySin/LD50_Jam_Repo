using UnityEngine;
using UnityEngine.UI;

public class Translater : MonoBehaviour
{
    [SerializeField] private string tagName;

    private void OnEnable()
    {
        LanguageManager.onChange += change;
    }

    private void OnDestroy()
    {
        LanguageManager.onChange -= change;
    }
    private void change()
    {
        switch (LanguageManager.lg.lang)
        {
            case "en":
                for(int i = 0; i < LanguageManager.lg.english.Count;i++)
                {
                    if (LanguageManager.lg.english[i].Contains(tagName))
                    {
                        string[] translation = LanguageManager.lg.english[i].Split('=');
                        GetComponent<Text>().text = translation[1];
                    }
                }
                break;
            case "de":
                for (int i = 0; i < LanguageManager.lg.german.Count; i++)
                {
                    if (LanguageManager.lg.german[i].Contains(tagName))
                    {
                        string[] translation = LanguageManager.lg.german[i].Split('=');
                        GetComponent<Text>().text = translation[1];
                    }
                }
                break;
        }
    }
}
