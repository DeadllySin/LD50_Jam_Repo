using System.Collections;
using UnityEngine;

public class Cutscene_01_Door : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject butt;
    [SerializeField] private float doorClose;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject cutscene;
    [SerializeField] private GameObject cinePlayer;
    [SerializeField] private GameObject cineCutscene;

    private void Start()
    {
        StartCoroutine(play());
        StartCoroutine(doorEnu());
    }

    IEnumerator doorEnu()
    {
        yield return new WaitForSeconds(4.5f);
        butt.GetComponent<Animator>().SetTrigger("isPressed");
        yield return new WaitForSeconds(1f);
        door.GetComponent<Animator>().SetTrigger("isOpen");
        yield return new WaitForSeconds(doorClose);
        door.GetComponent<Animator>().SetTrigger("isClosed");
    }

    IEnumerator play()
    {
        yield return new WaitForSeconds(19);
        player.transform.position = this.transform.position;
        cineCutscene.SetActive(false);
        cinePlayer.SetActive(true);
        cutscene.SetActive(false);
        player.SetActive(true);
    }
}
