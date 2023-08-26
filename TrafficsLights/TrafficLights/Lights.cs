namespace TrafficLights;

public enum Lights
{
    None = 0,
    Red = 1 << 0,
    Amber = 1 << 1,
    Green = 1 << 2,
}
