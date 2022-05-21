using UnityEngine;

public class PlayerCrouch : MonoBehaviour
{
    private CharacterController cc;
    private GameManager gm;
    private StarterAssets.FirstPersonController fps;
    [SerializeField] private float ceilingCrouchHeight;
    [HideInInspector] public bool allowStandingUp = true;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        cc = GetComponent<CharacterController>();
        fps = GetComponent<StarterAssets.FirstPersonController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) || gm.ceiling.transform.position.y < ceilingCrouchHeight || !allowStandingUp)
        {
            cc.height = 1;
            fps.MoveSpeed = 2;
            fps.SprintSpeed = 2;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl) && gm.ceiling.transform.position.y > ceilingCrouchHeight && allowStandingUp)
        {
            cc.height = 2;
            fps.MoveSpeed = 4;
            fps.SprintSpeed = 6;
        }
    }
}
