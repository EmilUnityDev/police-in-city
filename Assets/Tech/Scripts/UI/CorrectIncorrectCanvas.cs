using UnityEngine;

public class CorrectIncorrectCanvas : MonoBehaviour
{
    [SerializeField] private GameObject _correctPanel, _incorrectPanel;

    public void ClosePanels()
    {
        _correctPanel?.SetActive(false);
        _incorrectPanel?.SetActive(false);
    }

    public void OpenRespectivePanel(bool isOrderCorrect)
    {
        var panelToOpen = isOrderCorrect ? _correctPanel : _incorrectPanel;
        panelToOpen?.SetActive(true);
    }
}