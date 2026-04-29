using UnityEngine;

public class PoliceCarBumpState : IState
{
    public bool IsTimeOver
    {
        get => _startTime + _duration < Time.time;
    }

    private Rigidbody _rb;
    
    private float _force;
    private float _startTime;
    private float _duration;

    public PoliceCarBumpState(Rigidbody rb)
    {
        _rb = rb;
        _force = 800;
        _duration = 1f;
    }

    public void Enter()
    {
        var dir = _rb.transform.forward * -1;
        _rb.AddForce(dir * _force * Time.fixedDeltaTime, ForceMode.Impulse);
        _rb.drag = 1;
        _startTime = Time.time;
    }

    public void Exit()
    {
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        _rb.isKinematic = true;
    }

    public void FixedTick()
    {
        
    }

    public void Tick()
    {
        
    }
}