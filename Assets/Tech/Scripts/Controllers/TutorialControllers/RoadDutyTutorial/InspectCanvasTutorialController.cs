using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InspectCanvasTutorialController : MonoBehaviour
{
    public TrunkController TrunkController
    {
        set
        {
            _trunkController = value;
            _trunkController.TrunkClicked += OnTrunkClicked;
        }
    }

    public bool IsEnabled
    {
        private get;
        set;
    }

    [SerializeField] private Image _picture;
    [SerializeField] private Text _licensePlate;
    [SerializeField] private Slider _slider;
    [SerializeField] private InspectSliderController _sliderController;
    [SerializeField] private Button[] _buttons;
    [Header("Fingers")]
    [SerializeField] private GameObject _pictureFinger, _licensePlateFinger, _trunkFinger, _sliderFinger;
    [SerializeField] private GameObject[] _buttonFingers;
    [SerializeField] private DOTweenAnimation _tweenAnim;
    [SerializeField] private Sprite[] _enabledButtons, _disabledButtons;
    [SerializeField] private GameObject _leftArrow, _rightArrow, _middleArrow;

    private TrunkController _trunkController;
    private bool _hasOpenedTrunk, _hasCheckedPlate;
    
    public enum SliderFingerMode
    {
        FromLeftToRight,
        Middle,
        FromRightToLeft
    }

    private void OnEnable()
    {
        EnablePicture(true);
    }

    public void DisableAll()
    {
        if (!IsEnabled) return;

        _picture.raycastTarget = false;
        _licensePlate.raycastTarget = false;
        _slider.interactable = false;
        _sliderController.EnableSlider(false);
        int N = _buttons.Length;
        for (int i = 0; i < N; i++)
        {
            _buttons[i].enabled = false;
            _buttons[i].image.sprite = _disabledButtons[i];
            if (i == 2) continue; 
            _buttonFingers[i].SetActive(false);
        }
        _trunkController.IsEnabled = false;

        _pictureFinger.SetActive(false);
        _licensePlateFinger.SetActive(false);
        _trunkFinger.SetActive(false);
        _sliderFinger.SetActive(false);
        _rightArrow.SetActive(false);
        _leftArrow.SetActive(false);
        _middleArrow.SetActive(false);
    }

    public void EnablePicture(bool disableOthers)
    {
        if (!IsEnabled) return;

        if (disableOthers) DisableAll();

        _picture.raycastTarget = true;
        _pictureFinger.SetActive(true);
    }

    public void EnableSlider(bool disableOthers)
    {
        if (!IsEnabled) return;

        if (disableOthers) DisableAll();

        _slider.interactable = true;
        _sliderController.EnableSlider(true);
        _sliderFinger.SetActive(true);

        if (!_hasOpenedTrunk)
        {
            _rightArrow.SetActive(true);
        }

        if (!_hasCheckedPlate)
        {
            _leftArrow.SetActive(true);
        }

        if (_hasCheckedPlate && _hasOpenedTrunk)
        {
            _middleArrow.SetActive(true);
        }
    }

    public void OnSliderValueChanged(float value)
    {
        if (!IsEnabled) return;

        if (value <= 0.01f)
        {
            if (_hasCheckedPlate) return;
            EnableLicensePlate(true);
            _leftArrow.SetActive(false);
        }
        else if(value >= 0.95f)
        {
            if (_hasOpenedTrunk) return;
            EnableTrunk(true);
            _rightArrow.SetActive(false);
        }
        else if(value > 0.4f && value < 0.6f)
        {
            if (_hasCheckedPlate && _hasOpenedTrunk)
            {
                EnableButtonsExceptLetGo(true);
                _slider.enabled = true;
            }

            if (!_hasOpenedTrunk)
            {
                _rightArrow.SetActive(true);
            }

            if (!_hasCheckedPlate)
            {
                _leftArrow.SetActive(true);
            }
        }
    }

    public void EnableButtonsExceptLetGo(bool disableOthers)
    {
        if (!IsEnabled) return;


        if (disableOthers) DisableAll();

        int N = _buttons.Length;
        for (int i = 1; i < N; i++)
        {
            _buttons[i].enabled = true;
            _buttons[i].image.sprite = _enabledButtons[i];
            _buttonFingers[i - 1].SetActive(true);
        }
    }

    private void EnableTrunk(bool disableOthers)
    {
        if (!IsEnabled) return;


        if (disableOthers) DisableAll();

        _trunkController.IsEnabled = true;
        _trunkFinger.SetActive(true);
    }

    private void EnableLicensePlate(bool disableOthers)
    {
        if (!IsEnabled) return;

        if (disableOthers) DisableAll();

        _licensePlate.raycastTarget = true;
        _licensePlateFinger.SetActive(true);
    }

    public void OnLicensePlateClicked()
    {
        _hasCheckedPlate = true;
        EnableSlider(true);
        SetSliderFinger(SliderFingerMode.FromLeftToRight);
    }

    public void OnTrunkClicked()
    {
        _hasOpenedTrunk = true;
        EnableSlider(true);
        _trunkController.IsEnabled = true;
        SetSliderFinger(SliderFingerMode.FromRightToLeft);
    }

    public void SetFingerToMiddle()
    {
        SetSliderFinger(SliderFingerMode.Middle);
    }

    private void SetSliderFinger(SliderFingerMode sliderFingerMode)
    {
        RectTransform fingerRect = _sliderFinger.transform.GetComponent<RectTransform>();
        Vector2 anchPos = fingerRect.anchoredPosition;
        float x = 30;
        switch (sliderFingerMode)
        {
            case SliderFingerMode.FromLeftToRight:
                anchPos.x = 30;
                _tweenAnim.DOPlay();
                break;
            case SliderFingerMode.Middle:
                anchPos.x = 15;
                _tweenAnim.DOPlay();
                break;
            case SliderFingerMode.FromRightToLeft:                
                anchPos.x = 30;
                _tweenAnim.DOPlay();
                break;
            default:
                anchPos.x = 15;
                break;
        }

        fingerRect.anchoredPosition = anchPos;
    }
}