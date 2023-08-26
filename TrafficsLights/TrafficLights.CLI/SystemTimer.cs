using TrafficLights;

class SystemTimer : ITimer
{
    public Task Delay(TimeSpan delay) => Task.Delay(delay);
}