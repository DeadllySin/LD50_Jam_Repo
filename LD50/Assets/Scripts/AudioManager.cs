using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager am;

    [Header("Player")]
    public FMODUnity.EventReference testSoundEvent;


    void Awake()
    {
        if (am != null)
        {
            Destroy(this);
            Debug.Log("AudioManager not loaded");
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
            FMODUnity.RuntimeManager.PlayOneShot(testSoundEvent);
        }

    }
}
