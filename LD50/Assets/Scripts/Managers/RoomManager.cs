using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public int correctPieces;
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject trigger1;
    [SerializeField] private GameObject trigger2;

    private void FixedUpdate()
    {
        if (correctPieces == 2)
        {
            door.gameObject.SetActive(false);
            trigger1.SetActive(true);
        }
        if (correctPieces == 3)
        {
            trigger1.gameObject.SetActive(false);
            trigger2.SetActive(true);
        }
    }
}
