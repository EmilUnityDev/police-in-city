using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InspectSliderController : MonoBehaviour, IBeginDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private DOTweenUICard _driverCard, _buttons;

    private Slider _slider;    
    private bool _isInit;
    private bool _isMoving;
    private bool _isEnabled;

    public void EnableSlider(bool enabled)
    {
        _isEnabled = enabled;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {        
        if (!_isInit) Init();
        if (!_isEnabled) return;
        _driverCard?.MoveOut();
        _buttons?.MoveOut();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_isMoving || !_isEnabled) return;
        StopAllCoroutines();
        _isMoving = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!_isInit) Init();
        if (_slider.value < _slider.maxValue * 0.6f)
            _driverCard?.MoveIn();

        if(_slider.value < _slider.maxValue * 0.6f
            && _slider.value > _slider.maxValue * 0.4f)
        {
            _buttons?.MoveIn();
        }

        StartCoroutine(MoveToClosest());
    }   

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        if (_isInit) return;
        _slider = GetComponent<Slider>();
        _isEnabled = true;
        _isInit = true;
    }

    private IEnumerator MoveToClosest()
    {
        _isMoving = true;

        float currentValue = _slider.value;
        float endValue = GetEndValue(currentValue);
        while (Mathf.Abs(endValue - currentValue) >= 0.001f)
        {
            currentValue = Mathf.Lerp(currentValue, endValue, 0.1f);
            _slider.value = currentValue;

            if (_slider.value < _slider.maxValue * 0.6f)
                _driverCard?.MoveIn();

            if (_slider.value < _slider.maxValue * 0.6f
                && _slider.value > _slider.maxValue * 0.4f)
            {
                _buttons?.MoveIn();
            }

            yield return null;
        }

        _slider.value = endValue;
        _isMoving = false;
        yield break;
    }

    private float GetEndValue(float currentValue)
    {
        if (currentValue < 0.25f) return 0.0001f;
        else if (currentValue >= 0.25f && currentValue <= 0.75f) return 0.5f;
        else if (currentValue > 0.75f) return 1.0f;
        else return 0.5f;
    }
}