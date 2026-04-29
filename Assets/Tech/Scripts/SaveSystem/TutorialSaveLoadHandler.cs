public class TutorialSaveLoadHandler : AbstractSaveLoadHandler
{
    public override void ClearData()
    {
        _tutorialData.SetValues(false, false, false, false);
    }

    public override void LoadData()
    {
        
    }

    public override void OnApplicationFocus(bool focus)
    {
        base.OnApplicationFocus(focus);
    }

    public override void OnApplicationPause(bool pause)
    {
        base.OnApplicationPause(pause);
    }

    public override void ResetData()
    {
        _tutorialData.SetValues(false, false, false, false);
    }
}