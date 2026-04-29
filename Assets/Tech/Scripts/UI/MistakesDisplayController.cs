using UnityEngine;
using UnityEngine.UI;
using System;

public class MistakesDisplayController : MonoBehaviour
{
    public Action MaxMistakesMade;

    [SerializeField] private Image[] _healthCounts;
    private int _maxMistakesCount;
    private int _mistakesCount;
    public void SetMistakesCount(int count, int maxMistakesCount = 3)
    {
        _maxMistakesCount = maxMistakesCount;
        _mistakesCount = count;
        UpdateMistakesDisplay();
    }

    public void IncreaseMistakesCount()
    {
        SetMistakesCount(++_mistakesCount);
        if (_mistakesCount >= _maxMistakesCount)
        {
            MaxMistakesMade?.Invoke();
        }
    }

    private void UpdateMistakesDisplay()
    {
        for (int i = 0; i < _mistakesCount && i < _healthCounts.Length; i++)
        {
            _healthCounts[i].color = Color.red;
        }
    }
}