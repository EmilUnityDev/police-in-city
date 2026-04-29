using UnityEngine;

public class DriverAnimationController
{
    private Animator _animator;
    
    private string _sitInCarAnimName, _drivingAnimName, _arrestAnimName;

    public DriverAnimationController(Animator animator)
    {
        _animator = animator;
        _sitInCarAnimName = "SitInCar";
        _drivingAnimName = "Driving";
        _arrestAnimName = "Arrest";
    }

    public void SetLookValue(float value)
    {

    }

    public void SetSitting()
    {
        _animator.Play(_sitInCarAnimName);
    }

    public void SetDriving()
    {
        _animator.Play(_drivingAnimName);
    }

    public void SetArrest()
    {
        _animator.applyRootMotion = true;
        _animator.Play(_arrestAnimName);
    }
}