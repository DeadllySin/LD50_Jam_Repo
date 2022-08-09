using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Memory : MonoBehaviour
{
    public MeshRenderer plate;
    Color lerpedColor = Color.gray;
    public Color newCol;
    public bool activateColorState;
    float tick;



    public void Activate()
    {
        if(plate.material.color == newCol) return;
        GetComponentInParent<Room_Memory>().RevealPair(this);
    }

    private void FixedUpdate()
    {
        if (activateColorState)
        {
            tick = 0;
            while (plate.material.color != newCol)
            {
                tick += Time.deltaTime * speed;
                plate.material.color = Color.Lerp(Color.gray, newCol, tick);
            }
        }
        else
        {
            tick = 0;
            while (plate.material.color != Color.gray)
            {
                tick += Time.deltaTime * speed;
                plate.material.color = Color.Lerp(newCol, Color.gray, tick);
            }
        }
    }

    float speed = 50;
}
