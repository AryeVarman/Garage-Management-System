using System;
using System.Collections.Generic;
using Ex03.GarageLogic;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        // lists of the fuels types that the garage can receives
        private static readonly eFuelType[] r_FuelTypeGarageSupportForCarList = { eFuelType.Octan96 };
        private static readonly eFuelType[] r_FuelTypeGarageSupportForMotorList = { eFuelType.Octan95 };
        private static readonly eFuelType[] r_FuelTypeGarageSupportForTruckList = { eFuelType.Soler };

        // lists of the fuels tanks that the garage can receives
        private static readonly float[] r_MaxFuelTankGarageSupportForFuelCarList = { 60 };
        private static readonly float[] r_MaxFuelTankGarageSupportForFuelMotorList = { 7 };
        private static readonly float[] r_MaxFuelTankGarageSupportForTruckList = { 120 };

        // lists of the maximum battery time in hours that the garage can receives
        private static readonly float[] r_MaxBatteryTimeGarageSupportForElectricCarList = { 2.1f };
        private static readonly float[] r_MaxBatteryTimeGarageSupportForElectricMotorList = { 1.2f };

        // lists of the number of wheels that the garage can receives
        private static readonly byte[] r_NumOfWheelsGarageSupportForCarList = { 4 };
        private static readonly byte[] r_NumOfWheelsGarageSupportForMotorList = { 2 };
        private static readonly byte[] r_NumOfWheelsGarageSupportForTruckList = { 16 };

        // lists of the maximum wheel air pressure that the garage can receives
        private static readonly float[] r_MaxAirPressureGarageSupportForCarList = { 32 };
        private static readonly float[] r_MaxAirPressureGarageSupportForMotorList = { 30 };
        private static readonly float[] r_MaxAirPressureGarageSupportForTruckList = { 28 };

        private Dictionary<string, Customer> m_GarageCustomer;

        public Garage()
        {
            m_GarageCustomer = new Dictionary<string, Customer>();
        }

        public Dictionary<string, Customer> GarageCustomerList
        {
            get { return m_GarageCustomer; }
        }

        public void EnterNewVehicle(Customer i_NewCustomer, out string i_Message)
        {
            i_Message = null;

            if (!checkIfVehicleAlreadyInTheGarage(i_NewCustomer))
            {
                if(checkValidVehicle(i_NewCustomer.Vehicle))
                {
                    addVehicle(i_NewCustomer);
                }
            }
            else
            {
                i_Message = "The vehicle is already in the garage, change the vehicle status to 'In Repair'";
                m_GarageCustomer[i_NewCustomer.Vehicle.LicenseNumber].VehicleStatus = eVehicleStatus.InRepair;
            }
        }

        // check if the garage can handle with that king of vehicle
        private bool checkValidVehicle(Vehicle i_Vehicle)
        {
            bool validFuelCar = false, validElectronicCar = false, validFuelMotor = false, validElectronicMotor = false, validTruck = false;
            FuelBaseCar fuelBaseCar = i_Vehicle as FuelBaseCar;
            ElectricCar electronicCar = i_Vehicle as ElectricCar;
            FuelBaseMotorcycle fuelBaseMotorcycle = i_Vehicle as FuelBaseMotorcycle;
            ElectricMotorcycle electronicMotorcycle = i_Vehicle as ElectricMotorcycle;
            Truck truck = i_Vehicle as Truck;

            if (fuelBaseCar != null)
            {
                validFuelCar = checkFuelBaseVehicleValid(
                               fuelBaseCar, r_FuelTypeGarageSupportForCarList, r_MaxFuelTankGarageSupportForFuelCarList,
                               r_NumOfWheelsGarageSupportForCarList, r_MaxAirPressureGarageSupportForCarList);
            }
            else if (electronicCar != null)
            {
                validElectronicCar = checkElectronicVehicleValid(
                                    electronicCar, r_MaxBatteryTimeGarageSupportForElectricCarList,
                                    r_NumOfWheelsGarageSupportForCarList, r_MaxAirPressureGarageSupportForCarList);
            }
            else if (fuelBaseMotorcycle != null)
            {
                validFuelMotor = checkFuelBaseVehicleValid(
                    fuelBaseMotorcycle, r_FuelTypeGarageSupportForMotorList, r_MaxFuelTankGarageSupportForFuelMotorList,
                    r_NumOfWheelsGarageSupportForMotorList, r_MaxAirPressureGarageSupportForMotorList);
            }
            else if (electronicMotorcycle != null)
            {
                validElectronicMotor = checkElectronicVehicleValid(
                    electronicMotorcycle, r_MaxBatteryTimeGarageSupportForElectricMotorList,
                    r_NumOfWheelsGarageSupportForMotorList, r_MaxAirPressureGarageSupportForMotorList);
            }
            else if (truck != null)
            {
                validTruck = checkFuelBaseVehicleValid(
                    truck, r_FuelTypeGarageSupportForTruckList, r_MaxFuelTankGarageSupportForTruckList,
                    r_NumOfWheelsGarageSupportForTruckList, r_MaxAirPressureGarageSupportForTruckList);
            }

            return validFuelCar || validElectronicCar || validFuelMotor || validElectronicMotor || validTruck;
        }

        // check valid fuel base vehicles types
        private bool checkFuelBaseVehicleValid(
            FuelBaseVehicle i_FuelBaseVehicle, eFuelType[] i_Fuels, float[] i_MaxFuelTanks, byte[] i_NumOfWheels, float[] i_MaxAirPressures)
        {
            string message = "Error - vehicle could not enter the garage - ";
            bool numOfWheels = false, maxAirPressure = true, fuelType = false, maxFuelTank = false;

            numOfWheels = checkVehicleValidNumOfWheels(i_FuelBaseVehicle.Wheels, i_NumOfWheels);

            if (!numOfWheels)
            {
                message += "number of wheels is not valid";
                throw new Exception(message);
            }

            maxAirPressure = checkVehicleValidMaxAirPressure(i_FuelBaseVehicle.Wheels, i_MaxAirPressures);

            if (!maxAirPressure)
            {
                message += "maximum air pressure is not valid";
                throw new Exception(message);
            }

            fuelType = checkVehicleValidFuelType(i_FuelBaseVehicle.FuelType, i_Fuels);

            if (!fuelType)
            {
                message += "fuel type is not valid";
                throw new Exception(message);
            }

            maxFuelTank = checkVehicleValidFuelTank(i_FuelBaseVehicle.MaxAmountOfFuel, i_MaxFuelTanks);

            if (!maxFuelTank)
            {
                message += "maximum fuel tank is not valid";
                throw new Exception(message);
            }

            return numOfWheels && maxAirPressure && fuelType && maxFuelTank;
        }


        // $G$ DSN-003 (-10) You should not make UI calls from logic classes.

        // check valid electronic vehicles types 
        private bool checkElectronicVehicleValid(
            ElectricVehicle i_ElectricVehicle, float[] i_MaxBatteriesTime, byte[] i_NumOfWheels, float[] i_MaxAirPressures)
        {
            string message = "Error - vehicle could not enter the garage - ";
            bool numOfWheels = false, maxAirPressure = true, maxBatteryTime = false;

            numOfWheels = checkVehicleValidNumOfWheels(i_ElectricVehicle.Wheels, i_NumOfWheels);

            if (!numOfWheels)
            {
                message += "number of wheels is not valid";
                throw new Exception(message);
            }

            maxAirPressure = checkVehicleValidMaxAirPressure(i_ElectricVehicle.Wheels, i_MaxAirPressures);

            if (!maxAirPressure)
            {
                message += "maximum air pressure is not valid";
                throw new Exception(message);
            }

            maxBatteryTime = checkVehicleValidMaxBatteryTime(i_ElectricVehicle.MaximalBatteryTime, i_MaxBatteriesTime);

            if (!maxBatteryTime)
            {
                message += "maximum fuel tank is not valid";
                throw new Exception(message);
            }

            return numOfWheels && maxAirPressure && maxBatteryTime;
        }

        private bool checkVehicleValidNumOfWheels(List<Wheel> i_Wheels, byte[] i_NumOfWheels)
        {
            bool validNumOfWheels = false;
            foreach (byte numOfWheels in i_NumOfWheels)
            {
                if (i_Wheels.Count == numOfWheels)
                {
                    validNumOfWheels = true;
                    break;
                }
            }

            return validNumOfWheels;
        }

        private bool checkVehicleValidMaxAirPressure(List<Wheel> i_Wheels, float[] i_MaxAirPressures)
        {
            bool validMaxAirPressure = true;

            foreach (float maxAirPressure in i_MaxAirPressures)
            {
                foreach (Wheel wheel in i_Wheels)
                {
                    if (wheel.MaxAirPressure != maxAirPressure)
                    {
                        validMaxAirPressure = false;
                    }
                }

                if (validMaxAirPressure)
                {
                    break;
                }
            }

            return validMaxAirPressure;
        }

        private bool checkVehicleValidFuelType(eFuelType i_FuelType, eFuelType[] i_Fuels)
        {
            bool validFuelType = false;

            foreach (eFuelType fuelType in i_Fuels)
            {
                if (i_FuelType == fuelType)
                {
                    validFuelType = true;
                    break;
                }
            }

            return validFuelType;
        }

        private bool checkVehicleValidFuelTank(float i_MaxFuelTank, float[] i_MaxFuelTanks)
        {
            bool validFuelTank = false;
            foreach (float maxFuelTank in i_MaxFuelTanks)
            {
                if (i_MaxFuelTank == maxFuelTank)
                {
                    validFuelTank = true;
                    break;
                }
            }

            return validFuelTank;
        }

        private bool checkVehicleValidMaxBatteryTime(float i_MaxBatteryTime, float[] i_MaxBatteriesTime)
        {
            bool validMaxBatteryTime = false;

            foreach (float maxBatteryTime in i_MaxBatteriesTime)
            {
                if (i_MaxBatteryTime == maxBatteryTime)
                {
                    validMaxBatteryTime = true;
                    break;
                }
            }

            return validMaxBatteryTime;
        }

        // find vehicle with customer information
        private bool checkIfVehicleAlreadyInTheGarage(Customer i_Customer)
        {
            if (i_Customer == null)
            {
                throw new ArgumentNullException(nameof(i_Customer));
            }

            return m_GarageCustomer.ContainsKey(i_Customer.Vehicle.LicenseNumber);
        }

        // find vehicle with license number
        private bool checkIfVehicleAlreadyInTheGarage(string i_LicenseNumber)
        {
            return m_GarageCustomer.ContainsKey(i_LicenseNumber);
        }

        private void addVehicle(Customer i_NewCustomer)
        {
            m_GarageCustomer.Add(i_NewCustomer.Vehicle.LicenseNumber, i_NewCustomer);
        }

        public List<string> ShowLicenseNumbers(bool i_ShowInRepair, bool i_ShowFixed, bool i_ShowPaid)
        {
            List<string> licenseList = new List<string>();

            foreach (KeyValuePair<string, Customer> pair in m_GarageCustomer)
            {
                if (i_ShowInRepair)
                {
                    if (pair.Value.VehicleStatus == eVehicleStatus.InRepair)
                    {
                        licenseList.Add(pair.Value.Vehicle.LicenseNumber);
                    }
                }

                if (i_ShowFixed)
                {
                    if (pair.Value.VehicleStatus == eVehicleStatus.Fixed)
                    {
                        licenseList.Add(pair.Value.Vehicle.LicenseNumber);
                    }
                }

                if (i_ShowPaid)
                {
                    if (pair.Value.VehicleStatus == eVehicleStatus.Paid)
                    {
                        licenseList.Add(pair.Value.Vehicle.LicenseNumber);
                    }
                }
            }

            return licenseList;
        }

        public void ChangeStatus(string i_LicenseNumber, byte i_NewStatusNumber)
        {
            bool processIsLegal = true;
            string errorName = string.Empty;
            eVehicleStatus newStatus = Customer.ConvertNumberToStatus(i_NewStatusNumber);

            if (!Customer.CheckIfVehicleStatus(newStatus))
            {
                processIsLegal = false;
                errorName = "Change status - the status number in invalid";
            }

            if(processIsLegal && !checkIfVehicleAlreadyInTheGarage(i_LicenseNumber))
            {
                processIsLegal = false;
                errorName = "Change status - the vehicle doesn't exist in the garage";
            }

            if (processIsLegal)
            {
                m_GarageCustomer[i_LicenseNumber].VehicleStatus = newStatus;
            }
            else
            {
                throw new Exception(errorName);
            }
        }

        public void InflateWheels(string i_LicenseNumber, float i_AmountOfAirToAdd)
        {
            if (checkIfVehicleAlreadyInTheGarage(i_LicenseNumber))
            {
                Vehicle vehicle = m_GarageCustomer[i_LicenseNumber].Vehicle;

                foreach (Wheel wheel in vehicle.Wheels)
                {
                    wheel.Inflate(i_AmountOfAirToAdd); // change in wheel Inflate method
                }
            }
            else
            {
                const string k_ErrorName = "Inflate to max - the vehicle doesn't exist in the garage";
                throw new Exception(k_ErrorName);
            }
        }

        public void Refuel(string i_LicenseNumber, byte i_FuelNumber, float i_AmountFuelToAdd)
        {
            if (checkIfVehicleAlreadyInTheGarage(i_LicenseNumber))
            {
                FuelBaseVehicle fuelBaseVehicle = m_GarageCustomer[i_LicenseNumber].Vehicle as FuelBaseVehicle;
                
                if (fuelBaseVehicle != null)
                {
                    eFuelType fuelType = FuelBaseVehicle.ConvertNumToFuelType(i_FuelNumber);
                    fuelBaseVehicle.Refueling(i_AmountFuelToAdd, fuelType); // can throw exception
                }
                else
                {
                    const string k_ErrorName = "Refuel - the vehicle is not fuel base";
                    throw new Exception(k_ErrorName);
                }
            }
            else
            {
                const string k_ErrorName = "Refuel - the vehicle doesn't exist in the garage";
                throw new Exception(k_ErrorName);
            }
        }

        public void Charge(string i_LicenseNumber, float i_AmountHoursToAdd)
        {
            ElectricVehicle electricVehicle = m_GarageCustomer[i_LicenseNumber].Vehicle as ElectricVehicle;

            if (checkIfVehicleAlreadyInTheGarage(i_LicenseNumber))
            {
                if (electricVehicle != null)
                {
                    electricVehicle.ChargeBattery(i_AmountHoursToAdd);      // can throw exception
                }
                else
                {
                    const string k_ErrorName = "Charge - the vehicle is not electronic";
                    throw new Exception(k_ErrorName);
                }
            }
            else
            {
                const string k_ErrorName = "Charge - the vehicle doesn't exist in the garage";
                throw new Exception(k_ErrorName);
            }
        }

        public string ShowVehicleDetails(string i_LicenseNumber)
        {
            string vehicleDetails;
            if (checkIfVehicleAlreadyInTheGarage(i_LicenseNumber))
            {
                Vehicle vehicle = m_GarageCustomer[i_LicenseNumber].Vehicle;

                if (vehicle != null)
                {
                    string garageDetails = string.Format(
 @"Garage details:
Owners name: {0}, Owners phone-number: {1}, Vehicle status: {2}",
                        m_GarageCustomer[i_LicenseNumber].Name,
                        m_GarageCustomer[i_LicenseNumber].PhoneNumber,
                        m_GarageCustomer[i_LicenseNumber].VehicleStatus);

                    vehicleDetails = string.Format(@"{0}{2}{1}", garageDetails, vehicle.ToString(), Environment.NewLine);
                }
                else
                {
                    const string k_ErrorName = "Show Vehicle Details - couldn't cast to vehicle type ";
                    throw new Exception(k_ErrorName);
                }
            }
            else
            {
                const string k_ErrorName = "Show Vehicle Details - the vehicle doesn't exist in the garage";
                throw new Exception(k_ErrorName);
            }

            return vehicleDetails;
        }
    }
}
