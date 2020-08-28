using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    
    public class UserInterface
    {
        //-----------------------------------------------------------------------------------------------------------------------//
        public void InitializeUI()
        {
            openGarage();
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        private void openGarage()
        {
            Garage garage = new Garage();
            int userInputChoice;
            bool userExit = false;

            while (!userExit)
            {
                this.printMenu();
                userInputChoice = (int)this.getValidInputValueInRange(1, 8);
                this.doActionUserAsked(userInputChoice, ref userExit, garage);
                Console.Clear();
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        private void printMenu()
        {
            Console.Write(@"Menu options:

1. Enter a vehicle to the garage.
2. Show all the vehicle license plates admitted in the garage.
3. Change vehicle status in the garage.
4. Fill up tires of a chosen vehicle to the max capacity.
5. Fuel up a vehicle.
6. Charge up a vehicle.
7. Show all the information of a vehicle chosen by license plate number
8. Exit garage.

Please enter the choice number you would like do: ");
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        private void printVehicleMenu()
        {
            Console.Write(@"Please choose the vehicle type:

1. Electric car.
2. Fuel car.
3. Electric motorcycle.
4. Fuel motorcycle
5. Truck
Choice: ");
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        private int printMenuForLicensePlatesGetUserChoice()
        {
            Console.WriteLine("Printing license plates by state filter:" + Environment.NewLine);
            Console.Write(@"Menu options:

1. Filter by state in the garage
2. No filtering
Choice: ");
            int choiceNum = (int)this.getValidInputValueInRange(1, 2);

            return choiceNum;
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        private int printWrongInputMenuGetUserInput()
        {
            Console.WriteLine(@"Incorrect input, please choose: 

1. Input again
2. Go back to menu
Choice: ");
            int choiceNum = (int)this.getValidInputValueInRange(1, 2);

            return choiceNum;
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        private void printFuelMenu()
        {
            Console.Write(@"Please choose the fuel type:
1. Octan95.
2. Octan96.
3. Octan98.
4. Soler.
Choice: ");
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        private void printBackToMenuPause()
        {
            Console.WriteLine("Press any key to go back to menu");
            Console.ReadKey();
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        private void doActionUserAsked(int i_UserChoice, ref bool io_UserExit, Garage i_Garage)
        {
            io_UserExit = false;
            Console.Clear();

            switch(i_UserChoice)
            {
                case 1:
                    this.getVehicleInformation(i_Garage);
                    break;
                case 2:
                    this.printLicensePlatesInGarage((int)this.printMenuForLicensePlatesGetUserChoice(), i_Garage);
                    break;
                case 3:
                    this.changeVehicleStateInGarage(i_Garage);
                    break;
                case 4:
                    this.fillTiresOfVehicle(i_Garage);
                    break;
                case 5:
                    Console.WriteLine("Fueling up a vehicle in the garage:" + Environment.NewLine);
                    this.powerUpVehicle(i_Garage, Engine.eEngineType.Fuel);
                    break;
                case 6:
                    Console.WriteLine("Charging up a vehicle in the garage:" + Environment.NewLine);
                    this.powerUpVehicle(i_Garage, Engine.eEngineType.Electric);
                    break;
                case 7:
                    this.printVehicleInformationFromGarage(i_Garage);
                    break;
                case 8:
                    Console.WriteLine("Thanks for visiting, take care, adios, bye!" + Environment.NewLine);
                    System.Threading.Thread.Sleep(2000);
                    io_UserExit = true;
                    break;
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        private void getVehicleInformation(Garage i_Garage)
        {
            Console.WriteLine("Adding vehicle to the garage:" + Environment.NewLine);
            Garage.InformationOfVehicle informationOfVehicle = null;
            string licenseNumber = this.getLicenseNumberFromUser();

            if (i_Garage.VehiclesInTheGarage.TryGetValue(licenseNumber, out informationOfVehicle))
            {
                Console.WriteLine("This license number already exists in the garage, changing state to In Repair");
                informationOfVehicle.State = Garage.InformationOfVehicle.eVehicleStateInGarage.InRepair;
            }
            else
            {
                this.printVehicleMenu();
                VehicleAllocator.eVehicleType vehicleType = (VehicleAllocator.eVehicleType)this.getValidInputValueInRange(1, 5);
                Vehicle newVehicle = VehicleAllocator.AllocateVehicle(vehicleType, licenseNumber);
                informationOfVehicle = this.fillInformationForVehicle(newVehicle, vehicleType);
                i_Garage.VehiclesInTheGarage.Add(licenseNumber, informationOfVehicle);
                Console.WriteLine("Vehicle added to the garage successfully!");
            }

            this.printBackToMenuPause();
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        private Garage.InformationOfVehicle fillInformationForVehicle(Vehicle i_Vehicle, VehicleAllocator.eVehicleType i_VehicleType)
        {
            string manufactorName;
            float currentAirPressure, currentEnergyAmount;

            Console.Write("Please enter the model: ");
            i_Vehicle.Model = this.getValidStringInputLettersAndNumbers();
            Console.Write("Please enter wheels manufactor name: ");
            manufactorName = this.getValidStringInputLettersAndNumbers();
            Console.Write("Please enter wheels current air pressure: ");
            currentAirPressure = this.getValidInputValueInRange(0, i_Vehicle.Wheels.First().MaxAirPressure);
            Console.Write("Please enter current energy left in your vehicle: ");

            if (i_Vehicle.Engine is Engine.FuelEngine)
            {
                currentEnergyAmount = getValidInputValueInRange(0, (i_Vehicle.Engine as Engine.FuelEngine).MaxFuelCapacity);
            }
            else
            {
                currentEnergyAmount = getValidInputValueInRange(0, (i_Vehicle.Engine as Engine.ElectricEngine).MaxBatteryTime);
            }

            i_Vehicle.SetVehicleWheels(manufactorName, currentAirPressure);
            i_Vehicle.SetCurrentEnergyAmount(currentEnergyAmount);
            i_Vehicle.SetEnergyPrecentage();
            this.completeVehicleInformation(ref i_Vehicle, i_VehicleType);
            this.getOwnerInformation(out string ownerName, out string ownerPhoneNumber);
            return new Garage.InformationOfVehicle(ownerName, ownerPhoneNumber, i_Vehicle);
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        private string getLicenseNumberFromUser()
        {
            Console.Write("Please enter the license number of the vehicle: ");
            string licenseNumber = this.getValidStringInputLettersAndNumbers();

            return licenseNumber;
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        private void completeVehicleInformation(ref Vehicle io_Vehicle, VehicleAllocator.eVehicleType i_VehicleType)
        {
            List<string> questions = VehicleAllocator.GetQuestionsAboutVehicle(i_VehicleType, io_Vehicle);
            List<string> answers = this.getAnswersAboutVehicle(questions);
            bool isWrongAnswer = true;

            while (isWrongAnswer)
            {
                try
                {
                    VehicleAllocator.SetAnswersAboutVehicle(i_VehicleType, io_Vehicle, answers);
                    isWrongAnswer = false;
                }
                catch (Exception exception)
                {
                    if (exception is FormatException || exception is ValueOutOfRangeException || exception is ArgumentException)
                    {
                        while (exception != null)
                        {
                            Console.Write(exception.Message);
                            answers[int.Parse(exception.Source)] = Console.ReadLine();
                            exception = exception.InnerException;
                        }
                    }
                    else
                    {
                        throw exception;
                    }
                }
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        private List<string> getAnswersAboutVehicle(List<string> i_Questions)
        {
            List<string> answers = new List<string>(i_Questions.Capacity);

            foreach (string currentString in i_Questions)
            {
                Console.Write(currentString);
                answers.Add(Console.ReadLine());
            }

            return answers;
        }
        //-----------------------------------------------------------------------------------------------------------------------//

        private void getOwnerInformation(out string o_ownerName, out string o_ownerPhoneNumber)
        {
            o_ownerName = this.getValidStringInputLettersAndSpaces("Please Enter your name: ");
            o_ownerPhoneNumber = this.getValidUserPositiveNumberInput("Please enter your phone number: ").ToString();
        }
       
        private string getValidStringInputLettersAndSpaces(string i_Message)
        {
            bool isValidNameString = false;
            Console.Write(i_Message);
            string stringFromUser = Console.ReadLine();

            while (!isValidNameString)
            {
                isValidNameString = true;

                foreach (char currentChar in stringFromUser)
                {
                    if (!char.IsLetter(currentChar) && !(currentChar == ' '))
                    {
                        isValidNameString = false;
                        Console.Write("Only letters and spaces allowed, please enter again: ");
                        stringFromUser = Console.ReadLine();
                        break;
                    }
                }
            }

            return stringFromUser;
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        private void printLicensePlatesInGarage(int i_UserChoice, Garage i_Garage)
        {
            Garage.InformationOfVehicle.eVehicleStateInGarage state = Garage.InformationOfVehicle.eVehicleStateInGarage.Default;

            if (i_UserChoice == 1)
            {
                state = getVehicleStateFromUser(); 
            }

            string licensePlates = i_Garage.GetLicensePlatesByState(state);

            if (licensePlates.Length != 0)
            {
                Console.WriteLine(licensePlates);
            }
            else
            {
                Console.WriteLine("No vehicles with this state in the garage.");
            }

            this.printBackToMenuPause();
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        private Garage.InformationOfVehicle.eVehicleStateInGarage getVehicleStateFromUser()
        {
            Garage.InformationOfVehicle.eVehicleStateInGarage state;
            Console.Write(@"Choose the desired state: 
1. In repair
2. Repaired
3. Paid
Choice: ");
            int choiceNum = (int)this.getValidInputValueInRange(1, 3);
            state = (Garage.InformationOfVehicle.eVehicleStateInGarage)choiceNum;

            return state;
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        private void changeVehicleStateInGarage(Garage i_Garage)
        {
            Console.WriteLine("Changing vehicle state in the garage:" + Environment.NewLine);
            Garage.InformationOfVehicle.eVehicleStateInGarage state = this.getVehicleStateFromUser();
            int userChoice = 1;

            while (userChoice == 1)
            {
                try
                {
                    string licenseNumber = this.getLicenseNumberFromUser();
                    i_Garage.ChangeVehicleState(licenseNumber, state);
                    userChoice = 2;
                    Console.WriteLine("Vehicle state has been changed successfully!");
                    this.printBackToMenuPause();
                }
                catch (ArgumentException exception)
                {
                    Console.WriteLine(exception.Message);
                    userChoice = this.printWrongInputMenuGetUserInput();
                }
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        private void fillTiresOfVehicle(Garage i_Garage)
        {
            Console.WriteLine("Filling tires of a vehicle to the max:" + Environment.NewLine);
            int userChoice = 1;

            while (userChoice == 1)
            {
                try
                {
                    string licenseNumber = this.getLicenseNumberFromUser();
                    i_Garage.FillTiresToMaxByLicensePlate(licenseNumber);
                    userChoice = 2;
                    Console.WriteLine("Tires have been inflated to the maximum capacity successfully!");
                    this.printBackToMenuPause();
                }
                catch(ArgumentException exception)
                {
                    Console.WriteLine(exception.Message);
                    userChoice = this.printWrongInputMenuGetUserInput();
                }
                catch (ValueOutOfRangeException exception)
                {
                    Console.WriteLine(exception.Message);
                    userChoice = this.printWrongInputMenuGetUserInput();
                }
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        private string getValidStringInputLettersAndNumbers()
        {
            string userStringInput = Console.ReadLine();
            bool isInvalidString = true;
             
            while(isInvalidString)
            {
                isInvalidString = false;

                foreach(char currentChar in userStringInput)
                {
                    if (!char.IsDigit(currentChar) && !char.IsLetter(currentChar)) 
                    {
                        Console.Write("Input string can only consist of letters and numbers, please try again: ");
                        userStringInput = Console.ReadLine();
                        isInvalidString = true;
                        break;
                    }
                }  
            }

            return userStringInput;
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        private float getValidInputValueInRange(float i_MinValue, float i_MaxValue)
        {
            string userInputChoice = Console.ReadLine();
            float userInputNumerical = 0;

            while (!float.TryParse(userInputChoice, out userInputNumerical) ||
                   ValueOutOfRangeException.ValueOutOfRange(userInputNumerical, i_MaxValue, i_MinValue))
            {
                Console.WriteLine(@"Choice is out of boundaries, please enter input again in the range {0} to {1}: ",
                    i_MinValue,
                    i_MaxValue);
                userInputChoice = Console.ReadLine();
            }

            return userInputNumerical;
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        private void powerUpVehicle(Garage i_Garage, Engine.eEngineType i_EngineType)
        {
            int userMenuChoice = 1;

            while (userMenuChoice == 1)
            {
                try
                {
                    string licenseNumber = this.getLicenseNumberFromUser();
                    this.getFuelTypeFromUserAndHowMuchToAdd(out int userFuelTypeChoice, out float howMuchToAdd, i_EngineType);
                    i_Garage.FillEngineUp(licenseNumber, howMuchToAdd, i_EngineType, (Engine.FuelEngine.eFuelType)userFuelTypeChoice);
                    userMenuChoice = 2;
                    Console.WriteLine("Engine of vehicle succesfully filled up");
                    this.printBackToMenuPause();
                    break;
                }
                catch (FormatException exception)
                {
                    Console.WriteLine(exception.Message);
                }
                catch (ValueOutOfRangeException exception)
                {
                    Console.WriteLine(exception.Message);

                    if (exception.MinValue != exception.MaxValue)
                    {
                        Console.WriteLine("Value ranges are {0} to {1}", exception.MinValue, exception.MaxValue);
                    }
                }
                catch (ArgumentException exception)
                {
                    Console.WriteLine(exception.Message);
                }

                userMenuChoice = this.printWrongInputMenuGetUserInput();
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        private void getFuelTypeFromUserAndHowMuchToAdd(out int o_UserChoiceFuel, out float o_HowMuchToAdd, Engine.eEngineType i_EngineType)
        {
            o_UserChoiceFuel = 0;

            if (i_EngineType == Engine.eEngineType.Fuel)
            {
                Console.WriteLine("Please enter the type of fuel you want to add");
                this.printFuelMenu();
                o_UserChoiceFuel = (int)this.getValidInputValueInRange(1, 4);
            }
            
            Console.Write("Please enter the amount you want to power the engine: ");
            o_HowMuchToAdd = this.getValidInputValueInRange(0, 120);
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        private int getValidUserPositiveNumberInput(string i_Message)
        {
            Console.Write(i_Message);
            string userInput = Console.ReadLine();
            int userInputNumeric;

            while (!int.TryParse(userInput, out userInputNumeric) || userInputNumeric < 1) 
            {
                Console.WriteLine("Only positive numbers allowed, please try again: ");
                userInput = Console.ReadLine();
            }

            return userInputNumeric;
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        private void printVehicleInformationFromGarage(Garage i_Garage)
        {
            Console.WriteLine("Printing vehicle information from the garage by license number:" + Environment.NewLine);
            int userChoice = 1;

            while (userChoice == 1)
            {
                try
                {
                    string licenseNumber = this.getLicenseNumberFromUser();
                    Garage.InformationOfVehicle userVehicle = i_Garage.CheckForLicensePlate(licenseNumber);
                    Console.WriteLine(userVehicle.ToString());
                    this.printBackToMenuPause();
                    userChoice = 2;
                }
                catch (ArgumentException exception)
                {
                    Console.WriteLine(exception.Message);
                    userChoice = this.printWrongInputMenuGetUserInput();
                }
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------//
    }
}
