using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LoadingScreen : MonoBehaviour
{
    public float OpenDuration
    {
        get => _openDuration;
    }

    [SerializeField] private Image _blackScreen, _loadingBar;
    [SerializeField] private GameObject _text;
    [SerializeField] private float _openDuration = 1;

    private void Awake()
    {
        if (_openDuration > 0)
        {
            _loadingBar.fillAmount = 1;
            _loadingBar.gameObject.SetActive(false);
            _text.gameObject.SetActive(false);
            _blackScreen.raycastTarget = false;

            _blackScreen.DOFade(0, _openDuration).SetEase(Ease.Linear).OnComplete(() =>
            {
                _blackScreen.gameObject.SetActive(false);
            }).SetAutoKill();

            return;
        }

        _loadingBar.fillAmount = 0;
    }

    public void OpenLoadingScreen()
    {
        if (_openDuration <=0)
        {
            _loadingBar.fillAmount = 0;

            _blackScreen.gameObject.SetActive(true);
            Color c = _blackScreen.color;
            Color newColor = new Color(c.r, c.g, c.b, 1);
            _blackScreen.color = newColor;
            return;
        }
        _blackScreen.gameObject.SetActive(true);
        _blackScreen.raycastTarget = true;
        _blackScreen.DOFade(1, _openDuration).OnComplete(() =>
        {
            _loadingBar.gameObject.SetActive(true);
            _loadingBar.fillAmount = 0;
            _text.gameObject.SetActive(true);

        }).SetAutoKill();
    }

    public void SetLoadingProgress(float value)
    {
        _loadingBar.fillAmount = value;
    }
}