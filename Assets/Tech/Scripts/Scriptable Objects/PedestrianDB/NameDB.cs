using UnityEngine;

[CreateAssetMenu(fileName = "New Name DB", menuName = "NameDB")]
public class NameDB : ScriptableObject
{
    [SerializeField] private string[] _femaleNames, _maleNames;

    private int _lastIndex = -1;

    public string[] NamesByGender(Gender gender)
    {
        switch (gender)
        {
            case Gender.Female:
                return _femaleNames;

            case Gender.Male:
                return _maleNames;
            case Gender.None:
                if (Random.Range(0, 2) > 0)
                    return _maleNames;
                else
                    return _femaleNames;
            default:
                return _femaleNames;
        }
    }

    public string GetRandomNameByGender(Gender gender)
    {
        string[] names = NamesByGender(gender);
        int N = names.Length;
        int i = Random.Range(0, N);
        if (i == _lastIndex)
        {
            i = (i + 1) % (N - 1);
        }
        _lastIndex = i;
        return names[i];
    }
}