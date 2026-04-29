public class StreetDutySceneLoadController : SceneLoadController
{ 
    public override void Next()
    {        
        _sceneLoader.LoadNextRoadDutyScene();
    }

    public override void Reload()
    {        
        _sceneLoader.Reload();
    }
}