public struct StreetPatrolOrderData : IOrderData
{
    public StreetPatrolOrderData(CrimeType crimeTypeChosen, Pedestrian pedestrian)
    {
        CrimeTypeChosen = crimeTypeChosen;
        PedestrianCrimeType = pedestrian.PedestrianData.CrimeType;
    }

    public CrimeType CrimeTypeChosen { get; private set; }
    public CrimeType PedestrianCrimeType { get; private set; }    

    public bool IsOrderCorrect()
    {
        return CrimeTypeChosen == PedestrianCrimeType;
    }
}