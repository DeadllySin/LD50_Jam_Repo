using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene_01_Door : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject butt;
    [SerializeField] private float doorClose;

    private void Start()
    {
        StartCoroutine(doorEnu());
    }

    IEnumerator doorEnu()
    {
        yield return new WaitForSeconds(2.3f);
        butt.GetComponent<Animator>().SetTrigger("isPressed");
        yield return new WaitForSeconds(.1f);
        door.GetComponent<Animator>().SetTrigger("isOpen");
        yield return new WaitForSeconds(doorClose);
        door.GetComponent<Animator>().SetTrigger("isClosed");
    }
}
