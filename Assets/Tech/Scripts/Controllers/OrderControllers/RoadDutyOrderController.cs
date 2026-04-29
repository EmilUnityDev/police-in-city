using System.Collections;
using UnityEngine;

public class RoadDutyOrderController : AbstractOrderController
{
    [SerializeField] private DayController _dayController;
    [SerializeField] private CorrectIncorrectCanvas _correctIncorrectCanvas;

    public override void CheckOrder(IOrderData orderData)
    {
        bool isOrderCorrect = orderData.IsOrderCorrect();

        StopAllCoroutines();
        StartCoroutine(OpenPanelsAfterCheck(isOrderCorrect));
    }

    private IEnumerator OpenPanelsAfterCheck(bool isOrderCorrect)
    {
        _correctIncorrectCanvas?.OpenRespectivePanel(isOrderCorrect);
        yield return new WaitForSeconds(1.5f);
        _dayController.OnOrderCompleted();
    }
}