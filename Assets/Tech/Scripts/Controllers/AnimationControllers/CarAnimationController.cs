using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CarAnimationController : MonoBehaviour
{
    [SerializeField] private Transform[] _wheels;
    [SerializeField] private Transform _car;

    private List<Tween> _rotateWheelsTweens;

    private void OnDestroy()
    {
        StopMoving();
    }

    public void StartMoving()
    {
        if (_wheels == null) return;
        int N = _wheels.Length;

        if (_rotateWheelsTweens != null) StopMoving();

        _rotateWheelsTweens = new List<Tween>();
        for (int i = 0; i < N; i++)
        {
            Tween t = _wheels[i].DOLocalRotate(new Vector3(360, 0, 0), 1, RotateMode.LocalAxisAdd).SetLoops(-1).SetEase(Ease.Linear);
            _rotateWheelsTweens.Add(t);
        }

        if (_car != null)
        {
            Tween carMoveTween = _car.DOLocalMoveY(5, 0.5f).SetLoops(-1, LoopType.Yoyo);
            _rotateWheelsTweens.Add(carMoveTween);
        }
    }

    public void StopMoving()
    {
        if (_rotateWheelsTweens == null) return;
        int N = _rotateWheelsTweens.Count;
        for (int i = 0; i < N; i++)
        {
            _rotateWheelsTweens[i].Kill();
        }

        _rotateWheelsTweens = null;
    }
}