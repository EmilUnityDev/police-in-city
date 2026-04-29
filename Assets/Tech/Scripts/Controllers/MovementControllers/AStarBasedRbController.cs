using System.Collections.Generic;
using UnityEngine;

public class AStarBasedRbController : IMovementController
{
    private NodeGrid _nodeGrid;
    private AStar _astar;
    private Rigidbody _rb;
    private float _speed;

    private Stack<Vector3> _wayPointsA;
    private Stack<Vector3> _wayPointsB;

    private Vector3 _currentGoal, _currentPoint;

    private Stack<Vector3> _currentWay;
    private Queue<Vector3> _currentPoints;

    private bool _canMove;
    private float _requiredDistance = 0.5f;

    public AStarBasedRbController(Rigidbody rb, float speed, Vector3[] wayPoints, NodeGrid nodeGrid)
    {
        _rb = rb;
        _speed = speed;
        _nodeGrid = nodeGrid;
        _requiredDistance = _nodeGrid.NodeSize;
        Init(wayPoints);
    }

    private void Init(Vector3[] wayPoints)
    {
        _astar = new AStar(_nodeGrid);
        _currentPoints = new Queue<Vector3>();

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
        if (!_canMove) return;

        var dir = (_currentPoint - _rb.position) * 10;
        dir.Normalize();
        
        dir.y = 0f;
        _rb.velocity = dir * _speed * Time.deltaTime;
        Quaternion r = Quaternion.LookRotation(dir, Vector3.up);
        _rb.transform.rotation = Quaternion.Lerp(_rb.transform.rotation, r, 5 * Time.deltaTime);

        if (IsCloseEnough(_currentPoint))
        {
            if (TryGetNextPoint(out var nextPoint))
            {
                _currentPoint = nextPoint;
            }
            else
            {
                SetNextGoalPos();
            }            
        }
    }

    private bool IsCloseEnough(Vector3 goalPos)
    {
        var goalV2 = new Vector2(goalPos.x, goalPos.z);
        var selfPosV2 = new Vector2(_rb.position.x, _rb.position.z);
        
        return Vector2.Distance(selfPosV2, goalV2) <= _requiredDistance;
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

        _canMove = false;

        _currentGoal = _currentWay.Pop();
        Push(_currentGoal);

        var points = _astar.GetNodePath(_rb.position, _currentGoal, false);
        if (points != null)
        {            
            foreach (var p in points)
            {
                _currentPoints.Enqueue(p);                
            }
            _canMove = TryGetNextPoint(out _currentPoint);
        }        
    }

    private bool TryGetNextPoint(out Vector3 nextPoint)
    {
        if (_currentPoints.Count > 0)
        {
            nextPoint = _currentPoints.Dequeue();            
            return true;
        }
        else
        {
            nextPoint = new Vector3();
            return false;
        }
    }
}