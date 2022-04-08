using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject room;
    [SerializeField] private GameObject roof;

    private void Start()
    {
        roof = FindObjectOfType<MovingCeiling>().gameObject;
    }

    public void SpawnRoom(GameObject spawner)
    {
        Instantiate(room, new Vector3(spawner.transform.position.x, spawner.transform.position.y, spawner.transform.position.z + 10),Quaternion.identity);
    }

    private void FixedUpdate()
    {
        if (roof.transform.position.y <= 2) Debug.Log("You Are Dead");
    }
}
