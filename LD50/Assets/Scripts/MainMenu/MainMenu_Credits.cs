using UnityEngine;

public class MainMenu_Credits : MonoBehaviour
{
    [SerializeField] private GameObject main;

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            main.SetActive(true);
            this.gameObject.SetActive(false);

        }
    }
}
