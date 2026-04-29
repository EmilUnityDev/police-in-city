using UnityEngine;

[CreateAssetMenu(fileName = "New Scenes DB", menuName = "GameData/ScenesDB")]
public class SceneDBSO : ScriptableObject
{
    [SerializeField] private int[] _sceneIndices;
    [SerializeField] private int _tutorialSceneIndex;

    private int _lastSceneIndex = -1;

    public int GetNextSceneIndex()
    {
        if (_sceneIndices == null) return -1;
        
        int N = _sceneIndices.Length;
        int nextIndex = _lastSceneIndex + 1;
        if (nextIndex >= N || nextIndex < 0) nextIndex = 0;

        _lastSceneIndex = nextIndex;
        
        return _sceneIndices[nextIndex];         
    }

    public int GetNextOrRandomSceneIndex()
    {
        if (_sceneIndices == null) return -1;

        int N = _sceneIndices.Length;
        int nextIndex = _lastSceneIndex + 1;
        if (nextIndex < 0) nextIndex = 0;
        if (nextIndex >= N)
        {
            int randomIndex = Random.Range(0, N);
            return randomIndex;
        }

        _lastSceneIndex = nextIndex;

        return _sceneIndices[nextIndex];
    }

    public int GetRandomSceneIndex()
    {
        if (_sceneIndices == null) return -1;

        int N = _sceneIndices.Length;
        int randomIndex = Random.Range(0, N);
        _lastSceneIndex = randomIndex;
        
        return _sceneIndices[randomIndex];        
    }

    public int GetSceneIndexByDay(int day)
    {
        if (_sceneIndices == null) return -1;

        int N = _sceneIndices.Length;
        if (day > (N - 1)) return GetRandomSceneIndex();
        int sceneIndex = day;
        if (sceneIndex < 0) sceneIndex = 0;
        _lastSceneIndex = sceneIndex;

        return _sceneIndices[sceneIndex];
    }

    public int GetTutorialSceneIndex()
    {
        return _tutorialSceneIndex;
    }
}