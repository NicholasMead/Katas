using System.Security.Principal;

namespace TrafficLights.Tests;

public class TrafficLightTests
{
    record struct Delayed(TimeSpan Delay);

    record struct LightsSet(Lights Lights);

    class Mock : ITimer, ILightDriver
    {
        private List<object> Events { get; } = new();

        public Task Delay(TimeSpan delay)
        {
            Events.Add(new Delayed(delay));
            return Task.CompletedTask;
        }

        public Task SetLights(Lights lights)
        {
            Events.Add(new LightsSet(lights));
            return Task.CompletedTask;
        }

        public void AssertHistory(params object[] expected) => Assert.Equal(expected, Events.ToArray());
    }

    class TimerStub : ITimer, IDisposable
    {
        private readonly bool hold;
        private List<CancellationTokenSource> tokenSources = new();

        public TimerStub(bool hold)
        {
            this.hold = hold;
        }

        public async Task Delay(TimeSpan _)
        {
            if (hold)
            {
                using var tokenSource = new CancellationTokenSource();
                tokenSources.Add(tokenSource);

                try
                {
                    await Task.Delay(-1, tokenSource.Token);
                }
                catch (TaskCanceledException)
                {
                    tokenSources.Remove(tokenSource);
                }
            }
        }

        public void Release()
        {
            tokenSources.ForEach(t => t.Cancel());
        }

        public void Dispose()
        {
            tokenSources.ForEach(t => t.Dispose());
        }
    }

    [Fact]
    public async Task DoesStopOnRed()
    {
        var mock = new Mock();
        var trafficLight = new TrafficLight(mock, mock);

        await trafficLight.Stop();

        mock.AssertHistory(new LightsSet(Lights.Red));
    }

    [Fact]
    public async Task DoesGoOnGreen()
    {
        var mock = new Mock();
        var trafficLight = new TrafficLight(mock, mock);

        await trafficLight.Go();

        mock.AssertHistory(new LightsSet(Lights.Green));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    [InlineData(5)]
    public async Task DoesTransitionStopToGo(int seconds)
    {
        var options = new TrafficLightOptions() { ReadyTime = TimeSpan.FromSeconds(seconds) };
        var mock = new Mock();
        var trafficLight = new TrafficLight(options, mock, mock);

        await trafficLight.Stop();
        await trafficLight.Go();

        mock.AssertHistory(
            new LightsSet(Lights.Red),
            new LightsSet(Lights.RedAmber),
            new Delayed(options.ReadyTime),
            new LightsSet(Lights.Green));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    [InlineData(5)]
    public async Task DoesTransitionGoToStop(int seconds)
    {
        var options = new TrafficLightOptions() { StoppingTime = TimeSpan.FromSeconds(seconds) };
        var mock = new Mock();
        var trafficLight = new TrafficLight(options, mock, mock);

        await trafficLight.Go();
        await trafficLight.Stop();

        mock.AssertHistory(
            new LightsSet(Lights.Green),
            new LightsSet(Lights.Amber),
            new Delayed(options.StoppingTime),
            new LightsSet(Lights.Red));
    }

    [Fact]
    public async Task StopDoesThrowConflictException()
    {
        using TimerStub timer = new(true);
        Mock mock = new();
        var trafficLight = new TrafficLight(timer, mock);

        await trafficLight.Go();
        _ = trafficLight.Stop();

        var ex = await Assert.ThrowsAsync<OpperationConflictException>(trafficLight.Stop);
        Assert.Equal(nameof(trafficLight.Stop), ex.OpperationName);
    }

    [Fact]
    public async Task GoDoesThrowConflictException()
    {
        using TimerStub timer = new(true);
        Mock mock = new();
        var trafficLight = new TrafficLight(timer, mock);

        await trafficLight.Stop();
        _ = trafficLight.Go();

        var ex = await Assert.ThrowsAsync<OpperationConflictException>(trafficLight.Go);
        Assert.Equal(nameof(trafficLight.Go), ex.OpperationName);
    }

    [Fact]
    public async Task DoesReturnIfBusy()
    {
        using TimerStub timer = new(true);
        Mock mock = new();
        var trafficLight = new TrafficLight(timer, mock);

        await trafficLight.Stop();
        Assert.False(trafficLight.IsBusy);

        var goTask = trafficLight.Go();
        Assert.True(trafficLight.IsBusy);

        timer.Release();
        await goTask;
        Assert.False(trafficLight.IsBusy);
    }
}