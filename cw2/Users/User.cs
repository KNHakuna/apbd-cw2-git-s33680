

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public UserType Type { get; set; }

    private static int nextId = 1;
    public User(string firstName, string lastName, UserType type)
    {
        Id = nextId;
        nextId++;
        FirstName = firstName;
        LastName = lastName;
        Type = type;
    }
}