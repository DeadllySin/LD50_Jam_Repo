using UnityEngine;

public class Room_Ring : MonoBehaviour
{
    public Slot[] s;
    private PlayerHand ph;

    private void Awake()
    {
        ph = FindObjectOfType<PlayerHand>();
    }

    private void Start()
    {
        GameManager.gm.whatRoom = "ring";
    }

    public void MoveDown()
    {
        int ringMovedIndex = 0;
        for (int j = s.Length - 1; j > 0; j--)
        {
            if (s[j].ring == null)
            {
                ringMovedIndex = j + 1;
                break;
            }
        }
        for (int j = 0; j < s.Length; j++)
        {
            if (s[j].ring == null)
            {
                s[j].ring = s[ringMovedIndex].ring;
                s[j].ring.transform.position = s[j].pos;
                s[ringMovedIndex].ring = null;
                return;
            }
        }
    }

    public void MoveUp()
    {
        int ringMovedIndex = 0;
        for (int j = 0; j < s.Length; j++)
        {
            if (s[j].ring == null)
            {
                ringMovedIndex = j - 1;
                break;
            }
        }
        for (int j = s.Length - 1; j > -1; j--)
        {
            if (s[j].ring == null)
            {
                s[j].ring = s[ringMovedIndex].ring;
                s[j].ring.transform.position = s[j].pos;
                s[ringMovedIndex].ring = null;
                return;
            }
        }
    }
}

[System.Serializable]
public struct Slot
{
    public GameObject ring;
    public Vector3 pos;
} 
