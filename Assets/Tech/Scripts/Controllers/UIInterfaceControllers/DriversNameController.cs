using UnityEngine;
using UnityEngine.UI;

public class DriversNameController : AbstractDriversDataProcessorController
{
    [SerializeField] private Slider _slider;

    private Text _name;
    private bool _isInit;
    private bool _isFakeName;

    public override void CommitOnClickBehaviour()
    {
        if (!_isInit) Init();
        //if (_slider.value < 0.6f && _slider.value > 0.4f)
        //{
        //    _name.color = _isFakeName ? Color.red : Color.green;
        //}
    }

    public override void ProcessData(DriverData data)
    {
        if (!_isInit) Init();

        _name.text = data.Name;
        _isFakeName = data.IsNameFake;
    }

    private void Init()
    {
        if (_isInit) return;
        _name = GetComponent<Text>();
        _isInit = true;
    }
}