using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GoodCallCanvasController : AbstractPoliceCarStateController, IPointerClickHandler
{
    [SerializeField] private Image _blackPanel;
    [SerializeField] private GameObject _imageSmile, _textBlock;

    private bool _isClicked;
    private float _openTime;
    private float _duration = 2f;

    private void OnEnable()
    {
        _isClicked = false;
        _openTime = Time.time;
    }

    private void OnDisable()
    {
        Color color = _blackPanel.color;
        _blackPanel.color = new Color(color.r, color.g, color.b, 0);        
    }

    private void Update()
    {
        if (!_isClicked && _openTime + _duration < Time.time)
        {
            SetState();
            _isClicked = true;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isClicked) return;
        SetState();
        _isClicked = true;
    }

    public override void SetState()
    {
        _from = _policeCar.CurrentState;
        _to = _policeCar.TransitionToInspectCameraState;
        _imageSmile.SetActive(false);
        _textBlock.SetActive(false);
        Tween tween = _blackPanel.DOFade(1, 1).OnStart(() => _blackPanel.DOColor(Color.black, 1.1f).SetAutoKill()).OnComplete(() => base.SetState()).SetAutoKill();        
    }

    public virtual void Disappear()
    {
        Tween tween = _blackPanel.DOFade(0, 1).OnComplete(() => transform.parent.gameObject.SetActive(false)).SetAutoKill();
    }
}