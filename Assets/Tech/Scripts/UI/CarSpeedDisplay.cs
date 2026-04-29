using UnityEngine;
using UnityEngine.UI;

public class CarSpeedDisplay : MonoBehaviour
{
    [SerializeField] private Image _fill;
    [SerializeField] private Image _bg;
    [SerializeField] private Text _speedText;
    [SerializeField] private float _disappearenceSpeed = 1;

    private bool _isInit;
    private float _lastTimeScanned;
    private float _currentAlpha;
    private Color _defaultColor;
    private float _timeOnScreen;

    private void Awake()
    {
        Init();
        SetAlpha(0);
    }

    private void Init()
    {
        if (_isInit) return;
        _defaultColor = _fill.color;
        _timeOnScreen = 1f;
        _isInit = true;
    }

    private void Update()
    {
        if (Time.time > _lastTimeScanned + _timeOnScreen && _currentAlpha > 0)
        {
            _currentAlpha -= (_disappearenceSpeed * Time.deltaTime);
            SetAlpha(_currentAlpha);
        }
    }

    public void UpdateFill(float amount)
    {
        ResetAlpha();
        _fill.fillAmount = amount;
        _lastTimeScanned = Time.time;
    }

    public void ShowText(int speed, bool isAboveLimit)
    {
        _speedText.text = speed.ToString();
        if (isAboveLimit)
        {
            _fill.color = Color.red;
        }
    }

    public void ResetDisplay()
    {
        if (!_isInit) Init();
        _fill.color = _defaultColor;
        _speedText.text = "";
        SetAlpha(0);
    }

    private void ResetAlpha()
    {
        SetAlpha(1);
        _currentAlpha = 1;
    }

    private void SetAlpha(float a)
    {
        Color fillColor = _fill.color;
        _fill.color = new Color(fillColor.r, fillColor.g, fillColor.b, a);
        Color bgColor = _bg.color;
        _bg.color = new Color(bgColor.r, bgColor.g, bgColor.b, a);
        Color speedTextColor = _speedText.color;
        _speedText.color = new Color(speedTextColor.r, speedTextColor.g, speedTextColor.b, a);
    }
}