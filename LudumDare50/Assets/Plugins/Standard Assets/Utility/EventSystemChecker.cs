using UnityEngine;
using UnityEngine.EventSystems;

public class EventSystemChecker : MonoBehaviour
{
    //public GameObject eventSystem;

    // Use this for initialization
    [System.Obsolete]
    void Awake()
    {
        if (!FindObjectOfType<EventSystem>())
        {
            //Instantiate(eventSystem);
            GameObject obj = new GameObject("EventSystem");
            obj.AddComponent<EventSystem>();
            obj.AddComponent<StandaloneInputModule>().forceModuleActive = true;
        }
    }
}