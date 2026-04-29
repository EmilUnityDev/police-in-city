using System.Collections.Generic;
using UnityEngine;

public class PedestrianBehaviourController : MonoBehaviour
{
    [SerializeField] private PedestrianBehaviour _pedestrianBehaviour;

    private IMovementController _movementController;
    private Dictionary<PedestrianBehaviour, IState> _scanStatesByBehaviour;
    private bool _isInit;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        if (_isInit) return;
        _isInit = true;
    }
}