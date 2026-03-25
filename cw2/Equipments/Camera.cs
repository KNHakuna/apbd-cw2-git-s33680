
public class Camera : Equipment
{
    public bool hasAdditionalBattery;
    public string cameraType;
    public Camera(string name, bool hasAdditionalBattery, string cameraType) : base(name)
    {
        this.hasAdditionalBattery = hasAdditionalBattery;
        this.cameraType = cameraType;
    }
}