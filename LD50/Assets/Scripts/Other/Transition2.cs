using UnityEngine;
using System.Collections;

public class Transition2 : MonoBehaviour
{
    [SerializeField] private Animator doorAnim;
    [SerializeField] private GameObject fakeDoor;
    public void CloseDoor()
    {
        StartCoroutine(deleteLastTunnel());
    }
    
    IEnumerator deleteLastTunnel()
    {
        doorAnim.SetTrigger("isClosed");
        GameObject tunnelParent = GetComponentInParent<Transform>().GetComponentInParent<Tunnel>().gameObject;

        yield return new WaitForSeconds(.8f);
        GameObject fakeDoorTemp = Instantiate(fakeDoor, new Vector3(8.75f, 1, tunnelParent.GetComponent<Tunnel>().doorOut.transform.position.z), Quaternion.Euler(new Vector3(0, 90, 0)));
        fakeDoorTemp.transform.parent = GameManager.gm.currRoom.transform;
        Destroy(tunnelParent);
    }
}
