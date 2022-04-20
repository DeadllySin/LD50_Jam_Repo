using UnityEngine;

public class A_CeilingTrace : MonoBehaviour
{

    [SerializeField] bool isSeeingCeiling;
    [SerializeField] float rayDistance = 20f;
    [SerializeField] Transform EyesTest;
    bool isLookingAtCeiling;

    FMOD.Studio.EventInstance ceilingSSInstance;

    private void Start()
    {
        isLookingAtCeiling = false;

        ceilingSSInstance = FMODUnity.RuntimeManager.CreateInstance("snapshot:/LookingAtCeiling");
    }

    private void Update()
    {

        //Debug.DrawRay(EyesTest.transform.position, transform.forward * rayDistance, isSeeingCeiling ? Color.green : Color.red, 2.0f);

        RaycastHit hit;
        if (Physics.Raycast(EyesTest.transform.position, transform.forward, out hit, rayDistance))
        {
            if (hit.collider.gameObject.tag == "Ceiling" && isLookingAtCeiling == false && GameManager.gm.ceiling.transform.position.y > 8f)
            {
                isLookingAtCeiling = true;
                ceilingSSInstance.start();

                //Debug.Log("true");
            }
            else if (hit.collider.gameObject.tag != "Ceiling" && isLookingAtCeiling == true)
            {
                isLookingAtCeiling = false;
                ceilingSSInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

                //Debug.Log("false");
            }

            //Debug.Log(GameManager.gm.ceiling.transform.position.y);
            //Debug.Log(hit.collider.name); 
        }
    }
}