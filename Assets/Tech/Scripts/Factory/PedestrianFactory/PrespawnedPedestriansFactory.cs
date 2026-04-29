using System.Collections.Generic;
using UnityEngine;

public class PrespawnedPedestriansFactory : AbstractPedestrianFactory
{
    [SerializeField] private List<Pedestrian> _preSpawnedPedestrians;
    [SerializeField] private StreetDutyData _streetDutyData;

    private List<string> _detainedPedestriansNames;
    private List<Pedestrian> _spawnedPedestrians;

    public override void Init()
    {
        base.Init();
    }

    public override void GeneratePedestrians(int notGuiltyCount, int arrestGuiltyCount, int shockWorthyCount)
    {
        if (!_isInit) Init();

        _detainedPedestriansNames = _streetDutyData.ScannedPedestriansNames;
        _spawnedPedestrians = new List<Pedestrian>();

        if (_preSpawnedPedestrians == null || _preSpawnedPedestrians.Count == 0)
        {
            _preSpawnedPedestrians = new List<Pedestrian>();
            Pedestrian[] pedestrians = FindObjectsOfType<Pedestrian>();
            if (pedestrians == null || pedestrians.Length == 0)
            {
                return;
            }

            int pedestrianCount = pedestrians.Length;
            for (int p = 0; p < pedestrianCount; p++)
            {
                _preSpawnedPedestrians.Add(pedestrians[p]);
            }
        }

        int N = _preSpawnedPedestrians.Count;

        int pedestriansCount = 0;

        for (int i = 0; i < notGuiltyCount && pedestriansCount < N; i++)
        {
            GetNotGuiltyPedestrian();
            pedestriansCount++;
            
        }
        for (int i = 0; i < arrestGuiltyCount && pedestriansCount < N; i++)
        {
            GetArrestWorthyPedestrian();
            pedestriansCount++;
        }
        for (int i = 0; i < shockWorthyCount && pedestriansCount < N; i++)
        {
            GetShockWorthyPedestrian();
            pedestriansCount++;
        }

        int pedestriansLeft = _preSpawnedPedestrians.Count;
        for (int i = 0; i < pedestriansLeft; i++)
        {
            _preSpawnedPedestrians[i].gameObject.SetActive(false);
        }
    }

    public override Pedestrian GetNotGuiltyPedestrian()
    {
        return GetPedestrian(CrimeType.NotGuilty);
    }

    public override Pedestrian GetArrestWorthyPedestrian()
    {
        return GetPedestrian(CrimeType.ArrestWorthy);
    }

    public override Pedestrian GetShockWorthyPedestrian()
    {
        return GetPedestrian(CrimeType.ShockWorthy);
    }

    public override Pedestrian GetPedestrian(CrimeType crimeType)
    {
        if (_preSpawnedPedestrians.Count <= 0) return null;
        Pedestrian p = _preSpawnedPedestrians[0];
        Gender gender = p.Gender;
        string[] names = _namesDB.NamesByGender(gender);
        string name = names[Random.Range(0, names.Length)];
        DescriptionDB descriptionDB = _descriptionDBsByCrimeType[crimeType];
        string description = descriptionDB.GetNextDescription();
          
        p.InitPedestrian(name, description, crimeType, gender);

        if (p.HasBeenDetained && p.HasBeenArrestedOrShocked) p.gameObject.SetActive(false);
        _preSpawnedPedestrians.Remove(p);
        _spawnedPedestrians.Add(p);
        return p;
    }

    private bool HasBeenDetained(Pedestrian pedestrian)
    {
        if (_detainedPedestriansNames == null) return false;

        string name = pedestrian.name;

        return _detainedPedestriansNames.Contains(name);
    }

    public override List<Pedestrian> GetActivePedestrians()
    {
        List<Pedestrian> activePedestrians = new List<Pedestrian>();
        foreach (var p in _spawnedPedestrians)
        {
            if (p.gameObject.activeInHierarchy) activePedestrians.Add(p);
        }

        return activePedestrians;
    }
}