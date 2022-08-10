using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_Select : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Slider slider;

    private void OnEnable() {
        if(button != null) button.Select();
        else slider.Select();
    }
}
