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
    private StarterAssets.FirstPersonController fps;

    public void StartCutscene()
    {
        fps = player.GetComponent<StarterAssets.FirstPersonController>();
        StartCoroutine(play());
        StartCoroutine(doorEnu());
    }

    IEnumerator doorEnu()
    {
        yield return new WaitForSeconds(7.5f);
        butt.GetComponent<Animator>().SetTrigger("isPressed");
        yield return new WaitForSeconds(3f);
        door.GetComponent<Animator>().SetTrigger("isOpen");
        yield return new WaitForSeconds(2f);
        door.SetActive(false);
        yield return new WaitForSeconds(doorClose);
        dooropen.SetActive(true);
    }

    IEnumerator play()
    {
        fps.MoveSpeed = 0;
        yield return new WaitForSeconds(24);
        player.transform.position = this.transform.position;
        cineCutscene.SetActive(false);
        cinePlayer.SetActive(true);
        fps.MoveSpeed = 4;

    }
}
