using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_Task_6
{
    internal class WeatherControl:DependencyObject
    {
        public static readonly DependencyProperty TemperatureProperty;
        private string windDerection;
        private int windSpeed;
        private int precipitation;
       
        public int Temperature
        {
            get => (int)GetValue(TemperatureProperty);
            set => SetValue(TemperatureProperty, value);
        }
        
        public string WindDerection
        {
            get => windDerection;
            set => windDerection = value;
        }

        public int WindSpeed
        {
            get { return windSpeed; }
            set
            {
                if (value > 0)
                    windSpeed = value;
                else
                    windSpeed = 0;
            }
        }

        public int Precipitation
        {
            get { return precipitation; }
            set
            {
            if (value >= 0 && value < 4)
                    precipitation = value;
            else
                    precipitation = 4;
            }
        }

        enum PrecipitationName
        {
            sunny, // 0
            cloudy, // 1
            rain, // 2
            snow, // 3
            no_precipitation_data_available // 4
        }

        static WeatherControl()
        {
            TemperatureProperty = DependencyProperty.Register(
                nameof(Temperature), // Property name
                typeof(int), // Property type
                typeof(WeatherControl), // Property owner
                new FrameworkPropertyMetadata( // Property metadata
                    0, // Default value 
                    FrameworkPropertyMetadataOptions.AffectsMeasure | // Flags
                    FrameworkPropertyMetadataOptions.AffectsArrange,
                    null, // Property changed callback 
                    new CoerceValueCallback(CoerceTemperature)), // Correction callback
                new ValidateValueCallback(ValidateTemperature)); // Validation callback
        }

        private static bool ValidateTemperature(object value)
        {
            int v = (int)value;
            if (v > -51 && v < 51)
                return true;
            else
                return false;
        }

        private static object CoerceTemperature(DependencyObject d, object baseValue)
        {
            int v = (int)baseValue;
            if (v > -51 && v < 51)
                return v;
            else
                return 0;
        }

        public string Print()
        {
            PrecipitationName precipitationName = new PrecipitationName();

            switch (precipitation)
            {
                case 0:
                    precipitationName = (PrecipitationName)0;
                    break;
                case 1:
                    precipitationName = (PrecipitationName)1;
                    break;
                case 2:
                    precipitationName = (PrecipitationName)2;
                    break;
                case 3:
                    precipitationName = (PrecipitationName)3;
                    break;
                case 4:
                    precipitationName = (PrecipitationName)4;
                    break;
            }
            return $"The weather at present: {precipitationName}, temperature {Temperature}°C, wind {windDerection} {WindSpeed} mps";
        }
    }
}
