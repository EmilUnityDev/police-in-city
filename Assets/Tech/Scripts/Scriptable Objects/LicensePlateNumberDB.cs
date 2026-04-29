using UnityEngine;

[CreateAssetMenu(fileName = "New LicensePlateNumbersDB", menuName = "LicensePlateNumbersDB")]
public class LicensePlateNumberDB : ScriptableObject
{
    [SerializeField] private string[] _licensePlateNumbers;

    public string GetRandomLicensePlateNumber()
    {
        int N = _licensePlateNumbers.Length;
        int i = Random.Range(0, N);
        return _licensePlateNumbers[i];
    }
}