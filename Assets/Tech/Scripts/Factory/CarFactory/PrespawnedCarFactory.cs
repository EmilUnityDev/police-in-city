using System.Collections.Generic;
using UnityEngine;

public class PrespawnedCarFactory : MonoBehaviour
{
    [SerializeField] private Car[] _carPrefabs;
    [SerializeField] private NameDB _nameDb;
    [SerializeField] private AbstractPersonFactory _personFactory;
    [SerializeField] private Transform _maxZLane0, _maxZLane1, _minZ, _lane0SpawnPos, _lane1SpawnPos;
    [SerializeField] private LicensePlateNumberDB _licensePlateNumberDB;
    [SerializeField] private GameObject[] _dangerousTrunkSuff;    

    private List<Car> _carPool;
        
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

        CreateCarPool();

        _timeBetweenSpawns = 6;
        _isSpawning = true;
        _isInit = true;
    }

    private void Spawn()
    {
        if (!_isInit) Init();

        bool isTooFast = false;
        isTooFast = isTooFast.RandomBool(5);

        Car car = GetRandomOrNextCar();

        Driver driver = _personFactory.GetDriver();
        DriverData driverData = driver.DriverData;
        bool isArrestWorthy = driverData.IsPictureFake || driverData.IsNameFake;

        string licensePlateNumber;        

        if (isArrestWorthy)
        {
            licensePlateNumber = _licensePlateNumberDB.GetRandomLicensePlateNumber();

            DriverData temp = driver.DriverData;
            driver.DriverData = new DriverData(temp.Name, temp.LicensePlateNumber, temp.Gender, temp.Picture, temp.IsPictureFake, temp.IsNameFake, true);

        }
        else
        {
            licensePlateNumber = driver.DriverData.LicensePlateNumber;
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
        if (_dangerousTrunkSuff == null) return;

        int N = _dangerousTrunkSuff.Length;
        int r = Random.Range(0, N);
        GameObject trunkStuff = _dangerousTrunkSuff[r];
        trunkStuff.transform.position = car.TrunkStuffPlace.position;
        trunkStuff.transform.rotation = Quaternion.identity;
        trunkStuff.SetActive(true);
    }

    private void SpawnRegularTrunkStuff(Car car)
    {

    }

    private void OnCarInspected(Car car)
    {
        _isSpawning = false;
        List<Car> activeCars = GetActiveCars();
        int N = activeCars.Count;
        for (int i = 0; i < N; i++)
        {
            if (activeCars[i] == car)
                continue;
            else
                activeCars[i].ReturnToPool();
        }

        if (!car.CarData.IsArrestWorthy) return;

        bool isTrunkEmpty = false;

        isTrunkEmpty = isTrunkEmpty.RandomBool(0);        

        if (!isTrunkEmpty)
        {
            SpawnDangerousTrunkStuff(car);
        }
    }

    private int _lastIndex = -1;

    private void CreateCarPool()
    {
        if (_carPrefabs == null) return;
        int N = _carPrefabs.Length;

        _carPool = new List<Car>(N);

        for (int i = 0; i < N; i++)
        {
            Car car = Instantiate(_carPrefabs[i]);
            _carPool.Add(car);
            _carPrefabs[i].gameObject.SetActive(false);
            car.gameObject.SetActive(false);
        }
    }

    private Car GetRandomOrNextCar()
    {
        if (_carPool == null) return null;
        var inactiveCars = GetInactiveCars();
        int N = inactiveCars.Count;

        int nextIndex = _lastIndex + 1;
        if (nextIndex >= N) nextIndex = 0;

        if (N <= 0)
        {
            nextIndex = _lastIndex - 1;
            if (nextIndex < 0) nextIndex = _lastIndex + 1;
            if (nextIndex >= _carPrefabs.Length) nextIndex = _carPrefabs.Length - 1;
            Car newCar = Instantiate(_carPrefabs[nextIndex]);
            _carPool.Add(newCar);
            _lastIndex = -1;
            return newCar;
        }

        int randomIndex = Random.Range(0, N);
        if (randomIndex == _lastIndex) randomIndex = nextIndex;

        _lastIndex = randomIndex;

        return inactiveCars[randomIndex];
    }

    private List<Car> GetActiveCars()
    {
        if (_carPool == null) return null;
        int N = _carPool.Count;

        List<Car> carsToReturn = new List<Car>(N);

        for (int i = 0; i < N; i++)
        {
            if (_carPool[i].gameObject.activeInHierarchy)
            {
                carsToReturn.Add(_carPool[i]);
            }
        }

        return carsToReturn;
    }

    private List<Car> GetInactiveCars()
    {
        if (_carPool == null) return null;
        int N = _carPool.Count;

        List<Car> carsToReturn = new List<Car>(N);

        for (int i = 0; i < N; i++)
        {
            if (!_carPool[i].gameObject.activeInHierarchy)
            {
                carsToReturn.Add(_carPool[i]);
            }
        }

        return carsToReturn;
    }
}