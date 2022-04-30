using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    [HideInInspector] public GameObject hand;
    [HideInInspector] public GameObject handTarget;
    [SerializeField] private float distance;
    [HideInInspector] public string lookingAt = "none";

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (GameManager.gm.currRoomType == "color")
            {
                if (lookingAt == "color")
                {
                    if (DistanceFu(handTarget) == 1)
                    {
                        Interactable_ColorButton icb = handTarget.GetComponent<Interactable_ColorButton>();
                        icb.OnPressed();
                    }
                }

            }
            if (GameManager.gm.currRoomType == "ring")
            {
                if (lookingAt == "ring_up" && DistanceFu(handTarget) == 1)
                {
                    FindObjectOfType<Room_Ring>().MoveUp(handTarget.GetComponent<Interactable_Pole>().whichPole);
                    FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.pSlideUp);
                }
                if (lookingAt == "ring_down" && DistanceFu(handTarget) == 1)
                {
                    FindObjectOfType<Room_Ring>().MoveDown(handTarget.GetComponent<Interactable_Pole>().whichPole);
                    FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.pSlideDown);
                }
            }
            if (GameManager.gm.currRoomType == "statue")
            {
                if (lookingAt == "statue")
                {
                    if (hand == null && DistanceFu(handTarget) == 1)
                    {
                        FindObjectOfType<Room_Statue>().PickUpFrom();
                    }
                }
                if (lookingAt == "socket" && DistanceFu(handTarget) == 1)
                {
                    if (hand != null)
                    {
                        FindObjectOfType<Room_Statue>().Place();
                        FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.pInsertPiece);
                    }
                }
            }

            if(lookingAt == "confirm")
            {
                FindObjectOfType<Room_Main>().OnConfirm();
            }
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (GameManager.gm.currRoomType == "statue")
            {
                if (hand != null)
                {
                    FindObjectOfType<Room_Statue>().Drop();
                }
            }
        }
    }

    public int DistanceFu(GameObject target)
    {
        if (Vector3.Distance(target.transform.position, transform.position) < distance) return 1;
        else return 0;
    }
}
