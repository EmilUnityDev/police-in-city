using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public abstract class AbstractDriversDataProcessorController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] protected UnityEvent OnClicked;
    [SerializeField] protected DriversLicensePanelController _mainController;    

    public abstract void ProcessData(DriverData data);
    public abstract void CommitOnClickBehaviour();

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        CommitOnClickBehaviour();
    }
}