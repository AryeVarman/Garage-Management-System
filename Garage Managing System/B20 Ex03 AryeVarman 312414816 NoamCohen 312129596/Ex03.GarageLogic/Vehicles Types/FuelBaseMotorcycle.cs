﻿using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class FuelBaseMotorcycle : FuelBaseVehicle
    {
        private eLicenseType m_LicenseType;
        private int m_EngineCapacity;

        public FuelBaseMotorcycle(string i_LicenseNumber) : base(i_LicenseNumber)
        {
            const eLicenseType k_LicenseType = eLicenseType.A;
            const int k_DefaultEngineCapacity = 250;

            this.LicenseType = k_LicenseType;
            this.EngineCapacity = k_DefaultEngineCapacity;
        }

        public eLicenseType LicenseType
        {
            get { return m_LicenseType; }

            set
            {
                if (checkIfValidLicenseType(value))
                {
                    m_LicenseType = value;
                }
                else
                {
                    const string k_ErrorType = "Invalid license type exception";
                    throw new ArgumentException(k_ErrorType);
                }
            }
        }

        public int EngineCapacity
        {
            get { return m_EngineCapacity; }

            set // need to check valid input with exception
            {
                if (checkValidEngineCapacity(value))
                {
                    m_EngineCapacity = value;
                }
                else
                {
                    const string k_ErrorName = "EngineCapacity";
                    throw new ValueOutOfRangeException(k_ErrorName, int.MaxValue, 0);
                }
            }
        }

        public override List<Wheel> Wheels
        {
            get { return m_Wheels; }

            set
            {
                if (checkValidMotorWheels(value))
                {
                    m_Wheels = value;
                }
                else
                {
                    const string k_ErrorType = "Invalid Wheels exception";
                    throw new Exception(k_ErrorType);
                }
            }
        }

        private bool checkValidEngineCapacity(int i_EngineCapacity)
        {
            const int k_MinEngineCapacity = 0;

            return i_EngineCapacity > k_MinEngineCapacity;
        }

        private bool checkIfValidLicenseType(eLicenseType i_LicenseType)
        {
            return i_LicenseType == eLicenseType.A || i_LicenseType == eLicenseType.A1 ||
                   i_LicenseType == eLicenseType.AA || i_LicenseType == eLicenseType.B;
        }

        private bool checkValidMotorWheels(List<Wheel> i_Wheels)
        {
            const byte k_MinNumOfWheels = 2;           // motors wheels must comply: at least 2 wheels, maximal air pressure between 20 to 50
            const float k_MaximumMaxAirPressure = 50;
            const float k_MinimumMaxAirPressure = 20;

            return this.CheckValidWheels(i_Wheels, k_MinNumOfWheels, k_MaximumMaxAirPressure, k_MinimumMaxAirPressure);
        }

        public static eLicenseType ConvertNumToLicenseType(byte i_Num)
        {
            eLicenseType licenseType = 0;

            if (i_Num == 1)
            {
                licenseType = eLicenseType.A;
            }
            else if (i_Num == 2)
            {
                licenseType = eLicenseType.A1;
            }
            else if (i_Num == 3)
            {
                licenseType = eLicenseType.AA;
            }
            else if (i_Num == 4)
            {
                licenseType = eLicenseType.B;
            }

            return licenseType;
        }

        public override string ToString()
        {
            return string.Format(
@"Vehicle type: Fuel base motorcycle
{0}
License type: {1}, Engine capacity: {2}",
                base.ToString(), m_LicenseType, m_EngineCapacity);
        }
    }
}
