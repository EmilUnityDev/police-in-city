using UnityEngine;
using UnityEngine.UI;

public class DriversPictureController : AbstractDriversDataProcessorController
{    
    [SerializeField] private Slider _slider;
    [SerializeField] private Image _frame;

    private Image _picture;
    private bool _isInit;
    private bool _isFakePicture;

    public override void CommitOnClickBehaviour()
    {
        if (!_isInit) Init();
        if (_slider.value < 0.6f && _slider.value > 0.4f)
        {
            _frame.color = _isFakePicture ? Color.red : Color.green;
            OnClicked?.Invoke();
        }
    }

    public override void ProcessData(DriverData data)
    {
        if (!_isInit) Init();

        _picture.sprite = data.Picture;
        _isFakePicture = data.IsPictureFake;
    }

    private void Init()
    {
        if (_isInit) return;
        _picture = GetComponent<Image>();
        _isInit = true;
    }
}