using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private SceneDBSO _streetDutyScenesDb, _roadDutyScenesDb;
    [SerializeField] private LoadingScreen _loadingScreen;

    public void Reload()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        LoadScene(currentIndex);
    }

    public void LoadNextStreetDutyScene()
    {
        LoadNextOrRandomScene(_streetDutyScenesDb);
    }

    public void LoadNextRoadDutyScene()
    {
        LoadNextScene(_roadDutyScenesDb);
    }

    public void LoadRandomStreetDutyScene()
    {
        LoadRandomScene(_streetDutyScenesDb);
    }

    public void LoadRandomRoadDutyScene()
    {
        LoadRandomScene(_roadDutyScenesDb);
    }

    public void LoadStreetDutySceneByDay(int day)
    {
        int dayForIndex = (day - 3) / 2;
        LoadSceneByDay(_streetDutyScenesDb, dayForIndex);
    }

    public void LoadRoadDutySceneByDay(int day)
    {
        LoadSceneByDay(_roadDutyScenesDb, day);
    }

    public void LoadStreetDutyTutorial()
    {
        LoadTutorialScene(_streetDutyScenesDb);
    }

    public void LoadRoadDutyTutorial()
    {
        LoadTutorialScene(_roadDutyScenesDb);
    }

    private void LoadNextScene(SceneDBSO sceneDBSO)
    {
        int nextSceneIndex = sceneDBSO.GetNextSceneIndex();
        LoadScene(nextSceneIndex);
    }

    private void LoadRandomScene(SceneDBSO sceneDBSO)
    {
        int randSceneIndex = sceneDBSO.GetRandomSceneIndex();
        LoadScene(randSceneIndex);
    }

    private void LoadNextOrRandomScene(SceneDBSO sceneDBSO)
    {
        int nextSceneIndex = sceneDBSO.GetNextOrRandomSceneIndex();
        LoadScene(nextSceneIndex);
    }

    private void LoadSceneByDay(SceneDBSO sceneDBSO, int day)
    {
        int nextSceneIndex = sceneDBSO.GetSceneIndexByDay(day);
        LoadScene(nextSceneIndex);
    }

    private void LoadTutorialScene(SceneDBSO sceneDBSO)
    {
        int tutorialSceneIndex = sceneDBSO.GetTutorialSceneIndex();
        LoadScene(tutorialSceneIndex);
    }

    public void LoadScene(int index)
    {
        StopAllCoroutines();
        StartCoroutine(LoadSceneAsync(index));
    }

    private IEnumerator LoadSceneAsync(int index)
    {
        if (_loadingScreen == null)
        {
            SceneManager.LoadScene(index);
            yield break;
        }

        float openDuration = _loadingScreen.OpenDuration;
        _loadingScreen.OpenLoadingScreen();
        yield return new WaitForSeconds(openDuration);

        AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(index);

        while (!loadingOperation.isDone)
        {
            float progress = loadingOperation.progress;
            progress = Mathf.Clamp01(progress / 0.9f);
            _loadingScreen.SetLoadingProgress(progress);
            yield return null;
        }
    }
}