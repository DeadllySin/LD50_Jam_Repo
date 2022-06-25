using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VersionCheck : MonoBehaviour
{
    public static VersionCheck vc;
    [SerializeField] string URL;
    [HideInInspector] public string latestVersion;
    [SerializeField] private Text latest;
    private void Awake() => vc = this;

    private void Start() => StartCoroutine(GetNewestVersion(URL));

    IEnumerator GetNewestVersion(string url)
    {
#pragma warning disable CS0618 // Type or member is obsolete
        WWW www = new WWW(url);
#pragma warning restore CS0618 // Type or member is obsolete
        yield return www;
        latestVersion = www.text;
        if(latestVersion != Application.version)
        {
            latest.text += " - new version available!";
        }
    }
}
