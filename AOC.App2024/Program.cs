using AOC.Common.Services;

Console.WriteLine("Welcome to Advent of Code!");

try
{
    var today = DateTime.Today.Day; //.AddDays(-X) for previous days
    var svc = new AdventService(today);
    svc.Resolve(2);
}
catch (Exception ex)
{
    Console.Error.WriteLine(ex.ToString());
}
finally
{
    Console.ReadLine();
}

