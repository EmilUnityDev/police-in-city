using System.Collections.Generic;
using UnityEngine;

public class TutorialPersonFactory : AbstractPersonFactory
{
    [SerializeField] private PersonDB _personDB;
    [SerializeField] private NameDB _nameDB;
    [SerializeField] private LicensePlateNumberDB _licensePlateNumberDB;
    [SerializeField] private Driver TESTDRIVER;

    private PersonSO[] _persons;
    private ObjectPool<Driver> _driverPool;
    private bool _isInit;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        if (_isInit) return;
        _driverPool = new ObjectPool<Driver>(TESTDRIVER, transform, 4);
        _persons = _personDB.Persons;
        _isInit = true;
    }

    public override Driver GetDriver()
    {
        if (!_isInit) Init();

        int N = _persons.Length;
        int i = Random.Range(0, N);
        PersonSO person = _persons[i];
        Gender gender = person.Gender;
        string name = _nameDB.GetRandomNameByGender(gender);
        string licensePlateNumber = _licensePlateNumberDB.GetRandomLicensePlateNumber();

        bool isArrestWorthy = false;
        isArrestWorthy = isArrestWorthy.RandomBool(7);

        Sprite picture = _persons[i].Picture;

        if (isArrestWorthy)
        {
            List<Sprite> sprites = new List<Sprite>();
            foreach (var p in _persons)
            {
                if (p == person) continue;
                sprites.Add(p.Picture);
            }
            int M = sprites.Count;
            int j = Random.Range(0, M);
            picture = sprites[j];
        }

        DriverData driverData = new DriverData(name, licensePlateNumber, gender, picture, isArrestWorthy, isArrestWorthy, false);

        Driver driver = _driverPool.GetFromPool();
        driver.transform.SetParent(null);
        int childCount = driver.transform.childCount;
        if (childCount > 0)
        {
            for (int c = 0; c < childCount; c++)
            {
                DestroyImmediate(driver.transform.GetChild(c).gameObject);
            }
        }


        GameObject personMesh = Instantiate(person.Mesh, driver.transform);
        personMesh.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        personMesh.transform.localScale = new Vector3(0.45f, 0.45f, 0.45f);
        personMesh.transform.localEulerAngles = Vector3.zero;


        driver.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);

        driver.Init(driverData);
        driver.gameObject.SetActive(true);

        return driver;
    }
}