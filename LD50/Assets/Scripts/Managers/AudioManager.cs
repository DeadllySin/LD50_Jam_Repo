using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager am;
    GameManager gm;

    [Header("Player")]
    public FMODUnity.EventReference footsteps; //movement
    public FMODUnity.EventReference deathSFX;

    [Header("Environment")]
    public FMODUnity.EventReference ceilingLoop;
    public FMODUnity.EventReference ceilingDebris;
    public FMODUnity.EventReference doorOpen;
    public FMODUnity.EventReference doorClose;

    [Header("Music")]
    public FMODUnity.EventReference inGameMusic;
    public FMODUnity.EventReference mainMenuMusic;
    public FMODUnity.EventReference deadMusic;
    public FMODUnity.EventReference introStinger;

    [Header("Puzzle Combine")]
    public FMODUnity.EventReference pInsertPiece;
    public FMODUnity.EventReference pRemovePiece;
    public FMODUnity.EventReference pPickUp;
    public FMODUnity.EventReference pDrop;

    [Header("Puzzle Math")]
    public FMODUnity.EventReference pSlideUp;
    public FMODUnity.EventReference pSlideDown;
    public FMODUnity.EventReference pConfirm;

    [Header("Puzzle Color")]
    public FMODUnity.EventReference pColorPress;
    public FMODUnity.EventReference pColorTimer;

    [Header("UI")]
    public FMODUnity.EventReference puzzleCorrect;
    public FMODUnity.EventReference puzzleWrong;
    public FMODUnity.EventReference uiClick;


    public void Awake()
    {
        if (am != null)
        {
            Destroy(this);
            Debug.Log("AudioManager already exists or not loaded");
        }
        am = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("t"))
        {
            //FMODUnity.RuntimeManager.PlayOneShot(testSound);
        }
    }
}
