using UnityEngine;
using Cinemachine;

public class DollyTrackCameraController : MonoBehaviour, ILookAtEnemy
{
    public Transform Target { get; private set; }

    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private Transform _target;

    private CinemachineTrackedDolly _dollyCamera;
    private CinemachineSmoothPath _path;

    private bool _isInit;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        if (_isInit) return;
        _dollyCamera = _virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
        _path = GetComponent<CinemachineSmoothPath>();
        _isInit = true;
    }

    public void SetPathValue(float value)
    {
        if (!_isInit) Init();
        _dollyCamera.m_PathPosition = 2 - (value * 2);
    }

    public void SetTarget(Transform target)
    {
        if (!_isInit) Init();

        Target = target;
        _virtualCamera.m_LookAt = target;        
    }
    public void Bind()
    {
        if (!_isInit) Init();

        _dollyCamera.m_Path = _path;
        SetTarget(_target);

        _dollyCamera.m_PathPosition = 1;
    }

    public void SetPlace(Vector3 pos, Quaternion rot)
    {
        if (!_isInit) Init();

        transform.position = pos;
        transform.rotation = rot;
    }

    public void Detach()
    {
        transform.SetParent(null);
    }
}