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

        //isSeeingCeiling = A_Trace_Utils.CanYouSeeThis(transform, GameManager.gm.ceiling.transform, null, fov, rayDistance);
        //Debug.DrawRay(transform.position, transform.forward * rayDistance, isSeeingCeiling ? Color.green : Color.red);
        if (isSeeingCeiling)
        {
            //Debug.Log("I can see the ceiling");
        }
        else
        {
            //Debug.Log("I can't see the ceiling");
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.up, out hit, rayDistance))
        {
            //Debug.DrawRay(transform.position, Vector3.up * rayDistance, Color.red);
        }
        else
        {
            //Debug.DrawRay(transform.position, Vector3.up * rayDistance, Color.green);
        }
    }

}