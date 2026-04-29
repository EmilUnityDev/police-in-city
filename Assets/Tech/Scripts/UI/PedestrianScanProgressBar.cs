using UnityEngine;
using UnityEngine.UI;

public class PedestrianScanProgressBar : MonoBehaviour
{
    [SerializeField] private Image _progressBar;

    private Camera _mainCam;
    private bool _isEnabled;

    private void Awake()
    {
        _mainCam = Camera.main;
    }

    private void Update()
    {
        _progressBar.transform.LookAt(_mainCam.transform);
    }

    public void EnableProgressBar()
    {
        _progressBar.gameObject.SetActive(true);
    }
    public void DisableProgressBar()
    {
        _progressBar.gameObject.SetActive(false);
    }

    public void DisplayProgress(float progress)
    {
        if (!_isEnabled) EnableProgressBar();

        _progressBar.fillAmount = progress;
    }
}