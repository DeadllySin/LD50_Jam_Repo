using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    bool dieOnlyOnce;
    public GameObject currDoor;
    public GameObject currRoom;
    public GameObject player;
    public GameObject ceiling;
    public GameObject[] roomList;
    [HideInInspector] public int lastRoom = 0;
    [SerializeField] private float ceilingSpeed;
    [SerializeField] private GameObject blackScreen;
    [SerializeField] private float deathHeight;
    [SerializeField] private float fastHeight;

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
        if (ceiling.transform.position.y <= fastHeight && !dieOnlyOnce)
        {
            Debug.Log("You Are Dead");
            player.SetActive(false);
            ceilingSpeed *= 2;
            if (ceiling.transform.position.y < deathHeight)
            {
                FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.deathSFX);
                blackScreen.SetActive(true); 
                dieOnlyOnce = true;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.T))
        {
            GameManager.gm.currDoor.GetComponent<Animator>().SetTrigger("isOpen");
            FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.doorOpen, currDoor.transform.position);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            GameManager.gm.currDoor.GetComponent<Animator>().SetTrigger("isClosed");
            FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.doorClose, currDoor.transform.position);
        }

       ceiling.transform.position = Vector3.MoveTowards(ceiling.transform.position, new Vector3(ceiling.transform.position.x, ceiling.transform.position.y - 7, ceiling.transform.position.z), ceilingSpeed * Time.deltaTime);
    }
}