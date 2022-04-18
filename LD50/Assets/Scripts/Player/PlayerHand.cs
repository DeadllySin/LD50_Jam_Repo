using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    [HideInInspector]public GameObject hand;
    [HideInInspector] public GameObject handTarget;
    [HideInInspector] public GameObject handStatueTarget;
    [SerializeField] private float distance;
    public string lookingAt = "none";
    private Room_Ring ringman;

    private void Awake()
    {
        ringman = FindObjectOfType<Room_Ring>();
    }

    private void FixedUpdate()
    {
       Debug.Log(lookingAt);
    }

    private void Update()
    {
        if(GameManager.gm.currRoomType == "ring")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (lookingAt == "ring_up") ringman.MoveUp();
                else if (lookingAt == "ring_down") ringman.MoveDown();
            }
        }
        if(GameManager.gm.currRoomType == "statue")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (hand == null)
                {
                    if (Vector3.Distance(handTarget.transform.position, transform.position) < distance)
                    {
                        FindObjectOfType<Room_Statue>().PickUpFrom();
                    }
                }
                else if (hand != null)
                {
                    if (Vector3.Distance(handStatueTarget.transform.position, transform.position) < distance)
                    {
                        FindObjectOfType<Room_Statue>().Place(); Debug.Log("test1");
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.G)) FindObjectOfType<Room_Statue>().Drop();
        }
    }
}
