
public class Hoverboard : Equipment
{
    public int maxSpeedKmh;
    public int maxWorkTimeMinutes;
    public Hoverboard(string name, bool maxSpeedKmh, int maxWorkTimeMinutes) : base(name)
    {
        this.maxSpeedKmh = maxSpeedKmh;
        this.maxWorkTimeMinutes = maxWorkTimeMinutes;
    }
}