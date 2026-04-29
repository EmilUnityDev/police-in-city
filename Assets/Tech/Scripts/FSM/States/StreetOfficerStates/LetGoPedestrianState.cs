public class LetGoPedestrianState : IState
{    
    public bool CanTransition { get; private set; }

    private StreetOfficer _officer;
    private StreetPatrolOrderController _orderController;
    private ScannerController _scannerController;
    private Pedestrian _pedestrian;

    public LetGoPedestrianState(StreetOfficer officer, StreetPatrolOrderController orderController, ScannerController scannerController)
    {
        _officer = officer;
        _orderController = orderController;
        _scannerController = scannerController;
    }

    public void Enter()
    {
        _pedestrian = _officer.DetainedPedestrian;
        var orderData = new StreetPatrolOrderData(CrimeType.NotGuilty, _pedestrian);
        _orderController.EnablePanels();
        _orderController.CheckOrder(orderData);
        _orderController.DecreaseScannedPeople(_pedestrian.PedestrianData.CrimeType);
        _pedestrian.LetGo();
        _scannerController.RemoveCurrent();
        _scannerController.EnableScan();
        CanTransition = true;
    }

    public void Exit()
    {
        _orderController.CheckLastOrder();
    }

    public void FixedTick()
    {
        
    }

    public void Tick()
    {
        
    }
}