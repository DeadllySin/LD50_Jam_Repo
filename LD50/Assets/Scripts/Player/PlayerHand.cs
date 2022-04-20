using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    [HideInInspector] public GameObject hand;
    [HideInInspector] public GameObject handTarget;
    [HideInInspector] public GameObject handStatueTarget;
    [SerializeField] private float distance;
    public string lookingAt = "none";

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
                    Interactable_ColorButton icb = handTarget.GetComponent<Interactable_ColorButton>();
                    icb.OnPressed();
                }
            }
        }
        if (GameManager.gm.currRoomType == "ring")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log(lookingAt);
                if (lookingAt == "ring_up") handTarget.GetComponent<Interactable_Ring>().rr.MoveUp();
                else if (lookingAt == "ring_down") handTarget.GetComponent<Interactable_Ring>().rr.MoveDown();
                else if (lookingAt == "confirm") handTarget.GetComponent<Interactable_Ring>().rrm.OnChanged();
            }
        }
        if (GameManager.gm.currRoomType == "statue")
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
                else if (hand != null && handStatueTarget != null)
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
