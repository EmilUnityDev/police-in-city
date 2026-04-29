using UnityEngine;

public class SwatAnimationController : MonoBehaviour
{
    [SerializeField] private Swat _swat;

    private Animator _animator;
    private bool _isInit;
    private string _swatOutAnimName, _idleAnimName;

    private void Awake()
    {
        Init();
        _animator.Play(_idleAnimName);
    }

    public void SetSwatOut()
    {
        if (!_isInit) Init();
        transform.position = _swat.transform.position;
        _animator.Play(_swatOutAnimName);
    }

    public void TakePedestrian()
    {
        _swat?.TakePedestrian();
    }

    public void OnSwatOut()
    {
        _animator.Play(_idleAnimName);
        _swat?.Disappear();
        //transform.localPosition = _swat.transform.position;        
    }

    private void Init()
    {
        if (_isInit) return;
        _animator = GetComponent<Animator>();
        _swatOutAnimName = "SwatOut";
        _idleAnimName = "SwatIdle";
        _isInit = true;
    }
}