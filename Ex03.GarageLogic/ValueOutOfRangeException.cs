using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private float m_MaxValue;
        private float m_MinValue;

        //-----------------------------------------------------------------------------------------------------------------------//
        public float MaxValue
        {
            get
            {
                return this.m_MaxValue;
            }
            set
            {
                this.m_MaxValue = value;
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        public float MinValue
        {
            get
            {
                return this.m_MinValue;
            }
            set
            {
                this.m_MinValue = value;
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        public ValueOutOfRangeException(float i_MaxValue, float i_MinValue, string i_Message, Exception i_Exception = null) : base(i_Message, i_Exception)
        {
            this.m_MaxValue = i_MaxValue;
            this.m_MinValue = i_MinValue;
        }
        //-----------------------------------------------------------------------------------------------------------------------//
        public static bool ValueOutOfRange(float i_Value, float i_MaxValue, float i_MinValue)
        {
            return i_Value > i_MaxValue || i_Value < i_MinValue;
        }
        //-----------------------------------------------------------------------------------------------------------------------//
    }
}
