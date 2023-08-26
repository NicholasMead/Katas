namespace TrafficLights;

public class OpperationConflictException : TrafficLightException
{
    public string OpperationName {get;}

    public OpperationConflictException(string opperationName) : base($"Opperation ({opperationName}) conflicts with an ongoing opperation")
    {
        OpperationName = opperationName ?? throw new ArgumentNullException(nameof(opperationName));
    }
}
