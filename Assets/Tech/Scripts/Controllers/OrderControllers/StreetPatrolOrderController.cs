using UnityEngine;
using System;

public class StreetPatrolOrderController : AbstractOrderController
{
    public event Action OrderControllerInitialized;
    public event Action MistakesCountIncreased;
    public event Action AllPeopleScanned;

    [SerializeField] private AbstractPedestrianFactory _pedestrianFactory;
    [SerializeField] private WinLosePanel _winLosePanel;
    [SerializeField] private DayCounter _dayCounter;

    private int _completedCount, _overallCount, _dayNumber;

    public void Init(int notGuiltyCount, int arrestCount, int shockCount, int day, int completedCount = 0)
    {
        _pedestrianFactory.GeneratePedestrians(notGuiltyCount, arrestCount, shockCount);
        _completedCount = completedCount;
        _overallCount = notGuiltyCount + arrestCount + shockCount;
        _dayNumber = day;
        UpdateDayCounter(false);
        OrderControllerInitialized?.Invoke();
    }

    public override void CheckOrder(IOrderData orderData)
    {
        //if (!orderData.IsOrderCorrect())
        //{
        //    _mistakesDisplayController.IncreaseMistakesCount();
        //    MistakesCountIncreased?.Invoke();
        //}
    }

    public void DecreaseScannedPeople(CrimeType crimeType)
    {
        _completedCount++;
        UpdateDayCounter();
    }    

    public void CheckLastOrder()
    {
        if (_completedCount >= _overallCount)
        {
            OnAllPeopleScanned();
        }
    }

    public void EnablePanels()
    {
        _dayCounter.gameObject.SetActive(true);

    }

    public void DisablePanels()
    {
        _dayCounter.gameObject.SetActive(false);
    }

    private void OnAllPeopleScanned()
    {
        AllPeopleScanned?.Invoke();
        _winLosePanel.OpenWinPanel();        
    }

    private void OnMaxMistakesMade()
    {
        _winLosePanel.OpenLosePanel();       
    }

    private void UpdateDayCounter(bool animate = true)
    {
        _dayCounter.UpdateCounter(_completedCount, _overallCount, _dayNumber, animate);
    }
}