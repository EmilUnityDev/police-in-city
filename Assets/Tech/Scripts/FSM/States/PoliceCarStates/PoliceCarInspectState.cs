using UnityEngine;
using System;

public class PoliceCarInspectState : IState
{
    public static event Action<Car> CarInspected;

    private GameObject _sliderCanvas;
    private GameObject _joystickCanvas;
    private ILookAtEnemy _lookAtEnemy;
    private PoliceCarDetainController _detainController;
    private GoodCallCanvasController _goodCallController;
    private DriversLicensePanelController _driversLicensePanelController;
    private ButtonPanelController _buttonPanelController;
    private DayCounter _dayCounter;

    public PoliceCarInspectState(GameObject sliderCanvas, ILookAtEnemy lookAtEnemy, PoliceCarDetainController detainController, GoodCallCanvasController goodCallController, GameObject joystickCanvas, DriversLicensePanelController driversLicensePanelController, ButtonPanelController buttonPanelController, DayCounter dayCounter)
    {
        _sliderCanvas = sliderCanvas;
        _lookAtEnemy = lookAtEnemy;
        _detainController = detainController;
        _goodCallController = goodCallController;
        _joystickCanvas = joystickCanvas;
        _driversLicensePanelController = driversLicensePanelController;
        _buttonPanelController = buttonPanelController;
        _dayCounter = dayCounter;
    }

    public void Enter()
    {  
        _sliderCanvas.SetActive(true);
        _joystickCanvas.SetActive(false);
        
        
        Car car = _detainController.DetainedCar;
        car.DollyTrackCameraController.Bind();

        if (_goodCallController.gameObject.activeInHierarchy)
        {
            _goodCallController.Disappear();
        }
        _detainController.transform.position = new Vector3(-100, -110, 100);
        _driversLicensePanelController.ProcessDriverData(car.Driver.DriverData);

        if (!car.IsScanned)
        {
            CarData temp = car.CarData;
            car.CarData = new CarData(false, temp.IsArrestWorthy, temp.LicensePlateNumber, temp.Lane, temp.Speed);
        }

        Driver d = car.Driver;
        d.Detain();

        CarInspected?.Invoke(car);
    }

    public void Exit()
    {
        _sliderCanvas.SetActive(false);
    }

    public void FixedTick()
    {
        
    }

    public void Tick()
    {
        
    }
}