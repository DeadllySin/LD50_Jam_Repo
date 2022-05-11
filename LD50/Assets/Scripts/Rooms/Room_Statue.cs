using System.Collections.Generic;
using UnityEngine;

public class Room_Statue : MonoBehaviour
{
    public int correctPieces;
    private PlayerHand phand;
    private Room_Main room;
    readonly List<GameObject> spawners = new List<GameObject>();
    readonly List<GameObject> pieces = new List<GameObject>();
    [SerializeField] private GameObject[] piecess;
    [SerializeField] private GameObject spawnerParent;
    [HideInInspector] public Interactable_Statue sp;
    [HideInInspector] public Interactable_Socket ss;


    private void Start()
    {
        room = GetComponentInParent<Room_Main>();
        for (int i = 0; i < piecess.Length; i++) pieces.Add(piecess[i]);
        foreach (Transform child in spawnerParent.transform) spawners.Add(child.gameObject);
        while (pieces.Count > 0)
        {
            int temp = Random.Range(0, spawners.Count - 1);
            int temp2 = Random.Range(0, pieces.Count - 1);
            GameObject stat = Instantiate(pieces[temp2], spawners[temp].transform.position, Quaternion.identity);
            if (stat.GetComponent<Interactable_Statue>().isHead)
            {
                stat.transform.position = new Vector3(spawners[temp].transform.position.x, .60f, spawners[temp].transform.position.z);
            }
            spawners.RemoveAt(temp);
            pieces.RemoveAt(temp2);
        }
    }

    public void PickUpFrom()
    {
        phand = GameManager.gm.player.GetComponent<PlayerHand>();
        if (phand.handTarget != null && phand.hand == null)
        {
            if (sp.state == "ground")
            {
                PickUp(false);
                FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.pPickUp);
            }
            if (sp.state == "Ass") PickUp(true);
        }
    }

    public void Drop()
    {
        phand = GameManager.gm.player.GetComponent<PlayerHand>();
        if (phand.hand == null) return;
        FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.pDrop);
        phand.hand.GetComponent<Interactable_Statue>().state = "ground";
        phand.hand.transform.parent = null;
        phand.hand.transform.position = new Vector3(phand.transform.position.x, 1, phand.transform.position.z);
        if(phand.hand.GetComponent<Interactable_Statue>().isHead)
        {
            phand.hand.transform.position = new Vector3(phand.transform.position.x, .6f, phand.transform.position.z);
        }
        phand.hand.transform.rotation = Quaternion.Euler(0, 0, 0);
        phand.hand = null;
    }

    public void PickUp(bool setASNull = false)
    {
        phand = GameManager.gm.player.GetComponent<PlayerHand>();
        if (setASNull) phand.handTarget.GetComponent<Interactable_Statue>().ss.GetComponent<MeshCollider>().enabled = true;
        sp.state = "inHand";
        phand.hand = sp.gameObject;

        phand.hand.transform.parent = Camera.main.transform;
        phand.hand.transform.position = new Vector3(0, 0, 0);
        phand.hand.transform.rotation = new Quaternion(0, 0, 0, 0);
        phand.hand.transform.localRotation = new Quaternion(0, 0, 0, 0);
        phand.hand.transform.localPosition = new Vector3(.5f, -.5f, 1f);
        if (setASNull)
        {
            sp.ss.OnRemovedStatue();
            FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.pRemovePiece);
            sp.ss.assinedStatue = null;
            OnValueChanged();
        }
    }

    public void Place()
    {
        phand = GameManager.gm.player.GetComponent<PlayerHand>();
        if (phand.handTarget != null && phand.hand.GetComponent<Interactable_Statue>().state == "inHand" && phand.handTarget.GetComponent<Interactable_Socket>().assinedStatue == null)
        {
            if (phand.handTarget.GetComponent<Interactable_Socket>().correctStatue == phand.hand.GetComponent<Interactable_Statue>().statueNumber)
            {
                FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.pInsertPiece);
                phand.hand.transform.parent = phand.handTarget.transform;
                phand.hand.transform.localRotation = new Quaternion(0, 0, 0, 0);
                phand.hand.transform.localPosition = phand.handTarget.transform.position;
                phand.hand.GetComponent<Interactable_Statue>().state = "Ass";
                phand.hand.GetComponent<Interactable_Statue>().ss = ss;
                phand.hand.GetComponent<Interactable_Statue>().ss.assinedStatue = phand.hand;
                phand.hand.GetComponent<Interactable_Statue>().ss.OnAssienedStatue();
                phand.hand.transform.position = phand.handTarget.transform.position;
                phand.hand = null;
                phand.handTarget.GetComponent<Interactable_Socket>().GetComponent<MeshCollider>().enabled = false;
                OnValueChanged();
            }
        }
    }

    void OnValueChanged()
    {
        int max = piecess.Length;
        switch (piecess.Length)
        {
            case 3:
                switch (correctPieces)
                {
                    case 2:
                        room.state = "ok";
                        break;
                    case 3:
                        room.state = "perfect";
                        break;
                    default:
                        room.state = "bad";
                        break;
                }
                break;
            case 4:
                switch (correctPieces)
                {
                    case 3:
                        room.state = "ok";
                        break;
                    case 4:
                        room.state = "perfect";
                        break;
                    default:
                        room.state = "bad";
                        break;
                }
                break;
            case 5:
                switch (correctPieces)
                {
                    case 4:
                        room.state = "ok";
                        break;
                    case 5:
                        room.state = "perfect";
                        break;
                    default:
                        room.state = "bad";
                        break;
                }
                break;
            default:
                break;
        }

    }
}
