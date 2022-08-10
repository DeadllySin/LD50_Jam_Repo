using UnityEngine;

public class PlayerCrouch : MonoBehaviour
{
    private PlayerController pc;
    private GameManager gm;
    [HideInInspector] public bool allowStandingUp = true;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        pc = GetComponent<PlayerController>();
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.LeftControl) || Hinput.anyGamepad.leftStickClick.pressed) ||  !allowStandingUp)
        {
            //cc.height = 1;
            //fps.MoveSpeed = 2;
            //fps.SprintSpeed = 2;
        }
        if ((Input.GetKeyUp(KeyCode.LeftControl) || Hinput.anyGamepad.leftStickClick.released)  && allowStandingUp)
        {
            //cc.height = 2;
            //fps.MoveSpeed = 4;
            //fps.SprintSpeed = 6;
        }
    }
}
