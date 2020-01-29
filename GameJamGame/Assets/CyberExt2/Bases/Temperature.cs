using System.Collections.Generic;


namespace Cyberevolver
{
    public enum TemperatureMode
    {
        Celsius,
        Farenhait,
        Kelvin
    }
    /// <summary>
    /// Represents temperature. You can set or get by any temperature measure.
    /// </summary>
    public struct Temperature
    {
        private Dictionary<TemperatureMode, double> Value { get; }
        /// <summary>
        /// It will be used, when you call <see cref="ToString"/>
        /// </summary>
        public TemperatureMode DefaultMode { get; set; }
        public Temperature(TemperatureMode mode, double value)
        {
            DefaultMode = mode;
            Value = new Dictionary<TemperatureMode, double>();
            switch (mode)
            {
                case TemperatureMode.Celsius:
                    Value[TemperatureMode.Celsius] = value;
                    break;

                case TemperatureMode.Farenhait:
                    Value[TemperatureMode.Celsius] = (value - 32.0) / 1.8;
                    break;

                case TemperatureMode.Kelvin:
                    Value[TemperatureMode.Celsius] = value - 273.15;
                    break;
            }            
            Value[TemperatureMode.Kelvin] = Value[TemperatureMode.Celsius] + 273.15;
            Value[TemperatureMode.Farenhait] = Value[TemperatureMode.Celsius] * 1.8 + 32;
        }
        public double Celsius => Get(TemperatureMode.Celsius);
        public double Farenhait => Get(TemperatureMode.Farenhait);
        public double Kelvin => Get(TemperatureMode.Kelvin);
        /// <summary>
        /// Returning value as concrete <see cref="TemperatureMode"/>.
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public double Get(TemperatureMode mode)
        {
            return System.Math.Round(Value[mode], 0);
        }
        /// <summary>
        /// It returns temperature string value from current <see cref="DefaultMode"/>/
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Get(DefaultMode)} °{DefaultMode.ToString()[0]}";
        }

    }

}
