using UnityEngine;
using UnityEngine.UI;

public class DriversLicensePlateNumberController : AbstractDriversDataProcessorController
{
    [SerializeField] private Slider _slider;

    private Text _licensePlateNumber;
    private bool _isInit;
    private bool _isFakeNumber;

    public override void CommitOnClickBehaviour()
    {
        if (!_isInit) Init();
        if (_slider.value < _slider.maxValue * 0.1f)
        {
            _licensePlateNumber.color = _isFakeNumber ? Color.red : Color.green;

        }

        OnClicked?.Invoke();
    }

    public override void ProcessData(DriverData data)
    {
        if (!_isInit) Init();

        _licensePlateNumber.text = data.LicensePlateNumber;
        _isFakeNumber = data.IsNumberFake;
        
    }

    private void Init()
    {
        if (_isInit) return;
        _licensePlateNumber = GetComponent<Text>();
        _isInit = true;
    }
}