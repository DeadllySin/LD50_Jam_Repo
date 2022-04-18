using System.Collections.Generic;
using UnityEngine;

public class Room_Statue : MonoBehaviour
{
    public int correctPieces;
    [SerializeField] private GameObject[] piecess;
    readonly List <GameObject> spawners = new List<GameObject>();
    readonly List<GameObject> pieces = new List<GameObject>();
    [SerializeField] private GameObject spawnerParent;
    [SerializeField] private GameObject ceiling;
    [HideInInspector] public Interactable_Statue sp;
    [HideInInspector] public Interactable_Socket ss;
    private PlayerHand phand;

    private void Awake()
    {
        for (int i = 0; i < piecess.Length; i++) pieces.Add(piecess[i]);
        phand = FindObjectOfType<PlayerHand>();
        foreach (Transform child in spawnerParent.transform) spawners.Add(child.gameObject);
    }

    private void Start()
    {
        GameManager.gm.whatRoom = "statue";
        ceiling.transform.parent = GameManager.gm.ceiling.transform;
        ceiling.transform.localPosition = new Vector3(0, 0, transform.position.z);
        while (pieces.Count > 0)
        {
            int temp = Random.Range(0,spawners.Count - 1);
            int temp2 = Random.Range(0, pieces.Count - 1);
            Instantiate(pieces[temp2], spawners[temp].transform.position, Quaternion.identity);
            spawners.RemoveAt(temp);
            pieces.RemoveAt(temp2);
        }
    }

    public void PickUpFrom()
    {
        if (phand.handTarget != null && phand.hand == null)
        {
            if (sp.state == "ground") PickUp(false);
            if (sp.state == "Ass") PickUp(true);
        }
    }

    public void Drop()
    {
        if (phand.hand == null) return;
        phand.hand.GetComponent<Interactable_Statue>().state = "ground";
        phand.hand.transform.parent = null;
        phand.hand.transform.position = new Vector3(phand.transform.position.x, 1.5f, phand.transform.position.z);
        phand.hand = null;
    }

    public void PickUp(bool setASNull = false)
    {
        sp.state = "inHand";
        phand.hand = sp.gameObject;
        phand.hand.transform.parent = GameManager.gm.player.transform;
        phand.hand.transform.localPosition = new Vector3(1, 1.2f, 2f);
        if (setASNull)
        {
            sp.ss.OnRemovedStatue();
            if (GameManager.gm.currRoom.GetComponent<Room_Statue>().correctPieces == 2) GameManager.gm.currTunnel.CloseDoor(GameManager.gm.currTunnel.doorIn);
            sp.ss.assinedStatue = null;
        }
    }

    public void Place()
    {
        if (phand.handStatueTarget != null && phand.hand.GetComponent<Interactable_Statue>().state == "inHand" && phand.handStatueTarget.GetComponent<Interactable_Socket>().assinedStatue == null)
        {
            phand.hand.transform.parent = phand.handStatueTarget.transform;
            phand.hand.GetComponent<Interactable_Statue>().state = "Ass";
            phand.hand.GetComponent<Interactable_Statue>().ss = ss;
            phand.hand.GetComponent<Interactable_Statue>().ss.assinedStatue = phand.hand;
            int correctPiecesOld = correctPieces;
            phand.hand.GetComponent<Interactable_Statue>().ss.OnAssienedStatue();
            phand.hand.transform.position = phand.handStatueTarget.transform.position;
            phand.hand = null;
            if(correctPiecesOld != 2) if (correctPieces >= 2) GameManager.gm.currTunnel.OpenDoor(GameManager.gm.currTunnel.doorIn);
        }
    }
}
