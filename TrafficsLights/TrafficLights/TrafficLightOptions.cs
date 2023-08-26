namespace TrafficLights;

public record struct TrafficLightOptions
{
    public TrafficLightOptions()
    { }

    public TimeSpan StoppingTime { get; set; } = TimeSpan.FromSeconds(5);

    public TimeSpan ReadyTime { get; set; } = TimeSpan.FromSeconds(3);
}
