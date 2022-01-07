namespace Common.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class WeatherForecastNotFoundException : Exception
    {
        public WeatherForecastNotFoundException()
        {
        }

        public WeatherForecastNotFoundException(string message) : base(message)
        {
        }

        public WeatherForecastNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WeatherForecastNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
