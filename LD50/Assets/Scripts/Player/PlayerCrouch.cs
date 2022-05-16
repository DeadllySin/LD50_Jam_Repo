using UnityEngine;

public class PlayerCrouch : MonoBehaviour
{
    private CharacterController cc;
    private StarterAssets.FirstPersonController fps;
    [SerializeField] private float ceilingCrouchHeight;
    [HideInInspector] public bool allowStandingUp = true;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        fps = GetComponent<StarterAssets.FirstPersonController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) || GameManager.gm.ceiling.transform.position.y < ceilingCrouchHeight || !allowStandingUp)
        {
            cc.height = 1;
            fps.MoveSpeed = 2;
            fps.SprintSpeed = 2;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl) && GameManager.gm.ceiling.transform.position.y > ceilingCrouchHeight && allowStandingUp)
        {
            cc.height = 2;
            fps.MoveSpeed = 4;
            fps.SprintSpeed = 6;
        }
    }
}
