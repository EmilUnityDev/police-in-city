using UnityEngine;
using DG.Tweening;

public class DOTweenUICard : MonoBehaviour
{
    private RectTransform _rectTransform;
    private float _initX;
    private float _offset;
    private float _duration;
    private bool _isInit;

    private void Awake()
    {
        Init();
    }

    public void MoveIn()
    {
        Move(_initX);
    }

    public void MoveOut()
    {
        Move(_initX + _offset);
    }

    private void Move(float endValue)
    {
        if (!_isInit) Init();
        _rectTransform.DOAnchorPosX(endValue, _duration).SetAutoKill();
    }

    private void Init()
    {
        if (_isInit) return;
        _rectTransform = GetComponent<RectTransform>();
        _initX = _rectTransform.anchoredPosition.x;
        _offset = 500;
        _duration = 1;
        _isInit = true;
    }
}