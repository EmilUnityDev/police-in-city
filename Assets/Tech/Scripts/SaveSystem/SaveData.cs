using UnityEngine;
using System;

[Serializable]
public class SaveData
{
    [SerializeField] private string[] _detainedNames, _arrestedOrShockedNames;
    [SerializeField] private int _dayNumber, _completedOrdersCount, _overallOrdersCount, _mistakesCount;
    [SerializeField] private bool _hasFinishedStreetDuty, _hasTouchedScreenOnStreetDuty, _hasTouchedScreenOnRoadDuty, _hasFinishedStreetDutyTutorial, _hasFinishedRoadDutyTutorial;

    public SaveData(string[] detainedNames, string[] arrestedOrShockedNames, int dayNumber, int completedOrdersCount, int overallOrdersCount, bool hasFinishedStreetDuty, int mistakesCount, bool hasTouchedScreenOnStreetDuty, bool hasTouchedScreenOnRoadDuty, bool hasFinishedStreetDutyTutorial, bool hasFinishedRoadDutyTutorial)
    {
        _detainedNames = detainedNames;
        _arrestedOrShockedNames = arrestedOrShockedNames;
        _dayNumber = dayNumber;
        _completedOrdersCount = completedOrdersCount;
        _overallOrdersCount = overallOrdersCount;
        _hasFinishedStreetDuty = hasFinishedStreetDuty;
        _mistakesCount = mistakesCount;
        _hasTouchedScreenOnStreetDuty = hasTouchedScreenOnStreetDuty;
        _hasTouchedScreenOnRoadDuty = hasTouchedScreenOnRoadDuty;
        _hasFinishedStreetDutyTutorial = hasFinishedStreetDutyTutorial;
        _hasFinishedRoadDutyTutorial = hasFinishedRoadDutyTutorial;
    }

    public string[] DetainedNames { get => _detainedNames; set => _detainedNames = value; }
    public string[] ArrestedOrShockedNames { get => _arrestedOrShockedNames; set => _arrestedOrShockedNames = value; }
    public int DayNumber { get => _dayNumber; set => _dayNumber = value; }
    public int CompletedOrdersCount { get => _completedOrdersCount; set => _completedOrdersCount = value; }
    public int OverallOrdersCount { get => _overallOrdersCount; set => _overallOrdersCount = value; }
    public bool HasFinishedStreetDuty { get => _hasFinishedStreetDuty; set => _hasFinishedStreetDuty = value; }
    public int MistakesCount { get => _mistakesCount; set => _mistakesCount = value; }
    public bool HasTouchedScreenOnStreetDuty { get => _hasTouchedScreenOnStreetDuty; set => _hasTouchedScreenOnStreetDuty = value; }
    public bool HasTouchedScreenOnRoadDuty { get => _hasTouchedScreenOnRoadDuty; set => _hasTouchedScreenOnRoadDuty = value; }
    public bool HasFinishedStreetDutyTutorial { get => _hasFinishedStreetDutyTutorial; set => _hasFinishedStreetDutyTutorial = value; }
    public bool HasFinishedRoadDutyTutorial { get => _hasFinishedRoadDutyTutorial; set => _hasFinishedRoadDutyTutorial = value; }

    public void Save(StreetDutyData streetDutyData, RoadSceneGameData roadDutyData, TutorialGameData tutorialData)
    {
        var scannedPedestrians = streetDutyData.ScannedPedestriansNames;
        if (scannedPedestrians != null)
        {
            _detainedNames = scannedPedestrians.ToArray();
        }
        else
        {
            _detainedNames = null;
        }

        var arrestedOrShockedPedestrians = streetDutyData.ArrestedOrShockedPedestrianNames;

        if (arrestedOrShockedPedestrians != null)
        {
            _arrestedOrShockedNames = arrestedOrShockedPedestrians.ToArray();
        }
        else
        {
            _arrestedOrShockedNames = null;
        }
        
        _dayNumber = roadDutyData.DayNumber; ;
        _completedOrdersCount = roadDutyData.CompletedOrdersCount;
        _overallOrdersCount = roadDutyData.OverallOrdersCount;
        _hasFinishedStreetDuty = streetDutyData.HasFinishedStreetDuty;
        _mistakesCount = streetDutyData.MistakesCount;
        _hasTouchedScreenOnStreetDuty = tutorialData.HasTouchedScreenOnStreetDuty;
        _hasTouchedScreenOnRoadDuty = tutorialData.HasTouchedScreenOnRoadDuty;
        _hasFinishedStreetDutyTutorial = tutorialData.HasFinishedStreetDutyTutorial;
        _hasFinishedRoadDutyTutorial = tutorialData.HasFinishedRoadDutyTutorial;
    }
}