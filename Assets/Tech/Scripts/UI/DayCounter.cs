using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DayCounter : MonoBehaviour
{
    [SerializeField] private Image _fill;
    [SerializeField] private Text _dayCountText;

    private float _fillDuration = 0.8f;

    public void UpdateCounter(int completedOrdersCount, int overallOrdersCount, int dayNumber, bool animate = true)
    {
        _dayCountText.text = "Day: " + dayNumber.ToString();
        float amount = (float)completedOrdersCount / overallOrdersCount;       

        if (animate)
        {
            _fill.DOFillAmount(amount, _fillDuration).SetAutoKill();
        }
        else
        {
            _fill.fillAmount = amount;
        }
    }
}