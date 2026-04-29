using UnityEngine;

public class CarArrestState : IState
{
    private Car _car;
    private Driver _driver;

    public CarArrestState(Car car)
    {
        _car = car;
    }

    public void Enter()
    {
        _driver = _car.Driver;
        float targetZ = _car.TargetLeft.GetChild(0).position.z;
        _driver.transform.localPosition = new Vector3(-0.5f, 0.0f, 0.0f);
        Vector3 pos = _driver.transform.position;
        pos.z = targetZ;
        pos.y = 0;
        _driver.transform.position = pos;
        _driver.transform.localEulerAngles = new Vector3(0.0f, -90.0f, 0.0f);
        _driver.Arrest();
    }

    public void Exit()
    {
        
    }

    public void FixedTick()
    {
        
    }

    public void Tick()
    {
        
    }
}