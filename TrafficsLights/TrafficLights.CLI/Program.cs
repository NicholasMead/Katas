using TrafficLights.CLI;
using TrafficLights;

Console.CursorVisible = false;
var driver = new ConsoleLightDriver();
var trafficLight = new TrafficLight(new SystemTimer(), driver);

bool state = false;
await trafficLight.Stop();

while (true)
{

    //flush
    while (Console.KeyAvailable)
        _ = Console.ReadKey(true);

    var key = Console.ReadKey(true).Key;

    if (key == ConsoleKey.Spacebar)
    {
        switch (state)
        {
            case true:
                await trafficLight.Stop();
                state = false;
                break;
            case false:
                await trafficLight.Go();
                state = true;
                break;
        }
    }
}
