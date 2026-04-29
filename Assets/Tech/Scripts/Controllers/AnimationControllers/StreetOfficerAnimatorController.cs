using UnityEngine;
using CnControls;

public class StreetOfficerAnimatorController 
{    
    private Animator _animator;
    private string _speedParameter = "speed";
    private string _shockAnimName = "Shock";
    private string _idleAnimName = "IdleHumanoid";

    public StreetOfficerAnimatorController(Animator animator)
    {        
        _animator = animator;
    }

    public void SetAnimatorSpeed()
    {
        Vector3 moveDirection = new Vector3(CnInputManager.GetAxis("Horizontal"), 0, CnInputManager.GetAxis("Vertical"));
        float speed = moveDirection.normalized.sqrMagnitude;
        _animator.SetFloat(_speedParameter, speed);
    }

    public void SetLayerWeight(int layerIndex, float weight)
    {
        _animator.SetLayerWeight(layerIndex, weight);
    }

    public void SetShockState()
    {
        SetState(_shockAnimName);
    }

    public void SetDefaultState()
    {
        SetState(_idleAnimName);
    }

    private void SetState(string name)
    {
        _animator.Play(name);
    }
}