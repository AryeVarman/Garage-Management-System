using System;
using System.Linq;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private const float k_MinAirPressure = 0;
        private readonly string r_ManufactureName;
        private float m_CurrentAirPressure;


        // $G$ DSN-999 (-4) The "fuel type" field should be readonly member of class FuelEnergyProvider.
        private float m_MaxAirPressure;

        public Wheel(string i_ManufactureName)
        {
            const float k_DefaultMaxAirPressure = 32;

            r_ManufactureName = i_ManufactureName;
            MaxAirPressure = k_DefaultMaxAirPressure;
            CurrentAirPressure = k_DefaultMaxAirPressure;
        }

        public string ManufactureName
        {
            get { return r_ManufactureName; }
        }

        public float MaxAirPressure
        {
            get { return m_MaxAirPressure; }

            set
            {
                if (value > 0)
                {
                    m_MaxAirPressure = value;
                }
                else
                {
                    const string k_ErrorName = "Max Air Pressure";
                    throw new ValueOutOfRangeException(k_ErrorName, float.MaxValue, k_MinAirPressure);
                }
            }
        }

        public float CurrentAirPressure
        {
            get { return m_CurrentAirPressure; }

            set
            {
                if (value <= m_MaxAirPressure && value >= 0)
                {
                    m_CurrentAirPressure = value;
                }
                else
                {
                    const string k_ErrorName = "Current Air Pressure";
                    throw new ValueOutOfRangeException(k_ErrorName, m_MaxAirPressure - m_CurrentAirPressure, k_MinAirPressure);
                }
            }
        }

        public void Inflate(float i_AirToAdd)
        {
            const float k_MinAirPressure = 0;

            if (m_CurrentAirPressure + i_AirToAdd <= m_CurrentAirPressure && i_AirToAdd >= 0)
            {
                m_CurrentAirPressure += i_AirToAdd;
            }
            else
            {
                string errorName = "Inflate";
                throw new ValueOutOfRangeException(errorName, m_MaxAirPressure - m_CurrentAirPressure, k_MinAirPressure);
            }
        }

        public override string ToString()
        {
            return string.Format(
                "Manufacturer: {0}, Current air pressure: {1}, Maximum air pressure: {2}",
                r_ManufactureName, m_CurrentAirPressure, m_MaxAirPressure);
        }
    }
}
