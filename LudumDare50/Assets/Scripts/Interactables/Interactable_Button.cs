using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Interactable))]
public class Interactable_Button : MonoBehaviour
{
    public bool isPressed = true;
    private Animator anim;
    [SerializeField] private bool canBePressedOnce;
    [SerializeField] private float cooldown = .55f;
    [SerializeField] private Color coL;
    [SerializeField] private UnityEvent unityEvent;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void OnPressed()
    {
        if (!isPressed)
        {
            GetComponentInChildren<Light>().color = coL;
            isPressed = true;
            Debug.LogError("I think we had a basic button press sound. Add it here");
            anim.SetTrigger("isPressed");
            unityEvent.Invoke();
            StartCoroutine(buttonCooldown());
        }
    }

    IEnumerator buttonCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        if(!canBePressedOnce) isPressed = false;
    }
}
