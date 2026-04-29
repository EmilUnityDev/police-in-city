using UnityEngine;

[CreateAssetMenu(fileName = "New Description DB", menuName = "DescriptionDB")]
public class DescriptionDB : ScriptableObject
{
    public CrimeType CrimeType => _crimeType;
    public string[] Descriptions => _descriptions;

    [SerializeField] private CrimeType _crimeType;
    [SerializeField][TextArea] private string[] _descriptions;

    private int _lastUsedIndex;

    public string GetNextDescription()
    {
        int index = _lastUsedIndex + 1;
        if (index >= _descriptions.Length)
        {
            index = 0;
        }
        _lastUsedIndex = index;
        return _descriptions[index];
    }

    public string GetRandomDescription()
    {
        int index = Random.Range(0, _descriptions.Length);

        index = Mathf.Min(index, _descriptions.Length - 1);

        _lastUsedIndex = index;
        return _descriptions[index];
    }
}