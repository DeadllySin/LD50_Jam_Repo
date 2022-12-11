using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Translater : MonoBehaviour
{
    public string tagName;

    private void OnEnable()
    {
        TranslationManager.onChange += change;
        StartCoroutine(chanheCn());
    }

    private void OnDestroy()
    {
        TranslationManager.onChange -= change;
    }

    IEnumerator chanheCn()
    {
        yield return new WaitForSeconds(.005f);
        change();
    }

    private void change()
    {
        TranslationManager.instance.SetUIText(GetComponent<Text>(), tagName);
        Debug.Log("Updated Language Locally");
    }
}