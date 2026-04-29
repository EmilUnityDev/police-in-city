using UnityEngine;

public class Swat : MonoBehaviour
{
    [SerializeField] private Transform _hand;
    [SerializeField] private SwatAnimationController _animController;

    private Pedestrian _pedestrian;
    private Vector3 _initPos;

    private void Awake()
    {
        _initPos = transform.position;        
    }

    public void Call(Pedestrian pedestrian)
    {
        _pedestrian = pedestrian;

        transform.position = _pedestrian.transform.position + (-0.7f * _pedestrian.transform.right);
        transform.rotation = _pedestrian.transform.rotation;
        _animController.SetSwatOut();

    }

    public void TakePedestrian()
    {
        _pedestrian.transform.SetParent(_hand);
        _pedestrian.TakeOut();
    }

    public void Disappear()
    {
        transform.position = _initPos;
        
        _pedestrian.gameObject.SetActive(false);
    }
}