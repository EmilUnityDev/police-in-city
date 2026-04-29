using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodCallCreosCanvas : GoodCallCanvasController
{
    private float _openTime;
    private float _duration = 2f;
    private bool _isClicked;

    private void OnEnable()
    {        
        _openTime = Time.time;
    }

    private void OnDisable()
    {

    }

    private void Update()
    {
        if (!_isClicked && _openTime + _duration < Time.time)
        {
            SetState();
            _isClicked = true;
        }
    }

    public override void SetState()
    {
        _from = _policeCar.CurrentState;
        _to = _policeCar.TransitionToInspectCameraState;
        _policeCar.TrySetState(_from, _to);
    }

    public override void Disappear()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
