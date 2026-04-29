using UnityEngine;

public class ArrestPedestrianState : IState
{
    public bool CanTransition
    {
        get => _startTime + _duration < Time.time;
    }

    private Pedestrian _pedestrian;
    private Transform _streetOfficerTransform;
    private Transform _modelTransform, _followTarget;
    private ScannerController _scannerController;
    private StreetOfficerAnimatorController _animController;
    private StreetPatrolOrderController _orderController;
    private StreetOfficer _officer;
    private Swat _swat;

    private float _startTime;
    private float _duration;
    public ArrestPedestrianState(ScannerController scannerController, StreetOfficerAnimatorController animController, StreetPatrolOrderController orderController, Transform streetOfficerTransform, Transform modelTransform, Transform followTarget, StreetOfficer officer, Swat swat)
    {
        _scannerController = scannerController;
        _animController = animController;
        _orderController = orderController;
        _streetOfficerTransform = streetOfficerTransform;

        _duration = 1;
        _modelTransform = modelTransform;
        _followTarget = followTarget;
        _officer = officer;
        _swat = swat;
    }

    public void Enter()
    {
        _startTime = Time.time;
        _pedestrian = _officer.DetainedPedestrian;
        var orderData = new StreetPatrolOrderData(CrimeType.ArrestWorthy, _pedestrian);
        _orderController.EnablePanels();
        _orderController.CheckOrder(orderData);
        _orderController.DecreaseScannedPeople(_pedestrian.PedestrianData.CrimeType);

        Vector3 pedPos = _pedestrian.transform.position;
        Vector3 officerPos = _streetOfficerTransform.position;
        Vector3 officerLookVector = new Vector3(pedPos.x, officerPos.y, pedPos.z);
        Vector3 pedestrianLookVector = new Vector3(officerPos.x, pedPos.y, officerPos.z);

        _modelTransform.LookAt(officerLookVector);
        _followTarget.rotation = _modelTransform.rotation;
        _pedestrian.transform.LookAt(pedestrianLookVector);

        _animController.SetLayerWeight(1, 0);
        _animController.SetShockState();
        _modelTransform.Rotate(Vector3.up, 30);


        _pedestrian.Arrest();
        _swat.Call(_pedestrian);
    }

    public void Exit()
    {
        _orderController.CheckLastOrder();
        _animController.SetDefaultState();
        _scannerController.RemoveCurrent();
        
        _scannerController.EnableScan();
    }

    public void FixedTick()
    {
        
    }

    public void Tick()
    {
        
    }
}