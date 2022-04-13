using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject currDoor;
    public GameObject currRoom;
    public static GameManager gm;
    public GameObject ceiling;
    public GameObject[] roomList;
    public int lastRoom = 0;

    private void Awake()
    {
        gm = this;
    }

    public void OpenNextDoor()
    {
        GameManager.gm.currDoor.GetComponent<Animator>().SetTrigger("isOpen");
    }
    public float speed;
    private void Update() { ceiling.transform.position = Vector3.MoveTowards(ceiling.transform.position, new Vector3(ceiling.transform.position.x, ceiling.transform.position.y - 7, ceiling.transform.position.z), speed * Time.deltaTime); }

    private void FixedUpdate()
    {
        if (ceiling.transform.position.y <= 2)
        {
            Debug.Log("You Are Dead");
            speed = 0;
        }
    }
}