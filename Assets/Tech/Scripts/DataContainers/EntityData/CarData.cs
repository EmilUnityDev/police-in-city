public struct CarData
{
    public CarData(bool isTooFast, bool isArrestWorthy, string licensePlateNumber, int lane, int speed)
    {
        IsTooFast = isTooFast;
        IsArrestWorthy = isArrestWorthy;
        LicensePlateNumber = licensePlateNumber;
        Lane = lane;
        Speed = speed;
    }

    public bool IsTooFast { get; private set; }
    public bool IsArrestWorthy { get; private set; }
    public string LicensePlateNumber { get; private set; }
    public int Lane { get; private set; }
    public int Speed { get; private set; }

    public void SetIsTooFast(bool value)
    {
        IsTooFast = value;
    }
}