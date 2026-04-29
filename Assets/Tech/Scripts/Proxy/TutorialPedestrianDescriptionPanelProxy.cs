using UnityEngine;

public class TutorialPedestrianDescriptionPanelProxy : PedestrianDescriptionPanel
{
    [SerializeField] private PedestrianDescriptionPanel _pedestrianPanel;
    [SerializeField] private GameObject _letGoFinger, _arrestFinger, _shockFinger;

    public override void Activate()
    {
        _pedestrianPanel?.Activate();
    }

    public override void Deactivate()
    {
        _pedestrianPanel?.Deactivate();
    }

    public override void FeedPedestrianData(PedestrianData data)
    {
        CrimeType crimeType = data.CrimeType;
        switch (crimeType)
        {
            case CrimeType.NotGuilty:
                _letGoFinger.SetActive(true);
                _arrestFinger.SetActive(false);
                _shockFinger.SetActive(false);
                break;
            case CrimeType.ArrestWorthy:
                _arrestFinger.SetActive(true);
                _letGoFinger.SetActive(false);
                _shockFinger.SetActive(false);
                break;
            case CrimeType.ShockWorthy:
                _shockFinger.SetActive(true);
                _arrestFinger.SetActive(false);
                _letGoFinger.SetActive(false);
                break;
            default:
                _shockFinger.SetActive(false);
                _arrestFinger.SetActive(false);
                _letGoFinger.SetActive(false);
                break;
        }

        _pedestrianPanel?.FeedPedestrianData(data);
    }
}