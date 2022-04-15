using UnityEngine;

public class PlayerCrouch : MonoBehaviour
{
    private CharacterController cc;
    private StarterAssets.FirstPersonController fps;
    [SerializeField] private float ceilingCrouchHeight;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        fps = GetComponent<StarterAssets.FirstPersonController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) || GameManager.gm.ceiling.transform.position.y < ceilingCrouchHeight)
        {
            cc.height = 1;
            fps.MoveSpeed = 2;
            fps.SprintSpeed = 2;
            fps.JumpHeight = .7f;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl) && GameManager.gm.ceiling.transform.position.y > ceilingCrouchHeight)
        {
            fps.JumpHeight = 1.2f;
            cc.height = 2;
            fps.MoveSpeed = 4;
            fps.SprintSpeed = 6;
        }
    }
}
