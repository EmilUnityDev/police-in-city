using UnityEngine;
using Cinemachine;

public class DetainPedestrianState : IState
{
    private Transform _modelTransform;
    private ScannerController _scannerController;
    private Pedestrian _pedestrian;
    private PedestrianDescriptionPanel _pedestrianDescriptionPanel;
    private StreetPatrolOrderController _orderController;
    private AbstractInteractionPanel _interactionPanel;

    private StreetOfficer _officer;

    private CinemachineVirtualCamera _cinemachineVirtualCamera;
    private float _startTime;
    private float _transitionTime = 0.46f;
    private bool _isUIOpen;

    public DetainPedestrianState(Transform modelTransform, ScannerController scannerController, PedestrianDescriptionPanel pedestrianDescriptionPanel, AbstractInteractionPanel interactionPanel, StreetPatrolOrderController orderController, CinemachineVirtualCamera cinemachineVirtualCamera, StreetOfficer officer)
    {
        _modelTransform = modelTransform;
        _scannerController = scannerController;

        _pedestrianDescriptionPanel = pedestrianDescriptionPanel;
        _interactionPanel = interactionPanel;
        _orderController = orderController;
        _cinemachineVirtualCamera = cinemachineVirtualCamera;
        _officer = officer;
    }

    public void Enter()
    {
        _startTime = Time.time;
        _isUIOpen = false;
        _pedestrian = _scannerController.CurrentPedestrian;
        _officer.DetainedPedestrian = _pedestrian;
        _scannerController.DisableScan();
        Time.timeScale = 1;
        _pedestrian.Detain(_modelTransform);

        _pedestrianDescriptionPanel.FeedPedestrianData(_pedestrian.PedestrianData);

        _orderController.DisablePanels();        

        _cinemachineVirtualCamera.m_Follow = _pedestrian.DetainCamTransform;
        _cinemachineVirtualCamera.m_LookAt = _pedestrian.DetainCamLookAt;

        _cinemachineVirtualCamera.gameObject.SetActive(true);

        _modelTransform.gameObject.SetActive(false);
    }

    private void OpenDetainUI()
    {
        _pedestrianDescriptionPanel.Activate();
        _interactionPanel.Activate();
    }

    public void Exit()
    {        
        _pedestrianDescriptionPanel.Deactivate();
        _interactionPanel.Deactivate();
        
    }

    public void FixedTick()
    {
        
    }

    public void Tick()
    {
        if (_isUIOpen) return;

        if (_startTime + _transitionTime < Time.time)
        {
            OpenDetainUI();
            _isUIOpen = true;
        }
    }
}