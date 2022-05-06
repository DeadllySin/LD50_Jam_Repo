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
    [SerializeField] ParticleSystem dustStorm;

    float normalVFXSpeed = 1f;
    float spedVFXSpeed = 3f;
    float VFXFade = 0f;

    [System.Obsolete]
    public void StartCutscene()
    {
        fps = player.GetComponent<StarterAssets.FirstPersonController>();
        StartCoroutine(play());
        //StartCoroutine(doorEnu());  
    }

    [System.Obsolete]
    private void Start()
    {
        dustStorm.playbackSpeed = spedVFXSpeed;
    }

    [System.Obsolete]
    private void Update()
    {
        if (dustStorm.playbackSpeed >= normalVFXSpeed)
        {
            VFXFade += Time.deltaTime / 2.5f;
            dustStorm.playbackSpeed = Mathf.Lerp(spedVFXSpeed, normalVFXSpeed, VFXFade);
        }
    }

    public void Anim_OpenDoor()
    {
        door.GetComponent<Animator>().SetTrigger("isOpen");
    }

    public void Anim_CloseDoor()
    {
        door.GetComponent<Animator>().SetTrigger("isClosed");
    }

    public void Anim_PressButton()
    {
        butt.GetComponent<Animator>().SetTrigger("isPressed");
    }
    /*IEnumerator doorEnu()
    {
        yield return new WaitForSeconds(7.5f);
        butt.GetComponent<Animator>().SetTrigger("isPressed");
        //FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.pConfirm);
        //FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.puzzleCorrect);

        yield return new WaitForSeconds(2.9f);
        door.GetComponent<Animator>().SetTrigger("isOpen");
        //FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.doorOpen);

        yield return new WaitForSeconds(2f);
        door.SetActive(false);
        //FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.doorClose);
        //FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.puzzleWrong);

        yield return new WaitForSeconds(doorClose);
        dooropen.SetActive(true);
    }*/

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