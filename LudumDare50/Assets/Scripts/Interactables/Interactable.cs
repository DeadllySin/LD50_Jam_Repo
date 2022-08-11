using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    private Player_Hand ph;
    private Color col;
    public string arg;
    private GameManager gm;
    [SerializeField] private bool highlight = true;
    [SerializeField] private string type;
    [SerializeField] private UnityEvent onEnter;
    [SerializeField] private UnityEvent onExit;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        ph = gm.player.GetComponent<Player_Hand>();
        col = GetComponent<Renderer>().material.color;
    }

    public void OnMouseEnterFunc()
    {
        if (gameObject.GetComponent<Renderer>() && highlight)
        {
            GetComponent<Renderer>().material.color = Color.gray;
        }
        if (onEnter != null) onEnter.Invoke();
        ph.handTarget = this.gameObject;
        ph.lookingAt = type;
    }

    private void Update()
    {
        if (ph.handTarget == null)
        {
            if (onExit != null) onExit.Invoke();
            ph.lookingAt = "none";
            if (gameObject.GetComponent<Renderer>()) GetComponent<Renderer>().material.color = col;
        }
    }
}
