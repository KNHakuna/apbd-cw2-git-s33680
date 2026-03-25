

public class RentalManager
{
    private List<Rental> rentals = new List<Rental>();
    private List<Equipment> equipments = new List<Equipment>();
    private List<User> users = new List<User>();


    public Rental RentEquipment(User user, Equipment equipment)
    {
        if(equipment.Status != EquipmentStatus.Available)
        {
            Console.WriteLine($"{equipment.Name} cannot be rented.");
            return null;
        }

        int activeRentals = rentals.Count(r => r.User.Id == user.Id && r.ReturnDate == null);
        if (activeRentals >= GetLimit(user))
        {
            Console.WriteLine($"{user.FirstName} {user.LastName} has reached the rental limit.");
            return null;
        }
        var rental = new Rental(equipment, user);
        equipment.Status = EquipmentStatus.Rented;
        rentals.Add(rental);
        return rental;
    }

    public void ReturnEquipment(Rental rental)
    {
        if(rental == null)
        {
            Console.WriteLine("Invalid rental.");
            return;
        }
        rental.Returning();
        rental.Penalty = GetPenalty(rental);
        rental.Equipment.Status = EquipmentStatus.Available;
        Console.WriteLine($"{rental.Equipment.Name} returned by {rental.User.FirstName} {rental.User.LastName}. Penalty: {rental.Penalty:C}");
    }

    public void MarkEquipmentUnavailable(Equipment equipment)
    {
        if(equipment.Status == EquipmentStatus.Rented)
        {
            Console.WriteLine($"{equipment.Name} is currently rented and cannot be marked as unavailable.");
            return;
        }
        if (equipment.Status == EquipmentStatus.Unavailable)
        {
            Console.WriteLine($"{equipment.Name} is already marked as unavailable due to: {equipment.UnavailableReason}");
            return;
        }

        Console.WriteLine($"Enter reason for marking {equipment.Name} as unavailable:");
        string reason = Console.ReadLine();
        equipment.Status = EquipmentStatus.Unavailable;
        equipment.UnavailableReason = reason;
        Console.WriteLine($"{equipment.Name} is now marked as unavailable due to: {reason}");
    }

    private int GetLimit(User user)
    {
        switch (user.Type)
        {
            case UserType.Student:
                return 2;
            case UserType.Employee:
                return 5;
            default:
                return 0;
        }
    }

    private double GetPenalty(Rental rental)
    {
        if (rental.ReturnDate == null || (rental.ReturnDate <= rental.Deadline))
        {
            return 0;
        }
        var daysLate = (rental.ReturnDate.Value - rental.Deadline).Days;
        return daysLate * 12.50;
    }

    public List<Rental> GetUserRentals(User user)
    {
        return rentals.Where(r => r.User.Id == user.Id).ToList();
    }

    public void AddUser(User user)
    {
        users.Add(user);
    }

    public void AddEquipment(Equipment equipment)
    {
        equipments.Add(equipment);
    }

    public List<User> GetAllUsers()
    {
        return users;
    }

    public List<Rental> GetAllRentals()
    {
        return rentals;
    }

    public List<Equipment> GetAllEquipments() { 
        return equipments;
    }

    public List<Equipment> GetAvailableEquipments()
    {
        return equipments.Where(e => e.Status == EquipmentStatus.Available).ToList();
    }

    public List<Rental> GetOverdueRentals()
    {
        return rentals.Where(r => r.ReturnDate == null && r.Deadline < DateTime.Now).ToList();
    }

    public void PrintReport()
    {
        int totalRentals = rentals.Count;
        int activeRentals = rentals.Count(r => r.ReturnDate == null);
        int returnedRentals = rentals.Count(r => r.ReturnDate != null);
        int overdueRentals = rentals.Count(r => r.ReturnDate == null && r.Deadline < DateTime.Now);
        double totalPenalties = rentals.Sum(r => r.Penalty);
        int totalEquipments = equipments.Count;
        int availableEquipments = equipments.Count(e => e.Status == EquipmentStatus.Available);
        int rentedEquipments = equipments.Count(e => e.Status == EquipmentStatus.Rented);
        int unavailableEquipments = equipments.Count(e => e.Status == EquipmentStatus.Unavailable);
        int totalUsers = users.Count;
        int studentUsers = users.Count(u => u.Type == UserType.Student);
        int employeeUsers = users.Count(u => u.Type == UserType.Employee);
        Console.WriteLine("|*|*|*|*|*|*|*|*| Rental Report: |*|*|*|*|*|*|*|*|");
        Console.WriteLine("1.Rentals");
        Console.WriteLine($" -Total rentals: {totalRentals}");
        Console.WriteLine($" -Active rentals: {activeRentals}");
        Console.WriteLine($" -Returned rentals: {returnedRentals}");
        Console.WriteLine($" -Overdue rentals: {overdueRentals}");
        Console.WriteLine($" -Total penalties: {totalPenalties}");
        Console.WriteLine("2.Equipments");
        Console.WriteLine($" -Total equipments: {totalEquipments}");
        Console.WriteLine($" -Available equipments: {availableEquipments}");
        Console.WriteLine($" -Rented equipments: {rentedEquipments}");
        Console.WriteLine($" -Unavailable equipments: {unavailableEquipments}");
        Console.WriteLine("3.Users");
        Console.WriteLine($" -Total users: {totalUsers}");
        Console.WriteLine($" -Student users: {studentUsers}");
        Console.WriteLine($" -Employee users: {employeeUsers}");
    }
}