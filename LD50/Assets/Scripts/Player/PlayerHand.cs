using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    [HideInInspector] public GameObject hand;
    [HideInInspector] public GameObject handTarget;
    [SerializeField] private float distance;
    public string lookingAt = "none";
    public GameObject roomRing;

    private void FixedUpdate()
    {
        Debug.Log(lookingAt);
    }

    private void Update()
    {
        if (GameManager.gm.currRoomType == "color")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (lookingAt == "color")
                {   
                    if(distanceFu(handTarget) == 1)
                    {
                        Interactable_ColorButton icb = handTarget.GetComponent<Interactable_ColorButton>();
                        icb.OnPressed();
                    }
                }
                if (lookingAt == "confirm")
                {
                    if (distanceFu(handTarget) == 1)
                    {
                        Interactable_ColorButton icb = handTarget.GetComponent<Interactable_ColorButton>();
                        icb.rc.OnConfirm();
                    }
                }
            }
        }
        if (GameManager.gm.currRoomType == "ring")
        {
            if(lookingAt == "ring" && distanceFu(handTarget) == 1)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    FindObjectOfType<Room_Ring>().MoveUp();
                    FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.pSlideUp);
                    roomRing.GetComponent<Room_Ring_Main>().puzzleFeedback = true;
                }
                else if (Input.GetKeyDown(KeyCode.R))
                {
                    FindObjectOfType<Room_Ring>().MoveDown();
                    FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.pSlideDown);
                    roomRing.GetComponent<Room_Ring_Main>().puzzleFeedback = true;
                }
                else if (Input.GetKeyDown(KeyCode.T))
                {
                    FindObjectOfType<Room_Ring_Main>().OnChanged();
                    FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.pConfirm);
                }
            }
        }
        if (GameManager.gm.currRoomType == "statue")
        {

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (lookingAt == "statue")
                {
                    if (hand == null)
                    {
                        FindObjectOfType<Room_Statue>().PickUpFrom();
                    }
                }
                if(lookingAt == "socket")
                {
                    if (hand != null)
                    {
                        if (distanceFu(handTarget) == 1)
                        {
                            FindObjectOfType<Room_Statue>().Place();
                            FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.pInsertPiece);
                        }
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.G))
            {
                FindObjectOfType<Room_Statue>().Drop();
            }
        }
    }

    float distanceFu(GameObject target)
    {
        if (Vector3.Distance(target.transform.position, transform.position) < distance)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
