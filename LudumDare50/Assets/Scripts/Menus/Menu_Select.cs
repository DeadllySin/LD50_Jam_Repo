using UnityEngine;
using UnityEngine.UI;

public class Menu_Select : MonoBehaviour
{
    [SerializeField] private Button button;

    private void OnEnable()
    {
        button.Select();
    }
}
