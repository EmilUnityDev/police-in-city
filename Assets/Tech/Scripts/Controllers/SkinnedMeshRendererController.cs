using UnityEngine;

public class SkinnedMeshRendererController : MonoBehaviour
{
    [SerializeField] private int _blendShapesCount = 3;
    [Tooltip("Если какая-то эмоция отсутствует, ставьте -1 или любое другое число меньше 0")]
    [SerializeField] private int _happyFaceIndex = 0, _angryFaceIndex = 1, _scaredFaceIndex = 2;

    private SkinnedMeshRenderer _skinnedMeshRenderer;

    private void Awake()
    {
        _skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();        
    }

    public void SetSmile()
    {
        ResetBlendShapes();
        if (_happyFaceIndex < 0) return;
        _skinnedMeshRenderer.SetBlendShapeWeight(_happyFaceIndex, 100);
    }

    public void SetAngry()
    {
        ResetBlendShapes();
        if (_angryFaceIndex < 0) return;
        _skinnedMeshRenderer.SetBlendShapeWeight(_angryFaceIndex, 100);

    }

    public void SetScared()
    {
        ResetBlendShapes();
        if (_scaredFaceIndex < 0) return;
        _skinnedMeshRenderer.SetBlendShapeWeight(_scaredFaceIndex, 100);
    }

    public void SetRegular()
    {
        ResetBlendShapes();
    }

    private void ResetBlendShapes()
    {
        for (int i = 0; i < _blendShapesCount; i++)
        {
            _skinnedMeshRenderer.SetBlendShapeWeight(i, 0);
        }
    }
}