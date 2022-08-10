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
        col = GetComponent<Renderer>().material.color;
        gm = FindObjectOfType<GameManager>();
    }

    public void OnMouseEnterFunc()
    {
        if (onEnter != null) onEnter.Invoke();
        if (gameObject.GetComponent<Renderer>() && highlight)
        {
            GetComponent<Renderer>().material.color = Color.gray;
        }
        ph = gm.player.GetComponent<Player_Hand>();
        ph.lookingAt = type;
    }

    public void OnMouseExitFunc()
    {
        if (onExit != null) onExit.Invoke();
        ph.lookingAt = "none";
        if (gameObject.GetComponent<Renderer>() && highlight) GetComponent<Renderer>().material.color = col;
    }
}
