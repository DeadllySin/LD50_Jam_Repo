using UnityEngine;

public class Cutscene_01_Door : MonoBehaviour
{
    [SerializeField] public GameObject door;
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

    //FMOD
    FMOD.Studio.EventInstance doorOpenInstance;
    FMOD.Studio.EventInstance doorCloseInstance;

    [System.Obsolete]
    private void Start()
    {
        fps = player.GetComponent<StarterAssets.FirstPersonController>();
        fps.MoveSpeed = 0;
        dustStorm.playbackSpeed = spedVFXSpeed;
        doorOpenInstance = FMODUnity.RuntimeManager.CreateInstance(AudioManager.am.doorOpen);
        doorCloseInstance = FMODUnity.RuntimeManager.CreateInstance(AudioManager.am.doorClose);
    }

    [System.Obsolete]
    private void Update()
    {
        if (dustStorm.playbackSpeed >= normalVFXSpeed)
        {
            VFXFade += Time.deltaTime / 2.5f;
            dustStorm.playbackSpeed = Mathf.Lerp(spedVFXSpeed, normalVFXSpeed, VFXFade);
            //Anim_FadeParticles(); to finish fade out partciles
        }

        if (dooropen.activeSelf)
        {
            Anim_FadeParticles();
        }

        doorOpenInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(door));
        //doorOpenInstance.setVolume(AudioManager.am.sfxVolume);
        doorCloseInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(door));
    }

    [System.Obsolete]
    public void Anim_FadeParticles()
    {
        VFXFade -= Time.deltaTime / 2.5f;
        Color col = dustStorm.startColor;
        Debug.Log("initial color " + col.a);
        col.a = col.a * VFXFade;

        //dustStorm.playbackSpeed = Mathf.Lerp(spedVFXSpeed, normalVFXSpeed, VFXFade);

        Debug.Log("final" + col.a);
        dustStorm.startColor = col;
    }

    public void Anim_OpenDoor()
    {
        door.GetComponent<Animator>().SetTrigger("isOpen");
        doorOpenInstance.start();
    }

    public void Anim_EnableDoor()
    {
        dooropen.SetActive(true);
        doorCloseInstance.start();
        FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.puzzleWrong);
    }

    public void Anim_PressButton()
    {
        butt.GetComponent<Animator>().SetTrigger("isPressed");
        FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.pConfirm);
        FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.puzzleCorrect);
    }

    public void Anim_DeleteDoor()
    {
        door.SetActive(false);
    }

    public void Anim_ReleaseFMODInstances()
    {
        doorOpenInstance.release();
        doorCloseInstance.release();
    }

    public void turnPlayerOn()
    {
        player.transform.position = this.transform.position;
        cineCutscene.SetActive(false);
        cinePlayer.SetActive(true);
        fps.MoveSpeed = 4;
    }
}