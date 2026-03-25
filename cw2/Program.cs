


class Program
{

    static bool BooleanPrompt(string message)
    {
        while (true)
        {
            Console.Write(message + " [0/1]: ");
            var input = Console.ReadLine();
            if (input == "1")
            {
                return true;
            }
            if (input == "0")
            {
                return false;
            }
            Console.WriteLine("Invalid input. Try again.");
        }
    }
    static int IntHelper(string message)
    {
        while (true)
        {
            Console.WriteLine(message + ": ");
            if (int.TryParse(Console.ReadLine(), out int value))
            {
                return value;
            }
            Console.WriteLine("Invalid input. Try again.");
        }
    }


    static void AddUser(RentalManager manager)
    {
        Console.WriteLine("Adding new user:");
        Console.Write("Enter first name: ");
        string firstName = Console.ReadLine();
        Console.Write("Enter last name: ");
        string lastName = Console.ReadLine();
        string input;
        while (true)
        {
            Console.Write("Enter user type [1] Student or [2] Employee: ");
            input = Console.ReadLine();
            if (input == "1" || input == "2")
            {
                break;
            }
            Console.WriteLine("Invalid user type. Try again.");
        }
        var type = input == "1" ? UserType.Student : UserType.Employee;
        var user = new User(firstName, lastName, type);
        manager.AddUser(user);
        Console.WriteLine("User added successfully.");
    }

    static void ShowUsers(RentalManager manager)
    {
        Console.WriteLine("ID | Name | Type");
        foreach (var user in manager.GetAllUsers())
        {
            Console.WriteLine($"{user.Id} | {user.FirstName} {user.LastName} | {user.Type}");
        }
    }

