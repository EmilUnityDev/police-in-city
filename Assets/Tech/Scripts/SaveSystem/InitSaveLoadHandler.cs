using System.Collections.Generic;

public class InitSaveLoadHandler : AbstractSaveLoadHandler
{        
    public override void ClearData()
    {
        
    }

    public override void LoadData()
    {
        if (_isInit) return;
        _saveData = SaveDataToJSONHandler.Load();
        if (_saveData == null)
        {
            _saveData = new SaveData(null, null, 1, 0, 5, false, 0, false, false, false, false);
        }               

        LoadToStreetDutyScriptableObject();

        LoadToRoadDutyScriptableObject();

        LoadToTutorialScriptableObject();

        _isInit = true;
    }

    private void LoadToRoadDutyScriptableObject()
    {        
        int dayNumber = _saveData.DayNumber;
        int completedOrdersCount = _saveData.CompletedOrdersCount;

        _roadDutyData.SetValues(dayNumber, completedOrdersCount);
    }

    private void LoadToStreetDutyScriptableObject()
    {
        string[] scannedNamesFromData = _saveData.DetainedNames;
        var scannedNames = new List<string>();
        if (scannedNamesFromData != null)
        {
            scannedNames.AddRange(scannedNamesFromData);
        }
        else
        {
            scannedNames = null;            
        }

        string[] arrestedOrShockedNamesFromData = _saveData.ArrestedOrShockedNames;
        var arrestShockedNames = new List<string>();
        if (arrestedOrShockedNamesFromData != null)
        {
            arrestShockedNames.AddRange(arrestedOrShockedNamesFromData);
        }
        else
        {
            arrestShockedNames = null;
        }

        bool isInit = scannedNames != null || arrestShockedNames != null;
        bool hasFinishedStreetDuty = _saveData.HasFinishedStreetDuty;
        int mistakesCount = _saveData.MistakesCount;

        _streetDutyData.SetValues(scannedNames, arrestShockedNames, mistakesCount, isInit, hasFinishedStreetDuty);
    }

    private void LoadToTutorialScriptableObject()
    {
        bool hasTouchedScreenOnStreetDuty = _saveData.HasTouchedScreenOnStreetDuty;
        bool hasTouchedScreenOnRoadDuty = _saveData.HasTouchedScreenOnRoadDuty;
        bool hasFinishedStreetDutyTutorial = _saveData.HasFinishedStreetDutyTutorial;
        bool hasFinishedRoadDutyTutorial = _saveData.HasFinishedRoadDutyTutorial;
        _tutorialData.SetValues(hasTouchedScreenOnStreetDuty, hasTouchedScreenOnRoadDuty, hasFinishedStreetDutyTutorial, hasFinishedRoadDutyTutorial);
    }

    public override void ResetData()
    {
        
    }

    public override SaveData GetSaveData()
    {
        if (!_isInit) LoadData();
        return base.GetSaveData();
    }
}