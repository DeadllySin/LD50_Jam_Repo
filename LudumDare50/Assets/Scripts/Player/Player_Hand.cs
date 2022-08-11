using UnityEngine;
public class Player_Hand : MonoBehaviour
{
    private GameManager gm;
    [HideInInspector] public GameObject hand;
    [HideInInspector] public GameObject handTarget;
    [SerializeField] private float distance;
    [HideInInspector] public string lookingAt = "none";
    [SerializeField] private Camera cam;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, distance))
        {
            if (hit.transform.gameObject.GetComponent<Interactable>()) hit.transform.gameObject.GetComponent<Interactable>().OnMouseEnterFunc();
            else handTarget = null;
        }
        else
        {
            handTarget = null;
        }



        if (Input.GetKeyDown(KeyCode.E) || Hinput.anyGamepad.B.justPressed || Hinput.anyGamepad.A.justPressed || Hinput.anyGamepad.rightBumper.justPressed || Hinput.anyGamepad.rightTrigger.justPressed)
        {
            if (handTarget == null) return;
            switch (gm.currRoomType)
            {
                case "color":
                    {
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
                case "Cups":
                    if (lookingAt == "cup" && handTarget.GetComponent<Interactable_Cup>().canInteract && !handTarget.GetComponent<Interactable_Cup>().isUp)
                    {
                        handTarget.GetComponent<Interactable_Cup>().isUp = true;
                        handTarget.transform.position = new Vector3(handTarget.transform.position.x, handTarget.transform.position.y + .5f, handTarget.transform.position.z);
                        if (handTarget.transform.childCount == 1) FindObjectOfType<Room_Main>().state = "perfect";
                    }
                    break;
            }
            if (lookingAt == "Button")
            {
                handTarget.GetComponent<Interactable_Button>().OnPressed();
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
}