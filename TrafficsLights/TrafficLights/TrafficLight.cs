namespace TrafficLights;

public class TrafficLight
{
    private readonly ITimer timer;
    private readonly ILightDriver lights;
    private readonly TrafficLightOptions options = new();
    private Lights state;
    private Semaphore semaphore = new(1, 1);

    public TrafficLight(ITimer timer, ILightDriver lights)
    {
        this.timer = timer ?? throw new ArgumentNullException(nameof(timer));
        this.lights = lights ?? throw new ArgumentNullException(nameof(lights));
    }

    public TrafficLight(TrafficLightOptions options, ITimer timer, ILightDriver lights) : this(timer, lights)
    {
        this.options = options;
    }

    public Task Go()
    {
        return Attomic(async () =>
        {
            switch (state)
            {
                case Lights.None:
                    await Register(Lights.Green);
                    return;
                case Lights.Red:
                    await Register(Lights.RedAmber);
                    await timer.Delay(options.ReadyTime);
                    await Register(Lights.Green);
                    return;
            }
        }, nameof(Go));
    }

    public Task Stop()
    {
        return Attomic(async () =>
        {
            switch (state)
            {
                case Lights.None:
                    await Register(Lights.Red);
                    return;
                case Lights.Green:
                    await Register(Lights.Amber);
                    await timer.Delay(options.StoppingTime);
                    await Register(Lights.Red);
                    return;
            }
        }, nameof(Stop));
    }

    public bool IsBusy {get; private set;}

    private async Task Attomic(Func<Task> action, string name)
    {
        var locked = semaphore.WaitOne(10);

        if (!locked)
            throw new OpperationConflictException(name);

        try
        {
            IsBusy = true;
            await action();
            IsBusy = false;
        }
        finally
        {
            semaphore.Release();
        }
    }

    private async Task Register(Lights next)
    {
        await lights.SetLights(next);
        state = next;
    }
}
