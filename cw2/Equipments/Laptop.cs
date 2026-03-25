
public class Laptop : Equipment
{
    public string operatingSystem;
    public int batteryLifeHours;
    public Laptop(string name, string operatingSystem, int batteryLifeHours) : base(name)
    {
        this.operatingSystem = operatingSystem;
        this.batteryLifeHours = batteryLifeHours;
    }
}