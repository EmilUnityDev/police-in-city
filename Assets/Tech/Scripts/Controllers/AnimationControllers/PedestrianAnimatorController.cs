using UnityEngine;

public class PedestrianAnimatorController : MonoBehaviour
{
    private Animator _animator;
    private string _toTransitTrigger, _walkTrigger, _idleTrigger;
    private string _idleAnim, _walkAnim, _shockAnim, _arrestAnim, _sitAnim, _angryAnim, _fearAnim, _funnyAnim;

    private void Awake()
    {
        _animator = GetComponent<Animator>();        
        _toTransitTrigger = "ToTransit";
        _idleTrigger = "M_Idle";
        _walkTrigger = "M_WalkingSimple";

        _idleAnim = "Idle";
        bool isWalkingWithPhone = false;
        isWalkingWithPhone.RandomBool();

        _walkAnim = isWalkingWithPhone ? "WalkWithPhone" : "Walk";
        _sitAnim = "Sit";
        _shockAnim = "Shock";
        _arrestAnim = "PedestrianArrest";

        _angryAnim = "Angry_1";
        _fearAnim = "Fear_1";
        _funnyAnim = "Funny_1";
    }

    public void SetIdle()
    {
        _animator.Play(_idleAnim);
    }

    public void SetWalk()
    {
        _animator.Play(_walkAnim);
    }

    public void SetSitting()
    {
        _animator.Play(_sitAnim);
    }

    public void SetElectrocuted()
    {
        _animator.Play(_shockAnim);
    }

    public void SetArrest()
    {
        _animator.applyRootMotion = false;
        _animator.Play(_arrestAnim);
    }

    public void SetAngry()
    {
        _animator.Play(_angryAnim);
    }

    public void SetFear()
    {
        _animator.Play(_fearAnim);
    }

    public void SetFunny()
    {
        _animator.Play(_funnyAnim);
    }
}