

public class RentalManager
{
    public Rental RentEquipment(Equipment equipment)
    {
        if(equipment.Status != EquipmentStatus.Available)
        {
            Console.WriteLine($"{equipment.Name} cannot be rented.");
            return null;
        }
        var rental = new Rental(equipment);
        equipment.Status = EquipmentStatus.Rented;
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
}