using UnityEngine;

public class RoadDutySceneLoadController : SceneLoadController
{
    [SerializeField] private RoadSceneGameData _roadDutyData;

    public override void Next()
    {
        int day = _roadDutyData.DayNumber;
        
        _sceneLoader.LoadStreetDutySceneByDay(day);
    }

    public override void Reload()
    {
        _sceneLoader.Reload();
    }
}