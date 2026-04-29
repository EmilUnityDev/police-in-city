using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class TrunkController : MonoBehaviour, IPointerClickHandler
{
    public bool IsEnabled { private get; set; }

    public event System.Action TrunkClicked;

    [SerializeField] private Transform _trunkLid;
    [SerializeField] private Vector3 _targetEuler;
    
    private float _openSpeed;
    private Vector3 _initEuler;

    private bool _isInit;
    private bool _isOpen;
    private bool _isMoving;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isMoving && !IsEnabled) return;

        if (_isOpen) Close();
        else Open();

        TrunkClicked?.Invoke();
    }

    public void Open()
    {
        if (!_isInit) Init();
        _isMoving = true;
        _trunkLid.DOLocalRotate(_targetEuler, _openSpeed).SetAutoKill().OnComplete(() => _isMoving = false);
        _isOpen = true;
    }

    public void Close()
    {
        if (!_isInit) Init();
        _isMoving = true;

        _trunkLid.DOLocalRotateQuaternion(Quaternion.identity, _openSpeed).SetAutoKill().OnComplete(() => _isMoving = false);
        
        _isOpen = false;
    }

    private void Init()
    {
        if (_isInit) return;

        _initEuler = _trunkLid.localEulerAngles;
        _openSpeed = 1f;

        _isInit = true;
    }
}