using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_CeilingTrace : MonoBehaviour
{

    [SerializeField] bool isSeeingCeiling;
    [SerializeField] float rayDistance = 20f;
    [SerializeField] float fov = 90f;

    private void Start()
    {
        
    }

    private void Update()
    {

        isSeeingCeiling = A_Trace_Utils.CanYouSeeThis(transform.parent, GameManager.gm.ceiling.transform, null, fov, rayDistance);
        Debug.DrawRay(transform.parent.position, transform.forward * rayDistance, isSeeingCeiling ? Color.green : Color.red, 2.0f);

        RaycastHit hit;
        if (Physics.Raycast(transform.parent.position, transform.forward, out hit, rayDistance))
        {
            if (hit.collider.gameObject.tag == "Ceiling")
            {
                //isSeeingCeiling = true;

            }
            //Debug.Log(hit.collider.name);
            
            if (hit.collider.name != "Player")
            {
                Debug.Log(hit.collider.name);
            }
        }
        
        if (isSeeingCeiling)
        {
            Debug.Log("I can YES see the ceiling");
        }
        else
        {
            Debug.Log("I can NOT see the ceiling");
        }
    }

}