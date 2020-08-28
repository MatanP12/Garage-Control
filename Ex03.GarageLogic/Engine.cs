using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{


    public abstract class Engine
    {
        //-----------------------------------------------------------------------------------------------------------------------//
        public enum eEngineType
        {
            Fuel,
            Electric
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        ////-----------------------------------------Nested class----------------------------------------------////
        public class ElectricEngine : Engine
        {
            //-----------------------------------------------------------------------------------------------------------------------//
            private float m_MaxBatteryTime;
            private float m_BatteryTimeLeft;
            //-----------------------------------------------------------------------------------------------------------------------//
            public ElectricEngine(float i_MaxBatteryTime) : base()
            {
                this.m_MaxBatteryTime = i_MaxBatteryTime;
            }
            //-----------------------------------------------------------------------------------------------------------------------//
            public float MaxBatteryTime
            {
                get
                {
                    return this.m_MaxBatteryTime;
                }
                set
                {
                    this.m_MaxBatteryTime = value;
                }
            }
            //-----------------------------------------------------------------------------------------------------------------------//
            public float BatteryTimeLeft
            {
                get
                {
                    return this.m_BatteryTimeLeft;
                }
                set
                {
                    this.m_BatteryTimeLeft = value;
                }
            }
            //-----------------------------------------------------------------------------------------------------------------------//
            public void ChargeBattery(float i_ChargeToAdd)
            {
                string errorMessage;
                this.m_BatteryTimeLeft += i_ChargeToAdd;

                if (this.m_BatteryTimeLeft > this.m_MaxBatteryTime)
                {
                    this.m_BatteryTimeLeft -= i_ChargeToAdd;

                    if (this.m_BatteryTimeLeft == this.m_MaxBatteryTime)
                    {
                        errorMessage = "Battery is already fully charged";
                    }
                    else
                    {
                        errorMessage = "Maximum battery charge exceeded";
                    }

                    throw new ValueOutOfRangeException(this.m_MaxBatteryTime - this.m_BatteryTimeLeft, 0, errorMessage); 
                }
            }
            //-----------------------------------------------------------------------------------------------------------------------//
            public override string ToString()
            {
                StringBuilder information = new StringBuilder();
                information.AppendFormat(@"Electric engine:
    Maximum batteryTime: {0}
    Battery time left: {1}",
                this.m_MaxBatteryTime.ToString(),
                this.m_BatteryTimeLeft.ToString());
                information.Append(Environment.NewLine);

                return information.ToString();
            }
            //-----------------------------------------------------------------------------------------------------------------------//
        }
        ///-----------------------------------------Nested class----------------------------------------------///
        public class FuelEngine : Engine
        {
            //-----------------------------------------------------------------------------------------------------------------------//
            public enum eFuelType
            {
                Defualt = 0,
                Octan95 = 1,
                Octan96 = 2,
                Octan98 = 3,
                Soler = 4
            }
            //-----------------------------------------------------------------------------------------------------------------------//
            private eFuelType m_FuelType;
            private float m_MaxFuelCapacity;
            private float m_FuelLeft;
            //-----------------------------------------------------------------------------------------------------------------------//
            public FuelEngine(eFuelType i_FuelType, float i_MaxFuel)
            {
                this.m_FuelType = i_FuelType;
                this.m_MaxFuelCapacity = i_MaxFuel;
            }
            //-----------------------------------------------------------------------------------------------------------------------//
            public eFuelType FuelType
            {
                get
                {
                    return this.m_FuelType;
                }
                set
                {
                    this.m_FuelType = value;
                }
            }
            //-----------------------------------------------------------------------------------------------------------------------//
            public float FuelLeft
            {
                get
                {
                    return this.m_FuelLeft;
                }
                set
                {
                    this.m_FuelLeft = value;
                }
            }
            //-----------------------------------------------------------------------------------------------------------------------//
            public float MaxFuelCapacity
            {
                get
                {
                    return this.m_MaxFuelCapacity;
                }
                set
                {
                    this.m_MaxFuelCapacity = value;
                }
            }
            //-----------------------------------------------------------------------------------------------------------------------//
            public void Refuel(float i_FuelToAdd, eFuelType i_FuelType)
            {
                string errorMessage;

                if (this.m_FuelType != i_FuelType)
                {
                    throw new ArgumentException("Invalid Fuel type");
                }

                this.m_FuelLeft += i_FuelToAdd;

                if (this.m_FuelLeft > this.m_MaxFuelCapacity)
                {
                    this.m_FuelLeft -= i_FuelToAdd;

                    if (this.m_FuelLeft == this.m_MaxFuelCapacity)
                    {
                        errorMessage = "Tank is already full";
                    }
                    else
                    {
                        errorMessage = "Maximum tank capacity exceeded";
                    }

                    throw new ValueOutOfRangeException(this.m_MaxFuelCapacity - this.m_FuelLeft, 0, errorMessage);
                }
            }
            //-----------------------------------------------------------------------------------------------------------------------//
            public override string ToString()
            {
                StringBuilder information = new StringBuilder();
                information.AppendFormat(@"Fuel Engine:
    Fuel type: {0}
    Maximum fuel capacity: {1}
    Fuel left: {2}", 
                this.m_FuelType.ToString(),
                this.m_MaxFuelCapacity.ToString(),
                this.FuelLeft.ToString());
                information.Append(Environment.NewLine);

                return information.ToString();
            }
            //-----------------------------------------------------------------------------------------------------------------------//
        }
        ///-----------------------------------------Nested class----------------------------------------------///

    }
}
