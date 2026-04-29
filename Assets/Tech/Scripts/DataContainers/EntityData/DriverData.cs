using UnityEngine;

public struct DriverData
{
    public DriverData(string name, string licensePlateNumber, Gender gender, Sprite picture, bool isPictureFake, bool isNameFake, bool isNumberFake)
    {
        Name = name;
        LicensePlateNumber = licensePlateNumber;
        Gender = gender;
        Picture = picture;
        IsPictureFake = isPictureFake;
        IsNameFake = isNameFake;
        IsNumberFake = isNumberFake;
    }

    public string Name { get; private set; }
    public string LicensePlateNumber { get; private set; }
    public Gender Gender { get; private set; }
    public Sprite Picture { get; private set; }
    public bool IsPictureFake { get; private set; }
    public bool IsNameFake { get; private set; }
    public bool IsNumberFake { get; private set; }

    public void SetNumberFake(bool value)
    {        
        IsNumberFake = value;
    }
}