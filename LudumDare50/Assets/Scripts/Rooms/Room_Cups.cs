using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Cups : MonoBehaviour
{
    [SerializeField] private GameObject[] cups;
    [SerializeField] private List<Transform> positions;
    [SerializeField] private Transform coin;


    public void StartPuzzle()
    {
        cups[0].transform.position = new Vector3(cups[0].transform.position.x, cups[0].transform.position.y - .5f, cups[0].transform.position.z);
        cups[1].transform.position = new Vector3(cups[1].transform.position.x, cups[1].transform.position.y - .5f, cups[1].transform.position.z);
        cups[2].transform.position = new Vector3(cups[2].transform.position.x, cups[2].transform.position.y - .5f, cups[2].transform.position.z);
        coin.parent = cups[1].transform;
        coin.localPosition = Vector3.zero;
        StartCoroutine(cupsEnu());
    }

    IEnumerator cupsEnu()
    {
        yield return new WaitForSeconds(2f);

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

        for (int c = 0; c < cups.Length; c++)
        {
            int rdm = Random.Range(0, positions.Count);
            cups[c].GetComponent<Interactable_Cup>().goal = positions[rdm].position;
            positions.RemoveAt(rdm);
        }
        yield return new WaitForSeconds(4f);
        for (int b = 0; b < cups.Length; b++)
        {
            cups[b].GetComponent<Interactable_Cup>().canInteract = true;
        }

    }
}
