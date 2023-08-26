namespace TrafficLights;

public class TrafficLight
{
    private readonly ITimer timer;
    private readonly ILightDriver lights;
    private Lights state;

    public TrafficLight(ITimer timer, ILightDriver lights)
    {
        this.timer = timer ?? throw new ArgumentNullException(nameof(timer));
        this.lights = lights ?? throw new ArgumentNullException(nameof(lights));
    }

    public Task Go() => throw new NotImplementedException();

    public async Task Stop() {
        await lights.SetLights(Lights.Red);
    }
}
