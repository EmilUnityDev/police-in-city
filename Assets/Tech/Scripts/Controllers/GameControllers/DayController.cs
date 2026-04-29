using UnityEngine;
using System;

public class DayController : MonoBehaviour
{
    public static event Action DayFinished;

    [SerializeField] private RoadSceneGameData _gameData;    
    [SerializeField] private SceneLoadController _sceneLoadController;
    [SerializeField] private WinLosePanel _winLosePanel;
    [SerializeField] private CorrectIncorrectCanvas _correctIncorrectCanvas;
    [SerializeField] private AbstractSaveLoadHandler _saveLoadHandler;
    [SerializeField] private DayCounter _dayCounter;

    private void Awake()
    {
        if (!_gameData.IsRoadSceneReloaded)
        {
            int levelId = _gameData.DayNumber * 2;
        }

        UpdateCounter(false);
    }

    private void Start()
    {
        _saveLoadHandler?.SaveDataToJSON();
    }

    public void OnOrderCompleted()
    {
        _gameData.CompletedOrdersCount++;
        UpdateCounter();
        if (_gameData.CompletedOrdersCount == _gameData.OverallOrdersCount)
        {
            int levelId = _gameData.DayNumber;

            _gameData.IsRoadSceneReloaded = false;
            _gameData.DayNumber++;
            _gameData.CompletedOrdersCount = 0;
            _correctIncorrectCanvas.ClosePanels();
            DayFinished?.Invoke();
            _saveLoadHandler.SaveDataToJSON();
            _winLosePanel.OpenWinPanel();       
            
            return;
        }        
        _saveLoadHandler.SaveDataToJSON();
        _gameData.IsRoadSceneReloaded = true;
        _sceneLoadController.Reload();        
    }

    public void OnOrderFailed()
    {
        _gameData.IsRoadSceneReloaded = true;
        int levelId = _gameData.DayNumber;
        _saveLoadHandler.SaveDataToJSON();
        _sceneLoadController.Reload();        
    }

    private void UpdateCounter(bool animate = true)
    {
        int count = _gameData.CompletedOrdersCount;
        int overall = _gameData.OverallOrdersCount;
        int day = _gameData.DayNumber;

        _dayCounter.UpdateCounter(count, overall, day, animate);
    } 
}