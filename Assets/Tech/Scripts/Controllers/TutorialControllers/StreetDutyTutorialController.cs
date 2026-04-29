using UnityEngine;
using CnControls;

public class StreetDutyTutorialController : MonoBehaviour
{    
    [SerializeField] private StreetPatrolOrderController _orderController;
    [SerializeField] private TutorialSaveLoadHandler _tutorialSaveLoadHandler;
    [SerializeField] private GameObject _tutorialDragToMovePanel, _tutorialArrow;
    [SerializeField] private TutorialGameData _tutorialData;
    [SerializeField] private ScannerController _scannerController;

    private bool _isPanelClosed;

    private void Awake()
    {
        _scannerController.gameObject.SetActive(false);

        if (_tutorialData.HasTouchedScreenOnStreetDuty)
        { 
            CloseAndFinishDragTutorial(); 
        }

        _orderController.AllPeopleScanned += OnAllPeopleScanned;
    }

    private void OnDestroy()
    {        
        _orderController.AllPeopleScanned -= OnAllPeopleScanned;
    }

    private void Update()
    {
        if (_isPanelClosed) return;

        Vector3 moveDirection = new Vector3(CnInputManager.GetAxis("Horizontal"), 0, CnInputManager.GetAxis("Vertical"));
        if (moveDirection.sqrMagnitude > 0) CloseAndFinishDragTutorial();
    }

    private void CloseAndFinishDragTutorial()
    {
        _tutorialDragToMovePanel?.SetActive(false);
        _tutorialArrow?.SetActive(true);
        _scannerController.gameObject.SetActive(true);
        _tutorialData.HasTouchedScreenOnStreetDuty = true;
        _isPanelClosed = true;
    }

    private void OnAllPeopleScanned()
    {
        _tutorialData.HasFinishedStreetDutyTutorial = true;
    }
}