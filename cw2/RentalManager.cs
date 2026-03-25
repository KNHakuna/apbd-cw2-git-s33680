

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

    public List<Equipment> GetAvailableEquipments()
    {
        return equipments.Where(e => e.Status == EquipmentStatus.Available).ToList();
    }
}