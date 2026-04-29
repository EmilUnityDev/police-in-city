using System.Collections.Generic;
using UnityEngine;

public class AStar
{
    private List<Node> _openSet;
    private HashSet<Node> _closedSet;

    private Dictionary<Node, int> _gCost;
    private Dictionary<Node, int> _fCost;
    private Dictionary<Node, int> _hCost;

    private Dictionary<Node, Node> _nodeParents;

    private Node _startingNode;
    private Node _goalNode;
    private Node _currentNode;

    private NodeGrid _grid;

    public AStar(NodeGrid grid)
    {
        _grid = grid;
    }

    public List<Vector3> GetNodePath(Vector3 startingPosition, Vector3 goalPosition, bool canFly)
    {
        Node goal = GetGoalNode(startingPosition, goalPosition, canFly);

        if (goal == null) return null;

        if (_nodeParents.Count == 0) return null;

        List<Vector3> path = new List<Vector3>();

        Node child = goal;

        while (child != _startingNode)
        {
            path.Add(child.WorldPosition);

            child = _nodeParents[child];
        }

        path.Add(child.WorldPosition);
        path.Reverse();

        return path;
    }

    private Node GetGoalNode(Vector3 startingPosition, Vector3 goalPosition, bool canFly)
    {
        _openSet = new List<Node>();
        _closedSet = new HashSet<Node>();

        _gCost = new Dictionary<Node, int>();
        _fCost = new Dictionary<Node, int>();
        _hCost = new Dictionary<Node, int>();


        _nodeParents = new Dictionary<Node, Node>();


        _startingNode = _grid.GetNode(startingPosition);
        _goalNode = _grid.GetNode(goalPosition);

        if (!_goalNode.IsWalkable(false))
        {
            _goalNode = GetClosestTraversible(_goalNode);
            if (_goalNode == null) return null;
        }

        _openSet.Add(_startingNode);

        _gCost[_startingNode] = 0;
        _fCost[_startingNode] = 0;
        _hCost[_startingNode] = GetDistance(_startingNode, _goalNode);


        while (_openSet.Count > 0)
        {

            _currentNode = _openSet[0];

            for (int i = 0; i < _openSet.Count; i++)
            {
                if (_fCost[_openSet[i]] < _fCost[_currentNode] || _fCost[_openSet[i]] == _fCost[_currentNode] && _hCost[_openSet[i]] < _hCost[_currentNode])
                {
                    _currentNode = _openSet[i];
                }
            }

            _openSet.Remove(_currentNode);
            _closedSet.Add(_currentNode);

            if (_currentNode == _goalNode)
            {
                return _currentNode;
            }

            Node[] neighbours = _grid.GetNodeNeighbours(_currentNode);

            foreach (var n in neighbours)
            {
                if (!n.IsWalkable(canFly) || _closedSet.Contains(n))
                    continue;

                int newDistanceToNeighbour = _gCost[_currentNode] + GetDistance(_currentNode, n);

                if (!_openSet.Contains(n) || newDistanceToNeighbour < _gCost[n])
                {
                    _gCost[n] = newDistanceToNeighbour;
                    _hCost[n] = GetDistance(n, _goalNode);
                    _fCost[n] = _gCost[n] + _hCost[n];

                    if (!_openSet.Contains(n))
                        _openSet.Add(n);
                    _nodeParents[n] = _currentNode;
                }
            }
        }

        return null;
    }

    private int GetDistance(Node nodeA, Node nodeB)
    {
        int distX = Mathf.Abs(nodeA.X - nodeB.X);
        int distY = Mathf.Abs(nodeA.Y - nodeB.Y);
        if (distX > distY)
        {
            return 14 * distY + 10 * (distX - distY);
        }
        else
        {
            return 14 * distX + 10 * (distY - distX);
        }
    }    

    private Node GetClosestTraversible(Node startingNode)
    {
        var openSetForClosest = new Queue<Node>();
        var closedSet = new HashSet<Node>();

        openSetForClosest.Enqueue(startingNode);

        while (openSetForClosest.Count > 0)
        {
            Node currentNode = openSetForClosest.Dequeue();

            if (currentNode.IsWalkable(false)) return currentNode;

            Node[] neighbours = _grid.GetNodeNeighbours(currentNode);

            foreach (var n in neighbours)
            {
                if (closedSet.Contains(n)) continue;

                openSetForClosest.Enqueue(n);
            }

            closedSet.Add(currentNode);
        }

        return null;
    }
}