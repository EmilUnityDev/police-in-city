using UnityEngine;

public class InteractionButton : MonoBehaviour
{
    [SerializeField] private StreetOfficer _streetOfficer;
    [SerializeField] private CrimeType _crimeType;

    public void Interact()
    {
        _streetOfficer?.TransitionFromDetainState(_crimeType);
    }
}