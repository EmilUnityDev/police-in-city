using UnityEngine;

public abstract class AbstractSaveLoadHandler : MonoBehaviour
{
    [SerializeField] protected StreetDutyData _streetDutyData;
    [SerializeField] protected RoadSceneGameData _roadDutyData;
    [SerializeField] protected TutorialGameData _tutorialData;

    protected static SaveData _saveData;
    protected bool _isInit;

    public abstract void LoadData();
    public abstract void ResetData();
    public abstract void ClearData();

    public virtual void SaveDataToJSON()
    {
        _saveData?.Save(_streetDutyData, _roadDutyData, _tutorialData);
        SaveDataToJSONHandler.Save(_saveData);
    }

    public virtual SaveData GetSaveData()
    {
        return _saveData;
    }

    public virtual void OnApplicationPause(bool pause)
    {
        if (pause)
            SaveDataToJSON();
    }
    public virtual void OnApplicationFocus(bool focus)
    {
        if (!focus)
            SaveDataToJSON();
    }
}