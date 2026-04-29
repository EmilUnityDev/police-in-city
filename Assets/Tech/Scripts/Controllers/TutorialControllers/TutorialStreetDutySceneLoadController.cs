public class TutorialStreetDutySceneLoadController : SceneLoadController
{    
    public override void Next()
    {              
        _sceneLoader.LoadRoadDutyTutorial();
    }

    public override void Reload()
    {       
        _sceneLoader.LoadStreetDutyTutorial();
    }
}