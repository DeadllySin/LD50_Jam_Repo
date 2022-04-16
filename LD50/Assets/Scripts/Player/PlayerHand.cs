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

    private void Update()
    {
        Debug.Log(lookingAt);
        if(GameManager.gm.whatRoom == "ring")
        {
            if(lookingAt == "ring")
            {
                if (Input.GetKeyDown(KeyCode.E)) ringman.MoveUp();
                if (Input.GetKeyDown(KeyCode.R)) ringman.MoveDown();
            }

        }
        if(GameManager.gm.whatRoom == "statue")
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
