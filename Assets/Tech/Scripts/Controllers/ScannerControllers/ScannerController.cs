using System.Collections.Generic;
using UnityEngine;

public class ScannerController : MonoBehaviour
{
    public bool Enabled { get; set; }
    public Pedestrian CurrentPedestrian => _currentPedestrian;

    [SerializeField] private ScannerMeshController _scanMeshController;
    [SerializeField] private Transform _scannerTransform;
    [SerializeField] private AnimationCurve _curve;
    [SerializeField][Range(1, 3)] private float _speed = 1;

    private List<Pedestrian> _pedestriansNearby;
    private Pedestrian _currentPedestrian;

    private StreetOfficerAnimatorController _animatorController;

    private bool _isScanning;

    private float _scanProgress;

    private void Awake()
    {
        _pedestriansNearby = new List<Pedestrian>();
    }

    public void Init(StreetOfficerAnimatorController animatorController)
    {
        _animatorController = animatorController;
    }

    private void Check()
    {
        if (CanCheck())
        {
            _currentPedestrian = GetClosestPedestrian();

            
            _isScanning = true;

            _scanMeshController.GenerateScannerMesh(GetDistanceBetween(_currentPedestrian), _currentPedestrian.transform);
            _animatorController?.SetLayerWeight(1, 1);
            Time.timeScale = 0.5f;
        }
    }

    public void Scan()
    {
        if (!_isScanning) return;

        _scanProgress += Time.deltaTime * _speed;
        _scanProgress = _curve.Evaluate(_scanProgress);
        _scanMeshController.GenerateScannerMesh(GetDistanceBetween(_currentPedestrian), _currentPedestrian.transform);
        _currentPedestrian.Scan(_scanProgress);
    }

    public void DisableScan()
    {
        Enabled = false;
        _scanProgress = 0;
        _isScanning = false;
        _scanMeshController.DestroyScanner();
        _animatorController?.SetLayerWeight(1, 0);  
    }

    public void EnableScan()
    {
        Enabled = true;
    }

    public bool IsScanCompleted()
    {
        return _scanProgress >= 0.99999f;
    }

    public void RemoveCurrent()
    {
        _pedestriansNearby.Remove(_currentPedestrian);
        _currentPedestrian = null;
        _scanMeshController.DestroyScanner();        
    }

    private bool CanCheck()
    {
        return _pedestriansNearby != null && _pedestriansNearby.Count > 0 && Enabled;
    }

    private bool CanScan()
    {
        return _currentPedestrian != null;
    }

    private void OnTriggerEnter(Collider other)
    {
        Pedestrian pedestrian = other.GetComponentInParent<Pedestrian>();
        if (pedestrian == null || pedestrian.HasBeenDetained) return;

        if (!_pedestriansNearby.Contains(pedestrian))
        {
            _pedestriansNearby.Add(pedestrian);
        }

        Check();
    }

    private void OnTriggerExit(Collider other)
    {
        Pedestrian pedestrian = other.GetComponentInParent<Pedestrian>();
        if (pedestrian == null) return;

        if (pedestrian == _currentPedestrian)
        {
            _currentPedestrian.StopScan();
            _currentPedestrian = null;
            _isScanning = false;
            _scanMeshController.DestroyScanner();
            _scanProgress = 0;
            Time.timeScale = 1f;
            _animatorController?.SetLayerWeight(1, 0);
        }

        if (_pedestriansNearby.Contains(pedestrian))
        {
            _pedestriansNearby.Remove(pedestrian);
        }

        Check();
    }

    private Pedestrian GetClosestPedestrian()
    {
        int N = _pedestriansNearby.Count;
        int minIndex = 0;
        float minDist = GetDistanceBetween(_pedestriansNearby[minIndex]);
        for (int i = 1; i < N; i++)
        {
            float dist = GetDistanceBetween(_pedestriansNearby[i]);
            if (minDist > dist){
                minDist = dist;
                minIndex = i;
            }
        }

        return _pedestriansNearby[minIndex];
    }

    private float GetDistanceBetween(Pedestrian pedestrian)
    {
        return Vector3.Distance(transform.position, pedestrian.transform.position);
    }

    private float GetDot(Pedestrian pedestrian)
    {
        return Vector3.Dot(_scannerTransform.position, pedestrian.transform.forward.normalized);
    }

    private float GetAngle(Pedestrian pedestrian)
    {
        return Vector3.Angle(_scannerTransform.forward, pedestrian.transform.position);

    }
}