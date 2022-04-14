using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject currDoor;
    public GameObject currRoom;
    public static GameManager gm;
    public GameObject ceiling;
    public GameObject[] roomList;
    [HideInInspector]public int lastRoom = 0;
    public float ceilingSpeed;

    private void Awake()
    {
        gm = this;
    }

    public void OpenNextDoor()
    {
        GameManager.gm.currDoor.GetComponent<Animator>().SetTrigger("isOpen");
    }

    private void Update() { ceiling.transform.position = Vector3.MoveTowards(ceiling.transform.position, new Vector3(ceiling.transform.position.x, ceiling.transform.position.y - 7, ceiling.transform.position.z), ceilingSpeed * Time.deltaTime); }

    private void FixedUpdate()
    {
        if (ceiling.transform.position.y <= 2)
        {
            Debug.Log("You Are Dead");
            ceilingSpeed = 0;
        }
    }
}