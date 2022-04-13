using UnityEngine;

public class Tunnel : MonoBehaviour
{
    public GameObject doorIn;
    public GameObject doorOut;

    private void Awake()
    {
        doorIn.GetComponent<Animator>().SetTrigger("isClosed");
    }
}
