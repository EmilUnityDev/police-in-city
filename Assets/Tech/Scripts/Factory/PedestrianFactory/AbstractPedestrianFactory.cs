using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPedestrianFactory : MonoBehaviour
{
    public bool IsInit
    {
        get => _isInit;
    }

    [SerializeField] protected DescriptionDB _notGuiltyDescriptions, _arrestWorthyDescriptions, _shockWorthyDescriptions;
    [SerializeField] protected NameDB _namesDB;

    protected Dictionary<CrimeType, DescriptionDB> _descriptionDBsByCrimeType;
    protected bool _isInit;

    public virtual void Init()
    {
        if (_isInit) return;
        _descriptionDBsByCrimeType = new Dictionary<CrimeType, DescriptionDB>()
        {
            [CrimeType.NotGuilty] = _notGuiltyDescriptions,
            [CrimeType.ArrestWorthy] = _arrestWorthyDescriptions,
            [CrimeType.ShockWorthy] = _shockWorthyDescriptions
        };
        _isInit = true;
    }

    public abstract void GeneratePedestrians(int notGuiltyCount, int arrestGuiltyCount, int shockWorthyCount);
    public abstract Pedestrian GetNotGuiltyPedestrian();
    public abstract Pedestrian GetArrestWorthyPedestrian();
    public abstract Pedestrian GetShockWorthyPedestrian();
    public abstract Pedestrian GetPedestrian(CrimeType crimeType);
    public abstract List<Pedestrian> GetActivePedestrians();
}