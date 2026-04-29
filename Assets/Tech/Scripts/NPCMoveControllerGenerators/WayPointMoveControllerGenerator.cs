using UnityEngine;

public class WayPointMoveControllerGenerator : MonoBehaviour, INPCMoveControllerGenerator
{
    [SerializeField] private WayPoints _wayPoints;
    [SerializeField] private float _movementSpeed;

    private Rigidbody _rb;

    public IMovementController GetMoveController()
    {
        NodeGrid grid = FindObjectOfType<NodeGrid>();
        if (grid == null) return null;
        _rb = GetComponent<Rigidbody>();
        var wayPoints = _wayPoints.GetWayPoints();      
        return new AStarBasedRbController(_rb, _movementSpeed, wayPoints, grid);
    }
}