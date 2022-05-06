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

    public void Anim_EnableDoor()
    {
        dooropen.SetActive(true);
    }

    public void Anim_PressButton()
    {
        butt.GetComponent<Animator>().SetTrigger("isPressed");
    }

    public void Anim_DeleteDoor()
    {
        door.SetActive(false);
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