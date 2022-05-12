using UnityEngine;
public class Player_Hand : MonoBehaviour
{
    [HideInInspector] public GameObject hand;
    [HideInInspector] public GameObject handTarget;
    [SerializeField] private float distance;
    [HideInInspector] public string lookingAt = "none";
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (handTarget == null) return;
            if (DistanceFu(handTarget) != 1) return;
            switch (GameManager.gm.currRoomType)
            {
                case "color":
                    {
                    if (lookingAt == "color")
                    {
                        handTarget.GetComponent<Interactable_ColorButton>().OnPressed();
                    }
                    if (lookingAt == "restart")
                    {
                         GameManager.gm.currRoom.GetComponentInChildren<Room_Colors>().Restart(handTarget.GetComponent<Animator>());
                    }
                    break;
                    }
                case "ring":
                    if (lookingAt == "ring_up")
                    {
                        GameManager.gm.currRoom.GetComponentInChildren<Room_Ring>().MoveUp(handTarget.GetComponent<Interactable_Pole>().whichPole);
                        FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.pSlideUp);
                    }
                    if (lookingAt == "ring_down")
                    {
                        GameManager.gm.currRoom.GetComponentInChildren<Room_Ring>().MoveDown(handTarget.GetComponent<Interactable_Pole>().whichPole);
                        FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.pSlideDown);
                    }
                    break;
                case "statue":
                    if (lookingAt == "statue" && hand == null)
                    {
                        GameManager.gm.currRoom.GetComponentInChildren<Room_Statue>().PickUpFrom();
                    }
                    if (lookingAt == "socket" && hand != null)
                    {
                        GameManager.gm.currRoom.GetComponentInChildren<Room_Statue>().Place();
                    }
                    break;
            }

            if (lookingAt == "confirm")
            {
                FindObjectOfType<Room_Main>().OnConfirm(handTarget);
            }
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (GameManager.gm.currRoomType == "statue" && hand != null)
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