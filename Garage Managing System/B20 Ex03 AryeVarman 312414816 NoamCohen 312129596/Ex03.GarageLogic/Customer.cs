using System;

namespace Ex03.GarageLogic
{
    public enum eVehicleStatus
    {
            InRepair = 1,
            Fixed = 2,
            Paid = 3
    }

    public class Customer
    {
        private readonly string r_Name;
        private readonly string r_PhoneNumber;
        private Vehicle m_Vehicle;
        private eVehicleStatus m_VehicleStatus;

        public Customer(string i_Name, string i_PhoneNumber)
        { 
            checkCustomerName(i_Name);
            checkCustomerPhoneNumber(i_PhoneNumber);

            r_Name = i_Name;
            r_PhoneNumber = i_PhoneNumber;
            m_Vehicle = null;
            m_VehicleStatus = eVehicleStatus.InRepair;
        }

        public string Name
        {
            get { return r_Name; }
        }

        public string PhoneNumber
        {
            get { return r_PhoneNumber; }
        }

        public Vehicle Vehicle
        {
            get { return m_Vehicle; }

            set
            {
                if(value != null)
                {
                    m_Vehicle = value;
                }
                else
                {
                    const string k_ErrorType = "Vehicle is not assigned yet";
                    throw new ArgumentNullException(k_ErrorType);
                }
            }
        }

        public eVehicleStatus VehicleStatus
        {
            get { return m_VehicleStatus; }

            set
            {
                if(CheckIfVehicleStatus(value))
                {
                    m_VehicleStatus = value;
                }
                else
                {
                    const string k_ErrorType = "Vehicle Status not valid";
                    throw new ArgumentException(k_ErrorType);
                }
            }
        }

        private void checkCustomerName(string i_Name)
        {
            for(int i = 0; i < i_Name.Length; i++)
            {
                if(!char.IsLetter(i_Name[i]))
                {
                    const string k_ErrorType = "name must contain only letters";
                    throw new FormatException(k_ErrorType);
                }
            }
        }

        private void checkCustomerPhoneNumber(string i_PhoneNumber)
        {
            Exception phoneNumberLengthException = null;
            const int PhoneNumberLen = 10;

            if(i_PhoneNumber.Length != PhoneNumberLen)
            {
                const string k_ErrorType = "Phone Number should have 10 digits";
                phoneNumberLengthException = new Exception(k_ErrorType);
            }

            if(!isNumbersOnly(i_PhoneNumber))
            {
                const string k_ErrorType = "Phone Number should contain only digits";
                throw new FormatException(k_ErrorType, phoneNumberLengthException);
            }

            if(phoneNumberLengthException != null)
            {
                throw phoneNumberLengthException;
            }
        }
        
        private void checkCustomerVehicle(Vehicle i_Vehicle)
        {
            if(i_Vehicle == null)
            {
                const string k_ErrorType = "vehicle is not assigned";
                throw new ArgumentNullException(k_ErrorType);
            }
        }

        public static bool CheckIfVehicleStatus(eVehicleStatus i_VehicleStatus)
        {
            return i_VehicleStatus == eVehicleStatus.InRepair ||
                   i_VehicleStatus == eVehicleStatus.Fixed || i_VehicleStatus == eVehicleStatus.Paid;
        }

        private bool isNumbersOnly(string i_string)
        {
            bool validNum = true;

            foreach (char charecter in i_string)
            {
                if (!char.IsDigit(charecter))
                {
                    validNum = false;
                    break;
                }
            }

            return validNum;
        }

        public static eVehicleStatus ConvertNumberToStatus(byte i_NumberStatus)
        {
            eVehicleStatus newStatus = 0;

            if(i_NumberStatus == 1)
            {
                newStatus = eVehicleStatus.InRepair;
            }
            else if(i_NumberStatus == 2)
            {
                newStatus = eVehicleStatus.Fixed;
            }
            else if (i_NumberStatus == 3)
            {
                newStatus = eVehicleStatus.Paid;
            }

            return newStatus;
        }
    }
}
