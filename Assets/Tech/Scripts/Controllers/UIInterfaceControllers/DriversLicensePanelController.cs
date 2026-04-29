using UnityEngine;

public class DriversLicensePanelController : MonoBehaviour
{
    [SerializeField] AbstractDriversDataProcessorController[] _dataProcessors;

    public void ProcessDriverData(DriverData driverData)
    {
        if (_dataProcessors == null) return;
        foreach (var dataProcessor in _dataProcessors)
        {
            dataProcessor.ProcessData(driverData);
        }
    }
}