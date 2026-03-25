using System;
using System.Net.Http.Headers;

public class Equipment
{
    public int Id { get; set; }
    public string Name { get; set; }
    public EquipmentStatus Status { get; set; }

    private static int nextId = 1;
    public Equipment(string name)
    {
        Id = nextId;
        nextId++;
        Name = name;
        Status = EquipmentStatus.Available;
    }

    public void Rent()
    {
        if (Status == EquipmentStatus.Rented)
        {
            Console.WriteLine($"{Name} is already rented.");
            return;
        }
        if (Status == EquipmentStatus.Unavailable)
        {
            Console.WriteLine($"{Name} is unavailable");
            return;
        }
        Status = EquipmentStatus.Rented;
        Console.WriteLine($"{Name} rented.");
    }

    public void Return()
    {
        if (Status == EquipmentStatus.Available)
        {
            Console.WriteLine($"{Name} is already available.");
            return;
        }
        Status = EquipmentStatus.Available;
        Console.WriteLine($"{Name} returned.");
    }


}
