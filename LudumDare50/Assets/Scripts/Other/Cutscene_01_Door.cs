using UnityEngine;
using UnityEngine.EventSystems;

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
    [SerializeField] private GameManager gm;
    [SerializeField] private Light l;

    //FMOD
    FMOD.Studio.EventInstance doorOpenInstance;
    FMOD.Studio.EventInstance doorCloseInstance;

    private void Awake()
    {
        dooropen.GetComponent<Animator>().SetTrigger("isIdleOpen");
        gm = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        doorOpenInstance = FMODUnity.RuntimeManager.CreateInstance(AudioManager.am.doorOpen);
        doorCloseInstance = FMODUnity.RuntimeManager.CreateInstance(AudioManager.am.doorClose);
    }
    private void Update()
    {
        doorOpenInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(door));
        //doorOpenInstance.setVolume(AudioManager.am.sfxVolume);
        doorCloseInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(door));
    }

    public void Anim_OpenDoor()
    {
        door.GetComponent<Animator>().SetTrigger("isOpen");
        doorOpenInstance.start();
    }

    public void Anim_EnableDoor()
    {
        dooropen.GetComponent<Animator>().SetTrigger("isClosed");
        doorCloseInstance.start();
        l.gameObject.SetActive(false);
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

    public void Anim_FMODStepsSand()
    {
        FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.introSteps);
    }

    public void Anim_FMODStepsStone()
    {
        FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.footsteps);
    }

    public void Anim_FMODSSLookAtCeillingON()
    {
        AudioManager.am.ceilingSSInstance.start();
    }

    public void Anim_FMODSSLookAtCeillingOFF()
    {
        AudioManager.am.ceilingSSInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
    public void turnPlayerOn()
    {
        FindObjectOfType<EventSystem>().enabled = true;
        player.transform.position = this.transform.position;
        cineCutscene.SetActive(false);
        cinePlayer.SetActive(true);
        gm.player.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
    }
}