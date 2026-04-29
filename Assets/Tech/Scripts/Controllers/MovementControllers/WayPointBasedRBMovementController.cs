using System.Collections.Generic;
using UnityEngine;

public class WayPointBasedRBMovementController : IMovementController
{
    private Rigidbody _rb;
    private float _speed;

    private Stack<Vector3> _wayPointsA;
    private Stack<Vector3> _wayPointsB;

    private Vector3 _currentGoal;

    private Stack<Vector3> _currentWay;

    public WayPointBasedRBMovementController(Rigidbody rb, float speed, Vector3[] wayPoints)
    {
        _rb = rb;
        _speed = speed;

        int N = wayPoints.Length;

        _wayPointsA = new Stack<Vector3>();
        _wayPointsB = new Stack<Vector3>();

        for (int i = N - 1; i >= 0; i--)
        {
            _wayPointsA.Push(wayPoints[i]);
        }
        
        _currentWay = _wayPointsA;
        if (_currentWay.Count > 0)
            SetNextGoalPos();

        Vector3 rbPos = _rb.position;
        Vector3 currentGoalPos = _currentGoal;
        Vector3 p = new Vector3(currentGoalPos.x, rbPos.y, currentGoalPos.z);
        _rb.position = p;
    }

    public void Move()
    {
        var dir = _currentGoal - _rb.position;
        dir.Normalize();
        dir.y = 0f;
        _rb.velocity = dir * _speed * Time.deltaTime;
        Quaternion r = Quaternion.LookRotation(dir, Vector3.up);
        _rb.transform.rotation = Quaternion.Lerp(_rb.transform.rotation, r, 5 * Time.deltaTime);

        if (IsCloseEnough(_currentGoal))
        {
            SetNextGoalPos();
        }
        
    }

    private bool IsCloseEnough(Vector3 goalPos)
    {
        var goalV2 = new Vector2(goalPos.x, goalPos.z);
        var selfPosV2 = new Vector2(_rb.position.x, _rb.position.z);

        return Vector2.Distance(selfPosV2, goalV2) <= 0.02f;
    }

    private bool IsGoalReached()
    {
        return _currentWay.Count <= 0;
    }

    private void SwapWays()
    {
        if (_currentWay == _wayPointsA)
        {
            _currentWay = _wayPointsB;
        }
        else
        {
            _currentWay = _wayPointsA;
        }
    }

    private void Push(Vector3 point)
    {
        if (_currentWay == _wayPointsA)
        {
            _wayPointsB.Push(point);
        }
        else
        {
            _wayPointsA.Push(point);
        }
    }

    private void SetNextGoalPos()
    {
        if (IsGoalReached()) SwapWays();

        _currentGoal = _currentWay.Pop();
        Push(_currentGoal);
    }
}