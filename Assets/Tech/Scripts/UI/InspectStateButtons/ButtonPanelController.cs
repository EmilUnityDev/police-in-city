using UnityEngine;

public class ButtonPanelController : MonoBehaviour
{
    [SerializeField] private PoliceCar _policeCar;       
  
    public void OnLetGoButtonClicked() 
    {
        _policeCar.LetGo();
    }

    public void OnIssueFineButtonClicked()
    {
        _policeCar.IssueFine();
    }

    public void OnArrestButtonClicked()
    {
        _policeCar.Arrest();
    }
}