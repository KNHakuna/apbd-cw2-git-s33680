

public class Rental
{
    public int Id { get; set; }
    public Equipment Equipment { get; set; }
    public User User { get; set; }
    public DateTime RentalDate { get; set; }
    public DateTime Deadline { get; set; }
    public DateTime? ReturnDate { get; set; }
    public double Penalty { get; set; }

    private static int nextId = 1;
    public Rental(Equipment equipment, User user)
    {
        Id = nextId;
        nextId++;
        User = user;
        Equipment = equipment;
        RentalDate = DateTime.Now;
        Deadline = RentalDate.AddDays(30);
        ReturnDate = null;
    }

    public void Returning()
    {
        if (ReturnDate != null)
        {
            Console.WriteLine($"{Equipment.Name} has already been returned.");
            return;
        }
        ReturnDate = DateTime.Now;
    }
}