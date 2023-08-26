namespace TrafficLights;

public interface ITimer
{
    Task Delay(TimeSpan delay);
}
