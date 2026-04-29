using System.Collections.Generic;
using UnityEngine;

public class TutorialArrowController : MonoBehaviour
{
    [SerializeField] private GameObject _tutorialArrow;    
    [SerializeField] private ScannerController _scanner;
    [SerializeField] private AbstractPedestrianFactory _pedestriansFactory;

    private Pedestrian _currentPedestrian;

    private bool _isEnabled = true;
    private bool _noPedestriansLeft;

    private void Update()
    {
        if (_noPedestriansLeft) return;
        UpdateRotation();
        CheckForScannerEnabled();
        CheckForNextPedestrian();
    }

    private void UpdateRotation()
    {
        if (_currentPedestrian == null) return;
        Vector3 pedPos = _currentPedestrian.transform.position;
        pedPos.y = transform.position.y;
        transform.LookAt(pedPos);
    }

    private void CheckForScannerEnabled()
    {
        if (_scanner == null) return;
        EnableArrow(_scanner.Enabled && _scanner.CurrentPedestrian == null);
    }

    private void EnableArrow(bool value)
    {
        if (_isEnabled == value) return;

        _isEnabled = !_isEnabled;
        _tutorialArrow.SetActive(_isEnabled);
    }

    private void CheckForNextPedestrian()
    {
        if (!_pedestriansFactory.IsInit) return;
        _currentPedestrian = GetClosestPedestrian();
        if (_currentPedestrian == null || _currentPedestrian.HasBeenDetained || !_currentPedestrian.gameObject.activeInHierarchy)
        {
            if (_currentPedestrian != null) return;
            _noPedestriansLeft = true;
            EnableArrow(false);
        }
    }

    private Pedestrian GetClosestPedestrian()
    {
        List<Pedestrian> activePedestrians = _pedestriansFactory.GetActivePedestrians();
        if (activePedestrians.Count <= 0) return null;

        int N = activePedestrians.Count;
        Pedestrian closestPedestrian = null;

        foreach (var p in activePedestrians)
        {
            if (!p.HasBeenDetained)
            {
                closestPedestrian = p;
                break;
            }
        }

        if (closestPedestrian == null) return null;

        float minDistance = GetDistance(closestPedestrian);
        for (int i = 0; i < N; i++)
        {            
            Pedestrian p = activePedestrians[i];
            if (p.HasBeenDetained) continue;
            float dist = GetDistance(p);
            if (dist < minDistance)
            {
                minDistance = dist;
                closestPedestrian = p;
            }
        }

        return closestPedestrian;
    }

    private float GetDistance(Pedestrian p)
    {
        return Vector3.Distance(transform.position, p.transform.position);
    }
}