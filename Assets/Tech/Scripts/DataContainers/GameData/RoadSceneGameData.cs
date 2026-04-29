using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName ="RoadSceneGameData", menuName ="GameData/RoadSceneGameData")]
public class RoadSceneGameData : ScriptableObject
{
    [SerializeField] private int _dayNumber, _completedOrdersCount;
    [SerializeField] private int[] _ordersCount;

    public bool IsRoadSceneReloaded { get; set; }
    public int DayNumber 
    {
        get 
        {
            if (_dayNumber < 1)
                return 1;
            else return _dayNumber;
                
        }
        set => _dayNumber = value; 
    }

    public int OverallOrdersCount
    {
        get 
        {
            if (DayNumber < 3) return _ordersCount[0];
            else return _ordersCount[1];
        }   
    }

    public int CompletedOrdersCount { get => _completedOrdersCount; set => _completedOrdersCount = value; }

    public void SetValues(int dayNumber, int completedOrdersCount)
    {
        _dayNumber = dayNumber;
        _completedOrdersCount = completedOrdersCount;
    }
}