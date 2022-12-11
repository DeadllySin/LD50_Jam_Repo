using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Language
{
    [HideInInspector] public List<string> stuff = new List<string>();
    public TextAsset languages;
}