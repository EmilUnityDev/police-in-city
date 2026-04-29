using UnityEngine;
using CnControls;

public class RoadDutyTutorialController : MonoBehaviour
{
    [SerializeField] private RoadSceneGameData _roadDutyData;
    [SerializeField] private GameObject _tutorialCarFactory, _realCarFactory, _tutorialPersonFactory, _realPersonFactory, _tutorialDragToMovePanel;
    [SerializeField] private DetainButtonController _detainButtonController;
    [SerializeField] private TutorialGameData _tutorialData;
    [SerializeField] private InspectCanvasTutorialController _inspectCanvasTutorialController;
    [SerializeField] private GameObject _policeScanner;

    private bool _isPanelClosed;    

    private void Awake()
    {
        int ordersCompleted = _roadDutyData.CompletedOrdersCount;
        if (ordersCompleted > 0)
        {
            StartRegularGame();
        }
        else
        {
            StartTutorialGame();
        }

        if (_tutorialData.HasTouchedScreenOnRoadDuty) ClosePanel();

        DayController.DayFinished += OnDayFinished;
    }

    private void OnDestroy()
    {
        DayController.DayFinished -= OnDayFinished;
        Car.CarScanned -= OnCarScanned;
    }

    private void Update()
    {
        if (_isPanelClosed) return;

        Vector3 moveDirection = new Vector3(CnInputManager.GetAxis("Horizontal"), 0, CnInputManager.GetAxis("Vertical"));
        if (moveDirection.sqrMagnitude > 0) ClosePanel();
    }

    private void ClosePanel()
    {
        _tutorialDragToMovePanel?.SetActive(false);
        
        _tutorialData.HasTouchedScreenOnRoadDuty = true;
        _isPanelClosed = true;
        _policeScanner.SetActive(true);
    }

    private void StartTutorialGame()
    {
        _policeScanner.SetActive(false);
        _tutorialPersonFactory.SetActive(true);
        _tutorialCarFactory.SetActive(true);
        _detainButtonController.Init();
        _inspectCanvasTutorialController.IsEnabled = true;
        Car.CarScanned += OnCarScanned;
    }

    private void StartRegularGame()
    {
        _realPersonFactory.SetActive(true);
        _realCarFactory.SetActive(true);        
    }

    private void OnCarScanned(Car car)
    {
        if (car.CarData.IsTooFast)
        {
            _detainButtonController.gameObject.SetActive(true);
            _inspectCanvasTutorialController.TrunkController = car.TrunkController;
            Car.CarScanned -= OnCarScanned;
        }
    }

    private void OnDayFinished()
    {
        _tutorialData.HasFinishedRoadDutyTutorial = true;
        DayController.DayFinished -= OnDayFinished;
    }
}