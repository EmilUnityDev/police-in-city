using UnityEngine;

public abstract class SceneLoadController : MonoBehaviour
{
    [SerializeField] protected SceneLoader _sceneLoader;

    public abstract void Next();
    public abstract void Reload();
}