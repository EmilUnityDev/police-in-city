using System.Collections.Generic;
using UnityEngine;

public class PooledCarFactory : MonoBehaviour
{
    [SerializeField] private Car _carPrefab;
    [SerializeField] private NameDB _nameDb;
    [SerializeField] private PersonFactory _personFactory;
    [SerializeField] private Transform _maxZLane0, _maxZLane1, _minZ, _lane0SpawnPos, _lane1SpawnPos;
    [SerializeField] private LicensePlateNumberDB _licensePlateNumberDB;

    private ObjectPool<Car> _carPool;
    private float _timeBetweenSpawns, _lastSpawnTime;

    private bool _isInit;
    private bool _isSpawning;

    private void Awake()
    {
        Init();
        Spawn();
        PoliceCarInspectState.CarInspected += OnCarInspected;
    }

    private void OnDisable()
    {
        PoliceCarInspectState.CarInspected -= OnCarInspected;
    }

    private void Update()
    {
        if (_lastSpawnTime + _timeBetweenSpawns < Time.time
            && _isSpawning)
        {
            Spawn();
        }
    }

    private void Init()
    {
        if (_isInit) return;
        _carPool = new ObjectPool<Car>(_carPrefab, null, 4);
        _timeBetweenSpawns = 6;
        _isSpawning = true;
        _isInit = true;
    }

    private void Spawn()
    {
        if (!_isInit) Init();

        bool isTooFast = false;
        isTooFast = isTooFast.RandomBool(5); 

        Car car = _carPool.GetFromPool();        

        Driver driver = _personFactory.GetDriver();
        DriverData driverData = driver.DriverData;
        bool isArrestWorthy = driverData.IsPictureFake || driverData.IsNameFake;

        string licensePlateNumber;

        bool isTrunkEmpty = true;

        if (isArrestWorthy)
        {
            licensePlateNumber = _licensePlateNumberDB.GetRandomLicensePlateNumber();
            
            DriverData temp = driver.DriverData;
            driver.DriverData = new DriverData(temp.Name, temp.LicensePlateNumber, temp.Gender, temp.Picture, temp.IsPictureFake, temp.IsNameFake, true);

            isTrunkEmpty = isTrunkEmpty.RandomBool(5);

            if (!isTrunkEmpty)
            {
                SpawnDangerousTrunkStuff(car);
            }
        }
        else
        {            
            licensePlateNumber = driver.DriverData.LicensePlateNumber;
            isTrunkEmpty = isTrunkEmpty.RandomBool(5);

            if (!isTrunkEmpty)
            {
                SpawnRegularTrunkStuff(car);
            }
        }

        int speed = isTooFast ? Random.Range(31, 41) : Random.Range(20, 31);
        int lane = Random.Range(0, 2);

        car.transform.position = lane == 0 ? _lane0SpawnPos.position : _lane1SpawnPos.position;
        
        
        CarData carData = new CarData(isTooFast, isArrestWorthy, licensePlateNumber, lane, speed);


        car.Init(carData, driver);

        car.MinZPos = _minZ;
        car.MaxZPos = lane == 0 ? _maxZLane0 : _maxZLane1;

        car.gameObject.SetActive(true);

        _lastSpawnTime = Time.time;        
    }

    private void SpawnDangerousTrunkStuff(Car car)
    {

    }

    private void SpawnRegularTrunkStuff(Car car)
    {

    }

    private void OnCarInspected(Car car)
    {
        _isSpawning = false;
        List<Car> activeCars = _carPool.GetActive();
        int N = activeCars.Count;
        for (int i = 0; i < N; i++)
        {
            if (activeCars[i] == car)
                continue;
            else
                activeCars[i].ReturnToPool();
        }
    }
}