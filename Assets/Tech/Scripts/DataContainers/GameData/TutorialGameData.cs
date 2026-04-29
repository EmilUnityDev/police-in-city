using UnityEngine;

[CreateAssetMenu(fileName = "New Tutorial GameData", menuName = "GameData/TutorialData")]
public class TutorialGameData : AbstractGameData
{
    [SerializeField] private bool _hasTouchedScreenOnStreetDuty, _hasTouchedScreenOnRoadDuty, _hasFinishedStreetDutyTutorial, _hasFinishedRoadDutyTutorial;
    [SerializeField] private int _streetDutyTutorialIndex, _roadDutyTutorialIndex;

    public bool HasTouchedScreenOnStreetDuty { get => _hasTouchedScreenOnStreetDuty; set => _hasTouchedScreenOnStreetDuty = value; }
    public bool HasTouchedScreenOnRoadDuty { get => _hasTouchedScreenOnRoadDuty; set => _hasTouchedScreenOnRoadDuty = value; }
    public bool HasFinishedStreetDutyTutorial { get => _hasFinishedStreetDutyTutorial; set => _hasFinishedStreetDutyTutorial = value; }
    public bool HasFinishedRoadDutyTutorial { get => _hasFinishedRoadDutyTutorial; set => _hasFinishedRoadDutyTutorial = value; }
    public int RoadDutyTutorialIndex { get => _roadDutyTutorialIndex; set => _roadDutyTutorialIndex = value; }
    public int StreetDutyTutorialIndex { get => _streetDutyTutorialIndex; set => _streetDutyTutorialIndex = value; }

    public void SetValues(bool hasTouchedScreenOnStreetDuty, bool hasTouchedScreenOnRoadDuty, bool hasFinishedStreetDutyTutorial, bool hasFinishedRoadDutyutorial)
    {
        HasTouchedScreenOnStreetDuty = hasTouchedScreenOnStreetDuty;
        HasTouchedScreenOnRoadDuty = hasTouchedScreenOnRoadDuty;
        HasFinishedStreetDutyTutorial = hasFinishedStreetDutyTutorial;
        HasFinishedRoadDutyTutorial = hasFinishedRoadDutyutorial;
    }
}