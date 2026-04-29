using UnityEngine;

[CreateAssetMenu(fileName ="CarMeshDB", menuName ="CarMeshDB")]
public class CarMeshDB : ScriptableObject
{
    [SerializeField] GameObject[] _cars;

    private int _lastIndex = -1;
    public GameObject GetNextCar()
    {
        if (_cars == null) return null;
        int N = _cars.Length;
        int i = _lastIndex + 1;
        if (i >= N) i = 0;
        _lastIndex = i;
        return _cars[i];
    }
}