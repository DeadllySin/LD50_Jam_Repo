using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Translater : MonoBehaviour
{
    [SerializeField] private string tagName;

    private void OnEnable()
    {
        LanguageManager.onChange += change;
        StartCoroutine(chanheCn());
    }

    private void OnDestroy()
    {
        LanguageManager.onChange -= change;
    }

    IEnumerator chanheCn()
    {
        yield return new WaitForSeconds(.005f);
        change();
    }
    private void change()
    {
        Debug.Log("123");
        switch (LanguageManager.lg.lang)
        {
            case "en":
                Debug.Log("1235");
                for (int i = 0; i < LanguageManager.lg.english.Count;i++)
                {
                    string[] translation = LanguageManager.lg.english[i].Split('=');
                    if (translation[0] == tagName)
                    {
                        Debug.Log("en");
                        string trans = translation[1];
                        GetComponent<Text>().text = trans;
                    }
                }
                break;
            case "de":
                Debug.Log("1234");
                for (int i = 0; i < LanguageManager.lg.german.Count; i++)
                {
                    string[] translation = LanguageManager.lg.german[i].Split('=');
                    if (translation[0] == tagName)
                    {
                        Debug.Log("de");
                        string trans = translation[1];
                        GetComponent<Text>().text = trans;
                    }
                }
                break;
            case "pt":
                Debug.Log("1234");
                for (int i = 0; i < LanguageManager.lg.portu.Count; i++)
                {
                    string[] translation = LanguageManager.lg.portu[i].Split('=');
                    if (translation[0] == tagName)
                    {
                        Debug.Log("de");
                        string trans = translation[1];
                        GetComponent<Text>().text = trans;
                    }
                }
                break;
        }
    }
}
