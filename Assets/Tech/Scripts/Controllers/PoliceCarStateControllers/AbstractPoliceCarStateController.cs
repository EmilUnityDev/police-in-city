using UnityEngine;

public class AbstractPoliceCarStateController : MonoBehaviour
{
    [SerializeField] protected PoliceCar _policeCar;

    protected IState _from, _to;

    public virtual void SetState()
    {
        if (_from != null && _to != null)
            _policeCar.TrySetState(_from, _to);
    }
}