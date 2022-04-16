using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    [SerializeField] private Transform gDrop;
    [HideInInspector]public GameObject hand;
    [HideInInspector] public GameObject handTarget;
    [HideInInspector] public GameObject handStatueTarget;
    [HideInInspector] public StatuePiece sp;
    [HideInInspector] public StatueSocket ss;
    [SerializeField] private float distance;
    public string lookingAt = "none";
    private RingManager ringman;

    private void Awake()
    {
        ringman = FindObjectOfType<RingManager>();
    }

    private void Update()
    {
        Debug.Log(lookingAt);
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (lookingAt == "ring")
            {
                ringman.MoveUp();
            }
            else
            {
                if (hand == null)
                {
                    if (Vector3.Distance(handTarget.transform.position, transform.position) < distance)
                    {
                        PickUpFrom();
                    }
                }
                else if (hand != null)
                {
                    if (Vector3.Distance(handStatueTarget.transform.position, transform.position) < distance)
                    {
                        Place();
                    }
                }
            }



        }
        if(Input.GetKeyDown(KeyCode.R)) ringman.MoveDown();
        if (Input.GetKeyDown(KeyCode.G)) Drop();
    }

    void PickUpFrom()
    {
        if (handTarget != null && hand == null)
        {
            if (sp.state == "ground") PickUp(sp, ss, false);
            if (sp.state == "Ass") PickUp(sp, ss, true);
        }
    }

    void Drop()
    {
        if (hand == null) return;
        hand.transform.parent = null;
        hand.GetComponent<StatuePiece>().state = "ground";
        hand.transform.position = new Vector3(gDrop.position.x, 1.5f, gDrop.position.z);
        hand = null;
    }

    private void PickUp(StatuePiece sp ,StatueSocket ss , bool setASNull = false)
    {
        sp.state = "inHand";
        hand = sp.gameObject;
        hand.transform.parent = this.gameObject.transform;
        hand.transform.localPosition = new Vector3(1, 1.2f, 2f);
        if (setASNull)
        {
            if (sp.ss.assinedStatue.GetComponent<StatuePiece>().statueNumber == sp.ss.correctStatue) GameManager.gm.currRoom.GetComponent<StatueRoomManager>().correctPieces--;
            sp.ss.assinedStatue = null;
        }
    }

    void Place()
    {
        if (handStatueTarget != null && hand.GetComponent<StatuePiece>().state == "inHand" && handStatueTarget.GetComponent<StatueSocket>().assinedStatue == null)
        {
            hand.transform.parent = handStatueTarget.transform;
            hand.GetComponent<StatuePiece>().state = "Ass";
            hand.GetComponent<StatuePiece>().ss = ss;
            hand.GetComponent<StatuePiece>().ss.assinedStatue = hand;
            hand.GetComponent<StatuePiece>().ss.OnAssienedStatue();
            hand.transform.position = handStatueTarget.transform.position;
            hand = null;
            if(GameManager.gm.currRoom.GetComponent<StatueRoomManager>().correctPieces >= 2) GameManager.gm.OpenNextDoor();
            if (GameManager.gm.currRoom.GetComponent<StatueRoomManager>().correctPieces < 2) GameManager.gm.currDoor.GetComponent<Animator>().SetTrigger("isClosed");
        }
    }
}
