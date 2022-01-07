namespace Common.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class NoWeatherForecastException : Exception
    {
        public NoWeatherForecastException()
        {
        }

        public NoWeatherForecastException(string message) : base(message)
        {
        }

        public NoWeatherForecastException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoWeatherForecastException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
