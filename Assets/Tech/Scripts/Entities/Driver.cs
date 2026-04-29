using UnityEngine;

public class Driver : AbstractEntity
{
    public DriverData DriverData { get; set; }
    [SerializeField] private RuntimeAnimatorController _animatorController;

    private DriverAnimationController _animController;

    private void OnEnable()
    {
        _animController?.SetDriving();
    }

    public void Init(DriverData driverData)
    {
        DriverData = driverData;
        Animator animator = GetComponentInChildren<Animator>();
        animator.runtimeAnimatorController = _animatorController;
        animator.applyRootMotion = false;
        _animController = new DriverAnimationController(animator);        
    }

    public void Detain()
    {
        _animController.SetSitting();
    }

    public void LetGo()
    {
        _animController.SetDriving();
    }

    public void Arrest()
    {
        _animController.SetArrest();
    }
}