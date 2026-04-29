using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class OrdersDisplayController : MonoBehaviour
{
    public Action AllPeopleScanned;

    [SerializeField] private Text _notGuiltyCountText, _arrestCountText, _shockCountText;    

    private Dictionary<CrimeType, int> crimeCounts;

    private int _overallCount;

    public void Init(int notGuiltyCount, int arrestCount, int shockCount)
    {
        _overallCount = notGuiltyCount + arrestCount + shockCount;

        crimeCounts = new Dictionary<CrimeType, int>()
        {
            [CrimeType.NotGuilty] = notGuiltyCount,
            [CrimeType.ArrestWorthy] = arrestCount,
            [CrimeType.ShockWorthy] = shockCount
        };

        UpdateCountDisplays();
    }

    public void Decrease(CrimeType crimeType)
    {
        crimeCounts[crimeType]--;
        _overallCount--;
        UpdateCountDisplays();

        if (_overallCount <= 0)
        {
            AllPeopleScanned?.Invoke();
        }
    }

    private void UpdateCountDisplays()
    {
        _notGuiltyCountText.text = crimeCounts[CrimeType.NotGuilty].ToString();
        _arrestCountText.text = crimeCounts[CrimeType.ArrestWorthy].ToString();
        _shockCountText.text = crimeCounts[CrimeType.ShockWorthy].ToString();
    }
}