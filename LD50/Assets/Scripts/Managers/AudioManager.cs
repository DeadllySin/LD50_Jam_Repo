using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager am;

    [Header("Player")]
    public FMODUnity.EventReference testSound; //movement
    public FMODUnity.EventReference deathSFX;
    public FMODUnity.EventReference pDrop;
    public FMODUnity.EventReference pPickUp;

    [Header("Environment")]
    public FMODUnity.EventReference ceilingLoop;
    public FMODUnity.EventReference ceilingDebris;
    public FMODUnity.EventReference doorOpen;
    public FMODUnity.EventReference doorClose;

    [Header("Music")]
    public FMODUnity.EventReference mainMusic;

    [Header("Puzzle Combine")]
    public FMODUnity.EventReference pInsertPiece;

    [Header("Puzzle Math")]
    public FMODUnity.EventReference pSlideUp;
    public FMODUnity.EventReference pSlideDown;

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
            FMODUnity.RuntimeManager.PlayOneShot(testSound);
        }
    }
}
