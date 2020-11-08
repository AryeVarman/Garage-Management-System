using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class FuelBaseCar : FuelBaseVehicle
    {
        private eColor m_Color;
        private eDoorsNumber m_DoorsNumber;

        public FuelBaseCar(string i_LicenseNumber) : base(i_LicenseNumber)
        {
            const eColor k_DefaultColor = eColor.Black;
            const eDoorsNumber k_DefaultDoorNumber = eDoorsNumber.Four;

            this.Color = k_DefaultColor;
            this.DoorsNumber = k_DefaultDoorNumber;
        }

        public static eDoorsNumber CovertNumToDoorNumber(byte i_Num)
        {
            eDoorsNumber doorsNumber = 0;

            if (i_Num == 2)
            {
                doorsNumber = eDoorsNumber.Two;
            }
            else if (i_Num == 3)
            {
                doorsNumber = eDoorsNumber.Three;
            }
            else if (i_Num == 4)
            {
                doorsNumber = eDoorsNumber.Four;
            }
            else if (i_Num == 5)
            {
                doorsNumber = eDoorsNumber.Five;
            }

            return doorsNumber;
        }

        public eColor Color
        {
            get { return m_Color; }

            set
            {
                if (checkValidColor(value))
                {
                    m_Color = value;
                }
                else
                {
                    const string k_ErrorName = "Invalid color exception";
                    throw new ArgumentException(k_ErrorName);
                }
            }
        }

        public eDoorsNumber DoorsNumber
        {
            get { return m_DoorsNumber; }

            set
            {
                if (checkValidDoorsNumber(value))
                {
                    m_DoorsNumber = value;
                }
                else
                {
                    const string k_ErrorType = "Invalid doors number input";
                    throw new ArgumentException(k_ErrorType);
                }
            }
        }

        private bool checkValidColor(eColor i_Color)
        {
            return i_Color == eColor.Black || i_Color == eColor.Red ||
                   i_Color == eColor.Silver || i_Color == eColor.White;
        }

        private bool checkValidDoorsNumber(eDoorsNumber i_DoorsNumber)
        {
            return i_DoorsNumber == eDoorsNumber.Two || i_DoorsNumber == eDoorsNumber.Three ||
                   i_DoorsNumber == eDoorsNumber.Four || i_DoorsNumber == eDoorsNumber.Five;
        }

        public override List<Wheel> Wheels
        {
            get { return m_Wheels; }

            set
            {
                if (checkValidCarWheels(value))
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

        private bool checkValidCarWheels(List<Wheel> i_Wheels)
        {
            const byte k_MinNumOfWheels = 4;            // motors wheels must comply: at least 4 wheels, maximal air pressure between 20 to 40
            const float k_MaximumMaxAirPressure = 40;
            const float k_MinimumMaxAirPressure = 20;

            return this.CheckValidWheels(i_Wheels, k_MinNumOfWheels, k_MaximumMaxAirPressure, k_MinimumMaxAirPressure);
        }

        public static eColor CovertNumToColor(byte i_Num)
        {
            eColor Color = 0;

            if (i_Num == 1)
            {
                Color = eColor.Black;
            }
            else if (i_Num == 2)
            {
                Color = eColor.Red;
            }
            else if (i_Num == 3)
            {
                Color = eColor.Silver;
            }
            else if (i_Num == 4)
            {
                Color = eColor.White;
            }

            return Color;
        }

        public override string ToString()
        {
            return string.Format(
@"Vehicle type: Fuel base car
{0}
Color: {1}, Number of doors: {2}",
                base.ToString(), m_Color, m_DoorsNumber);
        }
    }
}
