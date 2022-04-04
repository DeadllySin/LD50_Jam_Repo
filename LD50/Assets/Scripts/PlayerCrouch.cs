using UnityEngine;

public class PlayerCrouch : MonoBehaviour
{
    private CharacterController cc;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            cc.height = 1;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            cc.height = 2;
        }
    }
}
