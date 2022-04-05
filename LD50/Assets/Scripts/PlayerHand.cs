using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    public GameObject hand;
    public GameObject handTarget;
    public GameObject handStatueTarget;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(hand == null)
            {
                StatuePiece tri = handTarget?.GetComponent<StatuePiece>();
                if (tri.state == "ground")
                {
                    tri.state = "inHand";
                    hand = tri.gameObject;
                    Debug.Log("Picked up " + hand.name);
                }
            }else
            {
                StatuePiece tri = hand.GetComponent<StatuePiece>();
                if(tri.state == "inHand" && handStatueTarget != null)
                {
                    StatueSocket ss = handStatueTarget.GetComponent<StatueSocket>();
                    ss.assinedStatue = hand;
                    Debug.Log("Assined "+ hand.name + " to " + handStatueTarget.name);
                }
            }
        }
    }
}
