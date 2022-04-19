using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_CeilingTrace : MonoBehaviour
{

    [SerializeField] bool isSeeingCeiling;
    [SerializeField] float rayDistance = 20f;
    [SerializeField] float fov = 90f;
    [SerializeField] Transform EyesTest;
    GameObject ceiling;

    private void Start()
    {
        ceiling = GameObject.FindGameObjectWithTag("Ceiling");
    }

    private void Update()
    {

        Debug.DrawRay(transform.position, transform.forward * rayDistance, isSeeingCeiling ? Color.green : Color.red, 2.0f);
        
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, rayDistance))
        {
            if (hit.collider.gameObject.tag == "Ceiling")
            {
                Debug.Log("true");
                //isSeeingCeiling = true;
                //Debug.Log(GameManager.gm.ceiling.transform);
            }
            //Debug.Log(hit.collider.name);
            
        }
        
        Debug.Log(GameManager.gm.ceiling.transform);
        //isSeeingCeiling = A_Trace_Utils.CanYouSeeThis(transform, GameManager.gm.ceiling.transform, null, fov, rayDistance);
        isSeeingCeiling = A_Trace_Utils.CanYouSeeThis(EyesTest, ceiling.transform, "Ceiling", fov, rayDistance);
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