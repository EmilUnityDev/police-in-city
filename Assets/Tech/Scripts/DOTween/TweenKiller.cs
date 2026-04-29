using UnityEngine;
using DG.Tweening;

public class TweenKiller : MonoBehaviour
{
    private DOTweenAnimation[] _tweenAnimations;
    private bool _isInit;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        if (_isInit) return;
        _tweenAnimations = GetComponents<DOTweenAnimation>();
        _isInit = true;
    }

    private void OnDestroy()
    {
        if (!_isInit) Init();
        if (_tweenAnimations == null) return;

        foreach (var t in _tweenAnimations)
        {
            t.DOKill();
        }
    }
}