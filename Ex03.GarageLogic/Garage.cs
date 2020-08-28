using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        //-----------------------------------------------------------------------------------------------------------------------//
        private readonly Dictionary<string, InformationOfVehicle> r_VehiclesInTheGarage;
        //-----------------------------------------------------------------------------------------------------------------------//
        public Garage()
        {
            r_VehiclesInTheGarage = new Dictionary<string, InformationOfVehicle>();
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        public Dictionary<string, InformationOfVehicle> VehiclesInTheGarage
        {
            get
            {
                return this.r_VehiclesInTheGarage;
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        public string GetLicensePlatesByState(InformationOfVehicle.eVehicleStateInGarage i_State)
        {
            StringBuilder KeyString = new StringBuilder();
            int index = 0;

            foreach (KeyValuePair<string, InformationOfVehicle> currentKey in this.r_VehiclesInTheGarage)
            {
                if (i_State == InformationOfVehicle.eVehicleStateInGarage.Default || currentKey.Value.State == i_State)
                {
                    ++index;
                    KeyString.Append(index.ToString() + ". ");
                    KeyString.AppendLine(currentKey.Key);
                }
            }

            return KeyString.ToString();
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        public void ChangeVehicleState(string i_LicenseNumber, InformationOfVehicle.eVehicleStateInGarage i_NewState)
        {
            try
            {
                InformationOfVehicle vehicleInformation = this.CheckForLicensePlate(i_LicenseNumber);
                vehicleInformation.State = i_NewState;
            }
            catch (ArgumentException exception)
            {
                throw exception;
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        public void FillTiresToMaxByLicensePlate(string i_LicenseNumber)
        {
            try
            {
                InformationOfVehicle vehicleInformation = this.CheckForLicensePlate(i_LicenseNumber);
                vehicleInformation.Vehicle.FillTiresToMax();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        public InformationOfVehicle CheckForLicensePlate(string i_LicenseNumber)
        {
            bool foundVehicle = this.r_VehiclesInTheGarage.TryGetValue(i_LicenseNumber, out InformationOfVehicle vehicleInformation);

            if (!foundVehicle)
            {
                ArgumentException exception = new ArgumentException("Vehicle by this license number doesn't exist in the garage");
                throw exception;
            }
            else
            {
                return vehicleInformation;
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        public void FillEngineUp(string i_LicensePlate, float i_HowMuchToIncrease, Engine.eEngineType i_EngineType, Engine.FuelEngine.eFuelType i_FuelType)
        {
            try
            {
                InformationOfVehicle vehicleToFuel = this.CheckForLicensePlate(i_LicensePlate);

                if ((vehicleToFuel != null) && (vehicleToFuel.Vehicle.Engine is Engine.FuelEngine) && (i_EngineType == Engine.eEngineType.Fuel))
                {
                    (vehicleToFuel.Vehicle.Engine as Engine.FuelEngine).Refuel(i_HowMuchToIncrease, i_FuelType);
                }
                else if ((vehicleToFuel != null) && (vehicleToFuel.Vehicle.Engine is Engine.ElectricEngine) && (i_EngineType == Engine.eEngineType.Electric))
                {
                    (vehicleToFuel.Vehicle.Engine as Engine.ElectricEngine).ChargeBattery(i_HowMuchToIncrease);
                }
                else
                {
                    if (i_EngineType == Engine.eEngineType.Fuel)
                    {
                        throw new ArgumentException("This Vehicle cannot be fueled" + Environment.NewLine);
                    }
                    else
                    {
                        throw new ArgumentException("This Vehicle cannot be charged" + Environment.NewLine); 
                    }
                }
            }
            catch (ArgumentException exception)
            {
                throw exception;
            }
            catch(ValueOutOfRangeException exeption)
            {
                throw exeption;
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        ///-----------------------------------------Nested class----------------------------------------------///
        public class InformationOfVehicle
        {
            //-----------------------------------------------------------------------------------------------------------------------//
            public enum eVehicleStateInGarage
            {
                Default,
                InRepair,
                Repaired,
                Paid
            }
            //-----------------------------------------------------------------------------------------------------------------------//
            private string m_OwnerName;
            private string m_OwnerPhoneNumber;
            private Vehicle m_Vehicle;
            private eVehicleStateInGarage m_State;
            //-----------------------------------------------------------------------------------------------------------------------//
            public InformationOfVehicle(string i_OwnerName, string i_PhoneNumber, Vehicle i_Vehicle)
            {
                m_OwnerName = i_OwnerName;
                m_OwnerPhoneNumber = i_PhoneNumber;
                m_State = eVehicleStateInGarage.InRepair;
                m_Vehicle = i_Vehicle;
            }
            //-----------------------------------------------------------------------------------------------------------------------//
            public eVehicleStateInGarage State
            {
                get
                {
                    return this.m_State;
                }
                set
                {
                    this.m_State = value;
                }
            }
            //-----------------------------------------------------------------------------------------------------------------------//
            public string OwnerName
            {
                get
                {
                    return this.m_OwnerName;
                }
                set
                {
                    this.m_OwnerName = value;
                }
            }
            //-----------------------------------------------------------------------------------------------------------------------//
            public string OwnerPhoneNumber
            {
                get
                {
                    return this.m_OwnerPhoneNumber;
                }
                set
                {
                    this.m_OwnerPhoneNumber = value;
                }
            }
            //-----------------------------------------------------------------------------------------------------------------------//
            public Vehicle Vehicle
            {
                get
                {
                    return this.m_Vehicle;
                }
                set
                {
                    this.m_Vehicle = value;
                }
            }
            //-----------------------------------------------------------------------------------------------------------------------//
            public override string ToString()
            {
                StringBuilder vehicleInGarageDetails = new StringBuilder();
                vehicleInGarageDetails.AppendFormat(@"Owner information:
    Name: {0}
    Phone Number: {1}
", 
this.m_OwnerName,
this.m_OwnerPhoneNumber);
                vehicleInGarageDetails.AppendFormat(this.m_Vehicle.ToString());
                vehicleInGarageDetails.AppendFormat("Vehicle's status: {0}" + Environment.NewLine, this.m_State.ToString());
                return vehicleInGarageDetails.ToString();
            }
            //-----------------------------------------------------------------------------------------------------------------------//
        }
        ///-----------------------------------------Nested class----------------------------------------------///
    }
}
