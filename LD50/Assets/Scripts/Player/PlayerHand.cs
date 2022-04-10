using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    public GameObject hand;
    public GameObject handTarget;
    public GameObject handStatueTarget;
    private RoomManager room;
    [HideInInspector] public StatuePiece sp;
    [HideInInspector] public StatueSocket ss;

    private void Start()
    {
        room = FindObjectOfType<RoomManager>();
    }

    private void Update()
    {
        //Interacting with objects in scene
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (hand == null) HandEmpty();
            else if(hand != null) HandFull();
        }
    }

    void HandEmpty()
    {
        //If player looks at a item
        if (handTarget != null && hand == null)
        {
            if (sp.state == "ground") PickUp(sp, ss, false);
            if (sp.state == "Ass") PickUp(sp, ss, true);
        }
    }

    private void PickUp(StatuePiece sp ,StatueSocket ss , bool setASNull = false)
    {
        sp.state = "inHand";
        hand = sp.gameObject;
        hand.transform.position = new Vector3(-100, -100, -100);
        if (setASNull)
        {
            if (sp.ss.assinedStatue.GetComponent<StatuePiece>().statueNumber == sp.ss.correctStatue) room.correctPieces--;
            sp.ss.assinedStatue = null;
        }
        Debug.Log("Picked up " + hand.name);
    }

    void HandFull()
    {
        //Places statue piece in socket
        if (handStatueTarget != null && hand.GetComponent<StatuePiece>().state == "inHand" && handStatueTarget.GetComponent<StatueSocket>().assinedStatue == null)
        {
            hand.GetComponent<StatuePiece>().state = "Ass";
            hand.GetComponent<StatuePiece>().ss = ss;
            hand.GetComponent<StatuePiece>().ss.assinedStatue = hand;
            hand.GetComponent<StatuePiece>().ss.OnAssienedStatue();
            Debug.Log("Assined " + hand.name + "to " + ss.gameObject.name);
            hand.transform.position = handStatueTarget.transform.position;
            hand = null;
        }

        if (handStatueTarget == null && hand.GetComponent<StatuePiece>().state == "inHand" && handTarget == null)
        {
            hand.GetComponent<StatuePiece>().state = "ground";
            hand.transform.position = Vector3.zero;
            hand = null;
        }
    }
}
