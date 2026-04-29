using UnityEngine;

public class StreetPatrolGameController : MonoBehaviour
{
    [SerializeField] private StreetPatrolOrderController _orderController;
    [SerializeField] int _notGuiltyCount, _arrestCount, _shockCount;
    [SerializeField] private AbstractSaveLoadHandler _saveLoadHandler;
    [SerializeField] private OrdersDisplayController _orderDisplayController;
    [SerializeField] private MistakesDisplayController _mistakesDisplayController;
    [SerializeField] private RoadSceneGameData _roadDutyData;

    private int _completedCount;
    private int _dayNumber;    

    private void Start()
    {
        _saveLoadHandler.LoadData();
        _orderController.Init(_notGuiltyCount, _arrestCount, _shockCount, _dayNumber, _completedCount);
        _orderController.AllPeopleScanned += OnAllPeopleScanned;
        _mistakesDisplayController.MaxMistakesMade += OnMaxMistakesMade;
        _saveLoadHandler.SaveDataToJSON();
    }

    private void OnDestroy()
    {
        _orderController.AllPeopleScanned -= OnAllPeopleScanned;
        _mistakesDisplayController.MaxMistakesMade -= OnMaxMistakesMade;
    }

    public void SetCompletedCount(int count)
    {
        _completedCount = count;
    }

    public void SetDay(int day)
    {
        _dayNumber = day;
        int levelId = _dayNumber; 
    }

    public void OnAllPeopleScanned()
    {
        int levelId = _dayNumber;
        _roadDutyData.DayNumber++;
        _saveLoadHandler.ClearData();
        _saveLoadHandler.SaveDataToJSON();
    }

    public void OnMaxMistakesMade()
    {
        int levelId = _dayNumber;
        _saveLoadHandler.ResetData();
        _saveLoadHandler.SaveDataToJSON();
    }    
}