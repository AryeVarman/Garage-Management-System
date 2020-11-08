using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Truck : FuelBaseVehicle
    {
        private bool m_CarryingDangerousMaterials;
        private float m_CargoVolume;
        
        public Truck(string i_LicenseNumber) : base(i_LicenseNumber)
        {
            const bool v_DangerousMatirials = true;
            const float k_DefaultCargoVolume = 100f;

            this.CarryingDangerousMaterials = !v_DangerousMatirials;
            this.CargoVolume = k_DefaultCargoVolume;
        }

        public bool CarryingDangerousMaterials
        {
            get { return m_CarryingDangerousMaterials; }

            set
            {
                m_CarryingDangerousMaterials = value;
            }
        }

        public float CargoVolume
        {
            get { return m_CargoVolume; }

            set
            {
                if (value >= 0)
                {
                    m_CargoVolume = value;
                }
                else
                {
                    const string k_ErrorName = "Cargo volume";
                    throw new ValueOutOfRangeException(k_ErrorName, float.MaxValue, 0);
                }
            }
        }

        public override List<Wheel> Wheels
        {
            get { return m_Wheels; }

            set
            {
                if (CheckValidTruckWheels(value))
                {
                    m_Wheels = value;
                }
                else
                {
                    const string k_ErrorName = "Wheels in Truck";
                    throw new Exception(k_ErrorName);
                }
            }
        }

        private bool CheckValidTruckWheels(List<Wheel> i_Wheels)
        {
            const byte k_MinNumOfWheels = 12;           // motors wheels must comply: at least 2 wheels, maximal air pressure between 25 to 40
            const float k_MaximumMaxAirPressure = 40;
            const float k_MinimumMaxAirPressure = 25;

            return this.CheckValidWheels(i_Wheels, k_MinNumOfWheels, k_MaximumMaxAirPressure, k_MinimumMaxAirPressure);
        }

        public override string ToString()
        {
            string dangerousMaterials = string.Empty;

            if(m_CarryingDangerousMaterials)
            {
                dangerousMaterials = "yes";
            }
            else
            {
                dangerousMaterials = "no";
            }

            return string.Format(
@"Vehicle type: Truck
{0}
Carrying dangerous materials: {1}, Cargo volume: {2}",
                base.ToString(), dangerousMaterials, m_CargoVolume);
        }
    }
}