using UnityEngine;

[CreateAssetMenu(fileName ="PersonDB", menuName ="GameData/PersonDB")]
public class PersonDB : ScriptableObject
{
    [SerializeField] private PersonSO[] _persons;

    public PersonSO[] Persons { get => _persons; }
}