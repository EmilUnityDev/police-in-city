using UnityEngine;

public abstract class AbstractOrderController : MonoBehaviour
{
    public abstract void CheckOrder(IOrderData orderData);
}