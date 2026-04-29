public struct PedestrianData
{
    public PedestrianData(string name, string description, CrimeType crimeType)
    {
        Name = name;
        Description = description;
        CrimeType = crimeType;
    }

    public string Name { get; private set; }
    public string Description { get; private set; }
    public CrimeType CrimeType { get; private set; }
}