public struct RoadDutyOrderData : IOrderData
{
    public RoadDutyOrderData(CarCrimeType crimeTypeChosen, CarData carData)
    {
        CrimeTypeChosen = crimeTypeChosen;
        IsTooFast = carData.IsTooFast;
        IsArrestWorthy = carData.IsArrestWorthy;
        
    }

    public CarCrimeType CrimeTypeChosen { get; private set; }
    public bool IsTooFast { get; private set; }
    public bool IsArrestWorthy { get; private set; }
    
    public bool IsOrderCorrect()
    {
        switch (CrimeTypeChosen)
        {
            case CarCrimeType.NotGuilty:
                return !IsTooFast && !IsArrestWorthy;
            case CarCrimeType.FineWorthy:
                return IsTooFast;
            case CarCrimeType.ArrestWorthy:
                return IsArrestWorthy;
            default:
                return false;
        }
    }
}