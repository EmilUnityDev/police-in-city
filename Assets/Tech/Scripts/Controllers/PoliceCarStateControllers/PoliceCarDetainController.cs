using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceCarDetainController : MonoBehaviour
{    
    public bool IsFinishedMoving { get; private set; }
    public Car DetainedCar { get; private set; }
    public bool SuccessfullyDetained { get; private set; }

    [SerializeField] private float _speed = 100;

    private Rigidbody _rb;

    public void BlockCar()
    {
        Car closestCar = ChooseCarToBlock();
        if (closestCar == null) return;
        DetainedCar = closestCar;
        float x = closestCar.transform.position.x;        
        _rb = GetComponent<Rigidbody>();
        StopAllCoroutines();
        StartCoroutine(MoveToLane(x));
    }

    public void StopMovement()
    {
        StopAllCoroutines();
    }

    private Car ChooseCarToBlock()
    {
        Car[] carArr = FindObjectsOfType<Car>();
        List<Car> cars = new List<Car>();

        foreach (var c in carArr)
        {
            if (c.transform.position.z < c.MaxZPos.position.z)
            {
                cars.Add(c);
            }
        }
        if (cars.Count <= 0) return null;


        float minDist = Vector3.Distance(transform.position, cars[0].transform.position);
        Car closestCar = cars[0];
        foreach (var car in cars)
        {
            float dist = Vector3.Distance(transform.position, car.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closestCar = car;
            }
        }

        return closestCar;
    }

    private IEnumerator MoveToLane(float x)
    {
        while (transform.position.x > x)
        {
            _rb.velocity = transform.forward * _speed * Time.fixedDeltaTime;            
            yield return new WaitForFixedUpdate();
        }

        _rb.velocity = Vector3.zero;
        _rb.isKinematic = true;
        SuccessfullyDetained = DetainedCar.TryDetain();
        IsFinishedMoving = true;

        yield break;
    }
}