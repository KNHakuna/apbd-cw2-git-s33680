
public class VRHeadset : Equipment
{
    public bool isStandaloneDevice;
    public int installedAppsCount;
    public VRHeadset(string name, bool isStandaloneDevice, int installedAppsCount) : base(name)
    {
        this.isStandaloneDevice = isStandaloneDevice;
        this.installedAppsCount = installedAppsCount;
    }
}