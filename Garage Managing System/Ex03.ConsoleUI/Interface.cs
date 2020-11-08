using System;
using System.Collections.Generic;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{

    // $G$ CSS-000 (-3) The class name is not meaningful and understandable - this is the user interface
    public class Interface
    {
        public void RunSystem(Garage i_TheGarage, GarageObjectGenerator i_VehicleGenerator)
        {
            Console.WriteLine("Welcome To The Garage!{0}", Environment.NewLine);

            string menuOption = string.Empty;

            // $G$ CSS-018 (-3) You should have used enumerations here.

            while (true)
            {
                PrintMenu();
                menuOption = Console.ReadLine();

                if (menuOption == "1")
                {
                    AddNewVehicleToGarage(i_VehicleGenerator, i_TheGarage);
                }
                else if (menuOption == "2")
                {
                    ShowLicensePlateNumberOfVehiclesInGarage(i_TheGarage);
                }
                else if (menuOption == "3")
                {
                    ChangeVehicleStatus(i_TheGarage);
                }
                else if (menuOption == "4")
                {
                    InflateVehicleWheels(i_TheGarage);
                }
                else if (menuOption == "5")
                {
                    FuelVehicle(i_TheGarage);
                }
                else if (menuOption == "6")
                {
                    ChargeVehicle(i_TheGarage);
                }
                else if (menuOption == "7")
                {
                    ShowDetailsOfVehicle(i_TheGarage);
                }
                else if (menuOption == "8")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Option not valid!{0}", Environment.NewLine);
                }
            }
        }

        public void PrintMenu()
        {
            System.Console.WriteLine(@"


Garage Menu, Please choose from the following:
1) Add new vehicle to the garage
2) Show vehicles license plates in the garage
3) Change vehicle status in the garage
4) Inflate vehicle wheels
5) Fuel vehicle
6) Charge electric vehicle
7) Show vehicle details (find by license plate)
8) Exit");
        }

        public void AddNewVehicleToGarage(GarageObjectGenerator i_VehicleGenerator, Garage i_TheGarage)
        {
            bool newCustomerCreated = false;
            Customer newCustomer = null;

            while (!newCustomerCreated)
            {
                Console.WriteLine("Hello and welcome to The Garage! {0}Please enter your name: (and press 'Enter')", Environment.NewLine);
                string name = Console.ReadLine();
                Console.WriteLine("Please enter your phone number: (10 digits and press 'Enter')");
                string phoneNumber = Console.ReadLine();

               try
               {
                    newCustomer = new Customer(name, phoneNumber);
                    newCustomerCreated = true;
               }
               catch (Exception exception)
               {
                    Console.WriteLine(exception.Message);
               }
            }

            List<string> informationDemandsForVehicle = null;

            int chosenVehicleToAdd = getVehicleNumberToGenerate(i_VehicleGenerator);

            string licenseNumber = getLicensePlateNumber();

            Vehicle vehicleToAddToGarage =
                i_VehicleGenerator.GenerateNewVehicle(chosenVehicleToAdd, licenseNumber, out informationDemandsForVehicle);

            newCustomer.Vehicle = vehicleToAddToGarage;

            do
            {
                List<string> answersListFromUser = getDetailsFromUser(informationDemandsForVehicle);

                i_VehicleGenerator.VehiclesSetter(informationDemandsForVehicle, answersListFromUser, vehicleToAddToGarage);
            }
            while (informationDemandsForVehicle.Count != 0); // continue from here

            string msgForUser;
            try
            {
                i_TheGarage.EnterNewVehicle(newCustomer, out msgForUser);
                if (msgForUser == null)
                {
                    Console.WriteLine("vehicle entered succesfully to the garage!");
                }
                else
                {
                    Console.WriteLine(msgForUser);
                }
            }
            catch (Exception valueExcepion)
            {
                Console.WriteLine(valueExcepion.Message);
                Console.WriteLine("could not enter your vehicle, the garage can not handle it");
            }
        }

        public void ShowLicensePlateNumberOfVehiclesInGarage(Garage i_TheGarage)
        {
            bool repairedVehicles = false, fixedVehicles = false, paidVehicles = false;
            string repairedVehiclesAnswer, fixedVehiclesAnswer, paidVehiclesAnswer;
            List<string> licenseNumberList;
            Console.WriteLine("Do you want to show all the repaired vehicles (yes/no)");
            repairedVehiclesAnswer = Console.ReadLine();
            Console.WriteLine("Do you want to show all the fixed vehicles (yes/no)");
            fixedVehiclesAnswer = Console.ReadLine();
            Console.WriteLine("Do you want to show all the paid vehicles (yes/no)");
            paidVehiclesAnswer = Console.ReadLine();

            try
            {
                repairedVehicles = convertStringYesOrNoToTrueOrFalse(repairedVehiclesAnswer);
                fixedVehicles = convertStringYesOrNoToTrueOrFalse(fixedVehiclesAnswer);
                paidVehicles = convertStringYesOrNoToTrueOrFalse(paidVehiclesAnswer);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            
            licenseNumberList = i_TheGarage.ShowLicenseNumbers(repairedVehicles, fixedVehicles, paidVehicles);
            const int k_NewLineCount = 5;
            int counter = 0;
            foreach (string licenseNum in licenseNumberList)
            {
                Console.Write(licenseNum + " ");
                counter++;
                if (counter == k_NewLineCount)
                {
                    Console.Write("{0}", Environment.NewLine);
                    counter = 0;
                }
            }

            Console.WriteLine("{0}", Environment.NewLine);
        }

        public void ChangeVehicleStatus(Garage i_TheGarage)
        {
            Console.WriteLine("Please enter your car's license plate number");
            string lisencePlateNumber = Console.ReadLine();

            Console.WriteLine("Please enter the desired status number you want {0}1) In Repair {0}2) Fixed {0}3) Paid{0} (enter a number and press 'Enter')", Environment.NewLine);
            bool isNumber = byte.TryParse(Console.ReadLine(), out byte statusNumber);

            if(isNumber)
            {
                try
                {
                    i_TheGarage.ChangeStatus(lisencePlateNumber, statusNumber);
                    Console.WriteLine("Vehicle Status has changed");
                }
                catch (FormatException formatException)
                {
                    Console.WriteLine(formatException.Message);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
            else
            {
                Console.WriteLine("Status number is illegal");
            }
        }

        public void InflateVehicleWheels(Garage i_TheGarage)
        {
            float airPressureToAdd;
            Console.WriteLine("Please enter your vehicle's license plate number");
            string licensePlateNumber = Console.ReadLine();
            Console.WriteLine("Please enter how much air pressure you want to add");
            string airPressureToAddStr = Console.ReadLine();

            try
            {
                airPressureToAdd = float.Parse(airPressureToAddStr);
                i_TheGarage.InflateWheels(licensePlateNumber, airPressureToAdd);
                Console.WriteLine("Process ended successfully");
            }
            catch (ValueOutOfRangeException valueException)
            {
                Console.WriteLine(valueException.Message);
                if (valueException.MaxValue == 0)
                {
                    Console.WriteLine("The wheels are already Inflated");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        public void FuelVehicle(Garage i_TheGarage)
        {
            Console.WriteLine("Please enter your car's license plate number");
            string lisencePlateNumber = Console.ReadLine();

            Console.WriteLine("Please choose the vehicle's fuel type: {0}1) 95 {0}2) 96 {0}3) 98  {0}4) Soler {0}choose 1, 2, 3 or 4 and then press 'Enter')", Environment.NewLine);
            bool validDigitFuelType = byte.TryParse(Console.ReadLine(), out byte fuelTypeNumber);

            if(validDigitFuelType)
            {
                Console.WriteLine("Please enter amount of fuel for refuling: ");
                bool validAmount = float.TryParse(Console.ReadLine(), out float amountForFueling);

                if(validAmount)
                {
                    try
                    {
                        i_TheGarage.Refuel(lisencePlateNumber, fuelTypeNumber, amountForFueling);
                        Console.WriteLine("Vehicle fueled successfully");
                    }
                    catch (ArgumentException argumentException)
                    {
                        Console.WriteLine(argumentException.Message);
                    }
                    catch (ValueOutOfRangeException ValueException)
                    {
                        Console.WriteLine(ValueException.Message);
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid fueling amount");
                }
            }
            else
            {
                Console.WriteLine("Fuel type");
            }
        }

        public void ChargeVehicle(Garage i_TheGarage)
        {
            Console.WriteLine("Please enter your car's license plate number");
            string licensePlateNumber = Console.ReadLine();

            Console.WriteLine("Please enter minutes to charge ");
            bool validTimeToCharge = float.TryParse(Console.ReadLine(), out float timeToCharge);

            if(validTimeToCharge)
            {
                try
                {
                    i_TheGarage.Charge(licensePlateNumber, timeToCharge / 60);
                    Console.WriteLine("Vehicle charged successfully");
                }
                catch (ValueOutOfRangeException ValueException)
                {
                    Console.WriteLine(ValueException.Message);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
            else
            {
                Console.WriteLine("Invalid charging amount");
            }
        }
        
        public void ShowDetailsOfVehicle(Garage i_TheGarage)
        {
            Console.WriteLine("Please enter your car's license plate number");
            string licensePlateNumber = Console.ReadLine();
            Console.WriteLine();

            try
            {
                string vehicleDetails = i_TheGarage.ShowVehicleDetails(licensePlateNumber);
                Console.WriteLine(vehicleDetails);
            }
            catch (ArgumentException argumentException)
            {
                Console.WriteLine(argumentException.Message);
            }
            catch (ValueOutOfRangeException ValueException)
            {
                Console.WriteLine(ValueException.Message);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private string getLicensePlateNumber()
        {
            string licensePlateNumber = string.Empty;

            bool validLicensePlateNumber = false;

            while (!validLicensePlateNumber)
            {
                Console.WriteLine("Please enter 7 digits license plate number:(and press 'Enter')");

                licensePlateNumber = Console.ReadLine();

                validLicensePlateNumber = true;

                foreach (char character in licensePlateNumber)
                {
                    if (!char.IsDigit(character))
                    {
                        validLicensePlateNumber = false;
                        break;
                    }
                }

                if (!validLicensePlateNumber || !(licensePlateNumber.ToString().Length == 7))
                {
                     validLicensePlateNumber = false;
                    Console.WriteLine("Invalid license plate number");
                }
            }

            return licensePlateNumber;
        }

        private int getVehicleNumberToGenerate(GarageObjectGenerator i_VehicleGenerator)
        {
            const int k_DefaultIllegalValue = -1;
            int userChosenVehicle = k_DefaultIllegalValue;

            Console.WriteLine(@"Please Choose the vehicle you want to enter to the garage: (choose a number and press 'Enter')");

            int i = 1;
            foreach (Type type in i_VehicleGenerator.VehicleTypesMaintainedByGarage)
            {
                Console.WriteLine(string.Format("{0}) {1}", i, type.Name));
                i++;
            }

            bool validOption = false;

            while (!validOption)
            {
                validOption = int.TryParse(Console.ReadLine(), out userChosenVehicle);

                if (validOption && userChosenVehicle >= 1 && userChosenVehicle <= i_VehicleGenerator.VehicleTypesMaintainedByGarage.Count)
                {
                    validOption = true;
                }
                else
                {
                    Console.WriteLine(string.Format(
                        "Invalid input, Please Enter a number from 1 to {0}",
                        i_VehicleGenerator.VehicleTypesMaintainedByGarage.Count));
                    validOption = false;
                }
            }

            return userChosenVehicle - 1;
        }


        // $G$ CSS-013 (-3) Bad variable name (should be in the form of: i_CamelCase).
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

        private List<string> getDetailsFromUser(List<string> i_QuestionsList)
        {
            List<string> answerList = new List<string>(i_QuestionsList.Capacity);

            foreach (string question in i_QuestionsList)
            {
                if (question != null)
                {
                    Console.WriteLine(question);
                }

                answerList.Add(Console.ReadLine());
            }

            return answerList;
        }


        // $G$ CSS-999 (-5) If you use string as a condition --> then you should have use constant here.
        private bool convertStringYesOrNoToTrueOrFalse(string i_YesOrNo)
        {
            string yesOrNoUpper = i_YesOrNo.ToUpper();
            bool answer;
            if (yesOrNoUpper == "YES")
            {
                answer = true;
            }
            else if (yesOrNoUpper == "NO")
            {
                answer = false;
            }
            else
            {
                throw new Exception("Invalid input - please type 'yes' or 'no'");
            }

            return answer;
        }
    }
}
