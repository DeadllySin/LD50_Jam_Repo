using UnityEngine;

public class RingManager : MonoBehaviour
{
    [SerializeField] private GameObject[] rings;

    public void MoveDown(GameObject i)
    {
        for (int j = 0; j < rings.Length; j++)
        {
            if (rings[j].gameObject == i)
            {
                if(j != rings.Length - 1)
                {
                    if (rings[j + 1] == null)
                    {
                        for (int k = j + 1; k < rings.Length; k++)
                        {
                            if (rings[k].gameObject != null)
                            {
                                rings[k - 1] = rings[j];
                                rings[j] = null;
                                return;
                            }
                            if(k == rings.Length - 1 && rings[k] == null)
                            {
                                rings[k] = rings[j];
                                rings[j] = null;
                                return;
                            }
                        }
                    }
                }
            }
        }
    }

    public void MoveUp(GameObject i)
    {
        for (int j = 0; j < rings.Length; j++)
        {
            if (rings[j].gameObject == i)
            {
                if (j != 0)
                {
                    if (rings[j - 1] == null)
                    {
                        for (int k = j - 1; k < rings.Length; k--)
                        {
                            if (rings[k].gameObject != null)
                            {
                                rings[k + 1] = rings[j];
                                rings[j] = null;
                                return;
                            }
                            if (k == 0 && rings[k] == null)
                            {
                                rings[0] = rings[j];
                                rings[j] = null;
                                return;
                            }
                        }
                    }
                }
            }
        }
    }
}
