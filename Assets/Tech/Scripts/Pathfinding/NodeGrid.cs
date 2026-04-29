using UnityEngine;

public class NodeGrid : MonoBehaviour
{
    public float NodeSize
    {
        get => _nodeSize;
    }

    [SerializeField] private LayerMask _wallLayer;
    [SerializeField] private LayerMask _obstacleLayer;

    private float _nodeSize;
    private int _gridSizeX, _gridSizeY;
    private float _planeX, _planeY;
    private Node[,] _grid;
    private float _xOffset, _yOffset;

    private void Awake()
    {
        _nodeSize = 1.5f;
        _xOffset = transform.position.x;
        _yOffset = transform.position.z;

        BoxCollider collider = GetComponent<BoxCollider>();

        _planeX = collider.bounds.size.x;
        _planeY = collider.bounds.size.z;
        _gridSizeX = Mathf.FloorToInt(_planeX / _nodeSize);
        _gridSizeY = Mathf.FloorToInt(_planeY / _nodeSize);
        CreateGrid();
    }

    private void CreateGrid()
    {
        Vector3 worldBottomLeft = transform.position + (Vector3.right * -_gridSizeX / 2 * _nodeSize) + (Vector3.forward * -_gridSizeY / 2 * _nodeSize) + (Vector3.right * _nodeSize / 2) + (Vector3.forward * _nodeSize / 2);

        _grid = new Node[_gridSizeX, _gridSizeY];


        for (int x = 0; x < _gridSizeX; x++)
        {
            for (int y = 0; y < _gridSizeY; y++)
            {
                Vector3 worldPosition = worldBottomLeft + (Vector3.right * _nodeSize * x) + (Vector3.forward * _nodeSize * y);
                bool isWall = Physics.CheckSphere(worldPosition + Vector3.up * _nodeSize /2, _nodeSize * 0.85f, _wallLayer);
                bool isObstacle = Physics.CheckSphere(worldPosition + Vector3.up * _nodeSize / 2, _nodeSize * 0.85f, _obstacleLayer);

                _grid[x, y] = new Node(worldPosition, isWall, isObstacle, x, y);
            }
        }
    }

    public Node GetNode(Vector3 worldPosition)
    {

        float xPercent = (_planeX / 2 + worldPosition.x - _xOffset) / _planeX;
        float yPercent = (_planeY / 2 + worldPosition.z - _yOffset) / _planeY;

        xPercent = Mathf.Clamp01(xPercent);
        yPercent = Mathf.Clamp01(yPercent);

        int x = Mathf.RoundToInt((_gridSizeX - 1) * xPercent);
        int y = Mathf.RoundToInt((_gridSizeY - 1) * yPercent);

        return _grid[x, y];
    }

    public Node GetNodeByIndeces(int x, int y)
    {
        if (x < 0 || y < 0 || x >= _gridSizeX || y >= _gridSizeY)
        {
            return null;
        }
        else
        {
            return _grid[x, y];
        }
    }

    public Node[] GetNodeNeighbours(Node node)
    {
        int length = 0;

        for (int x = -1; x < 2; x++)
        {
            for (int y = -1; y < 2; y++)
            {
                if (node.X + x < 0 || node.X + x >= _gridSizeX ||
                    node.Y + y < 0 || node.Y + y >= _gridSizeY ||
                    (x == 0 && y == 0)
                    )
                {
                    continue;
                }
                else
                {
                    length++;
                }
            }
        }

        Node[] neighbours = new Node[length];

        int i = 0;

        for (int x = -1; x < 2; x++)
        {
            for (int y = -1; y < 2; y++)
            {
                if (node.X + x < 0 || node.X + x >= _gridSizeX ||
                    node.Y + y < 0 || node.Y + y >= _gridSizeY ||
                    (x == 0 && y == 0)
                    )
                {
                    continue;
                }
                else
                {
                    neighbours[i] = _grid[node.X + x, node.Y + y];
                    i++;
                }
            }
        }

        return neighbours;
    }

    public int GridSizeX
    {
        get => _gridSizeX;
    }

    public int GridSizeY
    {
        get => _gridSizeY;
    }

    public Node[,] Grid
    {
        get => _grid;
    }

    //private void OnDrawGizmos()
    //{
    //    if (_grid == null) return;

    //    for (int x = 0; x < _gridSizeX; x++)
    //    {
    //        for (int y = 0; y < _gridSizeY; y++)
    //        {
    //            Gizmos.color = _grid[x, y].IsWalkable(false) ? Color.gray : Color.red;

    //            Gizmos.DrawCube(_grid[x, y].WorldPosition, Vector3.one * _nodeSize);
    //        }
    //    }
    //}
}