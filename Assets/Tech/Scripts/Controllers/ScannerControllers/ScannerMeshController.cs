using System.Collections.Generic;
using UnityEngine;

public class ScannerMeshController : MonoBehaviour
{
    [SerializeField] private Transform _mount;
    [SerializeField] private Transform _scanStart;
    [SerializeField] private Transform _scanModel;    

    private List<Vector3> _vertices;
    private List<int> _triangles;    

    private float _angle = 80f;

    private Mesh _mesh;
    private bool _isInit;

    private Transform _pedestrian;

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        if (_pedestrian != null)
        {
            Vector3 lookAt = new Vector3(_pedestrian.position.x, _pedestrian.position.y + 1, _pedestrian.position.z);

            _scanModel.LookAt(lookAt, Vector3.down);
            _scanModel.Rotate(Vector3.forward, -180);
            _scanModel.Rotate(Vector3.right, -90);
            _mount.position = _scanStart.position;
            _mount.LookAt(lookAt);
        }
    }

    private void Init()
    {
        if (_isInit) return;
        _mesh = GetComponent<MeshFilter>().mesh;
        _isInit = true;
    }

    public void GenerateScannerMesh(float length, Transform pedestrian)
    {
        if (!_isInit) Init();
        _mount.position = _scanStart.position;
        
        _vertices = new List<Vector3>();
        _triangles = new List<int>();

        float a = 0.01f;
        var v0 = new Vector3(-a * .5f, 0, 0);
        var v3 = new Vector3(a * .5f, 0, 0);

        float ctg = 1 / Mathf.Tan(_angle);

        float b = a - length * (ctg * 2);

        var v2 = new Vector3(-b * .5f, _scanStart.localPosition.y, length);
        var v1 = new Vector3(b * .5f, _scanStart.localPosition.y, length);

        _vertices.Add(v0);
        _vertices.Add(v1);
        _vertices.Add(v2);
        _vertices.Add(v3);


        _triangles.AddRange(new int[] { 0, 1, 2 });
        _triangles.AddRange(new int[] { 3, 1, 0 });

        UpdateMesh();
        _pedestrian = pedestrian;
    }

    public void DestroyScanner()
    {
        if (!_isInit) Init();

        _mesh.Clear();
        _pedestrian = null;
        _scanModel.localEulerAngles = new Vector3(0, 0, -180);
    }

    private void UpdateMesh()
    {
        _mesh.Clear();
        _mesh.vertices = _vertices.ToArray();
        _mesh.triangles = _triangles.ToArray();
        _mesh.RecalculateNormals();
    }
}