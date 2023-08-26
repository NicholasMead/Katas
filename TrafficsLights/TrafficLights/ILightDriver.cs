namespace TrafficLights;

public interface ILightDriver
{
    Task SetLights(Lights lights);
}
