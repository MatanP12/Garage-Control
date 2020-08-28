using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public static class VehicleAllocator
    {
        //-----------------------------------------------------------------------------------------------------------------------//
        public enum eVehicleType
        {
            ElectricCar = 1,
            FueledCar = 2,
            ElectricMotorcycle = 3,
            FueledMotorCycle = 4,
            Truck = 5
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        public static Vehicle AllocateVehicle(eVehicleType i_VehicleType, string i_LicenseNumber)
        {
            Vehicle newVehicle;
            Engine newEngine;

            switch (i_VehicleType)
            {
                case eVehicleType.ElectricCar:
                    newEngine = new Engine.ElectricEngine(2.1f);
                    newVehicle = new Car(newEngine, i_LicenseNumber);
                    break;
                case eVehicleType.FueledCar:
                    newEngine = new Engine.FuelEngine(Engine.FuelEngine.eFuelType.Octan96, 60);
                    newVehicle = new Car(newEngine, i_LicenseNumber);
                    break;
                case eVehicleType.ElectricMotorcycle:
                    newEngine = new Engine.ElectricEngine(1.2f);
                    newVehicle = new Motorcycle(newEngine, i_LicenseNumber);
                    break;
                case eVehicleType.FueledMotorCycle:
                    newEngine = new Engine.FuelEngine(Engine.FuelEngine.eFuelType.Octan95, 7f);
                    newVehicle = new Motorcycle(newEngine, i_LicenseNumber);
                    break;
                case eVehicleType.Truck:
                    newEngine = new Engine.FuelEngine(Engine.FuelEngine.eFuelType.Soler, 120f);
                    newVehicle = new Truck(newEngine, i_LicenseNumber);
                    break;
                default:
                    newEngine = null;
                    newVehicle = null;
                    break;
            }

            return newVehicle;
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        public static List<string> GetQuestionsAboutVehicle(VehicleAllocator.eVehicleType i_VehicleType, Vehicle i_Vehicle)
        {
            List<string> questionsList = null;

            switch(i_VehicleType)
            {
                case eVehicleType.ElectricCar:
                case eVehicleType.FueledCar:
                    questionsList = (i_Vehicle as Car).GetQuestionStrings();
                    break;
                case eVehicleType.ElectricMotorcycle:
                case eVehicleType.FueledMotorCycle:
                    questionsList = (i_Vehicle as Motorcycle).GetQuestionStrings();
                    break;
                case eVehicleType.Truck:
                    questionsList = (i_Vehicle as Truck).GetQuestionStrings();
                    break;
            }

            return questionsList;
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        public static void SetAnswersAboutVehicle(VehicleAllocator.eVehicleType i_VehicleType, Vehicle i_Vehicle, List<string> i_Answers)
        {
            switch (i_VehicleType)
            {
                case eVehicleType.ElectricCar:
                case eVehicleType.FueledCar:
                    (i_Vehicle as Car).SetAnswersToVehicle(i_Answers);
                    break;
                case eVehicleType.ElectricMotorcycle:
                case eVehicleType.FueledMotorCycle:
                    (i_Vehicle as Motorcycle).SetAnswersToVehicle(i_Answers);
                    break;
                case eVehicleType.Truck:
                    (i_Vehicle as Truck).SetAnswersToVehicle(i_Answers);
                    break;
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------//
    }
}
