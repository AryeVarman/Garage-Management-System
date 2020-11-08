using System;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private readonly float r_MaxValue;
        private readonly float r_MinValue;
        private readonly string r_FieldNameError;

        public ValueOutOfRangeException(
            string i_fieldName, float i_MaxValue, float i_MinValue)
            : base(string.Format("Error, value at {0} out of range, the value need to be between {1} to {2}", i_fieldName, i_MinValue, i_MaxValue))
        {
        }

        public float MaxValue
        {
            get { return r_MaxValue; }
        }

        public float MinValue
        {
            get { return r_MinValue; }
        }

        public string FieldNameError
        {
            get { return r_FieldNameError; }
        }
    }
}
