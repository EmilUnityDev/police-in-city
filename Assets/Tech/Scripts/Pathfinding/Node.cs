using UnityEngine;

public class Node
{
    private Vector3 _worldPosition;
    private bool _isWall;
    private bool _isObstacle;
    private int _gridX, _gridY;

    public Node(Vector3 worldPosition, bool isWall, bool isObstacle, int gridX, int gridY)
    {
        _worldPosition = worldPosition;
        _isWall = isWall;
        _isObstacle = isObstacle;
        _gridX = gridX;
        _gridY = gridY;
    }

    public bool IsWalkable(bool canFly)
    {
        if (canFly)
        {
            return !_isWall;
        }
        else
        {
            return !_isWall && !_isObstacle;
        }
    }

    public Vector3 WorldPosition
    {
        get => _worldPosition;
    }

    public int X
    {
        get => _gridX;
    }

    public int Y
    {
        get => _gridY;
    }
}