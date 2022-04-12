using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject currDoor;
    public GameObject currRoom;
    public static GameManager gm;

    private void Awake()
    {
        gm = this;
    }
}