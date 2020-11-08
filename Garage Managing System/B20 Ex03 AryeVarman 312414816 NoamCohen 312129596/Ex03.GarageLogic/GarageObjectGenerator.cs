using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{

    // $G$ DSN-003 (-10) You should not make UI calls from logic classes.

    public class GarageObjectGenerator
    {
        private readonly List<Type> r_VehicleTypesMaintainedByGarage;

        public GarageObjectGenerator()
        {
            int numberOfVehiclesMaintainedByGarage = 5;
            r_VehicleTypesMaintainedByGarage = new List<Type>(numberOfVehiclesMaintainedByGarage)
            {
                typeof(ElectricCar),
                typeof(ElectricMotorcycle),
                typeof(FuelBaseCar),
                typeof(FuelBaseMotorcycle),
                typeof(Truck)
            };
        }

        public GarageObjectGenerator(List<Type> i_TypesList)
        {
            r_VehicleTypesMaintainedByGarage = new List<Type>(i_TypesList);
        }

        public List<Type> VehicleTypesMaintainedByGarage
        {
            get { return r_VehicleTypesMaintainedByGarage; }
        }

        public Vehicle GenerateNewVehicle(int i_VehicleNumber, string i_LicenseNumber, out List<string> o_InfoRequiredMessagesForUser)
        {
            Vehicle newVehicle = null;
            o_InfoRequiredMessagesForUser = null;

            if (i_VehicleNumber == 0)
            {
                newVehicle = generateNewElectricCar(i_LicenseNumber, out o_InfoRequiredMessagesForUser);
            }
            else if (i_VehicleNumber == 1)
            {
                newVehicle = generateNewElectricMotorcycle(i_LicenseNumber, out o_InfoRequiredMessagesForUser);
            }
            else if (i_VehicleNumber == 2)
            {
                newVehicle = generateNewFuelBasedCar(i_LicenseNumber, out o_InfoRequiredMessagesForUser);
            }
            else if (i_VehicleNumber == 3)
            {
                newVehicle = generateNewFuelBasedMotorcycle(i_LicenseNumber, out o_InfoRequiredMessagesForUser);
            }
            else if (i_VehicleNumber == 4)
            {
                newVehicle = generateNewTruck(i_LicenseNumber, out o_InfoRequiredMessagesForUser);
            }

            return newVehicle;
        }

        private Vehicle generateNewElectricCar(string i_LicenseNumber, out List<string> o_InfoRequiredMessagesForUser)
        {
            o_InfoRequiredMessagesForUser = new List<string>(6);

            addVehicleRequiredMessagesForUser(o_InfoRequiredMessagesForUser);
            addElectricVehicleRequiredMessagesForUser(o_InfoRequiredMessagesForUser);
            addCarRequiredMessagesForUser(o_InfoRequiredMessagesForUser);

            return new ElectricCar(i_LicenseNumber);
        }

        private Vehicle generateNewFuelBasedCar(string i_LicenseNumber, out List<string> o_InfoRequiredMessagesForUser)
        {
            o_InfoRequiredMessagesForUser = new List<string>(8);

            addVehicleRequiredMessagesForUser(o_InfoRequiredMessagesForUser);
            addFuelBasedVehicleRequiredMessagesForUser(o_InfoRequiredMessagesForUser);
            addCarRequiredMessagesForUser(o_InfoRequiredMessagesForUser);

            return new FuelBaseCar(i_LicenseNumber);
        }

        private Vehicle generateNewTruck(string i_LicenseNumber, out List<string> o_InfoRequiredMessagesForUser)
        {
            o_InfoRequiredMessagesForUser = new List<string>(7);

            addVehicleRequiredMessagesForUser(o_InfoRequiredMessagesForUser);
            addFuelBasedVehicleRequiredMessagesForUser(o_InfoRequiredMessagesForUser);
            addTruckRequiredMessagesForUser(o_InfoRequiredMessagesForUser);

            return new Truck(i_LicenseNumber);
        }

        private Vehicle generateNewElectricMotorcycle(string i_LicenseNumber, out List<string> o_InfoRequiredMessagesForUser)
        {
            o_InfoRequiredMessagesForUser = new List<string>(6);

            addVehicleRequiredMessagesForUser(o_InfoRequiredMessagesForUser);
            addElectricVehicleRequiredMessagesForUser(o_InfoRequiredMessagesForUser);
            addMotorcycleRequiredMessagesForUser(o_InfoRequiredMessagesForUser);

            return new ElectricMotorcycle(i_LicenseNumber);
        }

        private Vehicle generateNewFuelBasedMotorcycle(string i_LicenseNumber, out List<string> o_InfoRequiredMessagesForUser)
        {
            o_InfoRequiredMessagesForUser = new List<string>(7);

            addVehicleRequiredMessagesForUser(o_InfoRequiredMessagesForUser);
            addFuelBasedVehicleRequiredMessagesForUser(o_InfoRequiredMessagesForUser);
            addMotorcycleRequiredMessagesForUser(o_InfoRequiredMessagesForUser);

            return new FuelBaseMotorcycle(i_LicenseNumber);
        }

        private void addVehicleRequiredMessagesForUser(List<string> o_InfoRequiredMessagesForUser)
        {
            o_InfoRequiredMessagesForUser.Add("Please enter the vehicle's model name: (and then press 'Enter')");
            o_InfoRequiredMessagesForUser.Add("Please choose the Wheel's manufacture name: (and then press 'Enter')");
            o_InfoRequiredMessagesForUser.Add("Please choose the Wheel's Maximal air pressure (and then press 'Enter')");
        }



        // $G$ NTT-999 (-5) You should have use: Environment.NewLine instead of "\n".
        private void addFuelBasedVehicleRequiredMessagesForUser(List<string> o_InfoRequiredMessagesForUser)
        {
            o_InfoRequiredMessagesForUser.Add("Please choose the vehicle's fuel type: \n1) 95 \n2) 96 \n3) 98  \n4) Soler \nchoose 1, 2, 3 or 4 and then press 'Enter')");
            o_InfoRequiredMessagesForUser.Add("Please choose the vehicle's fuel tank capacity: (and then press 'Enter')");
        }

        private void addElectricVehicleRequiredMessagesForUser(List<string> o_InfoRequiredMessagesForUser)
        {
            o_InfoRequiredMessagesForUser.Add("Please choose the Vehicle's Maximal Battery Time span: (for example 2.1 and then press 'Enter')");
        }

        private void addCarRequiredMessagesForUser(List<string> o_InfoRequiredMessagesForUser)
        {
            o_InfoRequiredMessagesForUser.Add("Please choose the car's color: \n1) Red \n2) White \n3) Black \n4) Silver \n(and then press 'Enter')");
            o_InfoRequiredMessagesForUser.Add("Please choose the car's door number: (2, 3, 4 or 5 and then press 'Enter')");
        }

        private void addMotorcycleRequiredMessagesForUser(List<string> o_InfoRequiredMessagesForUser)
        {
            o_InfoRequiredMessagesForUser.Add("Please enter the motorcycle's engine capacity: (and then press 'Enter')");
            o_InfoRequiredMessagesForUser.Add("Please enter your motorcycle's license type: \n1) A \n2) A1 \n3) AA \n4) B \n choose 1, 2, 3 or 4 and then press 'Enter')");
        }

        private void addTruckRequiredMessagesForUser(List<string> o_InfoRequiredMessagesForUser)
        {
            o_InfoRequiredMessagesForUser.Add("Please choose the truck's cargo capacity: (and then press 'Enter')");
            o_InfoRequiredMessagesForUser.Add("is the truck has dangerous materials on is? \n1) yes \n2) no \n(and then press 'Enter')");
        }

        public void VehiclesSetter(List<string> i_QuestionsList, List<string> i_AnswersList, Vehicle i_VehicleInSetting)
        {
            string answer;

            List<string> questionsToRemove = new List<string>(i_QuestionsList.Capacity);

            foreach (string question in i_QuestionsList)
            {
                if (i_AnswersList.Count != 0)
                {
                    answer = i_AnswersList[0];
                    i_AnswersList.RemoveAt(0);

                    if (checkQuestionAndSendAnswerToConvertToTheVehicle(question, answer, i_VehicleInSetting))
                    {
                        questionsToRemove.Add(question); // maybe need to remove next iteration
                    }
                }
            }

            foreach(string question in questionsToRemove)
            {
                i_QuestionsList.Remove(question);
            }
        }

        private bool checkQuestionAndSendAnswerToConvertToTheVehicle(string i_Question, string i_Answer, Vehicle i_VehicleInSetting)
        {
            bool succeed = false;

            if (i_Question.Equals("Please enter the vehicle's model name: (and then press 'Enter')"))
            {
                i_VehicleInSetting.ModelName = i_Answer; // every model name is valid
                succeed = true;
            }
            else if (i_Question.Equals("Please choose the Wheel's manufacture name: (and then press 'Enter')"))
            {
                succeed = handleWheelsManufactoreName(i_Answer, i_VehicleInSetting);
            }
            else if (i_Question.Equals("Please choose the Wheel's Maximal air pressure (and then press 'Enter')"))
            {
                succeed = handleWheelsMaxAirPressure(i_Answer, i_VehicleInSetting);
            }
            else if (i_Question.Equals("Please choose the vehicle's fuel type: \n1) 95 \n2) 96 \n3) 98  \n4) Soler \nchoose 1, 2, 3 or 4 and then press 'Enter')"))
            {
                succeed = handleFuelType(i_Answer, i_VehicleInSetting);
            }
            else if (i_Question.Equals("Please choose the vehicle's fuel tank capacity: (and then press 'Enter')"))
            {
                succeed = handleFuelTankCapacity(i_Answer, i_VehicleInSetting);
            }
            else if (i_Question.Equals("Please choose the Vehicle's Maximal Battery Time span: (for example 2.1 and then press 'Enter')"))
            {
                succeed = handleBatteryTimeSpan(i_Answer, i_VehicleInSetting);
            }
            else if (i_Question.Equals("Please choose the car's color: \n1) Red \n2) White \n3) Black \n4) Silver \n(and then press 'Enter')"))
            {
                succeed = handleCarColor(i_Answer, i_VehicleInSetting);
            }
            else if (i_Question.Equals("Please choose the car's door number: (2, 3, 4 or 5 and then press 'Enter')"))
            {
                succeed = handleCarDoorNumber(i_Answer, i_VehicleInSetting);
            }
            else if (i_Question.Equals("Please enter the motorcycle's engine capacity: (and then press 'Enter')"))
            {
                succeed = handleMotocycleEngineCapacity(i_Answer, i_VehicleInSetting);
            }
            else if (i_Question.Equals("Please enter your motorcycle's license type: \n1) A \n2) A1 \n3) AA \n4) B \n choose 1, 2, 3 or 4 and then press 'Enter')"))
            {
                succeed = handleMotocycleLicenseType(i_Answer, i_VehicleInSetting);
            }
            else if (i_Question.Equals("Please choose the truck's cargo capacity: (and then press 'Enter')"))
            {
                succeed = handleTruckCargoVolume(i_Answer, i_VehicleInSetting);
            }
            else if (i_Question.Equals("is the truck has dangerous materials on is? \n1) yes \n2) no \n(and then press 'Enter')"))
            {
                succeed = handleDangerousMatirials(i_Answer, i_VehicleInSetting);
            }

            return succeed;
        }

        private bool handleFuelType(string i_Answer, Vehicle i_VehicleInSetting)
        {
            bool succeed = true;

            FuelBaseVehicle fuelVehicle = i_VehicleInSetting as FuelBaseVehicle;

            if (fuelVehicle != null)
            {
                succeed = byte.TryParse(i_Answer, out byte fuelNumber);

                if (succeed)
                {
                    try
                    {
                        fuelVehicle.FuelType = FuelBaseVehicle.ConvertNumToFuelType(fuelNumber);
                        succeed = true;
                    }
                    catch
                    {
                        succeed = false;
                    }
                }
            }
            else
            {
                succeed = false;
            }

            return succeed;
        }

        private bool handleFuelTankCapacity(string i_Answer, Vehicle i_VehicleInSetting)
        {
            bool succeed = true;

            FuelBaseVehicle fuelVehicle = i_VehicleInSetting as FuelBaseVehicle;

            if (fuelVehicle != null)
            {
                succeed = float.TryParse(i_Answer, out float fuelTankCapacity);

                if (succeed)
                {
                    try
                    {
                        fuelVehicle.MaxAmountOfFuel = fuelTankCapacity;
                        fuelVehicle.CurrentAmountOfFuel = fuelTankCapacity / 2;
                    }
                    catch
                    {
                        succeed = false;
                    }
                }
                else
                {
                    succeed = false;
                }
            }

            return succeed;
        }

        private bool handleBatteryTimeSpan(string i_Answer, Vehicle i_VehicleInSetting)
        {
            bool succeed = true;

            ElectricCar electricVehicle = i_VehicleInSetting as ElectricCar;
            ElectricMotorcycle electricMotorcycle = i_VehicleInSetting as ElectricMotorcycle;

            if (electricVehicle != null)
            {
                succeed = float.TryParse(i_Answer, out float batteryCapacity);

                if (succeed)
                {
                    try
                    {
                        electricVehicle.MaximalBatteryTime = batteryCapacity;
                        electricVehicle.BatteryTimeLeft = batteryCapacity / 2;
                    }
                    catch
                    {
                        succeed = false;
                    }
                }
                else
                {
                    succeed = false;
                }
            }

            if (electricMotorcycle != null)
            {
                succeed = float.TryParse(i_Answer, out float batteryCapacity);

                if (succeed)
                {
                    try
                    {
                        electricMotorcycle.MaximalBatteryTime = batteryCapacity;
                        electricMotorcycle.BatteryTimeLeft = batteryCapacity / 2;
                    }
                    catch
                    {
                        succeed = false;
                    }
                }
                else
                {
                    succeed = false;
                }
            }

            return succeed;
        }

        private bool handleCarColor(string i_Answer, Vehicle i_VehicleInSetting)
        {
            bool succeed = byte.TryParse(i_Answer, out byte colorNumber);

            ElectricCar electricCar = i_VehicleInSetting as ElectricCar;
            FuelBaseCar fuelCar = i_VehicleInSetting as FuelBaseCar;

            eColor color = ElectricCar.CovertNumToColor(colorNumber);

            if (succeed && fuelCar != null)
            {
                try
                {
                    fuelCar.Color = color;
                    succeed = true;
                }
                catch
                {
                    succeed = false;
                }
            }
            else if (succeed && electricCar != null)
            {
                try
                {
                    electricCar.Color = color;
                    succeed = true;
                }
                catch
                {
                    succeed = false;
                }
            }
            else
            {
                succeed = false;
            }

            return succeed;
        }

        private bool handleCarDoorNumber(string i_Answer, Vehicle i_VehicleInSetting)
        {
            bool succeed = byte.TryParse(i_Answer, out byte doorNumberInByte);

            ElectricCar electricCar = i_VehicleInSetting as ElectricCar;
            FuelBaseCar fuelCar = i_VehicleInSetting as FuelBaseCar;

            eDoorsNumber doorNumber = ElectricCar.CovertNumToDoorNumber(doorNumberInByte);

            if (succeed && fuelCar != null)
            {
                try
                {
                    fuelCar.DoorsNumber = doorNumber;
                    succeed = true;
                }
                catch
                {
                    succeed = false;
                }
            }
            else if (succeed && electricCar != null)
            {
                try
                {
                    electricCar.DoorsNumber = doorNumber;
                    succeed = true;
                }
                catch
                {
                    succeed = false;
                }
            }
            else
            {
                succeed = false;
            }

            return succeed;
        }

        private bool handleMotocycleEngineCapacity(string i_Answer, Vehicle i_VehicleInSetting)
        {
            bool succeed = int.TryParse(i_Answer, out int engineCapacity);

            ElectricMotorcycle electricMotorcycle = i_VehicleInSetting as ElectricMotorcycle;
            FuelBaseMotorcycle fuelBaseMotorcycle = i_VehicleInSetting as FuelBaseMotorcycle;

            if (succeed && electricMotorcycle != null)
            {
                try
                {
                    electricMotorcycle.EngineCapacity = engineCapacity;
                }
                catch
                {
                    succeed = false;
                }
            }
            else if (succeed && fuelBaseMotorcycle != null)
            {
                try
                {
                    fuelBaseMotorcycle.EngineCapacity = engineCapacity;
                }
                catch
                {
                    succeed = false;
                }
            }
            else
            {
                succeed = false;
            }

            return succeed;
        }

        private bool handleMotocycleLicenseType(string i_Answer, Vehicle i_VehicleInSetting)
        {
            bool succeed = byte.TryParse(i_Answer, out byte licenseNumber);

            ElectricMotorcycle electricMotorcycle = i_VehicleInSetting as ElectricMotorcycle;
            FuelBaseMotorcycle fuelBaseMotorcycle = i_VehicleInSetting as FuelBaseMotorcycle;

            eLicenseType licenseType = ElectricMotorcycle.ConvertNumToLicenseType(licenseNumber);

            if (succeed && electricMotorcycle != null)
            {
                try
                {
                    electricMotorcycle.LicenseType = licenseType;
                    succeed = true;
                }
                catch
                {
                    succeed = false;
                }
            }
            else if (succeed && fuelBaseMotorcycle != null)
            {
                try
                {
                    fuelBaseMotorcycle.LicenseType = licenseType;
                    succeed = true;
                }
                catch
                {
                    succeed = false;
                }
            }
            else
            {
                succeed = false;
            }

            return succeed;
        }

        private bool handleTruckCargoVolume(string i_Answer, Vehicle i_VehicleInSetting)
        {
            bool succeed = float.TryParse(i_Answer, out float cargoVolume);

            Truck truck = i_VehicleInSetting as Truck;

            if (succeed && truck != null)
            {
                try
                {
                    truck.CargoVolume = cargoVolume;
                }
                catch
                {
                    succeed = false;
                }
            }
            else
            {
                succeed = false;
            }

            return succeed;
        }

        private bool handleDangerousMatirials(string i_Answer, Vehicle i_VehicleInSetting)
        {
            const bool v_HasDangerousMetiarails = true;
            bool succeed = true;

            Truck truck = i_VehicleInSetting as Truck;

            if (i_Answer == "1" && truck != null)
            {
                truck.CarryingDangerousMaterials = v_HasDangerousMetiarails;
            }
            else if (i_Answer == "2" && truck != null)
            {
                truck.CarryingDangerousMaterials = !v_HasDangerousMetiarails;
            }
            else
            {
                succeed = false;
            }

            return succeed;
        }

        private bool handleWheelsManufactoreName(string i_Answer, Vehicle i_VehicleInSetting)
        {
            bool succeed = true;
            int numberOfWheelsToAdd = 0;

            List<Wheel> newWheelsForVehicle = null;

            if (i_Answer != null && i_Answer != string.Empty)
            {
                if (i_VehicleInSetting.Wheels == null)
                {
                    if (i_VehicleInSetting is ElectricCar || i_VehicleInSetting is FuelBaseCar)
                    {
                        numberOfWheelsToAdd = 4;
                    }
                    else if (i_VehicleInSetting is Truck)
                    {
                        numberOfWheelsToAdd = 16;
                    }
                    else if (i_VehicleInSetting is ElectricMotorcycle || i_VehicleInSetting is FuelBaseMotorcycle)
                    {
                        numberOfWheelsToAdd = 2;
                    }

                    newWheelsForVehicle = new List<Wheel>(numberOfWheelsToAdd);
                }
                else
                {
                    numberOfWheelsToAdd = i_VehicleInSetting.Wheels.Count;
                }

                for (int i = 0; i < numberOfWheelsToAdd; i++)
                {
                    newWheelsForVehicle.Add(new Wheel(i_Answer));
                }

                i_VehicleInSetting.Wheels = newWheelsForVehicle;
            }
            else
            {
                succeed = false;
            }

            return succeed;
        }

        private bool handleWheelsMaxAirPressure(string i_Answer, Vehicle i_VehicleInSetting)
        {
            bool succeed = float.TryParse(i_Answer, out float maxAirPressure);

            if (succeed && i_VehicleInSetting.Wheels != null && maxAirPressure > 0)
            {
                foreach (Wheel wheel in i_VehicleInSetting.Wheels)
                {
                    wheel.MaxAirPressure = maxAirPressure;
                    wheel.CurrentAirPressure = maxAirPressure;
                }
            }
            else
            {
                succeed = false;
            }

            return succeed;
        }
    }
}