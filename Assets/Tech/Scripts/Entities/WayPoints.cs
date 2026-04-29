using UnityEngine;

public class WayPoints : MonoBehaviour
{
    private void Awake()
    {
        DisableChildren();
    }

    public Vector3[] GetWayPoints()
    {
        int N = transform.childCount;
        Vector3[] positions = new Vector3[N];
        for (int i = 0; i < N; i++)
        {
            positions[i] = transform.GetChild(i).position;
        }

        return positions;
    }

    private void DisableChildren()
    {
        int N = transform.childCount;        
        for (int i = 0; i < N; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}