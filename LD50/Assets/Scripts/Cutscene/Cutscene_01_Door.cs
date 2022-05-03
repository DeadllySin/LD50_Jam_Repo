using System.Collections;
using UnityEngine;

public class Cutscene_01_Door : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject butt;
    [SerializeField] private float doorClose;
    [SerializeField] private GameObject dooropen;
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
        yield return new WaitForSeconds(2f);
        door.SetActive(false);
        yield return new WaitForSeconds(doorClose);
        dooropen.SetActive(true);
    }

    IEnumerator play()
    {
        yield return new WaitForSeconds(22);
        player.transform.position = this.transform.position;
        cutscene.SetActive(false);
        cineCutscene.SetActive(false);

        yield return new WaitForSeconds(1);
        cinePlayer.SetActive(true);
        player.SetActive(true);


    }
}
