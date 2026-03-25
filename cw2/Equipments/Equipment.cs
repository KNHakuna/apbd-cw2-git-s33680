

public class Equipment
{
    public int Id { get; set; }
    public string Name { get; set; }
    public EquipmentStatus Status { get; set; }
    public string UnavailableReason { get; set; }

    private static int nextId = 1;
    public Equipment(string name)
    {
        Id = nextId;
        nextId++;
        Name = name;
        Status = EquipmentStatus.Available;
    }
}
