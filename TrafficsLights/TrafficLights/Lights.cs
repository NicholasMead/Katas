namespace TrafficLights;

public enum Lights
{
    //primative
    None = 0,
    Red = 1 << 0,
    Amber = 1 << 1,
    Green = 1 << 2,

    //derived
    RedAmber = Red | Amber,
}
