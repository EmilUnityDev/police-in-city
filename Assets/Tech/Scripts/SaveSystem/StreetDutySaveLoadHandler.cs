using UnityEngine;

public class StreetDutySaveLoadHandler : AbstractSaveLoadHandler
{
    [SerializeField] private StreetPatrolGameController _gameController;
    [SerializeField] private StreetPatrolOrderController _orderController;

    private Pedestrian[] _pedestrians;
    private StreetDutyData _dataAtTheStart;

    public override void LoadData()
    {
        _streetDutyData.HasFinishedStreetDuty = false;
        int mistakesCount = _streetDutyData.MistakesCount;
        var names = _streetDutyData.ScannedPedestriansNames;
        int completedCount = names == null ? 0 : names.Count;
        _gameController.SetCompletedCount(completedCount);
        int dayNumber = _roadDutyData.DayNumber;
        _gameController.SetDay(dayNumber);
        _orderController.MistakesCountIncreased += OnMistakesIncreased;

        _dataAtTheStart = ScriptableObject.CreateInstance<StreetDutyData>();
        _dataAtTheStart.CopyValues(_streetDutyData);
        
        _pedestrians = FindObjectsOfType<Pedestrian>();

        if (_pedestrians == null || _pedestrians.Length == 0) return;        

        if (names == null || names.Count == 0) return;
                
        int pedestriansCount = _pedestrians.Length;
        var arrestedOrShockedNames = _streetDutyData.ArrestedOrShockedPedestrianNames;
        for (int i = 0; i < pedestriansCount; i++)
        {
            Pedestrian p = _pedestrians[i];
            string name = p.name;
            if (names.Contains(name)) p.HasBeenDetained = true;
            if (arrestedOrShockedNames.Contains(name)) p.HasBeenArrestedOrShocked = true;
        }

        //_orderController.OrderControllerInitialized += OnOrderControllerInitialized;
    }

    public override void ResetData()
    {
        _streetDutyData.CopyValues(_dataAtTheStart);        
    }

    public override void ClearData()
    {
        _streetDutyData.ClearData();        
    }

    public override void SaveDataToJSON()
    {
        base.SaveDataToJSON();
    }

    public override void OnApplicationFocus(bool focus)
    {
        base.OnApplicationFocus(focus);
    }

    public override void OnApplicationPause(bool pause)
    {        
        base.OnApplicationPause(pause);
    }

    private void OnOrderControllerInitialized()
    {
        //if (_pedestrians == null) return;

        //int pedestriansCount = _pedestrians.Length;

        //for (int i = 0; i < pedestriansCount; i++)
        //{
        //    Pedestrian p = _pedestrians[i];
            
        //    if (p.HasBeenDetained)
        //    {
        //        _orderController.DecreaseScannedPeople(p.PedestrianData.CrimeType);
        //    }
        //}
    }

    private void OnMistakesIncreased()
    {
        _streetDutyData.IncreaseMistakes();
    }
}