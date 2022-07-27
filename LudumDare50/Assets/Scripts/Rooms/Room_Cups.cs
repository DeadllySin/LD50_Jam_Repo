using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Cups : MonoBehaviour
{
    [SerializeField] private GameObject[] cups;

    IEnumerator Start()
    {
        int i = 0;
        while (i < 10)
        {
            int rdm1 = Random.Range(0, 3);
            int rdm2 = Random.Range(0, 3);
            while (rdm1 == rdm2)
            {
                rdm2 = Random.Range(0, 3);
            }
            cups[rdm1].GetComponent<Interactable_Cup>().goal = cups[rdm2].transform.position;
            cups[rdm2].GetComponent<Interactable_Cup>().goal = cups[rdm1].transform.position;
            yield return new WaitWhile(() => cups[rdm2].GetComponent<Interactable_Cup>().transform.position != cups[rdm2].GetComponent<Interactable_Cup>().goal);
            yield return new WaitForSeconds(.5f);
            i++;
        }

    }
}
