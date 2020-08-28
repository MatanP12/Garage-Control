using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class Motorcycle : Vehicle
    {
        //-----------------------------------------------------------------------------------------------------------------------//
        public enum eLicenseType
        {
            A = 1,
            A1 = 2,
            AA = 3,
            B = 4
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        private eLicenseType m_License;
        private int m_EngineCapacity;
        //-----------------------------------------------------------------------------------------------------------------------//
        public Motorcycle(Engine i_Engine, string i_LicenseNumber) : base(i_Engine, i_LicenseNumber, 2, 30)
        {
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        public eLicenseType License
        {
            get
            {
                return this.m_License;
            }
            set
            {
                this.m_License = value;
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        public int EngineCapacity
        {
            get
            {
                return this.m_EngineCapacity;
            }
            set
            {
                this.m_EngineCapacity = value;
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        public override List<string> GetQuestionStrings()
        {
            new List<string>().Add(@"Please choose the motorcycle's license type:
1. A
2. A1
3. AA
4. B
");
            new List<string>().Add("Please enter the motorcycle's engine capacity: ");

            return new List<string>();
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        public override void SetAnswersToVehicle(List<string> i_Answers)
        {
            Exception exception = findExceptionsInAnswers(i_Answers, out int licenseType, out int engineVolume);

            if (exception != null)
            {
                throw exception;
            }
            else
            {
                this.m_License = (Motorcycle.eLicenseType)licenseType;
                this.m_EngineCapacity = engineVolume;
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        private Exception findExceptionsInAnswers(List<string> i_Answers, out int o_LicenseType, out int o_EngineVolume)
        {
            Exception exception = null;
            o_LicenseType = o_EngineVolume = -1;

            if (!int.TryParse(i_Answers[0], out o_LicenseType))
            {
                exception = new FormatException("Format of input of the license type isn't valid, please try again: ");
                exception.Source = "0";
            }
            else if (ValueOutOfRangeException.ValueOutOfRange(o_LicenseType, 4, 1))
            {
                exception = new ValueOutOfRangeException(4, 1, "License type for the motorcycle is out of range, please try again: ", exception);
                exception.Source = "0";
            }
            if (!int.TryParse(i_Answers[1], out o_EngineVolume))
            {
                exception = new FormatException("Format of input engine volume isn't valid, please try again: ", exception);
                exception.Source = "1";
            }
            else if (ValueOutOfRangeException.ValueOutOfRange(o_EngineVolume, 1500, 1))
            {
                exception = new ValueOutOfRangeException(1500, 1, "Engine volume for the motorcycle is out of range, please try again: ");
                exception.Source = "1";
            }

            return exception;
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        public override string ToString()
        {
            StringBuilder motorcycleDetails = new StringBuilder();
            motorcycleDetails.Append(base.ToString());
            motorcycleDetails.AppendFormat(@"Motorcycle Details: 
    License Type: {0}
    Engine Volume: {1}", 
            this.m_License.ToString(),
            this.m_EngineCapacity.ToString());
            motorcycleDetails.Append(Environment.NewLine);

            return motorcycleDetails.ToString();
        }
        //-----------------------------------------------------------------------------------------------------------------------//
    }
}
