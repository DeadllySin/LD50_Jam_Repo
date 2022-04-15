using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject currDoor;
    public GameObject currRoom;
    public static GameManager gm;
    public GameObject player;
    public GameObject ceiling;
    public GameObject[] roomList;
    [HideInInspector]public int lastRoom = 0;
    public float ceilingSpeed;
    bool dieOnlyOnce;
    [SerializeField] private GameObject blackScreen;

    private void Awake()
    {
        gm = this;
    }

    public void OpenNextDoor()
    {
        GameManager.gm.currDoor.GetComponent<Animator>().SetTrigger("isOpen");
        FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.doorOpen, currDoor.transform.position);
    }

    private void Update()
    {
        if (ceiling.transform.position.y <= 4 && !dieOnlyOnce)
        {
            Debug.Log("You Are Dead");
            player.SetActive(false);
            ceilingSpeed = ceilingSpeed * 2;



            // fmod stop music instance or change paramter to menu/death screen
            // stop all instances except menu/death screen
            // reset parameters?

            if (ceiling.transform.position.y < 2)
            {
                FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.deathSFX);
                blackScreen.SetActive(true); 
                dieOnlyOnce = true;
            }
        }

        
        if (Input.GetKeyDown("t"))
        {
            GameManager.gm.currDoor.GetComponent<Animator>().SetTrigger("isOpen");
            FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.doorOpen, currDoor.transform.position);
        }
        if (Input.GetKeyDown("y"))
        {
            GameManager.gm.currDoor.GetComponent<Animator>().SetTrigger("isClosed");
            FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.doorClose, currDoor.transform.position);
        }

       ceiling.transform.position = Vector3.MoveTowards(ceiling.transform.position, new Vector3(ceiling.transform.position.x, ceiling.transform.position.y - 7, ceiling.transform.position.z), ceilingSpeed * Time.deltaTime);
    }
}