    static void AddEquipment(RentalManager manager)
    {
        Console.WriteLine("Adding new equipment:");
        Console.WriteLine("Enter equipment name: ");
        string name = Console.ReadLine();
        while (true)
        {
            Console.WriteLine("Enter equipment type: ");
            Console.WriteLine("[1] Camera");
            Console.WriteLine("[2] Drone");
            Console.WriteLine("[3] Hoverboard");
            Console.WriteLine("[4] Laptop");
            Console.WriteLine("[5] VRHeadset");
            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    bool hasAdditionalBattery = BooleanPrompt("Does it have an additional battery?");
                    string cameraType;
                    Console.WriteLine("Enter camera type: ");
                    cameraType = Console.ReadLine();
                    var camera = new Camera(name, hasAdditionalBattery, cameraType);
                    manager.AddEquipment(camera);
                    Console.WriteLine("Camera added successfully.");
                    return;
                case "2":
                    bool requiresLicense = BooleanPrompt("Does it require a license?");
                    int maxFlightTimeMinutes = IntHelper("Enter max flight time in minutes");
                    var drone = new Drone(name, requiresLicense, maxFlightTimeMinutes);
                    manager.AddEquipment(drone);
                    Console.WriteLine("Drone added successfully.");
                    return;
                case "3":
                    int maxSpeedKmh = IntHelper("Enter max speed in km/h");
                    int maxWorkTimeMinutes = IntHelper("Enter max work time in minutes");
                    var hoverboard = new Hoverboard(name, maxSpeedKmh, maxWorkTimeMinutes);
                    manager.AddEquipment(hoverboard);
                    Console.WriteLine("Hoverboard added successfully.");
                    return;
                case "4":
                    string operatingSystem;
                    Console.WriteLine("Enter operating system: ");
                    operatingSystem = Console.ReadLine();
                    int batteryLifeHours = IntHelper("Enter battery life in hours");
                    var laptop = new Laptop(name, operatingSystem, batteryLifeHours);
                    manager.AddEquipment(laptop);
                    Console.WriteLine("Laptop added successfully.");
                    return;
                case "5":
                    bool isStandaloneDevice = BooleanPrompt("Is it a standalone device?");
                    int installedAppsCount = IntHelper("Enter number of installed apps");
                    var vrHeadset = new VRHeadset(name, isStandaloneDevice, installedAppsCount);
                    manager.AddEquipment(vrHeadset);
                    Console.WriteLine("VR Headset added successfully.");
                    return;
                default:
                    Console.WriteLine("Invalid equipment type. Try again.");
                    continue;
            }
        }
    }

    static void ShowAllEquipment(RentalManager manager)
    {
        Console.WriteLine("ID | Name | Status");
        foreach (var item in manager.GetAllEquipments())
        {
            Console.WriteLine($"{item.Id} | {item.Name} | {item.Status}");
        }
    }

    static void ShowAvailableEquipment(RentalManager manager)
    {
        Console.WriteLine("ID | Name | Status");
        foreach (var item in manager.GetAvailableEquipments())
        {
            Console.WriteLine($"{item.Id} | {item.Name} | {item.Status}");
        }
    }

    static void RentEquipment(RentalManager manager)
    {
        Console.WriteLine("Renting equipment:");
        int userId = IntHelper("Enter user ID");
        int equipmentId = IntHelper("Enter equipment ID");
        var user = manager.GetAllUsers().FirstOrDefault(u => u.Id == userId);
        var equipment = manager.GetAllEquipments().FirstOrDefault(e => e.Id == equipmentId);
        if (user == null || equipment == null)
        {
            Console.WriteLine("Invalid user or equipment ID.");
            return;
        }
        manager.RentEquipment(user, equipment);
        Console.WriteLine("Equipment rented successfully.");
    }

    static void ReturnEquipment(RentalManager manager)
    {
        Console.WriteLine("Returning equipment:");
        int rentalId = IntHelper("Enter rental ID");
        var rental = manager.GetAllRentals().FirstOrDefault(r => r.Id == rentalId);
        manager.ReturnEquipment(rental);
        Console.WriteLine("Equipment returned successfully.");
    }

    static void MarkEquipmentUnavailable(RentalManager manager)
    {
        Console.WriteLine("Marking equipment as unavailable:");
        int id = IntHelper("Enter equipment ID");
        var equipment = manager.GetAllEquipments().FirstOrDefault(e => e.Id == id);
        if (equipment == null)
        {
            Console.WriteLine("Invalid equipment ID.");
            return;
        }
        string reason;
        Console.WriteLine("What is the reason: ");
        reason = Console.ReadLine();
        manager.MarkEquipmentUnavailable(equipment);
        Console.WriteLine("Equipment marked as unavailable successfully.");
    }

    static void ShowUserRentals(RentalManager manager)
    {
        Console.WriteLine("Showing user rentals:");
        int userId = IntHelper("Enter user ID");
        var user = manager.GetAllUsers().FirstOrDefault(u => u.Id == userId);
        if (user == null)
        {
            Console.WriteLine("Invalid user ID.");
            return;
        }
        var rentals = manager.GetUserRentals(user);
        foreach (var rental in rentals)
        {
            Console.WriteLine($"{rental.Id} | {rental.Equipment.Name} | {rental.RentalDate} | {rental.ReturnDate}");
        }

    }

    static void ShowOverdueRentals(RentalManager manager)
    {
        Console.WriteLine("Showing overdue rentals:");
        var overdueRentals = manager.GetOverdueRentals();
        foreach (var rental in overdueRentals)
        {
            Console.WriteLine($"{rental.Id} | {rental.Equipment.Name} | {rental.User.FirstName} {rental.User.LastName} | {rental.RentalDate}");
        }
    }

    static void PrintReport(RentalManager manager)
    {
        manager.PrintReport();
    }

    static void DemonstrationScenario(RentalManager manager)
    {
        Console.WriteLine("|*|*|*| Demonstration scenario |*|*|*|");
        var student = new User("Willy", "Wonka", UserType.Student);
        Console.WriteLine($"Created user: {student.FirstName} {student.LastName}, Type: {student.Type}");
        var employee = new User("Chuck", "Norris", UserType.Employee);
        Console.WriteLine($"Created user: {employee.FirstName} {employee.LastName}, Type: {employee.Type}");
        manager.AddUser(student);
        manager.AddUser(employee);

        var camera = new Camera("Canon EOS R5", true, "Mirrorless");
        Console.WriteLine($"Created equipment: {camera.Name}, Type: Camera, Has additional battery: {camera.hasAdditionalBattery}, Camera type: {camera.cameraType}");
        var drone = new Drone("DJI Mavic Air 2", true, 34);
        Console.WriteLine($"Created equipment: {drone.Name}, Type: Drone, Requires license: {drone.requiresLicense}, Max flight time: {drone.maxFlightTimeMinutes} minutes");
        var hoverboard = new Hoverboard("Segway Ninebot S", 20, 120);
        Console.WriteLine($"Created equipment: {hoverboard.Name}, Type: Hoverboard, Max speed: {hoverboard.maxSpeedKmh} km/h, Max work time: {hoverboard.maxWorkTimeMinutes} minutes");
        var laptop = new Laptop("Dell XPS 15", "Windows 10", 10);
        Console.WriteLine($"Created equipment: {laptop.Name}, Type: Laptop, Operating system: {laptop.operatingSystem}, Battery life: {laptop.batteryLifeHours} hours");
        var vrHeadset = new VRHeadset("Oculus Quest 2", true, 50);
        Console.WriteLine($"Created equipment: {vrHeadset.Name}, Type: VR Headset, Is standalone device: {vrHeadset.isStandaloneDevice}, Installed apps count: {vrHeadset.installedAppsCount}");
        manager.AddEquipment(camera);
        manager.AddEquipment(drone);
        manager.AddEquipment(hoverboard);
        manager.AddEquipment(laptop);
        manager.AddEquipment(vrHeadset);

        var rental1 = manager.RentEquipment(student, camera);
        Console.WriteLine($"Rented {rental1.Equipment.Name} to {rental1.User.FirstName} {rental1.User.LastName} on {rental1.RentalDate}");
        var rental2 = manager.RentEquipment(employee, drone);
        Console.WriteLine($"Rented {rental2.Equipment.Name} to {rental2.User.FirstName} {rental2.User.LastName} on {rental2.RentalDate}");
        var rental3 = manager.RentEquipment(student, hoverboard);
        Console.WriteLine($"Rented {rental3.Equipment.Name} to {rental3.User.FirstName} {rental3.User.LastName} on {rental3.RentalDate}");
        var rental4 = manager.RentEquipment(employee, laptop);
        Console.WriteLine($"Rented {rental4.Equipment.Name} to {rental4.User.FirstName} {rental4.User.LastName} on {rental4.RentalDate}");
        var rental5 = manager.RentEquipment(employee, vrHeadset);
        Console.WriteLine($"Rented {rental5.Equipment.Name} to {rental5.User.FirstName} {rental5.User.LastName} on {rental5.RentalDate}");
        var rental6 = manager.RentEquipment(student, vrHeadset);
        manager.ReturnEquipment(rental5);

        manager.ReturnEquipment(rental1);

        rental6 = manager.RentEquipment(student, vrHeadset);
        Console.WriteLine($"Rented {rental6.Equipment.Name} to {rental6.User.FirstName} {rental6.User.LastName} on {rental6.RentalDate}");

        rental2.Deadline = DateTime.Now.AddDays(-5);
        manager.ReturnEquipment(rental2);
        Console.WriteLine($"Returned {rental2.Equipment.Name} from {rental2.User.FirstName} {rental2.User.LastName} on {rental2.ReturnDate}. Penalty: {rental2.Penalty:C}");

        manager.MarkEquipmentUnavailable(camera);
        manager.RentEquipment(employee, camera);

        manager.PrintReport();
    }

    public static void Main(string[] args)
    {
        var manager = new RentalManager();
        Console.WriteLine("|*|*|*|*|*| Welcome to the school equipment rental |*|*|*|*|*|");
        Console.WriteLine("How can I help you? Press specific button");
        while (true)
        {
            Console.WriteLine(" [1] Add user");
            Console.WriteLine(" [2] Show users");
            Console.WriteLine(" [3] Add equipment");
            Console.WriteLine(" [4] Show all equipment");
            Console.WriteLine(" [5] Show available equipment");
            Console.WriteLine(" [6] Rent equipment");
            Console.WriteLine(" [7] Return equipment");
            Console.WriteLine(" [8] Mark equipment unavailable");
            Console.WriteLine(" [9] Show user rentals");
            Console.WriteLine(" [10] Show overdue rentals");
            Console.WriteLine(" [11] Print raport");
            Console.WriteLine(" [12] Demonstration scenario");
            Console.WriteLine(" [X] Exit");

            Console.Write("Your choice: ");
            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    AddUser(manager);
                    break;
                case "2":
                    ShowUsers(manager);
                    break;
                case "3":
                    AddEquipment(manager);
                    break;
                case "4":
                    ShowAllEquipment(manager);
                    break;
                case "5":
                    ShowAvailableEquipment(manager);
                    break;
                case "6":
                    RentEquipment(manager);
                    break;
                case "7":
                    ReturnEquipment(manager);
                    break;
                case "8":
                    MarkEquipmentUnavailable(manager);
                    break;
                case "9":
                    ShowUserRentals(manager);
                    break;
                case "10":
                    ShowOverdueRentals(manager);
                    break;
                case "11":
                    PrintReport(manager);
                    break;
                case "12":
                    DemonstrationScenario(manager);
                    break;
                case "X":
                case "x":
                    Console.WriteLine("System closed");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
            Console.WriteLine();
        }
    }
}