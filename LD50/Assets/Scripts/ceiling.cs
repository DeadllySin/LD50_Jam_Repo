using UnityEngine;

public class ceiling : MonoBehaviour
{

    public float speed;
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y - 7, transform.position.z), speed * Time.deltaTime);
    }
}
