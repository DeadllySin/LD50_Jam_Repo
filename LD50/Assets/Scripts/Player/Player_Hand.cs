using UnityEngine;
public class Player_Hand : MonoBehaviour
{
    private GameManager gm;
    [HideInInspector] public GameObject hand;
    [HideInInspector] public GameObject handTarget;
    [SerializeField] private float distance;
    [HideInInspector] public string lookingAt = "none";

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (handTarget == null) return;
            if (DistanceFu(handTarget) != 1) return;
            switch (gm.currRoomType)
            {
                case "color":
                    {
                    if (lookingAt == "color")
                    {
                        handTarget.GetComponent<Interactable_ColorButton>().OnPressed();
                    }
                    if (lookingAt == "restart")
                    {
                         gm.currRoom.GetComponentInChildren<Room_Colors>().Restart(handTarget.GetComponent<Animator>());
                    }
                    break;
                    }
                case "ring":
                    if (lookingAt == "ring_up")
                    {
                        gm.currRoom.GetComponentInChildren<Room_Ring>().MoveUp(handTarget.GetComponent<Interactable_Pole>().whichPole);
                        
                    }
                    if (lookingAt == "ring_down")
                    {
                        gm.currRoom.GetComponentInChildren<Room_Ring>().MoveDown(handTarget.GetComponent<Interactable_Pole>().whichPole);
                    }
                    break;
                case "statue":
                    if (lookingAt == "statue" && hand == null)
                    {
                        gm.currRoom.GetComponentInChildren<Room_Statue>().PickUp();
                    }
                    if (lookingAt == "socket" && hand != null)
                    {
                        gm.currRoom.GetComponentInChildren<Room_Statue>().Place();
                    }
                    break;
            }

            if (lookingAt == "confirm")
            {
                FindObjectOfType<Room_Main>().OnConfirm();

            }
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (gm.currRoomType == "statue" && hand != null)
            {
                FindObjectOfType<Room_Statue>().Drop();
            }
        }
    }

    public int DistanceFu(GameObject target)
    {
        if (Vector3.Distance(target.transform.position, transform.position) < distance) return 1;
        else return 0;
    }
}