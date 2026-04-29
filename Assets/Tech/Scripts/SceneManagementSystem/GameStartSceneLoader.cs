using UnityEngine;

public class GameStartSceneLoader : MonoBehaviour
{
    [SerializeField] private SceneDBSO _streetDutyScenes, _roadDutyScenes;
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private InitSaveLoadHandler _saveLoadHandler;
    [SerializeField] private StreetDutyData _streetDutyData;
    [SerializeField] private RoadSceneGameData _roadDutyData;
    [SerializeField] private TutorialGameData _tutorialData;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    private void Start()
    {
        _saveLoadHandler.LoadData();        

        int day = _roadDutyData.DayNumber;
        bool isStreetDutyFinished = _streetDutyData.HasFinishedStreetDuty;
        
        if (_tutorialData.HasFinishedRoadDutyTutorial)
        {
            if (isStreetDutyFinished)
            {
                _sceneLoader.LoadRoadDutySceneByDay(day);
                return;
            }
            else
            {
                _sceneLoader.LoadStreetDutySceneByDay(day);
                return;
            }
        }
        else
        {
            if (_tutorialData.HasFinishedStreetDutyTutorial)
            {
                _sceneLoader.LoadRoadDutyTutorial();
                return;
            }
            else
            {
                _sceneLoader.LoadStreetDutyTutorial();
                return;
            }
        }                    
    }
}