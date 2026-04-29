using UnityEngine;
using CnControls;

public class PoliceCarSkannerController : MonoBehaviour
{
    [SerializeField] private LayerMask _carLayer;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _minEulerY = 230, _maxEulerY = 300;

    private Vector3 _hitPoint;
    private LineRenderer _lineRenderer;
    private bool _isInit;

    private void Awake()
    {
        Init();
    }    

    private void Update()
    {
        if (!_isInit) Init();
        UpdateLineRenderer();
        UpdateScannerRotation();
    }

    private void FixedUpdate()
    {     
        if (Physics.Raycast(transform.position, transform.forward, out var hitInfo, 100, _carLayer))
        {
            _hitPoint = hitInfo.point;
            Car car = hitInfo.transform.GetComponent<Car>();
            car?.Scan();
        }
        else
        {
            _hitPoint = transform.position + transform.forward * 100;
        }
    }

    private void Init()
    {
        if (_isInit) return;
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.SetPosition(0, transform.position);
        _hitPoint = transform.position + transform.forward * 100;
        UpdateScannerRotation();
        UpdateLineRenderer();
        _isInit = true;
    }

    private void UpdateLineRenderer()
    {
        _lineRenderer.SetPosition(1, _hitPoint);
    }

    private void UpdateScannerRotation()
    {
        float x = CnInputManager.GetAxis("Horizontal");
        
        var angles = transform.localEulerAngles;
        var angle = angles.y;
        angle += (x * _rotationSpeed * Time.deltaTime);
        //Debug.Log(transform.localEulerAngles);
        angle = Mathf.Clamp(angle, _minEulerY, _maxEulerY);
        angles.y = angle;
        transform.localEulerAngles = angles;
    }
}