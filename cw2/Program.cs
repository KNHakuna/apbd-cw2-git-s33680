
class Program
{
    public static void Main(string[] args)
    {
        var manager = new RentalManager();
        var equipment1 = new Equipment("Camera");
        var test1 = manager.RentEquipment(equipment1);
        Console.WriteLine(equipment1.Status);
        manager.ReturnEquipment(test1);
        Console.WriteLine(equipment1.Status);
        manager.ReturnEquipment(test1);
        Console.WriteLine(equipment1.Status);
    }
}