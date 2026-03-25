
class Program
{
    public static void Main(string[] args)
    {
        var test1 =  new Equipment("Test1");
        var test2 =  new Equipment("Test2");
        Console.WriteLine(test1.Id+" "+test1.Name+" "+test1.Status);
        Console.WriteLine(test2.Id+" "+test2.Name+" "+test2.Status);
        var e = new Equipment("VR");

        e.Rent();
        Console.WriteLine(e.Status);

        e.Return();
        Console.WriteLine(e.Status);

        e.Return();
    }
}