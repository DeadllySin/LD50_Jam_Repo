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
    bool temp;

    private void Awake() {
        temp = activateColorState;
    }

    public void Activate()
    {
        if (plate.material.color == newCol) return;
        FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.pMemFaceUp);
        GetComponentInParent<Room_Memory>().RevealPair(this);
    }

    private void FixedUpdate()
    {
        if(temp != activateColorState)
        {
            temp = activateColorState;
            StartCoroutine(ChangeColour());
        }
    }

    private IEnumerator ChangeColour()
    {
        if (activateColorState)
        {
            tick = 0;
            while (plate.material.color != newCol)
            {
                tick += Time.deltaTime * speed;
                plate.material.color = Color.Lerp(Color.gray, newCol, tick);
                yield return null;
            }
        }
        else
        {
            tick = 0;
            while (plate.material.color != Color.gray)
            {
                tick += Time.deltaTime * speed;
                plate.material.color = Color.Lerp(newCol, Color.gray, tick);
                yield return null;
            }
        }
    }

    [SerializeField] float speed = .35f;
}
