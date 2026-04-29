using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FailedCallCanvasController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _blackPanel;
    [SerializeField] private GameObject _imageSmile, _textBlock;
    [SerializeField] private DayController _dayController;

    private float _openTime, _duration = 2f;
    private bool _isClosing;

    private void OnEnable()
    {        
        _openTime = Time.time;
        _isClosing = false;
    }

    private void OnDisable()
    {
        Color color = _blackPanel.color;
        _blackPanel.color = new Color(color.r, color.g, color.b, 0);
    }

    private void Update()
    {
        if (!_isClosing && _openTime + _duration < Time.time)
        {
            FailOrder();
            _isClosing = true;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        FailOrder();
    }

    private void FailOrder()
    {
        _dayController.OnOrderFailed();
    }
}