using UnityEngine;

public class WinLosePanel : MonoBehaviour
{    
    [SerializeField] private GameObject _winPanel, _losePanel;
    [SerializeField] private SceneLoadController _sceneLoadController;

    public void OpenWinPanel()
    {
        _winPanel.SetActive(true);
        _losePanel.SetActive(false);
    }

    public void OpenLosePanel()
    {
        _winPanel.SetActive(false);
        _losePanel.SetActive(true);
    }

    public void OnNextButtonClicked()
    {
        _sceneLoadController?.Next();
    }

    public void OnTryAgainButtonClicked()
    {
        _sceneLoadController?.Reload();
    }    
}