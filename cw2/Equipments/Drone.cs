
public class Drone : Equipment
{
    public bool requiresLicense;
    public int maxFlightTimeMinutes;
    public Drone(string name, bool requiresLicense, int maxFlightTimeMinutes) : base(name)
    {
        this.requiresLicense = requiresLicense;
        this.maxFlightTimeMinutes = maxFlightTimeMinutes;
    }
}