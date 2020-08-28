using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class Car : Vehicle
    {
        //-----------------------------------------------------------------------------------------------------------------------//
        public enum eColor
        {
            Red = 1,
            White = 2,
            Black = 3,
            Silver = 4    
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        private eColor m_Color;
        private int m_NumberOfDoors;
        //-----------------------------------------------------------------------------------------------------------------------//
        public Car(Engine i_Engine, string i_LicenceNumber) : base(i_Engine, i_LicenceNumber, 4, 32)
        {
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        public eColor Color
        {
            get
            {
                return this.m_Color;
            }
            set
            {
                this.m_Color = value;
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        public int NumberOfDoors
        {
            get
            {
                return this.m_NumberOfDoors;
            }
            set
            {
                this.m_NumberOfDoors = value;
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        public override List<string> GetQuestionStrings()
        {
            List<string> questionString = new List<string>
            {
                @"Please choose the car's color:
1. Red
2. White
3. Black
4. Silver
",
                "Please enter the car's number of doors: "
            };

            return questionString;
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        public override void SetAnswersToVehicle(List<string> i_Answers)
        {
            Exception exception = findExceptionsInAnswers(i_Answers, out int colorChoice, out int amountOfDoors);

            if (exception != null)
            {
                throw exception;
            }
            else
            {
                this.m_Color = (Car.eColor)colorChoice;
                this.m_NumberOfDoors = amountOfDoors;
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        private Exception findExceptionsInAnswers(List<string> i_Answers, out int o_ColorChoice, out int o_AmountOfDoors)
        {
            Exception exception = null;
            o_ColorChoice = o_AmountOfDoors = -1;

            if (!int.TryParse(i_Answers[0], out o_ColorChoice))
            {
                exception = new FormatException("Format of input of the color isn't valid, please try again: ");
                exception.Source = "0";
            }
            else if (ValueOutOfRangeException.ValueOutOfRange(o_ColorChoice, 4, 1))
            {
                exception = new ValueOutOfRangeException(4, 1, "Color choice for the car is out of range, please try again: ", exception);
                exception.Source = "0";
            }

            if (!int.TryParse(i_Answers[1], out o_AmountOfDoors))
            {
                exception = new FormatException("Format of input of the number of doors isn't valid, please try again: ", exception);
                exception.Source = "1";
            }
            else if (ValueOutOfRangeException.ValueOutOfRange(o_AmountOfDoors, 5, 2))
            {
                exception = new ValueOutOfRangeException(5, 1, "Number of doors for the car is out of range, please try again: ", exception);
                exception.Source = "1";
            }

            return exception;
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        public override string ToString()
        {
            StringBuilder carDetails = new System.Text.StringBuilder();
            carDetails.Append(base.ToString());
            carDetails.AppendFormat(@"Car Details:
    Color: {0}
    Number of Doors: {1}
", 
            this.m_Color.ToString(),
            this.m_NumberOfDoors.ToString());
            carDetails.Append(Environment.NewLine);

            return carDetails.ToString();
        }
        //-----------------------------------------------------------------------------------------------------------------------//
    }
}
