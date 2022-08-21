namespace Functions.Contracts;

public interface IGreetingsService
{
    Task<string> GetGreeting(string name);
}
