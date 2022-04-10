using UnityEngine;

public class PlayerCrouch : MonoBehaviour
{
    private CharacterController cc;
    private StarterAssets.FirstPersonController fps;
    private GameObject cell;
    [SerializeField] private float hight;

    private void Start()
    {
        cell = FindObjectOfType<MovingCeiling>().gameObject;
        cc = GetComponent<CharacterController>();
        fps = GetComponent<StarterAssets.FirstPersonController>();
    }

    void Update()
    {
        //Makes the player crouch when pressing crouch key or the ceiling is to low
        if (Input.GetKeyDown(KeyCode.LeftControl) || cell.transform.position.y < hight)
        {
            cc.height = 1;
            fps.MoveSpeed = 2;
            fps.SprintSpeed = 2;
            fps.JumpHeight = .7f;
        }// Uncrouches the player when the crouch key is released
        if (Input.GetKeyUp(KeyCode.LeftControl) && cell.transform.position.y > hight)
        {
            fps.JumpHeight = 1.2f;
            cc.height = 2;
            fps.MoveSpeed = 4;
            fps.SprintSpeed = 6;
        }
    }
}
