using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Room_Statue : MonoBehaviour
{
    private Player_Hand phand;
    private Room_Main room;
    private int totalStatues;
    private int foundPieces;
    List<GameObject> spawners = new List<GameObject>();
    List<GameObject> pieces = new List<GameObject>();
    List<GameObject> prePlaced = new List<GameObject>();
    public StatuePiece[] sps;
    [SerializeField] private GameObject ramParent;
    [SerializeField] private GameObject spawnerParent;
    [HideInInspector] public Interactable_Statue sp;
    [SerializeField] private Material ramMaterial;
    IEnumerator Start()
    {
        room = GetComponentInParent<Room_Main>();
        for (int i = 0; i < sps.Length; i++)
        {
            pieces.Add(sps[i].piecePrefab);
            prePlaced.Add(sps[i].prePlaced);
        }
        Debug.Log(GameManager.gm.statueRoomPro);
        switch (GameManager.gm.statueRoomPro)
        {
            case 0:
                int rdm = Random.Range(0, pieces.Count - 1);
                pieces.RemoveAt(rdm);
                prePlaced[rdm].GetComponent<MeshRenderer>().material = ramMaterial;
                prePlaced.RemoveAt(rdm);
                yield return new WaitForSeconds(.05f);
                int rdm1 = Random.Range(0, pieces.Count - 1);
                pieces.RemoveAt(rdm1);
                prePlaced[rdm1].GetComponent<MeshRenderer>().material = ramMaterial;
                prePlaced.RemoveAt(rdm1);
                totalStatues = 3;
                break;
            case 1:
                int rdm2 = Random.Range(0, pieces.Count - 1);
                pieces.RemoveAt(rdm2);
                prePlaced[rdm2].GetComponent<MeshRenderer>().material = ramMaterial;
                prePlaced.RemoveAt(rdm2);
                yield return new WaitForSeconds(.05f);
                int rdm3 = Random.Range(0, pieces.Count - 1);
                pieces.RemoveAt(rdm3);
                prePlaced[rdm3].GetComponent<MeshRenderer>().material = ramMaterial;
                prePlaced.RemoveAt(rdm3);
                totalStatues = 3;
                break;
            case 2:
                int rdm4 = Random.Range(0, pieces.Count - 1);
                pieces.RemoveAt(rdm4);
                prePlaced[rdm4].GetComponent<MeshRenderer>().material = ramMaterial;
                prePlaced.RemoveAt(rdm4);
                totalStatues = 4;
                break;
            case 3:
                int rdm5 = Random.Range(0, pieces.Count - 1);
                pieces.RemoveAt(rdm5);
                prePlaced[rdm5].GetComponent<MeshRenderer>().material = ramMaterial;
                prePlaced.RemoveAt(rdm5);
                totalStatues = 4;
                break;
            default:
                totalStatues = 5;
                break;
        }
        foreach (Transform child in spawnerParent.transform) spawners.Add(child.gameObject);
        while (pieces.Count > 0)
        {
            int temp = Random.Range(0, spawners.Count - 1);
            int temp2 = Random.Range(0, pieces.Count - 1);
            GameObject stat = Instantiate(pieces[temp2], spawners[temp].transform.position, Quaternion.identity);
            spawners.RemoveAt(temp);
            pieces.RemoveAt(temp2);
        }

        GameManager.gm.statueRoomPro++;
        yield return 0;
    }

    public void Drop()
    {

        phand = GameManager.gm.player.GetComponent<Player_Hand>();
        if (phand.hand == null) return;
        FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.pDrop);
        phand.hand.GetComponentInChildren<Interactable_Statue>().state = "ground";
        phand.hand.transform.parent = null;
        phand.hand.transform.position = new Vector3(phand.transform.position.x, 1, phand.transform.position.z);
        phand.hand.transform.rotation = Quaternion.Euler(0, 0, 0);
        phand.hand = null;
    }

    public void PickUp()
    {
        phand = GameManager.gm.player.GetComponent<Player_Hand>();
        if (phand.handTarget != null && phand.hand == null)
        {
            FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.pPickUp);
            phand = GameManager.gm.player.GetComponent<Player_Hand>();
            sp.state = "inHand";
            phand.hand = sp.gameObject;
            foundPieces++;
            phand.hand.transform.parent = Camera.main.transform;
            phand.hand.transform.SetPositionAndRotation(new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
            phand.hand.transform.localRotation = new Quaternion(0, 0, 0, 0);
            phand.hand.transform.localPosition = new Vector3(.5f, -.5f, 1f);
        }
    }

    public void Place()
    {
        phand = GameManager.gm.player.GetComponent<Player_Hand>();
        if (phand.handTarget != null && phand.handTarget.GetComponent<Interactable_Socket>().correctStatue == phand.hand.GetComponent<Interactable_Statue>().statueNumber)
        {
            FMODUnity.RuntimeManager.PlayOneShot(AudioManager.am.pInsertPiece);
            phand.handTarget.GetComponent<Interactable_Socket>().placeHolder.GetComponent<MeshRenderer>().material = ramMaterial;
            Destroy(phand.hand);
            phand.hand = null;
            OnValueChanged();
        }
    }

    void OnValueChanged()
    {
        switch (totalStatues)
        {
            case 3:
                switch (foundPieces)
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
                switch (foundPieces)
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
            default:
                switch (foundPieces)
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
        }
    }
}

[System.Serializable]
public class StatuePiece
{
    public GameObject piecePrefab;
    public GameObject prePlaced;
}
