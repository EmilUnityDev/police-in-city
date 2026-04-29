using UnityEngine;
using UnityEngine.UI;

public class CarLicenseNumberPlate : MonoBehaviour
{
    private Text _licensePlateNumberText;
    private bool _isInit;

    private void Awake()
    {
        Init();
    }

    public void SetLicensePlateNumber(string text)
    {
        if (!_isInit) Init();
        _licensePlateNumberText.text = text;
    }

    private void Init()
    {
        if (_isInit) return;
        _licensePlateNumberText = GetComponent<Text>();
        _isInit = true;
    }
}