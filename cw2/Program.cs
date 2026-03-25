

class Program
{
    public static void Main(string[] args)
    {
        var employee = new User("anna", "Kowalczyk", UserType.Employee);
        var student = new User("jan", "Nowak", UserType.Student);

        var vr = new VRHeadset("Oculus Quest 2", false, 120);
        var hoverboard = new Hoverboard("Hoverboard X", 25, 60);
        var drone = new Drone("DJI Mavic Air 2", true, 34);
        var rentalManager = new RentalManager();
        var test1 = rentalManager.RentEquipment(employee, vr);
        Console.WriteLine($"Rental created: {test1.Equipment.Name} rented by {test1.User.FirstName} {test1.User.LastName} on {test1.RentalDate}");

        var test2 = rentalManager.RentEquipment(student, hoverboard);
        Console.WriteLine($"Rental created: {test2.Equipment.Name} rented by {test2.User.FirstName} {test2.User.LastName} on {test2.RentalDate}");

        var test3 = rentalManager.RentEquipment(student, drone);
        var test4 = rentalManager.RentEquipment(student, vr);
        Console.WriteLine($"Rental created: {test3.Equipment.Name} rented by {test3.User.FirstName} {test3.User.LastName} on {test3.RentalDate}");

        rentalManager.ReturnEquipment(test3);
        test2.Deadline = DateTime.Now.AddDays(-5);
        rentalManager.ReturnEquipment(test2);

    }
}