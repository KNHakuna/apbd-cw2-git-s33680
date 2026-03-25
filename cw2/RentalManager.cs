

public class RentalManager
{
    private List<Rental> rentals = new List<Rental>();
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
        rental.Equipment.Status = EquipmentStatus.Available;
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

    public List<Rental> GetUserRentals(User user)
    {
        return rentals.Where(r => r.User.Id == user.Id).ToList();
    }
}