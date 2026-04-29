using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Street Duty GameData", menuName = "GameData/StreetDutyData")]
public class StreetDutyData : ScriptableObject
{
    public bool HasFinishedStreetDuty { get; set; }
    public List<string> ScannedPedestriansNames
    {
        get => _scannedPedestriansNames;
    }

    public int MistakesCount
    {
        get => _mistakesCount;
    }

    public List<string> ArrestedOrShockedPedestrianNames 
    {
        get => _arrestedOrShockedPedestrianNames; 
    }

    [SerializeField] private List<string> _scannedPedestriansNames;
    [SerializeField] private List<string> _arrestedOrShockedPedestrianNames;
    [SerializeField] private bool _isInit;
    [SerializeField] private int _mistakesCount;

    private void Init()
    {
        if (_isInit) return;
        _scannedPedestriansNames = new List<string>();
        _arrestedOrShockedPedestrianNames = new List<string>();
        _mistakesCount = 0;
        
        _isInit = true;
    }

    public void AddScannedPedestrian(string name, bool arrestedOrShocked = true)
    {
        if (!_isInit) Init();

        _scannedPedestriansNames.Add(name);
        if (arrestedOrShocked) _arrestedOrShockedPedestrianNames.Add(name);
    }

    public void ClearData()
    {
        _scannedPedestriansNames = null;
        _arrestedOrShockedPedestrianNames = null;
        HasFinishedStreetDuty = true;
        _mistakesCount = 0;        
        _isInit = false;
    }   

    public void IncreaseMistakes()
    {
        _mistakesCount++;
    }

    public void CopyValues(StreetDutyData dutyData)
    {
        var names = dutyData.ScannedPedestriansNames;
        var arrestedOrShockedNames = dutyData.ArrestedOrShockedPedestrianNames;
        var mistakesCount = dutyData.MistakesCount;
        var isInit = !(names == null);
        SetValues(names, arrestedOrShockedNames, mistakesCount, isInit, dutyData.HasFinishedStreetDuty);
    }

    public void SetValues(List<string> names, List<string> arrestedOrShockedNames, int mistakesCount, bool isInit, bool hasFinishedStreetDuty)
    {
        if (names != null)
        {
            int nameCount = names.Count;
            _scannedPedestriansNames = new List<string>(nameCount);
            _scannedPedestriansNames.AddRange(names);
        }
        else
        {
            _scannedPedestriansNames = null;
        }

        if (arrestedOrShockedNames != null)
        {
            int nameCount = arrestedOrShockedNames.Count;
            _arrestedOrShockedPedestrianNames = new List<string>(nameCount);
            _arrestedOrShockedPedestrianNames.AddRange(arrestedOrShockedNames);
        }
        else
        {
            _arrestedOrShockedPedestrianNames = null;
        }
        
        _mistakesCount = mistakesCount;
        _isInit = isInit;
        HasFinishedStreetDuty = hasFinishedStreetDuty;
    }
}