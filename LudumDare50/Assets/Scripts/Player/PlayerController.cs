using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public GameObject lookingAt;
    Rigidbody rb;
    Vector3 smoothMovementVelocity, moveAmount;
    [SerializeField] private GameObject cameraHolder;
    [SerializeField] private float mouseSens, verticalLookRotation, walkSpeed, smoothTime, clamp1, clamp2;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move();
        Look();
        RayCast();
    }

    void RayCast()
    {
        Vector3 rayCastPos = new Vector3(transform.position.x, transform.position.y + .4f, transform.position.z);
        RaycastHit hit;
        if (Physics.Raycast(rayCastPos, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            lookingAt = hit.transform.gameObject;
            Interactable hitL = lookingAt.GetComponent<Interactable>();
            Debug.DrawRay(rayCastPos, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
            if(hitL != null) hitL.OnMouseEnterFunc();
        }
        else
        {
            if(lookingAt != null && lookingAt.GetComponent<Interactable>()) lookingAt.GetComponent<Interactable>().OnMouseExitFunc();
            lookingAt = null;
            Debug.DrawRay(rayCastPos, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
    }

    private void FixedUpdate()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
    }

    void Move()
    {
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * (Input.GetKey(KeyCode.LeftShift) ? walkSpeed * 2.5f : walkSpeed), ref smoothMovementVelocity, smoothTime);
    }

    void Look()
    {
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSens);
        verticalLookRotation += Input.GetAxisRaw("Mouse Y") * mouseSens;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, clamp1, clamp2);
        cameraHolder.transform.localEulerAngles = Vector3.left * verticalLookRotation;
    }
}
