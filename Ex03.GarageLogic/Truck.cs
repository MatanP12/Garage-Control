using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class Truck : Vehicle
    {
        private bool m_IsTransportingHazardousGoods;
        private float m_BaggageCapacity;
        //-----------------------------------------------------------------------------------------------------------------------//
        public Truck(Engine i_Engine, string i_LicenseNumber) : base(i_Engine, i_LicenseNumber, 16, 28)
        {
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        public bool IsTransportingHazardousGoods
        {
            get
            {
                return this.m_IsTransportingHazardousGoods;
            }
            set
            {
                this.m_IsTransportingHazardousGoods = value;
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        public float BaggageCapacity
        {
            get
            {
                return this.m_BaggageCapacity;
            }
            set
            {
                this.m_BaggageCapacity = value;
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        public override List<string> GetQuestionStrings()
        {
            List<string> questionString = new List<string>
            {
                "Is the truck transporting hazardous goods? Y/N: ",
                "Please enter the truck's baggage capacity: "
            };

            return questionString;
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        public override void SetAnswersToVehicle(List<string> i_Answers)
        {
            Exception exception = findExceptionsInAnswers(i_Answers, out char answerBool, out float baggageCapacity);

            if (exception != null)
            {
                throw exception;
            }
            else
            {
                this.applyAnswersToMembers(answerBool, baggageCapacity);
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        private Exception findExceptionsInAnswers(List<string> i_Answers, out char o_AnswerBool, out float o_BaggageCapacity)
        {
            Exception exception = null;
            o_AnswerBool = ' ';
            o_BaggageCapacity = -1;

            if (!char.TryParse(i_Answers[0], out o_AnswerBool))
            {
                exception = new FormatException("Format of input of the hazardous goods isn't valid, please try again: ");
                exception.Source = "0";
            }
            else if (!o_AnswerBool.Equals('Y') && !o_AnswerBool.Equals('y')
                && !o_AnswerBool.Equals('N') && !o_AnswerBool.Equals('n'))
            {
                exception = new ArgumentException("The argument you chose for the truck's hazardous carrying is invalid, please try again: ");
                exception.Source = "0";
            }

            if (!float.TryParse(i_Answers[1], out o_BaggageCapacity))
            {
                exception = new FormatException("Format of input of the baggage capacity isn't valid, please try again: ", exception);
                exception.Source = "1";
            }
            else if (o_BaggageCapacity < 0)
            {
                exception = new ValueOutOfRangeException(1000000, 1, "Baggage capacity for the truck is out of range, please try again:", exception);
                exception.Source = "1";
            }

            return exception;
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        private void applyAnswersToMembers(char i_AnswerToBool, float i_BaggageCapacity)
        {
            if (i_AnswerToBool == 'Y' || i_AnswerToBool == 'y')
            {
                this.m_IsTransportingHazardousGoods = true;
            }
            else
            {
                this.m_IsTransportingHazardousGoods = false;
            }

            this.m_BaggageCapacity = i_BaggageCapacity;
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        public override string ToString()
        {
            StringBuilder truckDetails = new StringBuilder();
            truckDetails.Append(base.ToString());
            truckDetails.AppendFormat(@"Truck Details:
    Hazardous Goods: {0}
    Baggage Capacity: {1}",
            this.m_IsTransportingHazardousGoods.ToString(),
            this.m_BaggageCapacity.ToString());
            truckDetails.Append(Environment.NewLine);

            return truckDetails.ToString();
        }
        //-----------------------------------------------------------------------------------------------------------------------//
    }
}
