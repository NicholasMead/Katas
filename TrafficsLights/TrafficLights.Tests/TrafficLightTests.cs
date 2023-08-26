namespace TrafficLights.Tests;

public class TrafficLightTests
{
    record struct Delayed(TimeSpan Delay);
    
    record struct LightsSet(Lights Lights);

    class Mock : ITimer, ILightDriver
    {

        public List<object> Events {get;} = new();

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

        public void AssertSame(params object[] events)
        {
            Assert.Equal(events.Length, Events.Count);

            for(var i = 0; i < events.Length; i++)
            {
                Assert.Equal(events[i], Events[i]);
            }
        }
    }

    [Fact]
    public async Task DoesStopOnRed()
    {
        var mock = new Mock();
        var trafficLight = new TrafficLight(mock, mock);

        await trafficLight.Stop();
        
        mock.AssertSame(new LightsSet(Lights.Red));
    }
}