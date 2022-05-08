using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed;
    public float defaultSpeed;

    private void FixedUpdate() { transform.position = Vector3.MoveTowards(transform.position, GameManager.gm.player.transform.position, speed * Time.deltaTime); }

    // The target marker.
    private Transform target;

    // Angular speed in radians per sec.
    public float ROspeed = 2.0f;

    private void Awake()
    {
        target = GameManager.gm.player.transform;
    }

    void Update()
    {
        // Determine which direction to rotate towards
        Vector3 targetDirection = target.position - transform.position;

        // The step size is equal to speed times frame time.
        float singleStep = ROspeed * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

        // Draw a ray pointing at our target in
        Debug.DrawRay(transform.position, newDirection, Color.red);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}