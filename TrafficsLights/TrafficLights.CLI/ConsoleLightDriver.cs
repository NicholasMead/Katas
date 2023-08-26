
namespace TrafficLights.CLI;

class ConsoleLightDriver : ILightDriver
{
    public Task SetLights(Lights lights)
    {
        var defaultColour = Console.ForegroundColor;

        Console.Clear();
        Console.Write("Lights: ");

        Console.ForegroundColor = lights.HasFlag(Lights.Red) ? ConsoleColor.Red : Console.BackgroundColor;
        Console.Write("Red ");

        Console.ForegroundColor = lights.HasFlag(Lights.Amber) ? ConsoleColor.Yellow : Console.BackgroundColor;
        Console.Write("Amber ");

        Console.ForegroundColor = lights.HasFlag(Lights.Green) ? ConsoleColor.Green : Console.BackgroundColor;
        Console.Write("Green");

        Console.ForegroundColor = defaultColour;
        return Task.CompletedTask;        
    }
}