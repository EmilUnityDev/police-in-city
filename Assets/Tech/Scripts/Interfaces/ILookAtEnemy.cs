using UnityEngine;

public interface ILookAtEnemy 
{
    public Transform Target { get; }
    void SetTarget(Transform target);
    void SetPlace(Vector3 pos, Quaternion rot);
}