using UnityEngine;
using UnityEngine.UI;

public class Menu_Pause : MonoBehaviour
{

    [SerializeField] private Slider sl;
    [SerializeField] private Button bu;
    private void OnEnable()
    {
        sl.value = PlayerPrefs.GetFloat("vol");
        bu.Select();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            FindObjectOfType<GameManager>().Pause();
            Debug.Log("NO ITS THIS");
        }
    }
}
