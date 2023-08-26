using System.Runtime.Serialization;

namespace TrafficLights;

public abstract class TrafficLightException : Exception
{
    protected TrafficLightException()
    {
    }

    protected TrafficLightException(string? message) : base(message)
    {
    }

    protected TrafficLightException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    protected TrafficLightException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
